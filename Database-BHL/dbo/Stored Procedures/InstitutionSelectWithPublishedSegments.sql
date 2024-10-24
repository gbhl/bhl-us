SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InstitutionSelectWithPublishedSegments]

@OnlyMemberLibraries bit = 1,
@InstitutionRoleName nvarchar(100) = null

AS 

SET NOCOUNT ON

SELECT	ins.InstitutionCode
INTO	#tmpInstitution
FROM	dbo.Institution ins WITH (NOLOCK)
		INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON ins.InstitutionCode = ii.InstitutionCode
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ii.ItemID = i.ItemID
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON i.ItemID = s.ItemID
		INNER JOIN dbo.InstitutionRole r WITH (NOLOCK) ON ii.InstitutionRoleID = r.InstitutionRoleID
WHERE	i.ItemStatusID IN (30, 40)
AND		((ins.BHLMemberLibrary = 1 AND @OnlyMemberLibraries = 1) OR	@OnlyMemberLibraries = 0)
AND		(r.InstitutionRoleName = @InstitutionRoleName OR @InstitutionRoleName IS NULL)

SELECT	InstitutionCode,
		InstitutionName,
		Note,
		ISNULL(InstitutionUrl, '') AS InstitutionUrl,
		BHLMemberLibrary
FROM	dbo.Institution
WHERE	InstitutionCode IN (SELECT DISTINCT InstitutionCode FROM #tmpInstitution)
ORDER BY InstitutionName

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure InstitutionSelectWithPublishedSegments. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
