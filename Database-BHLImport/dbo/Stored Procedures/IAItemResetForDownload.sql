CREATE PROCEDURE [dbo].[IAItemResetForDownload]

@IAIdentifier nvarchar(200),
@UserID int = 1

AS

BEGIN

SET NOCOUNT ON

DECLARE @ItemID INT

SELECT @ItemID = ItemID FROM IAItem WHERE IAIdentifier = @IAIdentifier

BEGIN TRY
	BEGIN TRANSACTION
	DELETE FROM iadcmetadata WHERE itemid = @itemid
	DELETE FROM iamarcsubfield WHERE marcdatafieldid IN (SELECT marcdatafieldid FROM iamarcdatafield WHERE marcid IN (SELECT marcid FROM iamarc WHERE itemid = @itemid))
	DELETE FROM iamarcdatafield WHERE marcid IN (SELECT marcid FROM iamarc WHERE itemid = @itemid)
	DELETE FROM iamarccontrol WHERE marcid IN (SELECT marcid FROM iamarc WHERE itemid = @itemid)
	DELETE FROM iamarc WHERE itemid = @itemid
	DELETE FROM iapage WHERE itemid = @itemid
	DELETE FROM iascandataaltpagenumber WHERE scandataid IN (SELECT scandataid FROM iascandata WHERE itemid = @itemid)
	DELETE FROM iascandataaltpagetype WHERE scandataid IN (SELECT scandataid FROM iascandata WHERE itemid = @itemid)
	DELETE FROM iascandata WHERE itemid = @itemid
	UPDATE iafile SET remotefilelastmodifieddate = '1/1/1980' WHERE itemid = @itemid AND remotefilelastmodifieddate IS NOT NULL
	UPDATE iaitem SET itemstatusid = 10, lastxmldataharvestdate = NULL, LastModifiedDate = GETDATE(), LastModifiedUserID = @UserID WHERE itemid = @itemid
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
	
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;

    SELECT	@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();
			
	SET @ErrorMessage = 'Error resetting ''' + @IAIdentifier + ''' for download.  ' + @ErrorMessage

    RAISERROR (@ErrorMessage,
               @ErrorSeverity,
               @ErrorState
               );	
END CATCH

END

GO
