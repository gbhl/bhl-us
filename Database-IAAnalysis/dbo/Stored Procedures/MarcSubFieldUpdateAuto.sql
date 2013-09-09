
-- MarcSubFieldUpdateAuto PROCEDURE
-- Generated 11/12/2008 3:12:29 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for MarcSubField

CREATE PROCEDURE MarcSubFieldUpdateAuto

@MarcSubFieldID INT,
@MarcDataFieldID INT,
@Code NCHAR(1),
@Value NVARCHAR(200)

AS 

SET NOCOUNT ON

UPDATE [dbo].[MarcSubField]

SET

	[MarcDataFieldID] = @MarcDataFieldID,
	[Code] = @Code,
	[Value] = @Value

WHERE
	[MarcSubFieldID] = @MarcSubFieldID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MarcSubFieldUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[MarcSubFieldID],
		[MarcDataFieldID],
		[Code],
		[Value],
		[CreationDate]

	FROM [dbo].[MarcSubField]
	
	WHERE
		[MarcSubFieldID] = @MarcSubFieldID
	
	RETURN -- update successful
END

