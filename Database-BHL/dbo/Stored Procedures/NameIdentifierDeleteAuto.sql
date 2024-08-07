﻿
-- NameIdentifierDeleteAuto PROCEDURE
-- Generated 9/18/2012 11:59:15 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for NameIdentifier

CREATE PROCEDURE NameIdentifierDeleteAuto

@NameIdentifierID INT

AS 

DELETE FROM [dbo].[NameIdentifier]

WHERE

	[NameIdentifierID] = @NameIdentifierID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameIdentifierDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

