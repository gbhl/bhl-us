
CREATE PROCEDURE [dbo].[DoAnalysis]

@SampleDate DATETIME = '1/1/1980'

AS

BEGIN

SET NOCOUNT ON

-- If a sample date was not supplied, use the most recent sample
IF (ISNULL(@SampleDate, '1/1/1980') = '1/1/1980') SELECT @SampleDate = MAX(SampleDate) FROM SubjectSample

-- Get the 650a subject headings from the IA materials.  Consider only subjects
-- where the second indicator is 0 or 3.
SELECT	ItemID, MarcDataFieldID, MarcSubFieldID, DataFieldTag, Code, SubFieldValue
INTO	#tmpMarc 
FROM	vwMarcDetail 
WHERE	DataFieldTag = '650' AND Code = 'a' AND Indicator2 IN ('0', '3')

--------------------------

-- Get the IA materials with subject headings that have a match in the sample of BHL subject headings
CREATE TABLE #tmpSub (
	ItemID int NOT NULL,
	MarcDataFieldID int NOT NULL,
	MarcSubFieldID int NOT NULL,
	DataFieldTag nchar(3) NOT NULL,
	Code nchar(1) NOT NULL,
	SubFieldValue nvarchar(200) NOT NULL
)

DECLARE curSubjects CURSOR READ_ONLY
FOR SELECT TagText FROM SubjectSample WHERE SampleDate = @SampleDate

DECLARE @TagText varchar(50)
OPEN curSubjects

FETCH NEXT FROM curSubjects INTO @TagText
WHILE (@@fetch_status <> -1)
BEGIN
	IF (@@fetch_status <> -2)
	BEGIN
		-------------------------------------------
		INSERT INTO #tmpSub SELECT * FROM  #tmpMarc WHERE SubFieldValue LIKE @TagText
		INSERT INTO #tmpSub SELECT * FROM  #tmpMarc WHERE SubFieldValue LIKE @TagText + '.'
		-------------------------------------------
	END
	FETCH NEXT FROM curSubjects INTO @TagText
END

CLOSE curSubjects
DEALLOCATE curSubjects

--------------------------

-- Handle special cases

-- Remove "mother" instances picked up with "moth"
DELETE FROM #tmpSub WHERE SubFieldValue LIKE '%mother%'

-- Remove instances of nuclear plants, coal plants, etc (picked up with "plant")
DELETE FROM #tmpSub 
WHERE SubFieldValue LIKE '%disposal%plants%'
OR SubFieldValue LIKE '%treatment%plants%'
OR SubFieldValue LIKE '%power%plants%'
OR SubFieldValue LIKE '%electric%plants%'
OR SubFieldValue LIKE '%nuclear%plants%'
OR SubFieldValue LIKE '%coal%plants%'

-- Remove instances of "fishing" picked up with "fish"
DELETE FROM #tmpSub WHERE SubFieldValue LIKE '%fishing%'

-- Remove instances of "Gynecology" picked up with "ecology"
DELETE FROM #tmpSub WHERE SubFieldValue LIKE '%gynecology%'

-- Remove instances of "semites" picked up with "mites"
DELETE FROM #tmpSub WHERE SubFieldValue LIKE '%semites%'

--------------------------

SELECT ItemID, DataFieldTag, Code, MIN(SubFieldValue) AS SubFieldValue
INTO #tmpItem
FROM  #tmpSub
GROUP BY ItemID, DataFieldTag, Code

SELECT	DISTINCT 
		i.itemid, i.identifier, i.sponsor, i.contributor, t.datafieldtag, 
		t.subfieldvalue AS category, 
		m.subfieldvalue AS title, 
		i.marcleader, 
		SPACE(100) AS oclc, 
		SPACE(100) AS lc, 
		SPACE(100) AS isbn, 
		SPACE(100) AS issn, 
		SPACE(100) AS ddc, 
		SPACE(100) AS nlm, 
		SPACE(100) AS nal, 
		GETDATE() AS creationdate
INTO	#tmpBySubject
FROM 	Item i INNER JOIN #tmpItem t
			ON i.itemid = t.itemid
		INNER JOIN vwMarcDetail m
			ON i.itemid = m.itemid
WHERE	m.datafieldtag = '245'
AND		m.code IN ('a')
ORDER BY category

DROP TABLE #tmpSub
DROP TABLE #tmpItem
DROP TABLE #tmpMarc


--------------------------

SELECT	ItemID, MarcDataFieldID, MarcSubFieldID, DataFieldTag, Code, SubFieldValue
INTO	#tmpCDL
FROM	vwmarcdetail
WHERE	itemid IN (
	SELECT	i.itemid 
	FROM	collection c INNER JOIN itemcollection ic
				ON c.collectionid = ic.collectionid
			INNER JOIN item i
				ON ic.itemid = i.itemid
	WHERE	itemstatusid = 20
	)
AND		(datafieldtag = '050' AND code = 'a')

INSERT INTO #tmpCDL
SELECT	ItemID, MarcDataFieldID, MarcSubFieldID, DataFieldTag, Code, SubFieldValue
FROM	vwmarcdetail
WHERE	itemid IN (
	SELECT	i.itemid 
	FROM	collection c INNER JOIN itemcollection ic
				ON c.collectionid = ic.collectionid
			INNER JOIN item i
				ON ic.itemid = i.itemid
	WHERE	itemstatusid = 20
	)
AND		itemid NOT IN (SELECT itemid FROM  #tmpCDL)
AND		(datafieldtag = '090' AND code = 'a')

INSERT INTO #tmpCDL
SELECT	ItemID, MarcDataFieldID, MarcSubFieldID, DataFieldTag, Code, SubFieldValue
FROM	vwmarcdetail
WHERE	itemid IN (
	SELECT	i.itemid 
	FROM	collection c INNER JOIN itemcollection ic
				ON c.collectionid = ic.collectionid
			INNER JOIN item i
				ON ic.itemid = i.itemid
	WHERE	itemstatusid = 20
	)
AND		itemid NOT IN (SELECT itemid FROM  #tmpCDL)
AND		(datafieldtag = '082' AND code = 'a')

INSERT INTO #tmpCDL
SELECT	ItemID, MarcDataFieldID, MarcSubFieldID, DataFieldTag, Code, SubFieldValue
FROM	vwmarcdetail
WHERE	itemid IN (
	SELECT	i.itemid 
	FROM	collection c INNER JOIN itemcollection ic
				ON c.collectionid = ic.collectionid
			INNER JOIN item i
				ON ic.itemid = i.itemid
	WHERE	itemstatusid = 20
	)
AND		itemid NOT IN (SELECT itemid FROM  #tmpCDL)
AND		(datafieldtag = '092' AND code = 'a')

-- Look for items IN the appropriate categories (LC Call Numbers)
SELECT	DISTINCT itemid, datafieldtag, code, subfieldvalue
INTO	#tmpItem2
FROM	#tmpCDL
WHERE	(SUBSTRING(SubFieldValue, 1, 2) IN ('QH', 'QK', 'QL', 'SB', 'SD', 'SF', 'SH')
OR		SUBSTRING(REPLACE(SubFieldValue, ' ', ''), 1, 3) LIKE 'QE7%'
OR		SUBSTRING(REPLACE(SubFieldValue, ' ', ''), 1, 3) LIKE 'QE8%'
OR		SUBSTRING(REPLACE(SubFieldValue, ' ', ''), 1, 3) LIKE 'QE9%')
AND		DataFieldTag IN ('050', '090')
AND		SubFieldValue <> 'SHELVED BY TITLE'

-- Look for items in the appropriate categories (Dewey Numbers)
INSERT INTO #tmpItem2
SELECT	DISTINCT ItemID, DataFieldTag, Code, SubFieldValue
FROM	#tmpCDL
WHERE	SUBSTRING(SubFieldValue, 1, 3) IN ('508', 
			'560', '561', '562', '563', '564', '565', '566', '567', '568', '569', '573', '582', '583', '584', 
			'585', '586', '587', '588', '589', '592', '593', '594', '595', '596', '597', '598', '599', '638')
AND		DataFieldTag IN ('082', '092')
AND		ItemID NOT IN (SELECT ItemID FROM #tmpItem2)

-- Get titles for the items found IN the correct categories (include category information)
SELECT	DISTINCT i.itemid, i.identifier, i.sponsor, i.contributor, t.datafieldtag, t.subfieldvalue AS category, m.subfieldvalue AS title, i.marcleader, SPACE(100) AS oclc, SPACE(100) AS lc, SPACE(100) AS isbn, SPACE(100) AS issn, SPACE(100) AS ddc, SPACE(100) AS nlm, SPACE(100) AS nal, getdate() AS creationdate
INTO	#tmpByCategory
FROM 	Item i INNER JOIN #tmpItem2 t
			ON i.itemid = t.itemid
		INNER JOIN vwMarcDetail m
			ON i.itemid = m.itemid
WHERE	m.datafieldtag = '245'
AND		m.code IN ('a')--,'b','c')
ORDER BY i.identifier

DROP TABLE #tmpCDL
DROP TABLE #tmpItem2

--------------------------

-- Remove items targeted to children and with inappropriate literary forms (fiction, poetry)
DELETE FROM #tmpByCategory WHERE ItemID IN (
SELECT	ItemID
FROM	dbo.MarcControl
WHERE 	-- preschool, primary, pre-adolescent, adolescent, juvenile
		(Tag = '008' AND SUBSTRING([Value], 23, 1) IN ('a', 'b', 'c', 'd', 'j'))
OR		(Tag = '006' AND SUBSTRING([Value], 6, 1) IN ('a', 'b', 'c', 'd', 'j'))
)

DELETE FROM #tmpBySubject WHERE ItemID IN (
SELECT	ItemID
FROM	dbo.MarcControl
WHERE 	-- preschool, primary, pre-adolescent, adolescent, juvenile
		(Tag = '008' AND SUBSTRING([Value], 23, 1) IN ('a', 'b', 'c', 'd', 'j'))
OR		(Tag = '006' AND SUBSTRING([Value], 6, 1) IN ('a', 'b', 'c', 'd', 'j'))
)

DELETE FROM #tmpByCategory WHERE ItemID IN (
SELECT	ItemID
FROM	dbo.MarcControl
WHERE 	-- fiction, comic strips, dramas, novels, humor, short stories, mixed forms, poetry
		(Tag = '008' AND SUBSTRING([Value], 34, 1) IN ('1', 'c', 'd', 'f', 'h', 'j', 'm', 'p'))
OR		(Tag = '006' AND SUBSTRING([Value], 17, 1) IN ('1', 'c', 'd', 'f', 'h', 'j', 'm', 'p'))
)

DELETE FROM #tmpBySubject WHERE ItemID IN (
SELECT	ItemID
FROM	dbo.MarcControl
WHERE 	-- fiction, comic strips, dramas, novels, humor, short stories, mixed forms, poetry
		(Tag = '008' AND SUBSTRING([Value], 34, 1) IN ('1', 'c', 'd', 'f', 'h', 'j', 'm', 'p'))
OR		(Tag = '006' AND SUBSTRING([Value], 17, 1) IN ('1', 'c', 'd', 'f', 'h', 'j', 'm', 'p'))
)

--------------------------

-- Get the OCLC numbers FROM  the 035a AND 010o MARC fields (in most cases it's located IN one
-- or the other of these)
UPDATE	#tmpByCategory
SET		OCLC = COALESCE(CONVERT(NVARCHAR(30), CONVERT(BIGINT, dbo.fnFilterString(m.subfieldvalue, '[0-9]', ''))), 
						CONVERT(NVARCHAR(30), CONVERT(BIGINT, dbo.fnFilterString(m2.subfieldvalue, '[0-9]', ''))))
FROM 	#tmpByCategory t
		LEFT JOIN (SELECT ItemID, CASE WHEN CHARINDEX(' ', SubFieldValue) > 0 
									THEN SUBSTRING(SubFieldValue, 1, CHARINDEX(' ', SubFieldValue) - 1) 
									ELSE SubFieldValue END AS SubFieldValue 
					FROM  dbo.vwMarcDetail
					WHERE DataFieldTag = '035' AND Code = 'a' AND 
					(SubFieldValue LIKE '(OCoLC)%' OR SubFieldValue LIKE 'ocm%' OR SubFieldValue LIKE 'ocn%' OR SubFieldValue LIKE 'on%')
					) m
			ON t.ItemID = m.ItemID
		LEFT JOIN (SELECT * FROM  dbo.vwMarcDetail
					WHERE DataFieldTag = '010' AND Code = 'o' AND SubFieldValue <> '') m2
			ON t.ItemID = m2.ItemID

UPDATE	#tmpBySubject
SET		OCLC = COALESCE(CONVERT(NVARCHAR(30), CONVERT(BIGINT, dbo.fnFilterString(m.subfieldvalue, '[0-9]', ''))), 
						CONVERT(NVARCHAR(30), CONVERT(BIGINT, dbo.fnFilterString(m2.subfieldvalue, '[0-9]', ''))))
FROM 	#tmpBySubject t
		LEFT JOIN (SELECT ItemID, CASE WHEN CHARINDEX(' ', SubFieldValue) > 0 
									THEN SUBSTRING(SubFieldValue, 1, CHARINDEX(' ', SubFieldValue) - 1) 
									ELSE SubFieldValue END AS SubFieldValue 
					FROM  dbo.vwMarcDetail
					WHERE DataFieldTag = '035' AND Code = 'a' AND 
					(SubFieldValue LIKE '(OCoLC)%' OR SubFieldValue LIKE 'ocm%' OR SubFieldValue LIKE 'ocn%' OR SubFieldValue LIKE 'on%')
					) m
			ON t.ItemID = m.ItemID
		LEFT JOIN (SELECT * FROM  dbo.vwMarcDetail
					WHERE DataFieldTag = '010' AND Code = 'o') m2
			ON t.ItemID = m2.ItemID

-- Next check MARC control 001 record for the OCLC number (not too many of these)
UPDATE	#tmpByCategory
SET		OCLC = CONVERT(NVARCHAR(30), CONVERT(INT, dbo.fnFilterString(mc.value, '[0-9]', '')))
FROM 	#tmpByCategory t 
		LEFT JOIN (SELECT * FROM  dbo.vwMarcControl WHERE tag = '001' AND [value] NOT LIKE 'Catkey%') mc
			ON t.ItemID = mc.ItemID
		LEFT JOIN (SELECT * FROM  dbo.vwMarcControl WHERE tag = '003' AND [value] = 'OCoLC') mc2
			ON t.ItemID = mc2.ItemID
WHERE	(mc.[Value] LIKE 'oc%' OR mc.[Value] LIKE 'on%' OR mc2.[value] = 'OCoLC')
AND		LTRIM(RTRIM(ISNULL(t.OCLC, ''))) = ''

UPDATE	#tmpBySubject
SET		OCLC = CONVERT(NVARCHAR(30), CONVERT(INT, dbo.fnFilterString(mc.value, '[0-9]', '')))
FROM 	#tmpBySubject t 
		LEFT JOIN (SELECT * FROM  dbo.vwMarcControl WHERE tag = '001' AND [value] NOT LIKE 'Catkey%') mc
			ON t.ItemID = mc.ItemID
		LEFT JOIN (SELECT * FROM  dbo.vwMarcControl WHERE tag = '003' AND [value] = 'OCoLC') mc2
			ON t.ItemID = mc2.ItemID
WHERE	(mc.[Value] LIKE 'oc%' OR mc.[Value] LIKE 'on%' OR mc2.[value] = 'OCoLC')
AND		LTRIM(RTRIM(ISNULL(t.OCLC, ''))) = ''


-- Get the Library Of Congress Control numbers
UPDATE	#tmpByCategory
SET		LC = LTRIM(RTRIM(m.SubFieldValue))
FROM 	#tmpByCategory t INNER JOIN dbo.vwMarcDetail m
			ON t.ItemID = m.ItemID
WHERE	m.DataFieldTag = '010'
AND		m.Code = 'a'

UPDATE	#tmpBySubject
SET		LC = LTRIM(RTRIM(m.SubFieldValue))
FROM 	#tmpBySubject t INNER JOIN dbo.vwMarcDetail m
			ON t.ItemID = m.ItemID
WHERE	m.DataFieldTag = '010'
AND		m.Code = 'a'


-- Get the ISBN identifiers
UPDATE	#tmpByCategory
SET		ISBN = m.SubFieldValue
FROM 	#tmpByCategory t INNER JOIN dbo.vwMarcDetail m
			ON t.ItemID = m.ItemID
WHERE	m.DataFieldTag = '020'
AND		m.Code = 'a'

UPDATE	#tmpBySubject
SET		ISBN = m.SubFieldValue
FROM 	#tmpBySubject t INNER JOIN dbo.vwMarcDetail m
			ON t.ItemID = m.ItemID
WHERE	m.DataFieldTag = '020'
AND		m.Code = 'a'


-- Get the ISSN identifiers
UPDATE	#tmpByCategory
SET		ISSN = m.SubFieldValue
FROM 	#tmpByCategory t INNER JOIN dbo.vwMarcDetail m
			ON t.ItemID = m.ItemID
WHERE	m.DataFieldTag = '022'
AND		m.Code = 'a'

UPDATE	#tmpBySubject
SET		ISSN = m.SubFieldValue
FROM 	#tmpBySubject t INNER JOIN dbo.vwMarcDetail m
			ON t.ItemID = m.ItemID
WHERE	m.DataFieldTag = '022'
AND		m.Code = 'a'


-- Get the Dewey Decimal classification
UPDATE	#tmpByCategory
SET		DDC = m.SubFieldValue
FROM 	#tmpByCategory t INNER JOIN dbo.vwMarcDetail m
			ON t.ItemID = m.ItemID
WHERE	m.DataFieldTag = '082'
AND		m.Code = 'a'

UPDATE	#tmpBySubject
SET		DDC = m.SubFieldValue
FROM 	#tmpBySubject t INNER JOIN dbo.vwMarcDetail m
			ON t.ItemID = m.ItemID
WHERE	m.DataFieldTag = '082'
AND		m.Code = 'a'


-- Get the National Library of Medicine call numbers
UPDATE	#tmpByCategory
SET		NLM = Z.SubFieldValue
FROM 	#tmpByCategory t INNER JOIN (
				SELECT	ItemID, MarcDataFieldID, 
						LTRIM(ISNULL(MIN([a]), '') + ' ' + ISNULL(MIN([b]), '')) AS SubFieldValue
				FROM 	(
						SELECT	ItemID, MarcDataFieldID, [a], [b]
						FROM 	(SELECT * FROM  dbo.vwMarcDetail
								WHERE DataFieldTag = '060' AND Code IN ('a', 'b')) AS m
						PIVOT	(MIN(SubFieldValue) FOR Code IN ([a], [b])) AS Pvt
						) X
				GROUP BY ItemID, MarcDataFieldID
				) Z
			ON t.ItemID = Z.ItemID

UPDATE	#tmpBySubject
SET		NLM = Z.SubFieldValue
FROM 	#tmpBySubject t INNER JOIN (
				SELECT	ItemID, MarcDataFieldID, 
						LTRIM(ISNULL(MIN([a]), '') + ' ' + ISNULL(MIN([b]), '')) AS SubFieldValue
				FROM 	(
						SELECT	ItemID, MarcDataFieldID, [a], [b]
						FROM 	(SELECT * FROM  dbo.vwMarcDetail
								WHERE DataFieldTag = '060' AND Code IN ('a', 'b')) AS m
						PIVOT	(MIN(SubFieldValue) FOR Code IN ([a], [b])) AS Pvt
						) X
				GROUP BY ItemID, MarcDataFieldID
				) Z
			ON t.ItemID = Z.ItemID


-- Get the National Agricultural Library call numbers
UPDATE	#tmpByCategory
SET		NAL = Z.SubFieldValue
FROM 	#tmpByCategory t INNER JOIN (
				SELECT	ItemID, MarcDataFieldID, 
						LTRIM(ISNULL(MIN([a]), '') + ' ' + ISNULL(MIN([b]), '')) AS SubFieldValue
				FROM 	(
						SELECT	ItemID, MarcDataFieldID, [a], [b]
						FROM 	(SELECT * FROM  dbo.vwMarcDetail
								WHERE DataFieldTag = '070' AND Code IN ('a', 'b')) AS m
						PIVOT	(MIN(SubFieldValue) FOR Code IN ([a], [b])) AS Pvt
						) X
				GROUP BY ItemID, MarcDataFieldID
				) Z
			ON t.ItemID = Z.ItemID

UPDATE	#tmpBySubject
SET		NAL = Z.SubFieldValue
FROM 	#tmpBySubject t INNER JOIN (
				SELECT	ItemID, MarcDataFieldID, 
						LTRIM(ISNULL(MIN([a]), '') + ' ' + ISNULL(MIN([b]), '')) AS SubFieldValue
				FROM 	(
						SELECT	ItemID, MarcDataFieldID, [a], [b]
						FROM 	(SELECT * FROM  dbo.vwMarcDetail
								WHERE DataFieldTag = '070' AND Code IN ('a', 'b')) AS m
						PIVOT	(MIN(SubFieldValue) FOR Code IN ([a], [b])) AS Pvt
						) X
				GROUP BY ItemID, MarcDataFieldID
				) Z
			ON t.ItemID = Z.ItemID

--------------------------

-- Add the creation dates to the tables
UPDATE	#tmpByCategory
SET		creationdate = i.CreationDate
FROM 	#tmpByCategory t INNER JOIN Item i
			ON t.identifier = i.identifier

UPDATE	#tmpBySubject
SET		creationdate = i.CreationDate
FROM 	#tmpBySubject t INNER JOIN Item i
			ON t.identifier = i.identifier

-- Build the final combined table
SELECT itemid, identifier, sponsor, contributor, SPACE(100) AS category, SPACE(200) AS subject, title, marcleader, oclc, lc, isbn, issn, ddc, nlm, nal, creationdate
INTO #tmpCombined
FROM  #tmpByCategory
UNION
SELECT itemid, identifier, sponsor, contributor, '' AS category, '' AS subject, title, marcleader, oclc, lc, isbn, issn, ddc, nlm, nal, creationdate FROM  #tmpBySubject

UPDATE #tmpCombined
SET category = cat.category
FROM  #tmpCombined c INNER JOIN #tmpByCategory cat
		ON c.itemid = cat.itemid

UPDATE #tmpCombined
SET [subject] = s.category
FROM  #tmpCombined c INNER JOIN #tmpBySubject s
		ON c.itemid = s.itemid

--------------------------

-- Build the final tables
-- tmpRptByCategory
SELECT	ItemID, Identifier, Sponsor, Contributor, DataFieldTag, Category, Title, 
		dbo.fnMarcStringForMarcDataField([1]) AS [650_1], 
		dbo.fnMarcStringForMarcDataField([2]) AS [650_2], 
		dbo.fnMarcStringForMarcDataField([3]) AS [650_3], 
		dbo.fnMarcStringForMarcDataField([4]) AS [650_4], 
		dbo.fnMarcStringForMarcDataField([5]) AS [650_5], 
		dbo.fnMarcStringForMarcDataField([6]) AS [650_6], 
		dbo.fnMarcStringForMarcDataField([7]) AS [650_7], 
		dbo.fnMarcStringForMarcDataField([8]) AS [650_8], 
		dbo.fnMarcStringForMarcDataField([9]) AS [650_9], 
		dbo.fnMarcStringForMarcDataField([10]) AS [650_10],
		MarcLeader, OCLC, LC, ISBN, ISSN, DDC, NLM, NAL, CreationDate
INTO	tmpRptByCategory
FROM (
	SELECT	ROW_NUMBER() OVER (PARTITION BY ItemID ORDER BY MarcDataFieldID) as RowNumber, 
			ItemID, Identifier, Sponsor, Contributor, DataFieldTag, Category, Title, 
			MarcLeader, OCLC, LC, ISBN, ISSN, DDC, NLM, NAL, CreationDate,
			MarcDataFieldID
	FROM (	SELECT DISTINCT
					r.ItemID, r.Identifier, r.Sponsor, r.Contributor, r.DataFieldTag, r.Category, r.Title, 
					r.MarcLeader, r.OCLC, r.LC, r.ISBN, r.ISSN, r.DDC, r.NLM, r.NAL, r.CreationDate,
					m.MarcDataFieldID
			FROM	#tmpByCategory r LEFT JOIN vwMarcDetail m
						ON r.ItemID = m.ItemID
						AND m.DataFieldTag = '650'
			) x
) AS SourceTable
PIVOT
(
	MIN(MarcDataFieldID) FOR RowNumber IN ([0], [1], [2], [3], [4], [5], [6], [7], [8], [9], [10])
) AS PivotTable
ORDER BY Identifier

-- tmpRptBySubject
SELECT	ItemID, Identifier, Sponsor, Contributor, DataFieldTag, Category, Title, 
		dbo.fnMarcStringForMarcDataField([1]) AS [650_1], 
		dbo.fnMarcStringForMarcDataField([2]) AS [650_2], 
		dbo.fnMarcStringForMarcDataField([3]) AS [650_3], 
		dbo.fnMarcStringForMarcDataField([4]) AS [650_4], 
		dbo.fnMarcStringForMarcDataField([5]) AS [650_5], 
		dbo.fnMarcStringForMarcDataField([6]) AS [650_6], 
		dbo.fnMarcStringForMarcDataField([7]) AS [650_7], 
		dbo.fnMarcStringForMarcDataField([8]) AS [650_8], 
		dbo.fnMarcStringForMarcDataField([9]) AS [650_9], 
		dbo.fnMarcStringForMarcDataField([10]) AS [650_10],
		MarcLeader, OCLC, LC, ISBN, ISSN, DDC, NLM, NAL, CreationDate
INTO	tmpRptBySubject
FROM (
	SELECT	ROW_NUMBER() OVER (PARTITION BY ItemID ORDER BY MarcDataFieldID) as RowNumber, 
			ItemID, Identifier, Sponsor, Contributor, DataFieldTag, Category, Title, 
			MarcLeader, OCLC, LC, ISBN, ISSN, DDC, NLM, NAL, CreationDate,
			MarcDataFieldID
	FROM (	SELECT DISTINCT
					r.ItemID, r.Identifier, r.Sponsor, r.Contributor, r.DataFieldTag, r.Category, r.Title, 
					r.MarcLeader, r.OCLC, r.LC, r.ISBN, r.ISSN, r.DDC, r.NLM, r.NAL, r.CreationDate,
					m.MarcDataFieldID
			FROM	#tmpBySubject r LEFT JOIN vwMarcDetail m
						ON r.ItemID = m.ItemID
						AND m.DataFieldTag = '650'
			) x
) AS SourceTable
PIVOT
(
	MIN(MarcDataFieldID) FOR RowNumber IN ([0], [1], [2], [3], [4], [5], [6], [7], [8], [9], [10])
) AS PivotTable
ORDER BY Category

-- tmpRptCombined
SELECT	ItemID, Identifier, Sponsor, Contributor, Category, [Subject], Title, 
		dbo.fnMarcStringForMarcDataField([1]) AS [650_1], 
		dbo.fnMarcStringForMarcDataField([2]) AS [650_2], 
		dbo.fnMarcStringForMarcDataField([3]) AS [650_3], 
		dbo.fnMarcStringForMarcDataField([4]) AS [650_4], 
		dbo.fnMarcStringForMarcDataField([5]) AS [650_5], 
		dbo.fnMarcStringForMarcDataField([6]) AS [650_6], 
		dbo.fnMarcStringForMarcDataField([7]) AS [650_7], 
		dbo.fnMarcStringForMarcDataField([8]) AS [650_8], 
		dbo.fnMarcStringForMarcDataField([9]) AS [650_9], 
		dbo.fnMarcStringForMarcDataField([10]) AS [650_10],
		MarcLeader, OCLC, LC, ISBN, ISSN, DDC, NLM, NAL, CreationDate
INTO	tmpRptCombined
FROM (
	SELECT	ROW_NUMBER() OVER (PARTITION BY ItemID ORDER BY MarcDataFieldID) as RowNumber, 
			ItemID, Identifier, Sponsor, Contributor, Category, [Subject], Title, 
			MarcLeader, OCLC, LC, ISBN, ISSN, DDC, NLM, NAL, CreationDate,
			MarcDataFieldID
	FROM (	SELECT DISTINCT
					r.ItemID, r.Identifier, r.Sponsor, r.Contributor, r.Category, r.[Subject], r.Title, 
					r.MarcLeader, r.OCLC, r.LC, r.ISBN, r.ISSN, r.DDC, r.NLM, r.NAL, r.CreationDate,
					m.MarcDataFieldID
			FROM	#tmpCombined r LEFT JOIN vwMarcDetail m
						ON r.ItemID = m.ItemID
						AND m.DataFieldTag = '650'
			) x
) AS SourceTable
PIVOT
(
	MIN(MarcDataFieldID) FOR RowNumber IN ([0], [1], [2], [3], [4], [5], [6], [7], [8], [9], [10])
) AS PivotTable
ORDER BY Identifier

-- Drop rows with excluded subjects
DECLARE curExclusions CURSOR
READ_ONLY
FOR SELECT '%' + Code + TagText + '%' FROM dbo.SubjectExclusion

DECLARE @Exclusion nvarchar(60)
OPEN curExclusions

FETCH NEXT FROM curExclusions INTO @Exclusion
WHILE (@@fetch_status <> -1)
BEGIN
	IF (@@fetch_status <> -2)
	BEGIN

	DELETE FROM tmpRptByCategory
	WHERE	[650_1] + [650_2] + [650_3] + [650_4] + [650_5] + [650_6] + 
			[650_7] + [650_8] + [650_9] + [650_10] LIKE @Exclusion
	
	DELETE FROM tmpRptBySubject
	WHERE	[650_1] + [650_2] + [650_3] + [650_4] + [650_5] + [650_6] + 
			[650_7] + [650_8] + [650_9] + [650_10] LIKE @Exclusion

	DELETE FROM tmpRptCombined
	WHERE	[650_1] + [650_2] + [650_3] + [650_4] + [650_5] + [650_6] + 
			[650_7] + [650_8] + [650_9] + [650_10] LIKE @Exclusion

	END
	FETCH NEXT FROM curExclusions INTO @Exclusion
END

CLOSE curExclusions
DEALLOCATE curExclusions

-- Rename tables
IF  EXISTS (SELECT * FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RptByCategory]') AND type IN (N'U'))
DROP TABLE [dbo].[RptByCategory]

exec sp_rename 'tmpRptByCategory', 'RptByCategory'

IF  EXISTS (SELECT * FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RptBySubject]') AND type IN (N'U'))
DROP TABLE [dbo].[RptBySubject]

exec sp_rename 'tmpRptBySubject', 'RptBySubject'

IF  EXISTS (SELECT * FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RptCombined]') AND type IN (N'U'))
DROP TABLE [dbo].[RptCombined]

exec sp_rename 'tmpRptCombined', 'RptCombined'

--------------------------

-- Clean up
DROP TABLE #tmpCombined
DROP TABLE #tmpByCategory
DROP TABLE #tmpBySubject

END
