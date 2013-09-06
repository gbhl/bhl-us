
-- TitleAssociationUpdateAuto PROCEDURE
-- Generated 2/4/2011 2:43:35 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for TitleAssociation

CREATE PROCEDURE TitleAssociationUpdateAuto

@TitleAssociationID INT,
@TitleID INT,
@TitleAssociationTypeID INT,
@Title NVARCHAR(500),
@Section NVARCHAR(500),
@Volume NVARCHAR(500),
@Active BIT,
@AssociatedTitleID INT,
@Heading NVARCHAR(500),
@Publication NVARCHAR(500),
@Relationship NVARCHAR(500),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[TitleAssociation]

SET

	[TitleID] = @TitleID,
	[TitleAssociationTypeID] = @TitleAssociationTypeID,
	[Title] = @Title,
	[Section] = @Section,
	[Volume] = @Volume,
	[Active] = @Active,
	[AssociatedTitleID] = @AssociatedTitleID,
	[LastModifiedDate] = getdate(),
	[Heading] = @Heading,
	[Publication] = @Publication,
	[Relationship] = @Relationship,
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[TitleAssociationID] = @TitleAssociationID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAssociationUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

