
-- TitleAssociationInsertAuto PROCEDURE
-- Generated 2/4/2011 2:43:35 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for TitleAssociation

CREATE PROCEDURE TitleAssociationInsertAuto

@TitleAssociationID INT OUTPUT,
@TitleID INT,
@TitleAssociationTypeID INT,
@Title NVARCHAR(500),
@Section NVARCHAR(500),
@Volume NVARCHAR(500),
@Active BIT,
@AssociatedTitleID INT = null,
@Heading NVARCHAR(500),
@Publication NVARCHAR(500),
@Relationship NVARCHAR(500),
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleAssociation]
(
	[TitleID],
	[TitleAssociationTypeID],
	[Title],
	[Section],
	[Volume],
	[Active],
	[AssociatedTitleID],
	[CreationDate],
	[LastModifiedDate],
	[Heading],
	[Publication],
	[Relationship],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@TitleID,
	@TitleAssociationTypeID,
	@Title,
	@Section,
	@Volume,
	@Active,
	@AssociatedTitleID,
	getdate(),
	getdate(),
	@Heading,
	@Publication,
	@Relationship,
	@CreationUserID,
	@LastModifiedUserID
)

SET @TitleAssociationID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAssociationInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[TitleAssociationID],
		[TitleID],
		[TitleAssociationTypeID],
		[Title],
		[Section],
		[Volume],
		[Active],
		[AssociatedTitleID],
		[CreationDate],
		[LastModifiedDate],
		[Heading],
		[Publication],
		[Relationship],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[TitleAssociation]
	
	WHERE
		[TitleAssociationID] = @TitleAssociationID
	
	RETURN -- insert successful
END

