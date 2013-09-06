
-- MarcDataFieldUpdateAuto PROCEDURE
-- Generated 4/15/2009 3:34:26 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for MarcDataField

CREATE PROCEDURE MarcDataFieldUpdateAuto

@MarcDataFieldID INT,
@MarcID INT,
@Tag NCHAR(3),
@Indicator1 NCHAR(1),
@Indicator2 NCHAR(1)

AS 

SET NOCOUNT ON

UPDATE [dbo].[MarcDataField]

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
	RAISERROR('An error occurred in procedure MarcDataFieldUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[MarcDataFieldID],
		[MarcID],
		[Tag],
		[Indicator1],
		[Indicator2],
		[CreationDate],
		[LastModifiedDate]

	FROM [dbo].[MarcDataField]
	
	WHERE
		[MarcDataFieldID] = @MarcDataFieldID
	
	RETURN -- update successful
END

