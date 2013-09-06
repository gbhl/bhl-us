
-- AnnotatedPageCharacteristicSelectAuto PROCEDURE
-- Generated 12/15/2010 3:05:49 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for AnnotatedPageCharacteristic

CREATE PROCEDURE annotation.AnnotatedPageCharacteristicSelectAuto

@AnnotatedPageCharacteristicID INT

AS 

SET NOCOUNT ON

SELECT 

	[AnnotatedPageCharacteristicID],
	[AnnotatedPageID],
	[CharacteristicDetail],
	[CharacteristicDetailClean],
	[CharacteristicDetailDisplay],
	[CreationDate],
	[LastModifiedDate]

FROM annotation.[AnnotatedPageCharacteristic]

WHERE
	[AnnotatedPageCharacteristicID] = @AnnotatedPageCharacteristicID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedPageCharacteristicSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

