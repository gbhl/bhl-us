CREATE FUNCTION [dbo].[fnContributorStringForSegment] 
(
	@SegmentID int
)
RETURNS nvarchar(MAX)
AS
BEGIN
	DECLARE @ContributorString nvarchar(MAX)

	SELECT	@ContributorString = COALESCE(@ContributorString, '') + i.InstitutionName + '|'
	FROM	dbo.SegmentInstitution si
			INNER JOIN Institution i ON si.InstitutionCode = i.InstitutionCode
			INNER JOIN InstitutionRole r ON si.InstitutionRoleID = r.InstitutionRoleID
	WHERE	si.SegmentID = @SegmentID
	AND		r.InstitutionRoleName = 'Contributor'
	ORDER BY
			i.InstitutionName ASC

	SET @ContributorString = LTRIM(RTRIM(COALESCE(@ContributorString, '')))
	SET @ContributorString = CASE WHEN LEN(@ContributorString) > 0 
								THEN LEFT(@ContributorString, LEN(@ContributorString) - 1) 
								ELSE @ContributorString END

	RETURN @ContributorString
END

