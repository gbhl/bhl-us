CREATE PROCEDURE [dbo].[BibliographicLevelSelectAuto]

@BibliographicLevelID INT

AS 

SET NOCOUNT ON

SELECT	
	[BibliographicLevelID],
	[BibliographicLevelName],
	[BibliographicLevelLabel],
	[MARCCode]
FROM	
	[dbo].[BibliographicLevel]
WHERE	
	[BibliographicLevelID] = @BibliographicLevelID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.BibliographicLevelSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
