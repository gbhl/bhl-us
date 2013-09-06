
-- CollectionInsertAuto PROCEDURE
-- Generated 4/2/2012 3:02:06 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Collection

CREATE PROCEDURE CollectionInsertAuto

@CollectionID INT OUTPUT,
@CollectionName NVARCHAR(50),
@CollectionDescription NVARCHAR(4000),
@CollectionURL NVARCHAR(50),
@ImageURL NVARCHAR(100),
@HtmlContent NVARCHAR(MAX),
@CanContainTitles SMALLINT,
@CanContainItems SMALLINT,
@InstitutionCode NVARCHAR(10) = null,
@LanguageCode NVARCHAR(10) = null,
@CollectionTarget NVARCHAR(30),
@ITunesImageURL NVARCHAR(100),
@ITunesURL NVARCHAR(100),
@Featured SMALLINT,
@Active SMALLINT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Collection]
(
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
)
VALUES
(
	@CollectionName,
	@CollectionDescription,
	@CollectionURL,
	@ImageURL,
	@HtmlContent,
	@CanContainTitles,
	@CanContainItems,
	@InstitutionCode,
	@LanguageCode,
	@CollectionTarget,
	@ITunesImageURL,
	@ITunesURL,
	@Featured,
	@Active,
	getdate(),
	getdate()
)

SET @CollectionID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure CollectionInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

