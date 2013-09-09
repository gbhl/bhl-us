﻿
-- BSItemStatusDeleteAuto PROCEDURE
-- Generated 10/23/2012 3:24:24 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for BSItemStatus

CREATE PROCEDURE BSItemStatusDeleteAuto

@ItemStatusID INT

AS 

DELETE FROM [dbo].[BSItemStatus]

WHERE

	[ItemStatusID] = @ItemStatusID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BSItemStatusDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

