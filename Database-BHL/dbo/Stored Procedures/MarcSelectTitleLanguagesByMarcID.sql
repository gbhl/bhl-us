CREATE PROCEDURE [dbo].[MarcSelectTitleLanguagesByMarcID]

@MarcID int

AS
BEGIN

SET NOCOUNT ON

-- =======================================================================
-- Build temp table

CREATE TABLE #tmpTitleLanguage(
	[LanguageCode] [nvarchar](10) NOT NULL DEFAULT('')
	)		

-- =======================================================================
-- Populate the temp table

INSERT INTO #tmpTitleLanguage (LanguageCode)
SELECT DISTINCT LanguageCode
FROM	dbo.fnSplitLanguage(@MarcID)


-- =======================================================================
-- Deliver the final result set
SELECT	LanguageCode
FROM	#tmpTitleLanguage

DROP TABLE #tmpTitleLanguage

END
