
-- MarcDataFieldUpdateAuto PROCEDURE
-- Generated 11/12/2008 3:12:29 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for MarcDataField

CREATE PROCEDURE MarcDataFieldUpdateAuto

@MarcDataFieldID INT,
@ItemID INT,
@Tag NCHAR(3),
@Indicator1 NCHAR(1),
@Indicator2 NCHAR(1)

AS 

SET NOCOUNT ON

UPDATE [dbo].[MarcDataField]

SET

	[ItemID] = @ItemID,
	[Tag] = @Tag,
	[Indicator1] = @Indicator1,
	[Indicator2] = @Indicator2

WHERE
	[MarcDataFieldID] = @MarcDataFieldID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MarcDataFieldUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[MarcDataFieldID],
		[ItemID],
		[Tag],
		[Indicator1],
		[Indicator2],
		[CreationDate]

	FROM [dbo].[MarcDataField]
	
	WHERE
		[MarcDataFieldID] = @MarcDataFieldID
	
	RETURN -- update successful
END

