
CREATE PROCEDURE [dbo].[MarcSelectAssociationIdsByMarcDataFieldID]

@MarcDataFieldID int

AS
BEGIN

SET NOCOUNT ON

-- =======================================================================
-- Build temp table

CREATE TABLE #tmpIdentifier (
	[TitleIdentifierID] [int] NOT NULL,
	[IdentifierValue] [varchar](125) NOT NULL DEFAULT ('')
)

-- =======================================================================
-- Populate the temp table

DECLARE @OCLC int
DECLARE @DLC int
DECLARE @ISSN int

SELECT @OCLC = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'OCLC'
SELECT @DLC = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DLC'
SELECT @ISSN = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISSN'

-- OCLC
INSERT INTO #tmpIdentifier (TitleIdentifierID, IdentifierValue)
SELECT	@OCLC, 
		LTRIM(RTRIM(REPLACE(m.SubFieldValue, '(OCoLC)', '')))
FROM	vwMarcDataField m
WHERE	m.Code = 'w' 
AND		m.SubFieldValue LIKE '(OCoLC)%'
AND		m.MarcDataFieldID = @MarcDataFieldID

-- DLC (Library of Congress)
INSERT INTO #tmpIdentifier (TitleIdentifierID, IdentifierValue)
SELECT	@DLC, 
		LTRIM(RTRIM(REPLACE(m.SubFieldValue, '(DLC)', '')))
FROM	vwMarcDataField m
WHERE	m.Code = 'w' 
AND		m.SubFieldValue LIKE '(DLC)%'
AND		m.MarcDataFieldID = @MarcDataFieldID

-- ISSN
INSERT INTO #tmpIdentifier (TitleIdentifierID, IdentifierValue)
SELECT	@ISSN, 
		LTRIM(RTRIM(REPLACE(m.SubFieldValue, ';', '')))
FROM	vwMarcDataField m
WHERE	Code = 'x'
AND		m.MarcDataFieldID = @MarcDataFieldID

-- =======================================================================
-- Deliver the final result set
SELECT	TitleIdentifierID,
		IdentifierValue
FROM	#tmpIdentifier
WHERE	IdentifierValue <> ''

DROP TABLE #tmpIdentifier

SET NOCOUNT OFF

END

