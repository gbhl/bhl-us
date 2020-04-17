CREATE FUNCTION dbo.fnGroupStringForInstitution
(
	@InstitutionCode nvarchar(10)
)
RETURNS nvarchar(max)
AS
BEGIN
	DECLARE @GroupString nvarchar(max)

	SELECT	@GroupString = COALESCE(@GroupString, '') + InstitutionGroupName + ' | '
	FROM	(
			-- Get all groups tied to this institution
			SELECT DISTINCT ig.InstitutionGroupName 
			FROM	dbo.InstitutionGroupInstitution igi
					INNER JOIN dbo.InstitutionGroup ig ON igi.InstitutionGroupID = ig.InstitutionGroupID
			WHERE	igi.InstitutionCode = @InstitutionCode
			) X
	ORDER BY 
			InstitutionGroupName ASC

	SELECT @GroupString = COALESCE(@GroupString, '')
	SELECT @GroupString = LTRIM(RTRIM(CASE WHEN LEN(@GroupString) > 3 THEN SUBSTRING(@GroupString, 1, LEN(@GroupString) - 2) ELSE @GroupString END))
	RETURN @GroupString
END
