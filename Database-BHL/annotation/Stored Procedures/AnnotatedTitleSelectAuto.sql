
-- AnnotatedTitleSelectAuto PROCEDURE
-- Generated 7/14/2010 1:25:28 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for AnnotatedTitle

CREATE PROCEDURE [annotation].AnnotatedTitleSelectAuto

@AnnotatedTitleID INT

AS 

SET NOCOUNT ON

SELECT 

	[AnnotatedTitleID],
	[AnnotationSourceID],
	[TitleID],
	[ExternalIdentifier],
	[Author],
	[Title],
	[Edition],
	[Volume],
	[PublicationDetails],
	[Date],
	[Location],
	[IsBeagleEra],
	[Inscription],
	[CreationDate],
	[LastModifiedDate]

FROM [annotation].[AnnotatedTitle]

WHERE
	[AnnotatedTitleID] = @AnnotatedTitleID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedTitleSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

