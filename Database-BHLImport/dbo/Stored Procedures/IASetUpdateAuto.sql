
-- IASetUpdateAuto PROCEDURE
-- Generated 5/27/2008 11:38:08 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for IASet

CREATE PROCEDURE IASetUpdateAuto

@SetID INT,
@SetSpecification NVARCHAR(200),
@DownloadAll BIT,
@LastDownloadDate DATETIME,
@LastFullDownloadDate DATETIME

AS 

SET NOCOUNT ON

UPDATE [dbo].[IASet]

SET

	[SetSpecification] = @SetSpecification,
	[DownloadAll] = @DownloadAll,
	[LastDownloadDate] = @LastDownloadDate,
	[LastFullDownloadDate] = @LastFullDownloadDate,
	[LastModifiedDate] = getdate()

WHERE
	[SetID] = @SetID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IASetUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

