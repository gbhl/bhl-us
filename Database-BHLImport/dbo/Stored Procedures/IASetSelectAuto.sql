
-- IASetSelectAuto PROCEDURE
-- Generated 5/27/2008 11:38:08 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for IASet

CREATE PROCEDURE IASetSelectAuto

@SetID INT

AS 

SET NOCOUNT ON

SELECT 

	[SetID],
	[SetSpecification],
	[DownloadAll],
	[LastDownloadDate],
	[LastFullDownloadDate],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IASet]

WHERE
	[SetID] = @SetID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IASetSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

