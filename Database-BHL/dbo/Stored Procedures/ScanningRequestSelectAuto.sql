
-- ScanningRequestSelectAuto PROCEDURE
-- Generated 6/10/2010 3:21:58 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for ScanningRequest

CREATE PROCEDURE ScanningRequestSelectAuto

@ScanningRequestID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ScanningRequestSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

