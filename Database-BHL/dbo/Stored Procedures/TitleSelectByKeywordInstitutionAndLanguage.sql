
CREATE PROCEDURE [dbo].[TitleSelectByKeywordInstitutionAndLanguage]
@Keyword nvarchar(50),
@InstitutionCode nvarchar(10),
@LanguageCode nvarchar(10) = ''

AS
SET NOCOUNT ON

-- Get titles tied directly to the specified TagText
SELECT DISTINCT 
		v.TitleID,
		t.FullTitle,
		t.ShortTitle,
		t.SortTitle,
		t.PartNumber,
		t.PartName,
		t.PublicationDetails,
		t.StartYear,
		t.EditionStatement,
		ISNULL(i.InstitutionName, '' ) AS InstitutionName
INTO	#tmpTitle
FROM	dbo.TitleKeywordView v INNER JOIN dbo.Title t
			ON v.TitleID = t.TitleID
		LEFT JOIN dbo.Institution i
			ON v.TitleInstitutionCode = i.InstitutionCode
		LEFT JOIN dbo.TitleLanguage tl
			ON v.TitleID = tl.TitleID
		LEFT JOIN dbo.ItemLanguage il
			ON v.ItemID = il.ItemID
WHERE	v.Keyword = @Keyword
AND		t.PublishReady=1
AND		(v.TitleInstitutionCode = @InstitutionCode OR 
		 v.ItemInstitutionCode = @InstitutionCode OR 
		 @InstitutionCode = '')
AND		(v.TitleLanguageCode = @LanguageCode OR
		 v.ItemLanguageCode = @LanguageCode OR
		 ISNULL(tl.LanguageCode, '') = @LanguageCode OR
		 ISNULL(il.LanguageCode, '') = @LanguageCode OR
		 @LanguageCode = '')
ORDER BY SortTitle

-- Add supporting information for each title to the result set
SELECT	t.TitleID,
		itm.ItemID,
		t.FullTitle,
		t.ShortTitle,
		t.SortTitle,
		t.PartNumber,
		t.PartName,
		t.PublicationDetails,
		CASE WHEN ISNULL(itm.Year, '') = '' THEN CONVERT(nvarchar(20), t.StartYear) ELSE itm.Year END AS [Year],
		t.EditionStatement,
		itm.Volume,
		itm.ExternalUrl,
		t.InstitutionName,
		c.Subjects,
		c.Authors,
		dbo.fnCollectionStringForTitleAndItem(t.TitleID, itm.ItemID) AS Collections--,
--		dbo.fnSeriesStringForTitle (t.TitleID) AS Associations
FROM	#tmpTitle t INNER JOIN (
				-- Get the first item for each title
				SELECT	TitleID, MIN(ItemSequence) MinSeq
				FROM	dbo.TitleItem ti INNER JOIN dbo.Item itm 
						ON ti.ItemID = itm.ItemID 
				WHERE	itm.ItemStatusID = 40
				GROUP BY TitleID
				) AS x 
				ON t.TitleID = x.TitleID
		INNER JOIN dbo.TitleItem ti ON x.TitleID = ti.TitleID AND x.MinSeq = ti.ItemSequence
		INNER JOIN dbo.Item itm ON ti.ItemID = itm.ItemID
		INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND c.ItemID = itm.ItemID
ORDER BY 
		t.SortTitle

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleSelectByTagTextInstitutionAndLanguage. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END





