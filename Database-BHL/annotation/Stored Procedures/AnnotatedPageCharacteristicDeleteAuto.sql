
-- AnnotatedPageCharacteristicDeleteAuto PROCEDURE
-- Generated 12/15/2010 3:05:49 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for AnnotatedPageCharacteristic

CREATE PROCEDURE annotation.AnnotatedPageCharacteristicDeleteAuto

@AnnotatedPageCharacteristicID INT

AS 

DELETE FROM annotation.[AnnotatedPageCharacteristic]

WHERE

	[AnnotatedPageCharacteristicID] = @AnnotatedPageCharacteristicID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedPageCharacteristicDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

