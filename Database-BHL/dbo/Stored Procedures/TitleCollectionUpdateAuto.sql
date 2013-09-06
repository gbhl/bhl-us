
-- TitleCollectionUpdateAuto PROCEDURE
-- Generated 7/30/2010 2:09:29 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for TitleCollection

CREATE PROCEDURE TitleCollectionUpdateAuto

@TitleCollectionID INT,
@TitleID INT,
@CollectionID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[TitleCollection]

SET

	[TitleID] = @TitleID,
	[CollectionID] = @CollectionID

WHERE
	[TitleCollectionID] = @TitleCollectionID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleCollectionUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

