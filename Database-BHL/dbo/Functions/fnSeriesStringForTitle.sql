
CREATE FUNCTION [dbo].[fnSeriesStringForTitle] 
(
	@TitleID int
)
RETURNS nvarchar(1024)
AS 

BEGIN
	
	DECLARE @AssocString nvarchar(1024)

	DECLARE @CurrentRecord int
	SELECT @CurrentRecord = 1

	SELECT 
		@AssocString = COALESCE(@AssocString, '') +
					(CASE WHEN @CurrentRecord = 1 THEN '' ELSE '|' END) + LTRIM(RTRIM(ta.Title)),
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

