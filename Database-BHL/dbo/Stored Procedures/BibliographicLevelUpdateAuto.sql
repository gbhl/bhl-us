
-- BibliographicLevelUpdateAuto PROCEDURE
-- Generated 8/3/2010 11:16:34 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for BibliographicLevel

CREATE PROCEDURE BibliographicLevelUpdateAuto

@BibliographicLevelID INT,
@BibliographicLevelName NVARCHAR(50),
@MARCCode NCHAR(1)

AS 

SET NOCOUNT ON

UPDATE [dbo].[BibliographicLevel]

SET

	[BibliographicLevelName] = @BibliographicLevelName,
	[MARCCode] = @MARCCode

WHERE
	[BibliographicLevelID] = @BibliographicLevelID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BibliographicLevelUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[BibliographicLevelID],
		[BibliographicLevelName],
		[MARCCode]

	FROM [dbo].[BibliographicLevel]
	
	WHERE
		[BibliographicLevelID] = @BibliographicLevelID
	
	RETURN -- update successful
END

