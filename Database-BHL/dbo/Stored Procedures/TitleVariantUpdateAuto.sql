
-- TitleVariantUpdateAuto PROCEDURE
-- Generated 2/15/2011 12:02:06 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for TitleVariant

CREATE PROCEDURE TitleVariantUpdateAuto

@TitleVariantID INT,
@TitleID INT,
@TitleVariantTypeID INT,
@Title NVARCHAR(MAX),
@TitleRemainder NVARCHAR(MAX),
@PartNumber NVARCHAR(255),
@PartName NVARCHAR(255),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[TitleVariant]

SET

	[TitleID] = @TitleID,
	[TitleVariantTypeID] = @TitleVariantTypeID,
	[Title] = @Title,
	[TitleRemainder] = @TitleRemainder,
	[PartNumber] = @PartNumber,
	[PartName] = @PartName,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[TitleVariantID] = @TitleVariantID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleVariantUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

