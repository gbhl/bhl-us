
-- TitleAssociationInsertAuto PROCEDURE
-- Generated 9/4/2008 2:16:32 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for TitleAssociation

CREATE PROCEDURE TitleAssociationInsertAuto

@TitleAssociationID INT OUTPUT,
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
@Active BIT,
@ProductionDate DATETIME = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleAssociation]
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
	[Active],
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
	@Active,
	@ProductionDate,
	getdate(),
	getdate()
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
		[Active],
		[ProductionDate],
		[CreatedDate],
		[LastModifiedDate]	

	FROM [dbo].[TitleAssociation]
	
	WHERE
		[TitleAssociationID] = @TitleAssociationID
	
	RETURN -- insert successful
END

