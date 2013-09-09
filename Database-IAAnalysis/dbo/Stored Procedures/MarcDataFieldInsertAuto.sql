
-- MarcDataFieldInsertAuto PROCEDURE
-- Generated 11/12/2008 3:12:29 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for MarcDataField

CREATE PROCEDURE MarcDataFieldInsertAuto

@MarcDataFieldID INT OUTPUT,
@ItemID INT,
@Tag NCHAR(3),
@Indicator1 NCHAR(1),
@Indicator2 NCHAR(1)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[MarcDataField]
(
	[ItemID],
	[Tag],
	[Indicator1],
	[Indicator2],
	[CreationDate]
)
VALUES
(
	@ItemID,
	@Tag,
	@Indicator1,
	@Indicator2,
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
		[ItemID],
		[Tag],
		[Indicator1],
		[Indicator2],
		[CreationDate]	

	FROM [dbo].[MarcDataField]
	
	WHERE
		[MarcDataFieldID] = @MarcDataFieldID
	
	RETURN -- insert successful
END

