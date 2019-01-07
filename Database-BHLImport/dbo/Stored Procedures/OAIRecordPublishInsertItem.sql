CREATE PROCEDURE [dbo].[OAIRecordPublishInsertItem]

@OAIRecordID int,
@ProductionTitleID int,
@ProductionItemID int OUTPUT

AS

BEGIN

SET NOCOUNT ON

DECLARE @Right nvarchar(500)
SET @Right = ''
SELECT TOP 1 @Right = [Right] FROM dbo.OAIRecordRight WHERE OAIRecordID = @OAIRecordID

DECLARE @HoldingInstitutionRoleID int
SELECT	@HoldingInstitutionRoleID = InstitutionRoleID FROM dbo.BHLInstitutionRole WHERE InstitutionRoleName = 'Holding Institution'

BEGIN TRAN

-- Insert a new Item record
INSERT	dbo.BHLItem
		(
		ItemStatusID,
		PrimaryTitleID,
		Barcode,
		MarcItemID,
		CallNumber,
		Volume,
		LanguageCode,
		ItemSourceID,
		Year,
		Rights,
		ExternalUrl,
		CopyrightStatus
		)
SELECT	40,
		@ProductionTitleID AS PrimaryTitleID,
		r.BHLInstitutionCode + REPLACE(REPLACE(o.OAIIdentifier, ':', ''), '/', '') AS Barcode,
		o.OAIIdentifier AS MarcItemID,
		o.CallNumber,
		o.Volume,
		l.BHLLanguageCode AS LanguageCode,
		src.BHLItemSourceID AS ItemSourceID,
		Date AS Year,
		@Right AS Rights,
		Url AS ExternalUrl,
		'Not provided. Contact contributing library to verify copyright status.' AS CopyrightStatus
FROM	dbo.OAIRecord o
		LEFT JOIN dbo.OAIRecordLanguage l ON o.Language = l.OAILanguage
		INNER JOIN dbo.OAIHarvestLog lg ON o.HarvestLogID = lg.HarvestLogID
		INNER JOIN dbo.OAIHarvestSet s ON lg.HarvestSetID = s.HarvestSetID
		INNER JOIN dbo.OAIRepositoryFormat rf ON s.RepositoryFormatID = rf.RepositoryFormatID
		INNER JOIN dbo.OAIRepository r ON rf.RepositoryID = r.RepositoryID
		INNER JOIN dbo.ImportSourceItemSource src ON r.ImportSourceID = src.ImportSourceID
WHERE	o.OAIRecordID = @OAIRecordID

-- Preserve the production identifier for the new item
SET @ProductionItemID = SCOPE_IDENTITY()

-- Insert an ItemInstitution record for the holding institution
INSERT	dbo.BHLItemInstitution (ItemID, InstitutionCode, InstitutionRoleID)
SELECT	@ProductionItemID, r.BHLInstitutionCode, @HoldingInstitutionRoleID
FROM	dbo.OAIRecord o
		INNER JOIN dbo.OAIHarvestLog lg ON o.HarvestLogID = lg.HarvestLogID
		INNER JOIN dbo.OAIHarvestSet s ON lg.HarvestSetID = s.HarvestSetID
		INNER JOIN dbo.OAIRepositoryFormat rf ON s.RepositoryFormatID = rf.RepositoryFormatID
		INNER JOIN dbo.OAIRepository r ON rf.RepositoryID = r.RepositoryID
WHERE	o.OAIRecordID = @OAIRecordID

-- Insert a TitleItem record to associate the title and item
INSERT dbo.BHLTitleItem (TitleID, ItemID, ItemSequence) VALUES (@ProductionTitleID, @ProductionItemID, 1)

-- Calculate the TitleItem.ItemSequence by ordering each title by the item volume
UPDATE	dbo.BHLTitleItem
SET		ItemSequence = x.Sequence
FROM	dbo.BHLTitleItem bti INNER JOIN (
				SELECT	ti.TitleItemID,
						ROW_NUMBER() OVER (PARTITION BY ti.TitleID 
											ORDER BY CONVERT(BIGINT, LEFT(dbo.fnFilterString(i.Volume, '[0-9]', ''), 19))) AS Sequence
				FROM	dbo.BHLTitleItem ti INNER JOIN dbo.BHLItem i ON ti.ItemID = i.ItemID
				WHERE	ti.TitleID = @ProductionTitleID
				) AS x
			ON bti.TitleItemID = x.TitleItemID

COMMIT TRAN

END
