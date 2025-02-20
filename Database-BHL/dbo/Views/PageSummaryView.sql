CREATE VIEW [dbo].[PageSummaryView]

AS

SELECT	-- Title
		t.MARCBibID, t.TitleID, t.RedirectTitleID, t.FullTitle, t.RareBooks, 
		t.ShortTitle, t.SortTitle, t.PartNumber, t.PartName,
		-- Item
		b.BookID, i.ItemStatusID, i.ItemID, b.RedirectBookID, pt.TitleID AS PrimaryTitleID,
		b.BarCode, b.Volume, i.FileRootFolder, b.Sponsor, b.PageProgression, b.IsVirtual,
		-- TitleItem
		it.ItemSequence,
		-- Page
		p.PageID, p.FileNamePrefix, p.PageDescription, ip.SequenceOrder, 
		p.Illustration, p.Active,
		-- Check the age of the Item record.  If less than 7 days old, use the
		-- ImageBaseDefaultURL.  This will most likely be http://www.archive.org.
		-- We use this address for new items to cover the period of time when an
		-- item is being transferred from Internet Archive to other image serving
		-- locations.
		CASE 
			WHEN ISNULL(p.ExternalURL, '') = '' THEN NULL 
			ELSE c.ConfigurationValue
		END AS ExternalBaseURL,
		CASE 
			WHEN ISNULL(p.ExternalURL, '') = '' THEN p.ExternalURL
			ELSE c.ConfigurationValue + p.ExternalURL
		END AS ExternalURL, 
		-- Vault
		v.WebVirtualDirectory, v.OCRFolderShare, 
		-- ItemSource
		src.DownloadUrl,
		src.ImageServerUrlFormat
FROM	dbo.Vault v
		INNER JOIN dbo.Item i ON v.VaultID = i.VaultID 
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.vwItemPrimaryTitle pt ON i.ItemID = pt.ItemID
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID 
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID 
		INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
		INNER JOIN dbo.Page p ON ip.PageID = p.PageID
		INNER JOIN dbo.ItemSource src ON i.ItemSourceID = src.ItemSourceID
		CROSS JOIN dbo.Configuration c
WHERE	c.ConfigurationName = 'ImageBaseURL'

GO
