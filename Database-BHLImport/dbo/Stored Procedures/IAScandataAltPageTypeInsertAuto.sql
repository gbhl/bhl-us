
-- IAScandataAltPageTypeInsertAuto PROCEDURE
-- Generated 11/23/2010 11:26:17 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for IAScandataAltPageType

CREATE PROCEDURE IAScandataAltPageTypeInsertAuto

@ScandataAltPageTypeID INT OUTPUT,
@ScandataID INT,
@PageType NVARCHAR(50)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IAScandataAltPageType]
(
	[ScandataID],
	[PageType],
	[CreatedDate],
	[LastModifiedDate]
)
VALUES
(
	@ScandataID,
	@PageType,
	getdate(),
	getdate()
)

SET @ScandataAltPageTypeID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAScandataAltPageTypeInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ScandataAltPageTypeID],
		[ScandataID],
		[PageType],
		[CreatedDate],
		[LastModifiedDate]	

	FROM [dbo].[IAScandataAltPageType]
	
	WHERE
		[ScandataAltPageTypeID] = @ScandataAltPageTypeID
	
	RETURN -- insert successful
END

