
-- AnnotatedPageSelectAuto PROCEDURE
-- Generated 7/14/2010 1:25:28 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for AnnotatedPage

CREATE PROCEDURE [annotation].AnnotatedPageSelectAuto

@AnnotatedPageID INT

AS 

SET NOCOUNT ON

SELECT 

	[AnnotatedPageID],
	[AnnotatedItemID],
	[PageID],
	[ExternalIdentifier],
	[AnnotatedPageTypeID],
	[PageNumber],
	[CreationDate],
	[LastModifiedDate]

FROM [annotation].[AnnotatedPage]

WHERE
	[AnnotatedPageID] = @AnnotatedPageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedPageSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

