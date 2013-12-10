CREATE PROCEDURE dbo. PageIllustrationInsert

@PageDetailID int ,
@Top int ,
@Bottom int ,
@Left int ,
@Right int

AS

BEGIN

SET NOCOUNT ON

DECLARE @PageIllustrationID int

INSERT dbo.PageIllustration
        (
        PageDetailID,
        [Top],
        Bottom,
        [Left],
        [Right]
        )
VALUES
        (
        @PageDetailID,
        @Top,
        @Bottom,
        @Left,
        @Right
        )

SELECT @PageIllustrationID = SCOPE_IDENTITY()

SELECT PageIllustrationID ,
               PageDetailID,
               [Top],
               Bottom,
               [Right],
               [Left],
               CreationDate,
               LastModifiedDate,
               CreationUserID,
               LastModifiedUserID
FROM   dbo .PageIllustration
WHERE  PageIllustrationID = @PageIllustrationID

END

