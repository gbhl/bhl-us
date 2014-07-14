CREATE PROCEDURE dbo.OAIRecordPublishToProduction

@HarvestLogID int = NULL

AS

------------------------------------------------------------------------------------------------
--
-- NOTES:
--
--  Multiple instances of this procedure should NOT be run in parallel
--
------------------------------------------------------------------------------------------------

BEGIN

SET NOCOUNT ON

DECLARE @OAIRecordID int
DECLARE @TotalInsert int
DECLARE @TotalUpdate int
DECLARE @TotalDelete int
DECLARE @PublishResult nvarchar(50)

SET @TotalInsert = 0
SET @TotalUpdate = 0
SET @TotalDelete = 0
SET @PublishResult = 'ok'

BEGIN TRY

	--@@@@@@@@@@@@@@@@@@@@@@@ Get production identifiers for new OAI records @@@@@@@@@@@@@@@@@@@@@@@

	exec dbo.OAIRecordPublishSetProductionIDs @HarvestLogID

	--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@     Delete segment records     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

	-- Get the first segment to delete
	SET @OAIRecordID = NULL
	SELECT	TOP 1 @OAIRecordID = OAIRecordID
	FROM	dbo.OAIRecord o 
			INNER JOIN dbo.OAIHarvestLog l ON o.HarvestLogID = l.HarvestLogID
			INNER JOIN dbo.OAIHarvestSet s ON l.HarvestSetID = s.HarvestSetID
	WHERE	(o.RecordType = 'Segment' OR (o.RecordType = 'Unknown' AND s.DefaultRecordType = 'Segment'))
	AND		o.OAIRecordStatusID = 10
	AND		o.ProductionSegmentID IS NOT NULL
	AND		o.OAIStatus = 'deleted'
	AND		o.HarvestLogID = ISNULL(@HarvestLogID, o.HarvestLogID)

	WHILE (@OAIRecordID IS NOT NULL)
	BEGIN
		exec dbo.OAIRecordPublishDeleteSegment @OAIRecordID

		SET @TotalDelete = @TotalDelete + 1

		-- Get the next segment to delete
		SET @OAIRecordID = NULL
		SELECT	TOP 1 @OAIRecordID = OAIRecordID
		FROM	dbo.OAIRecord o 
				INNER JOIN dbo.OAIHarvestLog l ON o.HarvestLogID = l.HarvestLogID
				INNER JOIN dbo.OAIHarvestSet s ON l.HarvestSetID = s.HarvestSetID
		WHERE	(o.RecordType = 'Segment' OR (o.RecordType = 'Unknown' AND s.DefaultRecordType = 'Segment'))
		AND		o.OAIRecordStatusID = 10
		AND		o.ProductionSegmentID IS NOT NULL
		AND		o.OAIStatus = 'deleted'
		AND		o.HarvestLogID = ISNULL(@HarvestLogID, o.HarvestLogID)
	END
	
	-- Set any segments marked for deletion that could not be found in production to "Delete Not Found"
	UPDATE	dbo.OAIRecord
	SET		OAIRecordStatusID = 30 -- Delete Not Found
	FROM	dbo.OAIRecord o 
			INNER JOIN dbo.OAIHarvestLog l ON o.HarvestLogID = l.HarvestLogID
			INNER JOIN dbo.OAIHarvestSet s ON l.HarvestSetID = s.HarvestSetID
	WHERE	(o.RecordType = 'Segment' OR (o.RecordType = 'Unknown' AND s.DefaultRecordType = 'Segment'))
	AND		o.OAIRecordStatusID = 10
	AND		o.ProductionSegmentID IS NULL
	AND		o.OAIStatus = 'deleted'
	AND		o.HarvestLogID = ISNULL(@HarvestLogID, o.HarvestLogID)


	--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@   Insert new segment records   @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

	-- Get the first segment to insert
	SET @OAIRecordID = NULL
	SELECT	TOP 1 @OAIRecordID = OAIRecordID
	FROM	dbo.OAIRecord o 
			INNER JOIN dbo.OAIHarvestLog l ON o.HarvestLogID = l.HarvestLogID
			INNER JOIN dbo.OAIHarvestSet s ON l.HarvestSetID = s.HarvestSetID
	WHERE	(o.RecordType = 'Segment' OR (o.RecordType = 'Unknown' AND s.DefaultRecordType = 'Segment'))
	AND		o.OAIRecordStatusID = 10
	AND		o.ProductionSegmentID IS NULL
	AND		o.HarvestLogID = ISNULL(@HarvestLogID, o.HarvestLogID)

	WHILE (@OAIRecordID IS NOT NULL)
	BEGIN
		exec dbo.OAIRecordPublishInsertSegment @OAIRecordID

		SET @TotalInsert = @TotalInsert + 1

		-- Get the next segment to insert
		SET @OAIRecordID = NULL
		SELECT	TOP 1 @OAIRecordID = OAIRecordID
		FROM	dbo.OAIRecord o 
				INNER JOIN dbo.OAIHarvestLog l ON o.HarvestLogID = l.HarvestLogID
				INNER JOIN dbo.OAIHarvestSet s ON l.HarvestSetID = s.HarvestSetID
		WHERE	(o.RecordType = 'Segment' OR (o.RecordType = 'Unknown' AND s.DefaultRecordType = 'Segment'))
		AND		o.OAIRecordStatusID = 10
		AND		o.ProductionSegmentID IS NULL
		AND		o.HarvestLogID = ISNULL(@HarvestLogID, o.HarvestLogID)
	END


	--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@     Update segment records     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

	-- Get the first segment to update
	SET @OAIRecordID = NULL
	SELECT	TOP 1 @OAIRecordID = OAIRecordID
	FROM	dbo.OAIRecord o 
			INNER JOIN dbo.OAIHarvestLog l ON o.HarvestLogID = l.HarvestLogID
			INNER JOIN dbo.OAIHarvestSet s ON l.HarvestSetID = s.HarvestSetID
	WHERE	(o.RecordType = 'Segment' OR (o.RecordType = 'Unknown' AND s.DefaultRecordType = 'Segment'))
	AND		o.OAIRecordStatusID = 10
	AND		o.ProductionSegmentID IS NOT NULL
	AND		o.HarvestLogID = ISNULL(@HarvestLogID, o.HarvestLogID)

	WHILE (@OAIRecordID IS NOT NULL)
	BEGIN
		exec dbo.OAIRecordPublishUpdateSegment @OAIRecordID

		SET @TotalUpdate = @TotalUpdate + 1

		-- Get the next segment to update
		SET @OAIRecordID = NULL
		SELECT	TOP 1 @OAIRecordID = OAIRecordID
		FROM	dbo.OAIRecord o 
				INNER JOIN dbo.OAIHarvestLog l ON o.HarvestLogID = l.HarvestLogID
				INNER JOIN dbo.OAIHarvestSet s ON l.HarvestSetID = s.HarvestSetID
		WHERE	(o.RecordType = 'Segment' OR (o.RecordType = 'Unknown' AND s.DefaultRecordType = 'Segment'))
		AND		o.OAIRecordStatusID = 10
		AND		o.ProductionSegmentID IS NOT NULL
		AND		o.HarvestLogID = ISNULL(@HarvestLogID, o.HarvestLogID)
	END


	--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@   Delete title/item records    @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

	-- Get the first title/item to delete
	SET @OAIRecordID = NULL
	SELECT	TOP 1 @OAIRecordID = OAIRecordID
	FROM	dbo.OAIRecord o 
			INNER JOIN dbo.OAIHarvestLog l ON o.HarvestLogID = l.HarvestLogID
			INNER JOIN dbo.OAIHarvestSet s ON l.HarvestSetID = s.HarvestSetID
	WHERE	(o.RecordType = 'BookJournal' OR (o.RecordType = 'Unknown' AND s.DefaultRecordType = 'BookJournal'))
	AND		o.OAIRecordStatusID = 10
	AND		o.ProductionTitleID IS NOT NULL
	AND		o.ProductionItemID IS NOT NULL
	AND		o.OAIStatus = 'deleted'
	AND		o.HarvestLogID = ISNULL(@HarvestLogID, o.HarvestLogID)

	WHILE (@OAIRecordID IS NOT NULL)
	BEGIN
		exec dbo.OAIRecordPublishDeleteTitleAndItem @OAIRecordID

		SET @TotalDelete = @TotalDelete + 1

		-- Get the next title/item to delete
		SET @OAIRecordID = NULL
		SELECT	TOP 1 @OAIRecordID = OAIRecordID
		FROM	dbo.OAIRecord o 
				INNER JOIN dbo.OAIHarvestLog l ON o.HarvestLogID = l.HarvestLogID
				INNER JOIN dbo.OAIHarvestSet s ON l.HarvestSetID = s.HarvestSetID
		WHERE	(o.RecordType = 'BookJournal' OR (o.RecordType = 'Unknown' AND s.DefaultRecordType = 'BookJournal'))
		AND		o.OAIRecordStatusID = 10
		AND		o.ProductionTitleID IS NOT NULL
		AND		o.ProductionItemID IS NOT NULL
		AND		o.OAIStatus = 'deleted'
		AND		o.HarvestLogID = ISNULL(@HarvestLogID, o.HarvestLogID)
	END

	-- Set any title/items marked for deletion that could not be found in production to "Delete Not Found"
	UPDATE	dbo.OAIRecord
	SET		OAIRecordStatusID = 30 -- Delete Not Found
	FROM	dbo.OAIRecord o 
			INNER JOIN dbo.OAIHarvestLog l ON o.HarvestLogID = l.HarvestLogID
			INNER JOIN dbo.OAIHarvestSet s ON l.HarvestSetID = s.HarvestSetID
	WHERE	(o.RecordType = 'BookJournal' OR (o.RecordType = 'Unknown' AND s.DefaultRecordType = 'BookJournal'))
	AND		o.OAIRecordStatusID = 10
	AND		o.ProductionTitleID IS NULL
	AND		o.ProductionItemID IS NULL
	AND		o.OAIStatus = 'deleted'
	AND		o.HarvestLogID = ISNULL(@HarvestLogID, o.HarvestLogID)


	--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Insert new title/item records  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

	-- Get the first title/item to insert
	SET @OAIRecordID = NULL
	SELECT	TOP 1 @OAIRecordID = OAIRecordID
	FROM	dbo.OAIRecord o 
			INNER JOIN dbo.OAIHarvestLog l ON o.HarvestLogID = l.HarvestLogID
			INNER JOIN dbo.OAIHarvestSet s ON l.HarvestSetID = s.HarvestSetID
	WHERE	(o.RecordType = 'BookJournal' OR (o.RecordType = 'Unknown' AND s.DefaultRecordType = 'BookJournal'))
	AND		o.OAIRecordStatusID = 10
	AND		o.ProductionTitleID IS NULL
	AND		o.ProductionItemID IS NULL
	AND		o.HarvestLogID = ISNULL(@HarvestLogID, o.HarvestLogID)

	WHILE (@OAIRecordID IS NOT NULL)
	BEGIN
		exec dbo.OAIRecordPublishInsertTitleAndItem @OAIRecordID

		SET @TotalInsert = @TotalInsert + 1

		-- Get the next title/item to insert
		SET @OAIRecordID = NULL
		SELECT	TOP 1 @OAIRecordID = OAIRecordID
		FROM	dbo.OAIRecord o 
				INNER JOIN dbo.OAIHarvestLog l ON o.HarvestLogID = l.HarvestLogID
				INNER JOIN dbo.OAIHarvestSet s ON l.HarvestSetID = s.HarvestSetID
		WHERE	(o.RecordType = 'BookJournal' OR (o.RecordType = 'Unknown' AND s.DefaultRecordType = 'BookJournal'))
		AND		o.OAIRecordStatusID = 10
		AND		o.ProductionTitleID IS NULL
		AND		o.ProductionItemID IS NULL
		AND		o.HarvestLogID = ISNULL(@HarvestLogID, o.HarvestLogID)
	END


	--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@   Update title/item records    @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

	-- Get the first title/item to update
	SET @OAIRecordID = NULL
	SELECT	TOP 1 @OAIRecordID = OAIRecordID
	FROM	dbo.OAIRecord o 
			INNER JOIN dbo.OAIHarvestLog l ON o.HarvestLogID = l.HarvestLogID
			INNER JOIN dbo.OAIHarvestSet s ON l.HarvestSetID = s.HarvestSetID
	WHERE	(o.RecordType = 'BookJournal' OR (o.RecordType = 'Unknown' AND s.DefaultRecordType = 'BookJournal'))
	AND		o.OAIRecordStatusID = 10
	AND		o.ProductionTitleID IS NOT NULL
	AND		o.ProductionItemID IS NOT NULL
	AND		o.HarvestLogID = ISNULL(@HarvestLogID, o.HarvestLogID)

	WHILE (@OAIRecordID IS NOT NULL)
	BEGIN
		exec dbo.OAIRecordPublishUpdateTitleAndItem @OAIRecordID

		SET @TotalUpdate = @TotalUpdate + 1

		-- Get the next title/item to insert
		SET @OAIRecordID = NULL
		SELECT	TOP 1 @OAIRecordID = OAIRecordID
		FROM	dbo.OAIRecord o 
				INNER JOIN dbo.OAIHarvestLog l ON o.HarvestLogID = l.HarvestLogID
				INNER JOIN dbo.OAIHarvestSet s ON l.HarvestSetID = s.HarvestSetID
		WHERE	(o.RecordType = 'BookJournal' OR (o.RecordType = 'Unknown' AND s.DefaultRecordType = 'BookJournal'))
		AND		o.OAIRecordStatusID = 10
		AND		o.ProductionTitleID IS NOT NULL
		AND		o.ProductionItemID IS NOT NULL
		AND		o.HarvestLogID = ISNULL(@HarvestLogID, o.HarvestLogID)
	END

END TRY
BEGIN CATCH

	-- Abort any open transactions
	IF @@TRANCOUNT > 0 ROLLBACK TRAN

	--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  Record errors   @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
	INSERT INTO dbo.OAIPublishError (OAIRecordID, Number, Severity, [State], [Procedure], Line, [Message])
	SELECT	@OAIRecordID, ERROR_NUMBER(), ERROR_SEVERITY(),ERROR_STATE(), ERROR_PROCEDURE(), ERROR_LINE(), ERROR_MESSAGE()

	SET @PublishResult = 'Error'

END CATCH

--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@   Log results    @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

INSERT INTO dbo.OAIPublishLog (HarvestLogID, PublishResult, TotalInsert, TotalUpdate, TotalDelete)
VALUES (@HarvestLogID, @PublishResult, @TotalInsert, @TotalUpdate, @TotalDelete)


END


GO


