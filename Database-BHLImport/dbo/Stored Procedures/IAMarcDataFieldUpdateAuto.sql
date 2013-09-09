
-- IAMarcDataFieldUpdateAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for IAMarcDataField

CREATE PROCEDURE IAMarcDataFieldUpdateAuto

@MarcDataFieldID INT,
@MarcID INT,
@Tag NCHAR(3),
@Indicator1 NCHAR(1),
@Indicator2 NCHAR(1)

AS 

SET NOCOUNT ON

UPDATE [dbo].[IAMarcDataField]

SET

	[MarcID] = @MarcID,
	[Tag] = @Tag,
	[Indicator1] = @Indicator1,
	[Indicator2] = @Indicator2,
	[LastModifiedDate] = getdate()

WHERE
	[MarcDataFieldID] = @MarcDataFieldID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAMarcDataFieldUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[MarcDataFieldID],
		[MarcID],
		[Tag],
		[Indicator1],
		[Indicator2],
		[CreatedDate],
		[LastModifiedDate]

	FROM [dbo].[IAMarcDataField]
	
	WHERE
		[MarcDataFieldID] = @MarcDataFieldID
	
	RETURN -- update successful
END

