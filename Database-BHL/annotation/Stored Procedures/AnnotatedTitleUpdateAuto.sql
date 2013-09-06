
-- AnnotatedTitleUpdateAuto PROCEDURE
-- Generated 7/14/2010 1:25:28 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for AnnotatedTitle

CREATE PROCEDURE [annotation].AnnotatedTitleUpdateAuto

@AnnotatedTitleID INT,
@AnnotationSourceID INT,
@TitleID INT,
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

UPDATE [annotation].[AnnotatedTitle]

SET

	[AnnotationSourceID] = @AnnotationSourceID,
	[TitleID] = @TitleID,
	[ExternalIdentifier] = @ExternalIdentifier,
	[Author] = @Author,
	[Title] = @Title,
	[Edition] = @Edition,
	[Volume] = @Volume,
	[PublicationDetails] = @PublicationDetails,
	[Date] = @Date,
	[Location] = @Location,
	[IsBeagleEra] = @IsBeagleEra,
	[Inscription] = @Inscription,
	[LastModifiedDate] = getdate()

WHERE
	[AnnotatedTitleID] = @AnnotatedTitleID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedTitleUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

