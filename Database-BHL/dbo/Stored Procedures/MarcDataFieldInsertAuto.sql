
-- MarcDataFieldInsertAuto PROCEDURE
-- Generated 4/15/2009 3:34:26 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for MarcDataField

CREATE PROCEDURE MarcDataFieldInsertAuto

@MarcDataFieldID INT OUTPUT,
@MarcID INT,
@Tag NCHAR(3),
@Indicator1 NCHAR(1),
@Indicator2 NCHAR(1)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[MarcDataField]
(
	[MarcID],
	[Tag],
	[Indicator1],
	[Indicator2],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@MarcID,
	@Tag,
	@Indicator1,
	@Indicator2,
	getdate(),
	getdate()
)

SET @MarcDataFieldID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MarcDataFieldInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

