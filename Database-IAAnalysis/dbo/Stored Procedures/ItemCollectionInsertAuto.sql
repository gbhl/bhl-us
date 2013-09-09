
-- ItemCollectionInsertAuto PROCEDURE
-- Generated 11/12/2008 3:12:29 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for ItemCollection

CREATE PROCEDURE ItemCollectionInsertAuto

@ItemID INT,
@CollectionID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[ItemCollection]
(
	[ItemID],
	[CollectionID],
	[CreationDate]
)
VALUES
(
	@ItemID,
	@CollectionID,
	getdate()
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemCollectionInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

