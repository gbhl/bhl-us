
-- BibliographicLevelInsertAuto PROCEDURE
-- Generated 8/3/2010 11:16:34 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for BibliographicLevel

CREATE PROCEDURE BibliographicLevelInsertAuto

@BibliographicLevelID INT OUTPUT,
@BibliographicLevelName NVARCHAR(50),
@MARCCode NCHAR(1)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[BibliographicLevel]
(
	[BibliographicLevelName],
	[MARCCode]
)
VALUES
(
	@BibliographicLevelName,
	@MARCCode
)

SET @BibliographicLevelID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BibliographicLevelInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

