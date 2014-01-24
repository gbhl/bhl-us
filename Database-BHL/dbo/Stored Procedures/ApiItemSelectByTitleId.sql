
CREATE PROCEDURE [dbo].[ApiItemSelectByTitleId]
@TitleID int
AS 

SET NOCOUNT ON

DECLARE @RedirID int
SELECT @RedirID = RedirectTitleID FROM dbo.Title WHERE TitleID = @TitleID

IF (@RedirID IS NOT NULL)
	exec dbo.ApiItemSelectByTitleId @RedirID
ELSE
	SELECT	i.ItemID,
			i.PrimaryTitleID,
			i.ThumbnailPageID,
			s.SourceName,
			i.Barcode,
			i.Volume,
			i.[Year],
			ISNULL(inst.InstitutionName, '') AS InstitutionName,
			i.Sponsor,
			ISNULL(l.LanguageName, '') AS Language,
			ISNULL(i.LicenseUrl, '') AS LicenseUrl,
			ISNULL(i.Rights, '') AS Rights,
			ISNULL(i.DueDiligence, '') AS DueDiligence,
			ISNULL(i.CopyrightStatus, '') AS CopyrightStatus,
			ISNULL(i.CopyrightRegion, '') AS CopyrightRegion,
			ISNULL(i.ExternalUrl, '') AS ExternalUrl
	FROM	dbo.Item i LEFT JOIN dbo.Institution inst
				ON i.InstitutionCode = inst.InstitutionCode
			LEFT JOIN dbo.[Language] l
				ON i.LanguageCode = l.LanguageCode
			INNER JOIN [dbo].[TitleItem] ti
				ON i.ItemID = ti.ItemID
			INNER JOIN [dbo].[Title] t
				ON ti.[TitleID] = t.[TitleID]
			INNER JOIN dbo.ItemSource s
				ON i.ItemSourceID = s.ItemSourceID
	WHERE	t.TitleId = @TitleId 
	AND		i.ItemStatusID = 40
	AND		t.PublishReady = 1
	ORDER BY ti.ItemSequence

