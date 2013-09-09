
CREATE PROCEDURE [dbo].[Title_CreatorSelectNewByKeyCreatorAndSource]

@ImportKey NVARCHAR(50),
@CreatorName NVARCHAR(255),
@MARCCreator_a NVARCHAR(450),
@MARCCreator_b NVARCHAR(450),
@MARCCreator_c NVARCHAR(450),
@MARCCreator_d NVARCHAR(450),
@CreatorRoleTypeID INT,
@ImportSourceID INT

AS 

SET NOCOUNT ON

SELECT 

	[TitleCreatorID],
	[CreatorName],
	[MARCCreator_a],
	[MARCCreator_b],
	[MARCCreator_c],
	[MARCCreator_d],
	[CreatorRoleTypeID],
	[ImportKey],
	[ImportStatusID],
	[ImportSourceID],
	[ExternalCreationDate],
	[ExternalLastModifiedDate],
	[ExternalCreationUser],
	[ExternalLastModifiedUser],
	[ProductionDate],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[Title_Creator]

WHERE
	[ImportKey] = @ImportKey
AND [CreatorName] = @CreatorName
AND ISNULL([MARCCreator_a], '') = ISNULL(@MARCCreator_a, '')
AND ISNULL([MARCCreator_b], '') = ISNULL(@MARCCreator_b, '')
AND ISNULL([MARCCreator_c], '') = ISNULL(@MARCCreator_c, '')
AND ISNULL([MARCCreator_d], '') = ISNULL(@MARCCreator_d, '')
AND [CreatorRoleTypeID] = @CreatorRoleTypeID
AND [ImportSourceID] = @ImportSourceID
AND	[ImportStatusID] = 10  -- new only

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Title_CreatorSelectNewByKeyCreatorAndSource. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
