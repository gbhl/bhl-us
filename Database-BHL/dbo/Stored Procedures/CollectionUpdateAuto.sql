
-- CollectionUpdateAuto PROCEDURE
-- Generated 4/2/2012 3:02:06 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Collection

CREATE PROCEDURE CollectionUpdateAuto

@CollectionID INT,
@CollectionName NVARCHAR(50),
@CollectionDescription NVARCHAR(4000),
@CollectionURL NVARCHAR(50),
@ImageURL NVARCHAR(100),
@HtmlContent NVARCHAR(MAX),
@CanContainTitles SMALLINT,
@CanContainItems SMALLINT,
@InstitutionCode NVARCHAR(10),
@LanguageCode NVARCHAR(10),
@CollectionTarget NVARCHAR(30),
@ITunesImageURL NVARCHAR(100),
@ITunesURL NVARCHAR(100),
@Featured SMALLINT,
@Active SMALLINT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Collection]

SET

	[CollectionName] = @CollectionName,
	[CollectionDescription] = @CollectionDescription,
	[CollectionURL] = @CollectionURL,
	[ImageURL] = @ImageURL,
	[HtmlContent] = @HtmlContent,
	[CanContainTitles] = @CanContainTitles,
	[CanContainItems] = @CanContainItems,
	[InstitutionCode] = @InstitutionCode,
	[LanguageCode] = @LanguageCode,
	[CollectionTarget] = @CollectionTarget,
	[ITunesImageURL] = @ITunesImageURL,
	[ITunesURL] = @ITunesURL,
	[Featured] = @Featured,
	[Active] = @Active,
	[LastModifiedDate] = getdate()

WHERE
	[CollectionID] = @CollectionID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure CollectionUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

