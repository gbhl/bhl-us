
-- CollectionSelectAuto PROCEDURE
-- Generated 4/2/2012 3:02:06 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Collection

CREATE PROCEDURE CollectionSelectAuto

@CollectionID INT

AS 

SET NOCOUNT ON

SELECT 

	[CollectionID],
	[CollectionName],
	[CollectionDescription],
	[CollectionURL],
	[ImageURL],
	[HtmlContent],
	[CanContainTitles],
	[CanContainItems],
	[InstitutionCode],
	[LanguageCode],
	[CollectionTarget],
	[ITunesImageURL],
	[ITunesURL],
	[Featured],
	[Active],
	[CreationDate],
	[LastModifiedDate]

FROM [dbo].[Collection]

WHERE
	[CollectionID] = @CollectionID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure CollectionSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

