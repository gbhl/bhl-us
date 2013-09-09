
CREATE PROCEDURE [dbo].[TitleSelectNewByKeyAndSource]

@ImportKey NVARCHAR(50),
@ImportSourceID INT

AS 

SET NOCOUNT ON

SELECT 

	[TitleID],
	[ImportKey],
	[ImportStatusID],
	[ImportSourceID],
	[MARCBibID],
	[MARCLeader],
	[FullTitle],
	[ShortTitle],
	[UniformTitle],
	[SortTitle],
	[PartNumber],
	[PartName],
	[CallNumber],
	[PublicationDetails],
	[StartYear],
	[EndYear],
	[Datafield_260_a],
	[Datafield_260_b],
	[Datafield_260_c],
	[InstitutionCode],
	[LanguageCode],
	[TitleDescription],
	[TL2Author],
	[PublishReady],
	[RareBooks],
	[OriginalCatalogingSource],
	[EditionStatement],
	[CurrentPublicationFrequency],
	[Note],
	[ExternalCreationDate],
	[ExternalLastModifiedDate],
	[ExternalCreationUser],
	[ExternalLastModifiedUser],
	[ProductionDate],
	[ProductionTitleID],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[Title]

WHERE
	ImportKey = @ImportKey
AND	ImportSourceID = @ImportSourceID
AND	ImportStatusID = 10	-- New only

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleSelectNewByKeyAndSource. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
