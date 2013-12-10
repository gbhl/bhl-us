CREATE PROCEDURE [dbo]. [PageDetailUpdateStatus]

@PageDetailID int ,
@PageDetailStatusID int

AS

BEGIN

SET NOCOUNT ON

UPDATE dbo .PageDetail
SET          PageDetailStatusID = @PageDetailStatusID ,
               StatusDate = GETDATE(),
               LastModifiedDate = GETDATE()
  WHERE  PageDetailID = @PageDetailID

END
