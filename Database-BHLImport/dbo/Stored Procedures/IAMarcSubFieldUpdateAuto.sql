
-- IAMarcSubFieldUpdateAuto PROCEDURE
-- Generated 7/8/2013 2:53:08 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for IAMarcSubField

CREATE PROCEDURE IAMarcSubFieldUpdateAuto

@MarcSubFieldID INT,
@MarcDataFieldID INT,
@Code NCHAR(1),
@Value NVARCHAR(2000)

AS 

SET NOCOUNT ON

UPDATE [dbo].[IAMarcSubField]

SET

	[MarcDataFieldID] = @MarcDataFieldID,
	[Code] = @Code,
	[Value] = @Value,
	[LastModifiedDate] = getdate()

WHERE
	[MarcSubFieldID] = @MarcSubFieldID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAMarcSubFieldUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[MarcSubFieldID],
		[MarcDataFieldID],
		[Code],
		[Value],
		[CreatedDate],
		[LastModifiedDate]

	FROM [dbo].[IAMarcSubField]
	
	WHERE
		[MarcSubFieldID] = @MarcSubFieldID
	
	RETURN -- update successful
END

