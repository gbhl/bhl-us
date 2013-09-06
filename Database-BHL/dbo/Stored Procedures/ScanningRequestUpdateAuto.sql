
-- ScanningRequestUpdateAuto PROCEDURE
-- Generated 6/10/2010 3:21:58 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for ScanningRequest

CREATE PROCEDURE ScanningRequestUpdateAuto

@ScanningRequestID INT,
@GeminiIssueID INT,
@Title NVARCHAR(500),
@Year NVARCHAR(20),
@Type NVARCHAR(20),
@Volume NVARCHAR(100),
@Edition NVARCHAR(100),
@OCLC NVARCHAR(30),
@ISBN NVARCHAR(30),
@ISSN NVARCHAR(30),
@Author NVARCHAR(200),
@Publisher NVARCHAR(200),
@Language NVARCHAR(20),
@Note NVARCHAR(MAX)

AS 

SET NOCOUNT ON

UPDATE [dbo].[ScanningRequest]

SET

	[GeminiIssueID] = @GeminiIssueID,
	[Title] = @Title,
	[Year] = @Year,
	[Type] = @Type,
	[Volume] = @Volume,
	[Edition] = @Edition,
	[OCLC] = @OCLC,
	[ISBN] = @ISBN,
	[ISSN] = @ISSN,
	[Author] = @Author,
	[Publisher] = @Publisher,
	[Language] = @Language,
	[Note] = @Note

WHERE
	[ScanningRequestID] = @ScanningRequestID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ScanningRequestUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ScanningRequestID],
		[GeminiIssueID],
		[Title],
		[Year],
		[Type],
		[Volume],
		[Edition],
		[OCLC],
		[ISBN],
		[ISSN],
		[Author],
		[Publisher],
		[Language],
		[Note],
		[CreationDate]

	FROM [dbo].[ScanningRequest]
	
	WHERE
		[ScanningRequestID] = @ScanningRequestID
	
	RETURN -- update successful
END

