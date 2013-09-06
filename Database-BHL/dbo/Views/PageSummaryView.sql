
CREATE VIEW [dbo].[PageSummaryView]

AS

SELECT	-- Title
		dbo.Title.MARCBibID, dbo.Title.TitleID, dbo.Title.RedirectTitleID, dbo.Title.FullTitle, dbo.Title.RareBooks, 
		dbo.Title.ShortTitle, dbo.Title.SortTitle, dbo.Title.PartNumber, dbo.Title.PartName,
		-- Item
		dbo.Item.ItemStatusID, dbo.Item.ItemID, dbo.Item.RedirectItemID, dbo.Item.PrimaryTitleID,
		dbo.Item.BarCode, dbo.Item.PDFSize, dbo.Item.Volume, dbo.Item.FileRootFolder,
		-- TitleItem
		dbo.TitleItem.ItemSequence,
		-- Page
		dbo.Page.PageID, dbo.Page.FileNamePrefix, dbo.Page.PageDescription, dbo.Page.SequenceOrder, 
		dbo.Page.Illustration, dbo.Page.Active, dbo.Page.ExternalURL, 
		-- Check the age of the Item record.  If less than 7 days old, use the
		-- ImageBaseDefaultURL.  This will most likely be http://www.archive.org.
		-- We use this address for new items to cover the period of time when an
		-- item is being transferred from Internet Archive to other image serving
		-- locations.
		CASE WHEN ISNULL(dbo.Page.AltExternalURL, '') = '' THEN NULL 
		ELSE 
			CASE WHEN DATEDIFF(d, dbo.Item.CreationDate, GETDATE()) < 7 
				THEN Configuration2.ConfigurationValue
				ELSE dbo.Configuration.ConfigurationValue
			END
		END AS ExternalBaseURL,
		CASE WHEN ISNULL(dbo.Page.AltExternalURL, '') = '' THEN dbo.Page.AltExternalURL
		ELSE 
			CASE WHEN DATEDIFF(d, dbo.Item.CreationDate, GETDATE()) < 7 
				THEN Configuration2.ConfigurationValue + dbo.Page.AltExternalURL
				ELSE dbo.Configuration.ConfigurationValue + dbo.Page.AltExternalURL
			END
		END AS AltExternalURL, 
		-- Vault
		dbo.Vault.WebVirtualDirectory, dbo.Vault.OCRFolderShare, 
		-- ItemSource
		dbo.ItemSource.DownloadUrl,
		dbo.ItemSource.ImageServerUrlFormat
FROM	dbo.Vault 
		INNER JOIN dbo.Item ON dbo.Vault.VaultID = dbo.Item.VaultID 
		INNER JOIN dbo.TitleItem ON dbo.Item.ItemID = dbo.TitleItem.ItemID 
		INNER JOIN dbo.Title ON dbo.TitleItem.TitleID = dbo.Title.TitleID 
		INNER JOIN dbo.Page ON dbo.Item.ItemID = dbo.Page.ItemID 
		INNER JOIN dbo.ItemSource ON dbo.Item.ItemSourceID = dbo.ItemSource.ItemSourceID
		CROSS JOIN dbo.Configuration
		CROSS JOIN dbo.Configuration AS Configuration2
WHERE	dbo.Configuration.ConfigurationName = 'ImageBaseURL'
AND		Configuration2.ConfigurationName = 'ImageBaseDefaultURL'

