
-- ItemNameFileLogUpdateAuto PROCEDURE
-- Generated 11/19/2009 2:21:40 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for ItemNameFileLog

CREATE PROCEDURE ItemNameFileLogUpdateAuto

@LogID INT,
@ItemID INT,
@DoCreate BIT,
@DoUpload BIT,
@LastCreateDate DATETIME,
@LastUploadDate DATETIME

AS 

SET NOCOUNT ON

UPDATE [dbo].[ItemNameFileLog]

SET

	[ItemID] = @ItemID,
	[DoCreate] = @DoCreate,
	[DoUpload] = @DoUpload,
	[LastCreateDate] = @LastCreateDate,
	[LastUploadDate] = @LastUploadDate,
	[LastModifiedDate] = getdate()

WHERE
	[LogID] = @LogID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemNameFileLogUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[LogID],
		[ItemID],
		[DoCreate],
		[DoUpload],
		[LastCreateDate],
		[LastUploadDate],
		[CreationDate],
		[LastModifiedDate]

	FROM [dbo].[ItemNameFileLog]
	
	WHERE
		[LogID] = @LogID
	
	RETURN -- update successful
END

