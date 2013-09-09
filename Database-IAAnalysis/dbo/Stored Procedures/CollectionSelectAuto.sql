
-- CollectionSelectAuto PROCEDURE
-- Generated 11/12/2008 3:38:13 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Collection

CREATE PROCEDURE CollectionSelectAuto

@CollectionID INT

AS 

SET NOCOUNT ON

SELECT 

	[CollectionID],
	[CollectionName],
	[CreationDate]

FROM [dbo].[Collection]

WHERE
	[CollectionID] = @CollectionID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure CollectionSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

