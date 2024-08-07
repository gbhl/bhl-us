﻿
-- NamePageDeleteAuto PROCEDURE
-- Generated 10/29/2012 3:17:36 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for NamePage

CREATE PROCEDURE NamePageDeleteAuto

@NamePageID INT

AS 

DELETE FROM [dbo].[NamePage]

WHERE

	[NamePageID] = @NamePageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NamePageDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

