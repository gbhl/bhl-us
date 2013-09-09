CREATE PROCEDURE [dbo].[IAScandataAltPageTypeSelectByScandataAndPageType]

@ScandataID INT,
@PageType NVARCHAR(50)

AS 

SET NOCOUNT ON

SELECT	[ScandataAltPageTypeID],
		[ScandataID],
		[PageType],
		[CreatedDate],
		[LastModifiedDate]
FROM	[dbo].[IAScandataAltPageType]
WHERE	[ScandataID] = @ScandataID
AND		[PageType] = @PageType

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAScandataAltPageTypeSelectByScandataAndPageType. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

