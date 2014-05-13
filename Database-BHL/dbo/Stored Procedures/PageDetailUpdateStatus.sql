CREATE PROCEDURE [dbo].[PageDetailUpdateStatus]

@PageID int ,
@PageDetailStatusID int

AS

BEGIN

SET NOCOUNT ON

UPDATE	dbo.PageDetail
SET		PageDetailStatusID = @PageDetailStatusID ,
		StatusDate = GETDATE(),
		LastModifiedDate = GETDATE()
WHERE	PageID = @PageID

END
