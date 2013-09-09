
-- CollectionUpdateAuto PROCEDURE
-- Generated 11/12/2008 3:38:13 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Collection

CREATE PROCEDURE CollectionUpdateAuto

@CollectionID INT,
@CollectionName NVARCHAR(200)

AS 

SET NOCOUNT ON

UPDATE [dbo].[Collection]

SET

	[CollectionName] = @CollectionName

WHERE
	[CollectionID] = @CollectionID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure CollectionUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[CollectionID],
		[CollectionName],
		[CreationDate]

	FROM [dbo].[Collection]
	
	WHERE
		[CollectionID] = @CollectionID
	
	RETURN -- update successful
END

