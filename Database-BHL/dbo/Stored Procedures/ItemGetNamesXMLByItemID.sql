CREATE PROCEDURE [dbo].[ItemGetNamesXMLByItemID]

@ItemID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @namebank int
DECLARE @eol int
SELECT @namebank = identifierid FROM dbo.Identifier WHERE IdentifierName = 'Namebank'
SELECT @eol = identifierid FROM dbo.Identifier WHERE IdentifierName = 'EOL'

DECLARE @Barcode nvarchar(200)
SELECT @Barcode = Barcode FROM dbo.Book WHERE ItemID = @ItemID
IF @Barcode IS NULL
BEGIN
	SELECT @Barcode = Barcode FROM dbo.Segment WHERE ItemID = @ItemID
END

-- Read the name information into a temp table.  This will preserve the "flat" structure of the
-- XML established by the previous name data model.
-- Do this in two steps, to ensure that the query engine uses the most efficient plan (it was
-- not choosing the best plan when done all in one step [2013-04-04]).
SELECT	np.namepageid,
		np.pageid,
		np.nameid,
		np.creationdate
INTO	#namepage
FROM	dbo.Page p 
		INNER JOIN dbo.NamePage np ON p.pageid = np.pageid
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
WHERE	ip.ItemID = @ItemID

SELECT	np.namepageid,
		np.pageid, 
		nr.nameresolvedid,
		n.namestring as found, 
		nr.resolvednamestring as confirmed,
		np.creationdate as dateadded
INTO	#pagename
FROM	#namepage np
		INNER JOIN dbo.Name n ON np.NameID = n.NameID
		LEFT JOIN dbo.NameResolved nr ON n.NameResolvedID = nr.NameResolvedID

-- Get Names XML for items with new or updated names
SELECT	'<?xml version="1.0" encoding="UTF-8" ?>' + (
SELECT	@Barcode + '_' + RIGHT('0000' + CONVERT(varchar(4), ip.sequenceorder), 4) AS map,
		'https://www.biodiversitylibrary.org/page/' + CONVERT(varchar(20), [page].pageid) AS bhlurl, 
		-- For this procedure, always use www.archive.org as the base domain for images.
		-- However, we should check the value of AltExternalURL before adding the 
		-- www.archive.org prefix. This allows for never-converted Page records (on the 
		-- beta site) and slowly converted Page records (on the production site).
		CASE WHEN ISNULL([page].ExternalURL, '') = '' THEN [page].ExternalURL
		ELSE 
			CASE WHEN LEFT([page].ExternalURL, 22) = 'https://www.archive.org'
				THEN [page].ExternalURL
				ELSE 'https://www.archive.org' + [page].ExternalURL
			END
		END AS imageurl, 
		name.found, 
		name.confirmed, 
		'http://www.ubio.org/browser/details.php?namebankID=' + CASE WHEN namebank.identifiervalue IS NULL THEN NULL ELSE namebank.identifiervalue END as ubiourl,
		'http://www.eol.org/pages/' + CASE WHEN eol.identifiervalue IS NULL THEN NULL ELSE eol.identifiervalue END AS eolurl,
		CONVERT(varchar(19), name.dateadded, 120) AS dateadded
FROM	dbo.ItemPage ip
		INNER JOIN dbo.page [page] ON ip.PageID = [page].PageID
		LEFT JOIN #pagename name ON [page].pageid = name.pageid
		LEFT JOIN nameidentifier as namebank 
			ON name.nameresolvedid = namebank.nameresolvedid
			AND namebank.identifierid = @namebank 
		LEFT JOIN nameidentifier as eol
			ON name.nameresolvedid = eol.nameresolvedid 
			AND eol.identifierid = @eol
WHERE	ip.ItemID = @ItemID
ORDER BY 
		ip.sequenceorder, 
		name.namepageid
FOR XML AUTO, ROOT ('book')
)

END

GO
