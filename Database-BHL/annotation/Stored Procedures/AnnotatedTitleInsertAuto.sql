
-- AnnotatedTitleInsertAuto PROCEDURE
-- Generated 7/14/2010 1:25:28 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for AnnotatedTitle

CREATE PROCEDURE [annotation].AnnotatedTitleInsertAuto

@AnnotatedTitleID INT OUTPUT,
@AnnotationSourceID INT,
@TitleID INT = null,
@ExternalIdentifier NVARCHAR(50),
@Author NVARCHAR(100),
@Title NVARCHAR(200),
@Edition NVARCHAR(50),
@Volume NVARCHAR(50),
@PublicationDetails NVARCHAR(100),
@Date NVARCHAR(20),
@Location NVARCHAR(50),
@IsBeagleEra NVARCHAR(200),
@Inscription NVARCHAR(200)

AS 

SET NOCOUNT ON

INSERT INTO [annotation].[AnnotatedTitle]
(
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
)
VALUES
(
	@AnnotationSourceID,
	@TitleID,
	@ExternalIdentifier,
	@Author,
	@Title,
	@Edition,
	@Volume,
	@PublicationDetails,
	@Date,
	@Location,
	@IsBeagleEra,
	@Inscription,
	getdate(),
	getdate()
)

SET @AnnotatedTitleID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedTitleInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- insert successful
END

