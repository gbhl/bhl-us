
-- ItemCollectionUpdateAuto PROCEDURE
-- Generated 7/30/2010 2:09:29 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for ItemCollection

CREATE PROCEDURE ItemCollectionUpdateAuto

@ItemCollectionID INT,
@ItemID INT,
@CollectionID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[ItemCollection]

SET

	[ItemID] = @ItemID,
	[CollectionID] = @CollectionID

WHERE
	[ItemCollectionID] = @ItemCollectionID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemCollectionUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ItemCollectionID],
		[ItemID],
		[CollectionID],
		[CreationDate]

	FROM [dbo].[ItemCollection]
	
	WHERE
		[ItemCollectionID] = @ItemCollectionID
	
	RETURN -- update successful
END

