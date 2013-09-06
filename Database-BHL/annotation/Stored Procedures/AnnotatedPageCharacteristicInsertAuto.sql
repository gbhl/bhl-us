
-- AnnotatedPageCharacteristicInsertAuto PROCEDURE
-- Generated 12/15/2010 3:05:49 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for AnnotatedPageCharacteristic

CREATE PROCEDURE annotation.AnnotatedPageCharacteristicInsertAuto

@AnnotatedPageCharacteristicID INT OUTPUT,
@AnnotatedPageID INT = null,
@CharacteristicDetail NVARCHAR(MAX),
@CharacteristicDetailClean NVARCHAR(MAX),
@CharacteristicDetailDisplay NVARCHAR(MAX)

AS 

SET NOCOUNT ON

INSERT INTO annotation.[AnnotatedPageCharacteristic]
(
	[AnnotatedPageID],
	[CharacteristicDetail],
	[CharacteristicDetailClean],
	[CharacteristicDetailDisplay],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@AnnotatedPageID,
	@CharacteristicDetail,
	@CharacteristicDetailClean,
	@CharacteristicDetailDisplay,
	getdate(),
	getdate()
)

SET @AnnotatedPageCharacteristicID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedPageCharacteristicInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

