CREATE PROCEDURE [dbo].[MarcResolveTitles]

@MarcImportBatchID INT

AS
BEGIN

SET NOCOUNT ON

-- =======================================================================
-- =======================================================================
-- =======================================================================
-- Create temp tables

CREATE TABLE #tmpTitle (
	[MarcID] int NOT NULL,
	[InstitutionCode] [nvarchar](10) NULL,
	[MARCBibID] [nvarchar](50) NULL,
	[ShortTitle] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[TitleID] [int] NULL
	)

CREATE TABLE #tmpTitleIdentifier
	(
	[MarcID] int NOT NULL,
	[IdentifierName] [nvarchar](40) NOT NULL,
	[IdentifierValue] [nvarchar](125) NULL,
	)

BEGIN TRY
	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Titles

	-- Get the initial list of titles
	INSERT	#tmpTitle (MarcID, InstitutionCode)
	SELECT	MarcID, InstitutionCode
	FROM	dbo.Marc
	WHERE	MarcImportStatusID = 10 -- New
	AND		MarcImportBatchID = @MarcImportBatchID

	-- Get the MARC BIB ID
	UPDATE	#tmpTitle
	SET		MARCBibID = REPLACE(m.Leader, ' ', 'x')
	FROM	#tmpTitle t INNER JOIN dbo.Marc m
				ON t.MarcID = m.MarcID

	-- Get the publication titles
	UPDATE	#tmpTitle
	SET		ShortTitle = df.SubFieldValue
	FROM	#tmpTitle t INNER JOIN dbo.vwMarcDataField df
				ON t.MarcID = df.MarcID
	WHERE	df.DataFieldTag = '245'
	AND		df.Code = 'a'

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Title Identifiers

	-- Get the OCLC numbers from the 035a and 010o MARC fields (in most cases it's located in one
	-- or the other of these)
	INSERT INTO #tmpTitleIdentifier
	SELECT	t.MarcID,
			'OCLC',
			COALESCE(CONVERT(NVARCHAR(30), CONVERT(BIGINT, dbo.fnFilterString(m.subfieldvalue, '[0-9]', ''))), 
					CONVERT(NVARCHAR(30), CONVERT(BIGINT, dbo.fnFilterString(m2.subfieldvalue, '[0-9]', ''))))
	FROM	#tmpTitle t 
			LEFT JOIN (SELECT * FROM dbo.vwMarcDataField 
						WHERE DataFieldTag = '035' AND code = 'a' AND 
						(SubFieldValue LIKE '(OCoLC)%' OR SubFieldValue LIKE 'ocm%' OR SubFieldValue LIKE 'ocn%' OR SubFieldValue LIKE 'on%')
						) m
				ON t.MarcID = m.MarcID
			LEFT JOIN (SELECT * FROM dbo.vwMarcDataField
						WHERE DataFieldTag = '010' AND Code = 'o') m2
				ON t.MarcID = m2.MarcID

	-- Next check MARC control 001 record for the OCLC number (not too many of these)
	INSERT INTO #tmpTitleIdentifier
	SELECT	t.MarcID,
			'OCLC',
			CONVERT(NVARCHAR(30), CONVERT(INT, dbo.fnFilterString(mc.value, '[0-9]', '')))
	FROM	#tmpTitle t 
			LEFT JOIN (SELECT * FROM dbo.vwMarcControl WHERE tag = '001' AND [value] NOT LIKE 'Catkey%') mc
				ON t.MarcID = mc.MarcID
			LEFT JOIN (SELECT * FROM dbo.vwMarcControl WHERE tag = '003' AND [value] = 'OCoLC') mc2
				ON t.MarcID = mc2.MarcID
	WHERE	(mc.[Value] LIKE 'oc%' OR mc.[Value] LIKE 'on%' OR mc2.[value] = 'OCoLC')
	AND		NOT EXISTS (SELECT IdentifierValue FROM #tmpTitleIdentifier 
						WHERE MarcID = t.MarcID
						AND IdentifierValue IS NOT NULL)

	-- Get the Library Of Congress Control numbers
	INSERT INTO #tmpTitleIdentifier
	SELECT DISTINCT
			t.MarcID,
			'DLC',
			LTRIM(RTRIM(m.SubFieldValue))
	FROM	#tmpTitle t INNER JOIN dbo.vwMarcDataField m
				ON t.MarcID = m.MarcID
	WHERE	DataFieldTag = '010'
	AND		Code = 'a'

	-- Get the ISBN identifiers
	INSERT INTO #tmpTitleIdentifier
	SELECT DISTINCT
			t.MarcID,
			'ISBN',
			m.SubFieldValue
	FROM	#tmpTitle t INNER JOIN dbo.vwMarcDataField m
				ON t.MarcID = m.MarcID
	WHERE	m.DataFieldTag = '020'
	AND		m.Code = 'a'

	-- Get the ISSN identifiers
	INSERT INTO #tmpTitleIdentifier
	SELECT DISTINCT
			t.MarcID,
			'ISSN',
			m.SubFieldValue
	FROM	#tmpTitle t INNER JOIN dbo.vwMarcDataField m
				ON t.MarcID = m.MarcID
	WHERE	m.DataFieldTag = '022'
	AND		m.Code = 'a'

	-- Get the WonderFetch identifiers (look for a MARC
	-- 001 control record with a value including 'catkey')
	INSERT INTO #tmpTitleIdentifier
	SELECT DISTINCT 
			t.MarcID,
			'WonderFetch',
			LTRIM(RTRIM(REPLACE(m.[Value], 'catkey', ''))) 
	FROM	#tmpTitle t INNER JOIN dbo.vwMarcControl m
				ON t.MarcID = m.MarcID
	WHERE	m.Tag = '001' 
	AND		m.[Value] LIKE 'catkey%'

	-- Get the non-OCLC and non-WonderFetch local identifiers from the 
	-- MARC 001 control record
	INSERT INTO #tmpTitleIdentifier
	SELECT DISTINCT
			t.MarcID,
			'MARC001',
			m.[Value]
	FROM	#tmpTitle t INNER JOIN dbo.vwMarcControl m
				ON t.MarcID = m.MarcID
	WHERE	m.Tag = '001'
	AND		m.[Value] NOT LIKE 'catkey%'
	AND		m.[Value] NOT LIKE 'oc%'

	-- Remove null values
	DELETE FROM #tmpTitleIdentifier WHERE IdentifierValue IS NULL

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Resolve titles.  
	--
	-- Multiple attempts are made to find a matching title in production.  In
	-- order, the following criteria are used to find a match:
	--
	--	1) OCLC
	--	2) WonderFetch TitleID
	--	3) MARC 001 value
	--

	-- Match on OCLC
	UPDATE	#tmpTitle
	SET		TitleID = bt.TitleID
	FROM	#tmpTitle t INNER JOIN #tmpTitleIdentifier tti
				ON t.MarcID = tti.MarcID
				AND 'OCLC' = tti.IdentifierName
			INNER JOIN dbo.Title_Identifier bti
				ON tti.IdentifierValue = bti.IdentifierValue
			INNER JOIN dbo.Identifier bi
				ON bti.IdentifierID = bi.IdentifierID
				AND tti.IdentifierName = bi.IdentifierName
			INNER JOIN dbo.Title bt
				ON bti.TitleID = bt.TitleID
	WHERE	t.TitleID IS NULL

	-- Match on WonderFetch Title ID + Institution Code
	UPDATE	#tmpTitle
	SET		TitleID = bt.TitleID
	FROM	#tmpTitle t INNER JOIN #tmpTitleIdentifier tti
				ON t.MarcID = tti.MarcID
				AND 'WonderFetch' = tti.IdentifierName
			INNER JOIN dbo.Title_Identifier bti
				ON tti.IdentifierValue = bti.IdentifierValue
			INNER JOIN dbo.Identifier bi
				ON bti.IdentifierID = bi.IdentifierID
				AND tti.IdentifierName = bi.IdentifierName
			INNER JOIN dbo.Title bt
				ON bti.TitleID = bt.TitleID
			INNER JOIN dbo.TitleItem btitem
				ON bt.TitleID = btitem.TitleID
			INNER JOIN dbo.ItemInstitution bii
				ON btitem.ItemID = bii.ItemID
				AND t.InstitutionCode = bii.InstitutionCode
			INNER JOIN dbo.InstitutionRole br
				ON bii.InstitutionRoleID = br.InstitutionRoleID
	WHERE	t.TitleID IS NULL
	AND		br.InstitutionRoleName = 'Contributor'

	-- Match on MARC 001 Value + Institution Code
	UPDATE	#tmpTitle
	SET		TitleID = bt.TitleID
	FROM	#tmpTitle t INNER JOIN #tmpTitleIdentifier tti
				ON t.MarcID = tti.MarcID
				AND 'MARC001' = tti.IdentifierName
			INNER JOIN dbo.Title_Identifier bti
				ON tti.IdentifierValue = bti.IdentifierValue
			INNER JOIN dbo.Identifier bi
				ON bti.IdentifierID = bi.IdentifierID
				AND tti.IdentifierName = bi.IdentifierName
			INNER JOIN dbo.Title bt
				ON bti.TitleID = bt.TitleID
			INNER JOIN dbo.TitleItem btitem
				ON bt.TitleID = btitem.TitleID
			INNER JOIN dbo.ItemInstitution bii
				ON btitem.ItemID = bii.ItemID
				AND t.InstitutionCode = bii.InstitutionCode
			INNER JOIN dbo.InstitutionRole br
				ON bii.InstitutionRoleID = br.InstitutionRoleID
	WHERE	t.TitleID IS NULL
	AND		br.InstitutionRoleName = 'Contributor'

	-- ** REMOVED 4/24/2015 TO PREVENT FALSE POSITIVES **
	/*
	-- Match on MARC Bib ID + Short Title
	UPDATE	#tmpTitle
	SET		TitleID = bt.TitleID
	FROM	#tmpTitle t INNER JOIN dbo.Title bt
				ON t.MARCBibID = bt.MARCBibID
				AND t.ShortTitle = bt.ShortTitle
	WHERE	t.TitleID IS NULL
	*/

	-- If the selected production title has been redirected to a different 
	-- title, then use that title instead.  Follow the "redirect" chain up 
	-- to ten levels.
	UPDATE	#tmpTitle
	SET		TitleID = COALESCE(bt10.TitleID, bt9.TitleID, bt8.TitleiD, bt7.TitleID, bt6.TitleID,
										bt5.TitleID, bt4.TitleID, bt3.TitleID, bt2.TitleID, bt1.TitleID),
			MarcBibID = COALESCE(bt10.MarcBibID, bt9.MarcBibID, bt8.MarcBibID, bt7.MarcBibID, bt6.MarcBibID,
								bt5.MarcBibID, bt4.MarcBibID, bt3.MarcBibID, bt2.MarcBibID, bt1.MarcBibID)
	FROM	#tmpTitle t INNER JOIN dbo.Title bt1
				ON t.TitleID = bt1.TitleID
			LEFT JOIN dbo.Title bt2 ON bt1.RedirectTitleID = bt2.TitleID
			LEFT JOIN dbo.Title bt3 ON bt2.RedirectTitleID = bt3.TitleID
			LEFT JOIN dbo.Title bt4 ON bt3.RedirectTitleID = bt4.TitleID
			LEFT JOIN dbo.Title bt5 ON bt4.RedirectTitleID = bt5.TitleID
			LEFT JOIN dbo.Title bt6 ON bt5.RedirectTitleID = bt6.TitleID
			LEFT JOIN dbo.Title bt7 ON bt6.RedirectTitleID = bt7.TitleID
			LEFT JOIN dbo.Title bt8 ON bt7.RedirectTitleID = bt8.TitleID
			LEFT JOIN dbo.Title bt9 ON bt8.RedirectTitleID = bt9.TitleID
			LEFT JOIN dbo.Title bt10 ON bt9.RedirectTitleID = bt10.TitleID
	WHERE	t.TitleID IS NOT NULL

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Update the Marc records with the resolved title identifiers, and set
	-- the batch import status to 20 (Resolved)

	UPDATE	dbo.Marc
	SET		TitleID = t.TitleID,
			MarcImportStatusID = 20
	FROM	dbo.Marc m INNER JOIN #tmpTitle t
				ON m.MarcID = t.MarcID

	SELECT 1 AS Result
END TRY
BEGIN CATCH
	-- Record the error
	INSERT INTO dbo.MarcImportError (MarcImportBatchID, ErrorDate, Number, Severity, State, 
		[Procedure], Line, [Message])
	SELECT	@MarcImportBatchID, GETDATE(), ERROR_NUMBER(), ERROR_SEVERITY(),
		ERROR_STATE(), ERROR_PROCEDURE(), ERROR_LINE(), ERROR_MESSAGE()

	-- Mark the item as in error
	UPDATE	dbo.Marc
	SET		MarcImportStatusID = 99	-- Error
	WHERE	MarcImportBatchID = @MarcImportBatchID

	SELECT 0 AS RESULT
END CATCH

-- =======================================================================
-- =======================================================================
-- =======================================================================
-- Clean up temp tables

DROP TABLE #tmpTitle
DROP TABLE #tmpTitleIdentifier

END
