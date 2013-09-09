
-- ItemCollectionUpdateAuto PROCEDURE
-- Generated 11/12/2008 3:12:29 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for ItemCollection

CREATE PROCEDURE ItemCollectionUpdateAuto

@ItemID INT,
@CollectionID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[ItemCollection]

SET

	[ItemID] = @ItemID,
	[CollectionID] = @CollectionID

WHERE
	[ItemID] = @ItemID AND
	[CollectionID] = @CollectionID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemCollectionUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ItemID],
		[CollectionID],
		[CreationDate]

	FROM [dbo].[ItemCollection]
	
	WHERE
		[ItemID] = @ItemID AND 
		[CollectionID] = @CollectionID
	
	RETURN -- update successful
END

