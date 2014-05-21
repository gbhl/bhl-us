CREATE PROCEDURE [dbo].[PageDetailUpdateColor]

@PageID int ,
@Color nvarchar(40)

AS

BEGIN

SET NOCOUNT ON

UPDATE	dbo.PageDetail
SET		Color = @Color,
        LastModifiedDate = GETDATE()
WHERE	PageID = @PageID

END
