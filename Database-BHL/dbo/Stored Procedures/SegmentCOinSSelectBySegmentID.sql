CREATE PROCEDURE [dbo].[SegmentCOinSSelectBySegmentID]

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

SELECT DISTINCT 
		SegmentID
		,rft_atitle
		,rft_jtitle
		,rft_date
		,rft_volume
		,rft_issue
		,rft_spage
		,rft_epage
		,rft_pages
		,rft_language
		,rft_issn
		,rft_eissn
		,rft_aulast
		,rft_aufirst
		,rft_au
		,rft_subject
		,rft_isbn
		,rft_coden
		,rft_genre
		,rft_contributor
FROM	dbo.SegmentCOinSView
WHERE	SegmentID = @SegmentID

END

GO
