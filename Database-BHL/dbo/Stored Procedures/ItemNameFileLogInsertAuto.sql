
-- ItemNameFileLogInsertAuto PROCEDURE
-- Generated 11/19/2009 2:21:40 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for ItemNameFileLog

CREATE PROCEDURE ItemNameFileLogInsertAuto

@LogID INT OUTPUT,
@ItemID INT,
@DoCreate BIT,
@DoUpload BIT,
@LastCreateDate DATETIME = null,
@LastUploadDate DATETIME = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[ItemNameFileLog]
(
	[ItemID],
	[DoCreate],
	[DoUpload],
	[LastCreateDate],
	[LastUploadDate],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@ItemID,
	@DoCreate,
	@DoUpload,
	@LastCreateDate,
	@LastUploadDate,
	getdate(),
	getdate()
)

SET @LogID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemNameFileLogInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

