SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fnContributorStringForSegment] 
(
	@SegmentID int
)
RETURNS nvarchar(MAX)
AS
BEGIN
	DECLARE @ContributorString nvarchar(MAX)

	SELECT	@ContributorString = COALESCE(@ContributorString, '') + i.InstitutionName + '|'
	FROM	dbo.Segment s
			INNER JOIN dbo.ItemInstitution ii ON s.ItemID = ii.ItemID
			INNER JOIN Institution i ON ii.InstitutionCode = i.InstitutionCode
			INNER JOIN InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
	WHERE	s.SegmentID = @SegmentID
	AND		r.InstitutionRoleName = 'Contributor'
	ORDER BY
			i.InstitutionName ASC

	SET @ContributorString = LTRIM(RTRIM(COALESCE(@ContributorString, '')))
	SET @ContributorString = CASE WHEN LEN(@ContributorString) > 0 
								THEN LEFT(@ContributorString, LEN(@ContributorString) - 1) 
								ELSE @ContributorString END

	RETURN @ContributorString
END


GO
