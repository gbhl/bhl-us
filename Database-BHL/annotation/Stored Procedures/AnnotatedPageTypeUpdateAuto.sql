
-- AnnotatedPageTypeUpdateAuto PROCEDURE
-- Generated 12/20/2010 4:03:38 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for AnnotatedPageType

CREATE PROCEDURE [annotation].AnnotatedPageTypeUpdateAuto

@AnnotatedPageTypeID INT,
@AnnotatedPageTypeName NVARCHAR(30),
@AnnotatedPageTypeDescription NVARCHAR(500)

AS 

SET NOCOUNT ON

UPDATE [annotation].[AnnotatedPageType]

SET

	[AnnotatedPageTypeName] = @AnnotatedPageTypeName,
	[AnnotatedPageTypeDescription] = @AnnotatedPageTypeDescription,
	[LastModifiedDate] = getdate()

WHERE
	[AnnotatedPageTypeID] = @AnnotatedPageTypeID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedPageTypeUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

