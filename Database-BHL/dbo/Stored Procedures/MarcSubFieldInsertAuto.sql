CREATE PROCEDURE dbo.MarcSubFieldInsertAuto

@MarcSubFieldID INT OUTPUT,
@MarcDataFieldID INT,
@Code NCHAR(1),
@Value NVARCHAR(MAX)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[MarcSubField]
( 	[MarcDataFieldID],
	[Code],
	[Value],
	[CreationDate],
	[LastModifiedDate] )
VALUES
( 	@MarcDataFieldID,
	@Code,
	@Value,
	getdate(),
	getdate() )

SET @MarcSubFieldID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.MarcSubFieldInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

