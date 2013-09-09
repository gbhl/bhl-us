
-- IAMarcSubFieldInsertAuto PROCEDURE
-- Generated 7/8/2013 2:53:08 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for IAMarcSubField

CREATE PROCEDURE IAMarcSubFieldInsertAuto

@MarcSubFieldID INT OUTPUT,
@MarcDataFieldID INT,
@Code NCHAR(1),
@Value NVARCHAR(2000)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IAMarcSubField]
(
	[MarcDataFieldID],
	[Code],
	[Value],
	[CreatedDate],
	[LastModifiedDate]
)
VALUES
(
	@MarcDataFieldID,
	@Code,
	@Value,
	getdate(),
	getdate()
)

SET @MarcSubFieldID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAMarcSubFieldInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

