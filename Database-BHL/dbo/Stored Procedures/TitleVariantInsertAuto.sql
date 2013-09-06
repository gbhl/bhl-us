
-- TitleVariantInsertAuto PROCEDURE
-- Generated 2/15/2011 12:02:06 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for TitleVariant

CREATE PROCEDURE TitleVariantInsertAuto

@TitleVariantID INT OUTPUT,
@TitleID INT,
@TitleVariantTypeID INT,
@Title NVARCHAR(MAX),
@TitleRemainder NVARCHAR(MAX),
@PartNumber NVARCHAR(255),
@PartName NVARCHAR(255),
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleVariant]
(
	[TitleID],
	[TitleVariantTypeID],
	[Title],
	[TitleRemainder],
	[PartNumber],
	[PartName],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@TitleID,
	@TitleVariantTypeID,
	@Title,
	@TitleRemainder,
	@PartNumber,
	@PartName,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @TitleVariantID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleVariantInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[TitleVariantID],
		[TitleID],
		[TitleVariantTypeID],
		[Title],
		[TitleRemainder],
		[PartNumber],
		[PartName],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[TitleVariant]
	
	WHERE
		[TitleVariantID] = @TitleVariantID
	
	RETURN -- insert successful
END

