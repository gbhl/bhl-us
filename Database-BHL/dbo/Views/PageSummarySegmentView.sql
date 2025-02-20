CREATE VIEW [dbo].[PageSummarySegmentView]

AS

SELECT	-- Title
		t.MARCBibID, t.TitleID, t.RedirectTitleID, t.FullTitle, t.RareBooks, 
		t.ShortTitle, t.SortTitle, t.PartNumber, t.PartName,
		-- Item
		s.SegmentID AS BookID, isg.ItemStatusID, isg.ItemID, s.RedirectSegmentID AS RedirectBookID, pt.TitleID AS PrimaryTitleID,
		s.BarCode, s.Volume, isg.FileRootFolder, CAST(NULL AS nvarchar(100)) AS Sponsor, s.PageProgression, b.IsVirtual,
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
FROM	dbo.Item isg
		LEFT JOIN dbo.Vault v ON isg.VaultID = v.VaultID
		INNER JOIN dbo.Segment s ON isg.ItemID = s.ItemID
		LEFT JOIN dbo.ItemRelationship r ON isg.ItemID = r.ChildID
		LEFT JOIN dbo.Item ibk ON r.ParentID = ibk.ItemID
		LEFT JOIN dbo.Book b ON r.ParentID = b.ItemID
		LEFT JOIN dbo.vwItemPrimaryTitle pt ON ibk.ItemID = pt.ItemID
		LEFT JOIN dbo.ItemTitle it ON ibk.ItemID = it.ItemID
		LEFT JOIN dbo.Title t ON it.TitleID = t.TitleID 
		INNER JOIN dbo.ItemPage ip ON isg.ItemID = ip.ItemID
		INNER JOIN dbo.Page p ON ip.PageID = p.PageID
		LEFT JOIN dbo.ItemSource src ON isg.ItemSourceID = src.ItemSourceID
		CROSS JOIN dbo.Configuration c
WHERE	c.ConfigurationName = 'ImageBaseURL'

GO
