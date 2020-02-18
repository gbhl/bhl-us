CREATE PROCEDURE dbo.BibliographicLevelUpdateAuto

@BibliographicLevelID INT,
@BibliographicLevelName NVARCHAR(50),
@BibliographicLevelLabel NVARCHAR(50),
@MARCCode NCHAR(1)

AS 

SET NOCOUNT ON

UPDATE [dbo].[BibliographicLevel]
SET
	[BibliographicLevelName] = @BibliographicLevelName,
	[BibliographicLevelLabel] = @BibliographicLevelLabel,
	[MARCCode] = @MARCCode
WHERE
	[BibliographicLevelID] = @BibliographicLevelID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.BibliographicLevelUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[BibliographicLevelID],
		[BibliographicLevelName],
		[BibliographicLevelLabel],
		[MARCCode]
	FROM [dbo].[BibliographicLevel]
	WHERE
		[BibliographicLevelID] = @BibliographicLevelID
	
	RETURN -- update successful
END
