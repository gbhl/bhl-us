CREATE PROCEDURE [dbo].[OAIRecordPublishInsertTitleAndItem]

@OAIRecordID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @ProductionTitleID int
DECLARE @ProductionItemID int

DECLARE @IdentifierISSN int
DECLARE @IdentifierISBN int
DECLARE @IdentifierLCCN int

SELECT @IdentifierISSN = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'ISSN'
SELECT @IdentifierISBN = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'ISBN'
SELECT @IdentifierLCCN = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'DLC'

-- =======================================================================
-- Resolve title.  
--
-- Multiple attempts are made to find a matching title in production.  In
-- order, the following criteria are used to find a match:
--
--	1) DOI
--	2) Issn
--	3) Isbn
--  4) Lccn
--
-- After titles have been resolved, if a ProductionTitleID has not been found
-- then a new title record will be inserted into the production database.

-- Match on DOI
SELECT	@ProductionTitleID = t.TitleID
FROM	dbo.OAIRecord o 
		INNER JOIN dbo.BHLDOI d ON o.Doi = d.DoiName AND d.DOIEntityTypeID = 10 -- title
		INNER JOIN dbo.BHLTitle t ON d.EntityID = t.TitleID
WHERE	o.OAIRecordID = @OAIRecordID

-- Match on ISSN
IF @ProductionTitleID IS NULL
BEGIN
	SELECT	@ProductionTitleID = t.TitleID
	FROM	dbo.OAIRecord o
			INNER JOIN dbo.BHLTitle_Identifier ti ON o.Issn = ti.IdentifierValue AND ti.IdentifierID = @IdentifierISSN
			INNER JOIN dbo.BHLTitle t ON ti.TitleID = t.TitleID
	WHERE	o.OAIRecordID = @OAIRecordID
END

-- Match on ISBN
IF @ProductionTitleID IS NULL
BEGIN
	SELECT	@ProductionTitleID = t.TitleID
	FROM	dbo.OAIRecord o
			INNER JOIN dbo.BHLTitle_Identifier ti ON o.Isbn = ti.IdentifierValue AND ti.IdentifierID = @IdentifierISBN
			INNER JOIN dbo.BHLTitle t ON ti.TitleID = t.TitleID
	WHERE	o.OAIRecordID = @OAIRecordID
END

-- Match on LCCN
IF @ProductionTitleID IS NULL
BEGIN
	SELECT	@ProductionTitleID = t.TitleID
	FROM	dbo.OAIRecord o
			INNER JOIN dbo.BHLTitle_Identifier ti ON o.Lccn = ti.IdentifierValue AND ti.IdentifierID = @IdentifierLCCN
			INNER JOIN dbo.BHLTitle t ON ti.TitleID = t.TitleID
	WHERE	o.OAIRecordID = @OAIRecordID
END

-- If the selected production title has been redirected to a different 
-- title, then use that title instead.  Follow the "redirect" chain up 
-- to ten levels.
SELECT	@ProductionTitleID = COALESCE(bt10.TitleID, bt9.TitleID, bt8.TitleiD, bt7.TitleID, bt6.TitleID,
									bt5.TitleID, bt4.TitleID, bt3.TitleID, bt2.TitleID, bt1.TitleID)
FROM	dbo.BHLTitle bt1
		LEFT JOIN dbo.BHLTitle bt2 ON bt1.RedirectTitleID = bt2.TitleID
		LEFT JOIN dbo.BHLTitle bt3 ON bt2.RedirectTitleID = bt3.TitleID
		LEFT JOIN dbo.BHLTitle bt4 ON bt3.RedirectTitleID = bt4.TitleID
		LEFT JOIN dbo.BHLTitle bt5 ON bt4.RedirectTitleID = bt5.TitleID
		LEFT JOIN dbo.BHLTitle bt6 ON bt5.RedirectTitleID = bt6.TitleID
		LEFT JOIN dbo.BHLTitle bt7 ON bt6.RedirectTitleID = bt7.TitleID
		LEFT JOIN dbo.BHLTitle bt8 ON bt7.RedirectTitleID = bt8.TitleID
		LEFT JOIN dbo.BHLTitle bt9 ON bt8.RedirectTitleID = bt9.TitleID
		LEFT JOIN dbo.BHLTitle bt10 ON bt9.RedirectTitleID = bt10.TitleID
WHERE	bt1.TitleID = @ProductionTitleID

-- =======================================================================

-- Start a new transaction while we perform the inserts/updates
BEGIN TRAN

IF @ProductionTitleID IS NOT NULL
BEGIN
	exec dbo.OAIRecordPublishUpdateTitle @OAIRecordID, @ProductionTitleID
END
ELSE
BEGIN
	exec dbo.OAIRecordPublishInsertTitle @OAIRecordID, @ProductionTitleID OUTPUT
END

exec dbo.OAIRecordPublishInsertItem @OAIRecordID, @ProductionTitleID, @ProductionItemID OUTPUT

-- Update the ProductionTitleID, ProductionItemID and OAIRecordStatus of the just-inserted record
UPDATE	OAIRecord
SET		OAIRecordStatusID = 20,
		ProductionTitleID = @ProductionTitleID,
		ProductionItemID = @ProductionItemID,
		LastModifiedDate = GETDATE()
WHERE	OAIRecordID = @OAIRecordID

COMMIT TRAN

END

GO
