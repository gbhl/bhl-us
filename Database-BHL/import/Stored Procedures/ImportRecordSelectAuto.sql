CREATE PROCEDURE [import].[ImportRecordSelectAuto]

@ImportRecordID INT

AS 

SET NOCOUNT ON

SELECT	
	[ImportRecordID],
	[ImportFileID],
	[ImportRecordStatusID],
	[TitleID],
	[ItemID],
	[SegmentID],
	[Genre],
	[Title],
	[TranslatedTitle],
	[JournalTitle],
	[Volume],
	[Series],
	[Issue],
	[Edition],
	[PublicationDetails],
	[PublisherName],
	[PublisherPlace],
	[Year],
	[StartYear],
	[EndYear],
	[Language],
	[Summary],
	[Notes],
	[Rights],
	[DueDiligence],
	[CopyrightStatus],
	[License],
	[LicenseUrl],
	[PageRange],
	[StartPage],
	[StartPageID],
	[EndPage],
	[EndPageID],
	[Url],
	[DownloadUrl],
	[DOI],
	[ISSN],
	[ISBN],
	[OCLC],
	[LCCN],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID],
	[ImportSegmentID],
	[ARK],
	[Biostor],
	[JSTOR],
	[TL2],
	[Wikidata]
FROM	
	[import].[ImportRecord]
WHERE	
	[ImportRecordID] = @ImportRecordID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure import.ImportRecordSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
