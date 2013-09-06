
-- ItemCollectionSelectAuto PROCEDURE
-- Generated 7/30/2010 2:09:29 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for ItemCollection

CREATE PROCEDURE ItemCollectionSelectAuto

@ItemCollectionID INT

AS 

SET NOCOUNT ON

SELECT 

	[ItemCollectionID],
	[ItemID],
	[CollectionID],
	[CreationDate]

FROM [dbo].[ItemCollection]

WHERE
	[ItemCollectionID] = @ItemCollectionID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemCollectionSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

