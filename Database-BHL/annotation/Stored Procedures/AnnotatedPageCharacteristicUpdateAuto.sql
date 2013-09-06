
-- AnnotatedPageCharacteristicUpdateAuto PROCEDURE
-- Generated 12/15/2010 3:05:49 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for AnnotatedPageCharacteristic

CREATE PROCEDURE annotation.AnnotatedPageCharacteristicUpdateAuto

@AnnotatedPageCharacteristicID INT,
@AnnotatedPageID INT,
@CharacteristicDetail NVARCHAR(MAX),
@CharacteristicDetailClean NVARCHAR(MAX),
@CharacteristicDetailDisplay NVARCHAR(MAX)

AS 

SET NOCOUNT ON

UPDATE annotation.[AnnotatedPageCharacteristic]

SET

	[AnnotatedPageID] = @AnnotatedPageID,
	[CharacteristicDetail] = @CharacteristicDetail,
	[CharacteristicDetailClean] = @CharacteristicDetailClean,
	[CharacteristicDetailDisplay] = @CharacteristicDetailDisplay,
	[LastModifiedDate] = getdate()

WHERE
	[AnnotatedPageCharacteristicID] = @AnnotatedPageCharacteristicID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedPageCharacteristicUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

