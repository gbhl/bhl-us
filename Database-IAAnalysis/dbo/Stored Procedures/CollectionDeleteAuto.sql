﻿
-- CollectionDeleteAuto PROCEDURE
-- Generated 11/12/2008 3:38:13 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Collection

CREATE PROCEDURE CollectionDeleteAuto

@CollectionID INT

AS 

DELETE FROM [dbo].[Collection]

WHERE

	[CollectionID] = @CollectionID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure CollectionDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

