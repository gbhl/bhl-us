
-- BibliographicLevelSelectAuto PROCEDURE
-- Generated 8/3/2010 11:16:34 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for BibliographicLevel

CREATE PROCEDURE BibliographicLevelSelectAuto

@BibliographicLevelID INT

AS 

SET NOCOUNT ON

SELECT 

	[BibliographicLevelID],
	[BibliographicLevelName],
	[MARCCode]

FROM [dbo].[BibliographicLevel]

WHERE
	[BibliographicLevelID] = @BibliographicLevelID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BibliographicLevelSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

