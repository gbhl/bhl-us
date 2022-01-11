CREATE PROCEDURE [dbo].[ItemCOinSSelectByTitleID]

@TitleID int

AS

BEGIN

SET NOCOUNT ON

SELECT DISTINCT 
		TitleID
		,ItemID
		,lccn
		,oclc
		,rft_title
		,rft_stitle
		,rft_volume
		,rft_language
		,rft_isbn
		,rft_aulast
		,rft_aufirst
		,rft_au_BOOK
		,rft_au_DC
		,rft_aucorp
		,rft_place
		,rft_pub
		,rft_publisher
		,rft_date_ITEM
		,rft_date_TITLE
		,rft_edition
		,rft_tpages
		,rft_issn
		,rft_eissn
		,rft_coden
		,rft_subject
		,rft_contributor_ITEM
		,rft_contributor_TITLE
		,rft_genre
FROM	dbo.ItemCOinSView
WHERE	TitleID = @TitleID

END

GO
