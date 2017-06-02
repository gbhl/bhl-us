CREATE FUNCTION [dbo].[fnContributorStringForTitle] 
(
	@TitleID int,
	@OnlyActiveItems tinyint
)
RETURNS nvarchar(MAX)
AS
BEGIN
	DECLARE @ContributorString nvarchar(MAX)

	SELECT	@ContributorString = COALESCE(@ContributorString, '') + InstitutionName + '|'
	FROM	(	SELECT	DISTINCT i.InstitutionName
				FROM	dbo.Title t
						INNER JOIN dbo.TitleItem ti ON t.TitleID = ti.TitleID
						INNER JOIN dbo.Item itm ON ti.ItemID = itm.ItemID
						INNER JOIN dbo.ItemInstitution ii ON itm.ItemID = ii.ItemID
						INNER JOIN dbo.Institution i ON ii.InstitutionCode = i.InstitutionCode
						INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
				WHERE	t.TitleID = @TitleID
				AND		(itm.ItemStatusID = 40 OR @OnlyActiveItems = 0)
				AND		r.InstitutionRoleName = 'Holding Institution'
				) X
	ORDER BY InstitutionName ASC

	SET @ContributorString = LTRIM(RTRIM(COALESCE(@ContributorString, '')))
	SET @ContributorString = CASE WHEN LEN(@ContributorString) > 0 
								THEN LEFT(@ContributorString, LEN(@ContributorString) - 1) 
								ELSE @ContributorString END

	RETURN @ContributorString
END
