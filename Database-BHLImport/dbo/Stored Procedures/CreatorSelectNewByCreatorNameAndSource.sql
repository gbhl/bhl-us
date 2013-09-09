
CREATE PROCEDURE [dbo].[CreatorSelectNewByCreatorNameAndSource]

@MARCCreator_a NVARCHAR(450),
@MARCCreator_b NVARCHAR(450),
@MARCCreator_c NVARCHAR(450),
@MARCCreator_d NVARCHAR(450),
@ImportSourceID INT

AS 

SET NOCOUNT ON

SELECT 

	[CreatorID],
	[ImportStatusID],
	[ImportSourceID],
	[CreatorName],
	[FirstNameFirst],
	[SimpleName],
	[DOB],
	[DOD],
	[Biography],
	[CreatorNote],
	[MARCDataFieldTag],
	[MARCCreator_a],
	[MARCCreator_b],
	[MARCCreator_c],
	[MARCCreator_d],
	[MARCCreator_Full],
	[ExternalCreationDate],
	[ExternalLastModifiedDate],
	[ProductionDate],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[Creator]

WHERE
	ISNULL([MARCCreator_a], '') = ISNULL(@MARCCreator_a, '')
AND	ISNULL([MARCCreator_b], '') = ISNULL(@MARCCreator_b, '')
AND	ISNULL([MARCCreator_c], '') = ISNULL(@MARCCreator_c, '')
AND	ISNULL([MARCCreator_d], '') = ISNULL(@MARCCreator_d, '')
AND [ImportSourceID] = @ImportSourceID
AND	[ImportStatusID] = 10  -- new only

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure CreatorSelectNewByCreatorNameAndSource. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

