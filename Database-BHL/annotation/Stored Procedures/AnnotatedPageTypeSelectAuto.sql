
-- AnnotatedPageTypeSelectAuto PROCEDURE
-- Generated 12/20/2010 4:03:38 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for AnnotatedPageType

CREATE PROCEDURE [annotation].AnnotatedPageTypeSelectAuto

@AnnotatedPageTypeID INT

AS 

SET NOCOUNT ON

SELECT 

	[AnnotatedPageTypeID],
	[AnnotatedPageTypeName],
	[AnnotatedPageTypeDescription],
	[CreationDate],
	[LastModifiedDate]

FROM [annotation].[AnnotatedPageType]

WHERE
	[AnnotatedPageTypeID] = @AnnotatedPageTypeID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedPageTypeSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

