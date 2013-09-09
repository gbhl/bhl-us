
-- TitleAssociation_TitleIdentifierUpdateAuto PROCEDURE
-- Generated 9/4/2008 2:16:32 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for TitleAssociation_TitleIdentifier

CREATE PROCEDURE TitleAssociation_TitleIdentifierUpdateAuto

@TitleAssociation_TitleIdentifierID INT,
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
@ProductionDate DATETIME

AS 

SET NOCOUNT ON

UPDATE [dbo].[TitleAssociation_TitleIdentifier]

SET

	[ImportKey] = @ImportKey,
	[ImportStatusID] = @ImportStatusID,
	[ImportSourceID] = @ImportSourceID,
	[MARCTag] = @MARCTag,
	[MARCIndicator2] = @MARCIndicator2,
	[Title] = @Title,
	[Section] = @Section,
	[Volume] = @Volume,
	[Heading] = @Heading,
	[Publication] = @Publication,
	[Relationship] = @Relationship,
	[IdentifierName] = @IdentifierName,
	[IdentifierValue] = @IdentifierValue,
	[ProductionDate] = @ProductionDate,
	[LastModifiedDate] = getdate()

WHERE
	[TitleAssociation_TitleIdentifierID] = @TitleAssociation_TitleIdentifierID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAssociation_TitleIdentifierUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

