
-- TitleCollectionInsertAuto PROCEDURE
-- Generated 7/30/2010 2:09:29 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for TitleCollection

CREATE PROCEDURE TitleCollectionInsertAuto

@TitleCollectionID INT OUTPUT,
@TitleID INT,
@CollectionID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleCollection]
(
	[TitleID],
	[CollectionID],
	[CreationDate]
)
VALUES
(
	@TitleID,
	@CollectionID,
	getdate()
)

SET @TitleCollectionID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleCollectionInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[TitleCollectionID],
		[TitleID],
		[CollectionID],
		[CreationDate]	

	FROM [dbo].[TitleCollection]
	
	WHERE
		[TitleCollectionID] = @TitleCollectionID
	
	RETURN -- insert successful
END

