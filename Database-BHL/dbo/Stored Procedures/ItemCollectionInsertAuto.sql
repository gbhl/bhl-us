
-- ItemCollectionInsertAuto PROCEDURE
-- Generated 7/30/2010 2:09:29 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for ItemCollection

CREATE PROCEDURE ItemCollectionInsertAuto

@ItemCollectionID INT OUTPUT,
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

SET @ItemCollectionID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemCollectionInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

