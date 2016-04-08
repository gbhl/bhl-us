CREATE FUNCTION [dbo].[fnSeriesStringForTitle] 
(
	@TitleID int
)
RETURNS nvarchar(max)
AS 

BEGIN
	
	DECLARE @AssocString nvarchar(max)

	DECLARE @CurrentRecord int
	SELECT @CurrentRecord = 1

	SELECT 
		@AssocString = COALESCE(@AssocString, '') +
					(CASE WHEN @CurrentRecord = 1 THEN '' ELSE '|' END) + 
					LTRIM(RTRIM(ta.Title)) + ' ' + LTRIM(RTRIM(ta.Section)) + ' ' +
					LTRIM(RTRIM(ta.Volume)) + ' ' + LTRIM(RTRIM(ta.Heading)) + ' ' + 
					LTRIM(RTRIM(ta.Publication)) + ' ' + LTRIM(RTRIM(ta.Relationship)),
		@CurrentRecord = @CurrentRecord + 1
	FROM	dbo.TitleAssociation ta INNER JOIN dbo.TitleAssociationType tat
				ON ta.TitleAssociationTypeID = tat.TitleAssociationTypeID
	WHERE	ta.TitleID = @TitleID
	AND		tat.TitleAssociationLabel = 'Series'
	AND		ta.Active = 1
	ORDER BY
			tat.TitleAssociationLabel, tat.MarcIndicator2, ta.Title, ta.Section, ta.Volume

	RETURN LTRIM(RTRIM(COALESCE(@AssocString, '')))
END
