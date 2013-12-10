CREATE PROCEDURE dbo. PageDetailInsert

@PageID int ,
@PageDetailStatusID int ,
@Height int ,
@Width int ,
@PixelDepth int ,
@AbbyyHasImage smallint ,
@ContrastHasImage smallint ,
@PercentCoverage decimal (5, 2)

AS

BEGIN

SET NOCOUNT ON

DECLARE @PageDetailID int

INSERT dbo.PageDetail
        (
        PageID,
        PageDetailStatusID,
        Height,
        Width,
        PixelDepth,
        AbbyyHasImage,
        ContrastHasImage,
        PercentCoverage
        )
VALUES
        (
        @PageID,
        @PageDetailStatusID,
        @Height,
        @Width,
        @PixelDepth,
        @AbbyyHasImage,
        @ContrastHasImage,
        @PercentCoverage
        )


SELECT @PageDetailID = SCOPE_IDENTITY()

SELECT PageDetailID,
               PageID,
               PageDetailStatusID,
               StatusDate,
               Height,
               Width,
               PixelDepth,
               AbbyyHasImage,
               ContrastHasImage,
               PercentCoverage,
               CreationDate,
               LastModifiedDate,
               CreationUserID,
               LastModifiedUserID
FROM   dbo.PageDetail
WHERE  PageDetailID = @PageDetailID

END

