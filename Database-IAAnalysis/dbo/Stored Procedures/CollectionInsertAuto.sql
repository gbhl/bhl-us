
-- CollectionInsertAuto PROCEDURE
-- Generated 11/12/2008 3:38:13 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Collection

CREATE PROCEDURE CollectionInsertAuto

@CollectionID INT OUTPUT,
@CollectionName NVARCHAR(200)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Collection]
(
	[CollectionName],
	[CreationDate]
)
VALUES
(
	@CollectionName,
	getdate()
)

SET @CollectionID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure CollectionInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

