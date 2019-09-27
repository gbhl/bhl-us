CREATE PROCEDURE dbo.MarcSubFieldUpdateAuto

@MarcSubFieldID INT,
@MarcDataFieldID INT,
@Code NCHAR(1),
@Value NVARCHAR(MAX)

AS 

SET NOCOUNT ON

UPDATE [dbo].[MarcSubField]
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
	RAISERROR('An error occurred in procedure dbo.MarcSubFieldUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[MarcSubFieldID],
		[MarcDataFieldID],
		[Code],
		[Value],
		[CreationDate],
		[LastModifiedDate]
	FROM [dbo].[MarcSubField]
	WHERE
		[MarcSubFieldID] = @MarcSubFieldID
	
	RETURN -- update successful
END
