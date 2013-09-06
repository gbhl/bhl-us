
CREATE FUNCTION [dbo].[fnAssociationStringForTitle] 
(
	@TitleID int
)
RETURNS nvarchar(1024)
AS 

BEGIN
	DECLARE @AssocString nvarchar(max)

	-- "FOR XML PATH('')" is a better solution for concatenation than COALESCE
	-- http://msmvps.com/blogs/robfarley/archive/2007/04/08/coalesce-is-not-the-answer-to-string-concatentation-in-t-sql.aspx
	-- The STUFF exists simply to remove the leading '|'.
	SELECT @AssocString = STUFF((
		SELECT	'|' + RTRIM(LTRIM(RTRIM(tat.TitleAssociationLabel COLLATE SQL_Latin1_General_CP1_CI_AI)) + ': ' +  
							LTRIM(RTRIM(ta.Title)) + ' ' + LTRIM(RTRIM(ta.Section)) + ' ' +
							LTRIM(RTRIM(ta.Volume)) + ' ' + LTRIM(RTRIM(ta.Heading)) + ' ' + 
							LTRIM(RTRIM(ta.Publication)) + ' ' + LTRIM(RTRIM(ta.Relationship)))
		FROM	dbo.TitleAssociation ta INNER JOIN dbo.TitleAssociationType tat
					ON ta.TitleAssociationTypeID = tat.TitleAssociationTypeID
		WHERE	ta.TitleID = @TitleID
		AND		ta.Active = 1
		ORDER BY tat.TitleAssociationLabel, tat.MarcIndicator2, ta.Title, ta.Section, ta.Volume
		FOR XML PATH('')
		),1,1,'')

	RETURN SUBSTRING(COALESCE(@AssocString, '') + SPACE(1024), 1, 1024)
END

