CREATE PROCEDURE dbo.InstitutionSelectWithPublishedSegments

@OnlyMemberLibraries bit = 1

AS 

SET NOCOUNT ON

SELECT DISTINCT 
		ins.InstitutionCode,
		ins.InstitutionName,
		ins.Note,
		ISNULL(ins.InstitutionUrl, '') AS InstitutionUrl,
		ins.BHLMemberLibrary
FROM	dbo.Institution ins INNER JOIN dbo.Segment s 
			ON ins.InstitutionCode = s.ContributorCode
WHERE	s.SegmentStatusID IN (10, 20)
AND		((ins.BHLMemberLibrary = 1 AND @OnlyMemberLibraries = 1) OR	@OnlyMemberLibraries = 0)
ORDER BY
		ins.InstitutionName

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure InstitutionSelectWithPublishedSegments. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

