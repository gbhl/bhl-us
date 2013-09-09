
-- TitleAssociation_TitleIdentifierInsertAuto PROCEDURE
-- Generated 9/4/2008 2:16:32 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for TitleAssociation_TitleIdentifier

CREATE PROCEDURE TitleAssociation_TitleIdentifierInsertAuto

@TitleAssociation_TitleIdentifierID INT OUTPUT,
@ImportKey NVARCHAR(50),
@ImportStatusID INT,
@ImportSourceID INT,
@MARCTag NVARCHAR(20),
@MARCIndicator2 NCHAR(1),
@Title NVARCHAR(500),
@Section NVARCHAR(500),
@Volume NVARCHAR(500),
@Heading NVARCHAR(500),
@Publication NVARCHAR(500),
@Relationship NVARCHAR(500),
@IdentifierName NVARCHAR(40),
@IdentifierValue NVARCHAR(125),
@ProductionDate DATETIME = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleAssociation_TitleIdentifier]
(
	[ImportKey],
	[ImportStatusID],
	[ImportSourceID],
	[MARCTag],
	[MARCIndicator2],
	[Title],
	[Section],
	[Volume],
	[Heading],
	[Publication],
	[Relationship],
	[IdentifierName],
	[IdentifierValue],
	[ProductionDate],
	[CreatedDate],
	[LastModifiedDate]
)
VALUES
(
	@ImportKey,
	@ImportStatusID,
	@ImportSourceID,
	@MARCTag,
	@MARCIndicator2,
	@Title,
	@Section,
	@Volume,
	@Heading,
	@Publication,
	@Relationship,
	@IdentifierName,
	@IdentifierValue,
	@ProductionDate,
	getdate(),
	getdate()
)

SET @TitleAssociation_TitleIdentifierID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAssociation_TitleIdentifierInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[TitleAssociation_TitleIdentifierID],
		[ImportKey],
		[ImportStatusID],
		[ImportSourceID],
		[MARCTag],
		[MARCIndicator2],
		[Title],
		[Section],
		[Volume],
		[Heading],
		[Publication],
		[Relationship],
		[IdentifierName],
		[IdentifierValue],
		[ProductionDate],
		[CreatedDate],
		[LastModifiedDate]	

	FROM [dbo].[TitleAssociation_TitleIdentifier]
	
	WHERE
		[TitleAssociation_TitleIdentifierID] = @TitleAssociation_TitleIdentifierID
	
	RETURN -- insert successful
END

