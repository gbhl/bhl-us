
-- TitleSelectAuto PROCEDURE
-- Generated 5/19/2009 10:35:29 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for Title

CREATE PROCEDURE TitleSelectAuto

@TitleID INT

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
	[TitleID] = @TitleID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

