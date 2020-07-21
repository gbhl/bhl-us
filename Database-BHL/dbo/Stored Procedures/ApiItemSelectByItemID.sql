﻿CREATE PROCEDURE [dbo].[ApiItemSelectByItemID]

@ItemID int

AS 

SET NOCOUNT ON

DECLARE @RedirItemID int
SELECT @RedirItemID = RedirectItemID FROM dbo.Item WHERE ItemID = @ItemID

IF (@RedirItemID IS NOT NULL)
	exec dbo.ApiItemSelectByItemID @RedirItemID
ELSE
	SELECT	i.ItemID,
			i.PrimaryTitleID,
			c.FirstPageID AS ThumbnailPageID,
			s.SourceName,
			i.Barcode,
			i.Volume,
			i.[Year],
			i.EndYear,
			i.ItemDescription,
			c.ItemContributors AS InstitutionName,
			i.Sponsor,
			ISNULL(l.LanguageName, '') AS Language,
			ISNULL(i.LicenseUrl, '') AS LicenseUrl,
			ISNULL(i.Rights, '') AS Rights,
			ISNULL(i.DueDiligence, '') AS DueDiligence,
			ISNULL(i.CopyrightStatus, '') AS CopyrightStatus,
			ISNULL(i.CopyrightRegion, '') AS CopyrightRegion,
			ISNULL(i.ExternalUrl, '') AS ExternalUrl,
			i.CreationDate
	FROM	dbo.Item i 
			LEFT JOIN dbo.[Language] l ON i.LanguageCode = l.LanguageCode
			INNER JOIN dbo.ItemSource s ON i.ItemSourceID = s.ItemSourceID
			INNER JOIN dbo.SearchCatalog c ON c.ItemID = i.ItemID AND c.TitleID = i.PrimaryTitleID
	WHERE	i.ItemID = @ItemId
	AND		i.ItemStatusID = 40
