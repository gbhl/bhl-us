
-- ItemCollectionSelectAuto PROCEDURE
-- Generated 11/12/2008 3:12:29 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for ItemCollection

CREATE PROCEDURE ItemCollectionSelectAuto

@ItemID INT,
@CollectionID INT

AS 

SET NOCOUNT ON

SELECT 

	[ItemID],
	[CollectionID],
	[CreationDate]

FROM [dbo].[ItemCollection]

WHERE
	[ItemID] = @ItemID AND
	[CollectionID] = @CollectionID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemCollectionSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

