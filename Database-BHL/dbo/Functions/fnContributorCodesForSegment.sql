CREATE FUNCTION dbo.fnContributorCodesForSegment
(
	@SegmentID int
)
RETURNS nvarchar(MAX)
AS
BEGIN
	DECLARE @ContributorString nvarchar(MAX)

	SELECT	@ContributorString = COALESCE(@ContributorString, '') + ii.InstitutionCode + '|'
	FROM	dbo.Segment s
			INNER JOIN dbo.ItemInstitution ii ON s.ItemID = ii.ItemID
			INNER JOIN InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
	WHERE	s.SegmentID = @SegmentID
	AND		r.InstitutionRoleName = 'Contributor'
	ORDER BY
			ii.InstitutionCode ASC

	SET @ContributorString = LTRIM(RTRIM(COALESCE(@ContributorString, '')))
	SET @ContributorString = CASE WHEN LEN(@ContributorString) > 0 
								THEN LEFT(@ContributorString, LEN(@ContributorString) - 1) 
								ELSE @ContributorString END

	RETURN @ContributorString
END

GO
