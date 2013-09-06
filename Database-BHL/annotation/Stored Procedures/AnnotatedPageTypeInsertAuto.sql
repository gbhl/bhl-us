
-- AnnotatedPageTypeInsertAuto PROCEDURE
-- Generated 12/20/2010 4:03:38 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for AnnotatedPageType

CREATE PROCEDURE [annotation].AnnotatedPageTypeInsertAuto

@AnnotatedPageTypeID INT OUTPUT,
@AnnotatedPageTypeName NVARCHAR(30),
@AnnotatedPageTypeDescription NVARCHAR(500)

AS 

SET NOCOUNT ON

INSERT INTO [annotation].[AnnotatedPageType]
(
	[AnnotatedPageTypeName],
	[AnnotatedPageTypeDescription],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@AnnotatedPageTypeName,
	@AnnotatedPageTypeDescription,
	getdate(),
	getdate()
)

SET @AnnotatedPageTypeID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedPageTypeInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[AnnotatedPageTypeID],
		[AnnotatedPageTypeName],
		[AnnotatedPageTypeDescription],
		[CreationDate],
		[LastModifiedDate]	

	FROM [annotation].[AnnotatedPageType]
	
	WHERE
		[AnnotatedPageTypeID] = @AnnotatedPageTypeID
	
	RETURN -- insert successful
END

