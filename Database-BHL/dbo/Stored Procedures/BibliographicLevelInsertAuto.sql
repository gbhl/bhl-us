CREATE PROCEDURE dbo.BibliographicLevelInsertAuto

@BibliographicLevelID INT OUTPUT,
@BibliographicLevelName NVARCHAR(50),
@BibliographicLevelLabel NVARCHAR(50),
@MARCCode NCHAR(1)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[BibliographicLevel]
( 	[BibliographicLevelName],
	[BibliographicLevelLabel],
	[MARCCode] )
VALUES
( 	@BibliographicLevelName,
	@BibliographicLevelLabel,
	@MARCCode )

SET @BibliographicLevelID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.BibliographicLevelInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
