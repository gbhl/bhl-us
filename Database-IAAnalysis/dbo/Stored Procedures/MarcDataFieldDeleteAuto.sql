﻿
-- MarcDataFieldDeleteAuto PROCEDURE
-- Generated 11/12/2008 3:12:29 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for MarcDataField

CREATE PROCEDURE MarcDataFieldDeleteAuto

@MarcDataFieldID INT

AS 

DELETE FROM [dbo].[MarcDataField]

WHERE

	[MarcDataFieldID] = @MarcDataFieldID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MarcDataFieldDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

