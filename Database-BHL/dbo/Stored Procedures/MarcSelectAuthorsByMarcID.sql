﻿CREATE PROCEDURE [dbo].[MarcSelectAuthorsByMarcID]

@MarcID int

AS
BEGIN

SET NOCOUNT ON

-- =======================================================================
-- Build temp table

CREATE TABLE #tmpAuthor (
	[AuthorRoleID] [int] NOT NULL,
	[AuthorTypeID] [int] NOT NULL,
	[FullName] [nvarchar](255) NOT NULL,
	[StartDate] [nvarchar](25) NULL,
	[EndDate] [nvarchar](25) NULL,
	[MARCDataFieldID] [int] NULL,
	[MARCDataFieldTag] [nvarchar](3) NULL,
	[MARCCreator_a] [nvarchar](450),
	[MARCCreator_b] [nvarchar](300),
	[MARCCreator_c] [nvarchar](200),
	[MARCCreator_d] [nvarchar](450),
	[MARCCreator_e] [nvarchar](150),
	[MARCCreator_q] [nvarchar](150),
	[MARCCreator_t] [nvarchar](500),
	[MARCCreator_5] [nvarchar](450) NULL,
	[SequenceOrder] [smallint] NULL,
	[AuthorID] [int] NULL
	)

-- =======================================================================
-- Populate the temp table

-- Get the initial creator information (MARC subfield code 'a')
INSERT INTO #tmpAuthor (FullName, AuthorRoleID, AuthorTypeID, MARCDataFieldID, 
						MARCDataFieldTag, MARCCreator_a, SequenceOrder)
SELECT	dbo.fnConvertToTitleCase(dbo.fnAddAuthorNameSpaces(m.SubFieldValue)),
		0,
		0,
		m.MARCDataFieldID, 
		m.DataFieldTag,
		m.SubFieldValue,
		ROW_NUMBER() OVER (PARTITION BY m.MarcID ORDER BY m.DataFieldTag, m.MARCSubFieldID) AS SequenceOrder
FROM	dbo.vwMarcDataField m
WHERE	m.DataFieldTag IN ('100', '110', '111', '700', '710', '711')
AND		m.Code = 'a'
AND		m.MarcID = @MarcID

-- Get the Author Type
UPDATE	#tmpAuthor
SET		AuthorTypeID = CASE WHEN MARCDataFieldTag IN ('100', '700') THEN 1
							WHEN MARCDataFieldTag IN ('110', '710') THEN 2
							WHEN MARCDataFieldTag IN ('111', '711') THEN 3 END

-- Get creator MARC subfield 'b'
UPDATE	#tmpAuthor
SET		MARCCreator_b = m.SubFieldValue
FROM	#tmpAuthor t INNER JOIN dbo.vwMarcDataField m
			ON t.MarcDataFieldID = m.MarcDataFieldID
			AND t.MarcDataFieldTag = m.DataFieldTag
			AND m.Code = 'b'

-- Get creator MARC subfield 'c'
UPDATE	#tmpAuthor
SET		MARCCreator_c = m.SubFieldValue
FROM	#tmpAuthor t INNER JOIN dbo.vwMarcDataField m
			ON t.MarcDataFieldID = m.MarcDataFieldID
			AND t.MarcDataFieldTag = m.DataFieldTag
			AND m.Code = 'c'

-- Get creator MARC subfield 'd'
UPDATE	#tmpAuthor
SET		MARCCreator_d = m.SubFieldValue
FROM	#tmpAuthor t INNER JOIN dbo.vwMarcDataField m
			ON t.MarcDataFieldID = m.MarcDataFieldID
			AND t.MarcDataFieldTag = m.DataFieldTag
			AND m.Code = 'd'

-- Get creator MARC subfield 'e'
UPDATE	#tmpAuthor
SET		MARCCreator_e = m.SubFieldValue
FROM	#tmpAuthor t INNER JOIN dbo.vwMarcDataField m
			ON t.MarcDataFieldID = m.MarcDataFieldID
			AND t.MarcDataFieldTag = m.DataFieldTag
			AND m.Code = 'e'

-- Get creator MARC subfield 'q'
UPDATE	#tmpAuthor
SET		MARCCreator_q = m.SubFieldValue
FROM	#tmpAuthor t INNER JOIN dbo.vwMarcDataField m
			ON t.MarcDataFieldID = m.MarcDataFieldID
			AND t.MarcDataFieldTag = m.DataFieldTag
			AND m.Code = 'q'

-- Get creator MARC subfield 't'
UPDATE	#tmpAuthor
SET		MARCCreator_t = m.SubFieldValue
FROM	#tmpAuthor t INNER JOIN dbo.vwMarcDataField m
			ON t.MarcDataFieldID = m.MarcDataFieldID
			AND t.MarcDataFieldTag = m.DataFieldTag
			AND m.Code = 't'

-- Get creator MARC subfield '5'
UPDATE	#tmpAuthor
SET		MARCCreator_5 = m.SubFieldValue
FROM	#tmpAuthor t INNER JOIN dbo.vwMarcDataField m
			ON t.MarcDataFieldID = m.MarcDataFieldID
			AND t.MarcDataFieldTag = m.DataFieldTag
			AND m.Code = '5'

-- Get the creator role type identifier
UPDATE	#tmpAuthor
SET		AuthorRoleID = r.AuthorRoleID
FROM	#tmpAuthor t INNER JOIN dbo.AuthorRole r
			ON t.MARCDataFieldTag = r.MARCDataFieldTag

-- Get the creator DOB and DOD values
SELECT	MARCCreator_a,
		MARCCreator_b,
		MARCCreator_c,
		MARCCreator_d,
		MARCCreator_d AS Dates
INTO	#tmpAuthorDates
FROM	#tmpAuthor
WHERE	ISNULL(MARCCreator_d, '') <> ''

UPDATE	#tmpAuthor
SET		StartDate = u.StartDate,
		EndDate = u.EndDate
FROM	#tmpAuthor c INNER JOIN #tmpAuthorDates d
			ON ISNULL(c.MARCCreator_a, '') = ISNULL(d.MARCCreator_a, '')
			AND ISNULL(c.MARCCreator_b, '') = ISNULL(d.MARCCreator_b, '')
			AND ISNULL(c.MARCCreator_c, '') = ISNULL(d.MARCCreator_c, '')
			AND ISNULL(c.MARCCreator_d, '') = ISNULL(d.MARCCreator_d, '')
		CROSS APPLY dbo.fnGetDatesFromString(d.Dates) u

DROP TABLE #tmpAuthorDates

-- Delete any authors where subfield 5 is NOT null
DELETE	#tmpAuthor
WHERE	MARCCreator_5 IS NOT NULL

-- Remove "from old catalog" text from creatorname and MARCCreator_q
UPDATE	#tmpAuthor
SET		FullName = RTRIM(REPLACE(FullName, '[from old catalog]', '')),
		MARCCreator_q = RTRIM(REPLACE(MARCCreator_q, '[from old catalog]', ''))

-- =======================================================================
-- Add new author and authorname records, if necessary
BEGIN TRY

	-- Get existing authors with matching types/names
	SELECT	a.AuthorID,
			a.FullName,
			a.FullNameToken,
			a.FullerForm,
			a.AuthorTypeID,
			a.StartDate,
			a.EndDate,
			a.Numeration,
			a.Title,
			a.Unit,
			a.Location
	INTO	#tmpNameMatch
	FROM	#tmpAuthor t INNER JOIN vwAuthorName a
				ON t.AuthorTypeID = a.AuthorTypeID
				AND dbo.fnRemoveNonAlphaNumericCharacters(t.FullName) = a.FullNameToken
	GROUP BY
			a.AuthorID,
			a.FullName,
			a.FullNameToken,
			a.FullerForm,
			a.AuthorTypeID,
			a.StartDate,
			a.EndDate,
			a.Numeration,
			a.Title,
			a.Unit,
			a.Location

	-- Get the new authors
	DECLARE curNew CURSOR
	FOR 	
	SELECT 	t.FullName,
			t.AuthorTypeID,
			ISNULL(t.StartDate, ''),
			ISNULL(t.EndDate, ''),
			ISNULL(t.MARCCreator_b, ''),
			ISNULL(t.MARCCreator_c, ''),
			ISNULL(t.MARCCreator_q, '')
	FROM	#tmpAuthor t LEFT JOIN #tmpNameMatch a
				ON t.AuthorTypeID = a.AuthorTypeID
				AND ISNULL(dbo.fnRemoveNonNumericCharacters(t.StartDate), '') = ISNULL(dbo.fnRemoveNonNumericCharacters(a.StartDate), '')
				AND ISNULL(dbo.fnRemoveNonNumericCharacters(t.EndDate), '') = ISNULL(dbo.fnRemoveNonNumericCharacters(a.EndDate), '')
				AND dbo.fnRemoveNonAlphaNumericCharacters(t.FullName) = a.FullNameToken
				AND	(  -- If b is blank, match records with blank Numeration/Unit values
					(ISNULL(t.MARCCreator_b, '') = '' AND ISNULL(a.Numeration, '') = '' AND ISNULL(a.Unit, '') = '') 
					OR  -- If b is not blank, find records with matching Numeration/Unit values
					(ISNULL(t.MARCCreator_b, '') <> '' AND
						(ISNULL(t.MARCCreator_b, '') = ISNULL(a.Numeration, '') OR ISNULL(t.MARCCreator_b, '') = ISNULL(a.Unit, ''))) 
					)
				AND	(  -- If c is blank, match records with blank Numeration/Unit values
					(ISNULL(t.MARCCreator_c, '') = '' AND ISNULL(a.Title, '') = '' AND ISNULL(a.Location, '') = '')
					OR  -- If c is not blank, find records with matching Numeration/Unit values
					(ISNULL(t.MARCCreator_c, '') <> '' AND
						(ISNULL(t.MARCCreator_c, '') = ISNULL(a.Title, '') OR ISNULL(t.MARCCreator_c, '') = ISNULL(a.Location, ''))) 
					)
				AND ISNULL(t.MARCCreator_q, '') = ISNULL(a.FullerForm, '')
	WHERE	a.AuthorID IS NULL
	GROUP BY
			t.FullName,
			t.AuthorTypeID,
			t.StartDate,
			t.EndDate,
			t.MARCCreator_b,
			t.MARCCreator_c,
			t.MARCCreator_q
			
	DECLARE @FullName nvarchar(255)
	DECLARE @TypeID int
	DECLARE @Start nvarchar(25)
	DECLARE @End nvarchar(25)
	DECLARE @NumUnit nvarchar(300)
	DECLARE @TitleLoc nvarchar(200)
	DECLARE @FullerForm nvarchar(150)
	DECLARE	@AuthID int

	OPEN curNew

	-- Insert new Author and AuthorName records for each new author
	FETCH NEXT FROM curNew INTO @FullName, @TypeID, @Start, @End, @NumUnit, @TitleLoc, @FullerForm
	WHILE (@@fetch_status <> -1)
	BEGIN
		IF (@@fetch_status <> -2)
		BEGIN
			INSERT INTO dbo.Author (AuthorTypeID, StartDate, EndDate, Numeration, Title, Unit, Location, IsActive)
			VALUES (@TypeID, @Start, @End, 
					CASE WHEN @TypeID = 1 THEN @NumUnit ELSE '' END, 
					CASE WHEN @TypeID = 1 THEN @TitleLoc ELSE '' END, 
					CASE WHEN @TypeID = 2 THEN @NumUnit ELSE '' END, 
					CASE WHEN @TypeID <> 1 THEN @TitleLoc ELSE '' END, 
					1)

			SET @AuthID = SCOPE_IDENTITY()
			
			INSERT INTO dbo.AuthorName (AuthorID, FullName, FullerForm, IsPreferredName)
			VALUES (@AuthID, @FullName, @FullerForm, 1)
		END
		FETCH NEXT FROM curNew INTO @FullName, @TypeID, @Start, @End, @NumUnit, @TitleLoc, @FullerForm
	END

	CLOSE curNew
	DEALLOCATE curNew

	DROP TABLE #tmpNameMatch
END TRY
BEGIN CATCH
	-- Record the error
	INSERT INTO dbo.MarcImportError (ErrorDate, Number, Severity, State, [Procedure], Line, [Message])
	SELECT	GETDATE(), ERROR_NUMBER(), ERROR_SEVERITY(), ERROR_STATE(), ERROR_PROCEDURE(), ERROR_LINE(), 
			ERROR_MESSAGE()
END CATCH

-- =======================================================================
-- Add the production author IDs to the temp table

-- Get existing authors with matching types/names
SELECT	a.AuthorID,
		a.FullName,
		a.FullNameToken,
		a.FullerForm,
		a.AuthorTypeID,
		a.StartDate,
		a.EndDate,
		a.Numeration,
		a.Title,
		a.Unit,
		a.Location
INTO	#tmpNameMatch2
FROM	#tmpAuthor t INNER JOIN vwAuthorName a
			ON t.AuthorTypeID = a.AuthorTypeID
			AND dbo.fnRemoveNonAlphaNumericCharacters(t.FullName) = a.FullNameToken
GROUP BY
		a.AuthorID,
		a.FullName,
		a.FullNameToken,
		a.FullerForm,
		a.AuthorTypeID,
		a.StartDate,
		a.EndDate,
		a.Numeration,
		a.Title,
		a.Unit,
		a.Location

UPDATE	#tmpAuthor
SET		AuthorID = a.AuthorID
FROM	#tmpAuthor t INNER JOIN #tmpNameMatch2 a
			ON t.AuthorTypeID = a.AuthorTypeID
			AND ISNULL(dbo.fnRemoveNonNumericCharacters(t.StartDate), '') = ISNULL(dbo.fnRemoveNonNumericCharacters(a.StartDate), '')
			AND ISNULL(dbo.fnRemoveNonNumericCharacters(t.EndDate), '') = ISNULL(dbo.fnRemoveNonNumericCharacters(a.EndDate), '')
			AND dbo.fnRemoveNonAlphaNumericCharacters(t.FullName) = a.FullNameToken
			AND	(  -- If b is blank, match records with blank Numeration/Unit values
				(ISNULL(t.MARCCreator_b, '') = '' AND ISNULL(a.Numeration, '') = '' AND ISNULL(a.Unit, '') = '') 
				OR  -- If b is not blank, find records with matching Numeration/Unit values
				(ISNULL(t.MARCCreator_b, '') <> '' AND
					(ISNULL(t.MARCCreator_b, '') = ISNULL(a.Numeration, '') OR ISNULL(t.MARCCreator_b, '') = ISNULL(a.Unit, ''))) 
				)
			AND	(  -- If c is blank, match records with blank Numeration/Unit values
				(ISNULL(t.MARCCreator_c, '') = '' AND ISNULL(a.Title, '') = '' AND ISNULL(a.Location, '') = '')
				OR  -- If c is not blank, find records with matching Numeration/Unit values
				(ISNULL(t.MARCCreator_c, '') <> '' AND
					(ISNULL(t.MARCCreator_c, '') = ISNULL(a.Title, '') OR ISNULL(t.MARCCreator_c, '') = ISNULL(a.Location, ''))) 
				)
			AND ISNULL(t.MARCCreator_q, '') = ISNULL(a.FullerForm, '')

-- If a selected production Author ID has been redirected to a different 
-- author ID, then use that author ID instead.  Follow the "redirect" chain
-- up to ten levels.
UPDATE	#tmpAuthor
SET		AuthorID = COALESCE(a10.AuthorID, a9.AuthorID, a8.AuthorID, a7.AuthorID, a6.AuthorID,
							a5.AuthorID, a4.AuthorID, a3.AuthorID, a2.AuthorID, a1.AuthorID)
FROM	#tmpAuthor t INNER JOIN dbo.Author a1 ON t.AuthorID = a1.AuthorID
		LEFT JOIN dbo.Author a2 ON a1.RedirectAuthorID = a2.AuthorID
		LEFT JOIN dbo.Author a3 ON a2.RedirectAuthorID = a3.AuthorID
		LEFT JOIN dbo.Author a4 ON a3.RedirectAuthorID = a4.AuthorID
		LEFT JOIN dbo.Author a5 ON a4.RedirectAuthorID = a5.AuthorID
		LEFT JOIN dbo.Author a6 ON a5.RedirectAuthorID = a6.AuthorID
		LEFT JOIN dbo.Author a7 ON a6.RedirectAuthorID = a7.AuthorID
		LEFT JOIN dbo.Author a8 ON a7.RedirectAuthorID = a8.AuthorID
		LEFT JOIN dbo.Author a9 ON a8.RedirectAuthorID = a9.AuthorID
		LEFT JOIN dbo.Author a10 ON a9.RedirectAuthorID = a10.AuthorID
WHERE	t.AuthorID IS NOT NULL

-- =======================================================================
-- Deliver the final result set
SELECT DISTINCT
		AuthorRoleID,
		FullName,
		StartDate,
		EndDate,
		ISNULL(CASE WHEN RIGHT(MARCCreator_e, 1) = '.' 
			THEN LEFT(MARCCreator_e, LEN(MARCCreator_e) - 1) 
			ELSE MARCCreator_e END, '') AS Relationship,
		ISNULL(CASE WHEN RIGHT(MARCCreator_t, 1) = '.' 
			THEN LEFT(MARCCreator_t, LEN(MARCCreator_t) - 1) 
			ELSE MARCCreator_t END, '') AS TitleOfWork,
		MARCDataFieldTag,
		AuthorID,
		SequenceOrder
FROM	#tmpAuthor
WHERE	AuthorID IS NOT NULL

DROP TABLE #tmpAuthor

SET NOCOUNT OFF

END

GO
