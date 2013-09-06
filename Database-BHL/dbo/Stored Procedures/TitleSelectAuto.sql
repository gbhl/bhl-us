
-- TitleSelectAuto PROCEDURE
-- Generated 8/3/2010 11:16:34 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for Title

CREATE PROCEDURE TitleSelectAuto

@TitleID INT

AS 

SET NOCOUNT ON

SELECT 

	[TitleID],
	[MARCBibID],
	[MARCLeader],
	[BibliographicLevelID],
	[TropicosTitleID],
	[RedirectTitleID],
	[FullTitle],
	[ShortTitle],
	[UniformTitle],
	[SortTitle],
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
	[Note],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID],
	[OriginalCatalogingSource],
	[EditionStatement],
	[CurrentPublicationFrequency],
	[PartNumber],
	[PartName]

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

