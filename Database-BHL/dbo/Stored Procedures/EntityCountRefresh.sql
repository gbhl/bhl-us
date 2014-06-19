CREATE PROCEDURE dbo.EntityCountRefresh

AS

BEGIN

SET NOCOUNT ON

DECLARE @ActiveTitleTypeID int
DECLARE @ActiveItemTypeID int
DECLARE @ActivePageTypeID int
DECLARE @ActiveSegmentTypeID int
DECLARE @ActiveNameTypeID int

SELECT @ActiveTitleTypeID = EntityCountTypeID FROM dbo.EntityCountType WHERE FullName = 'Active Titles'
SELECT @ActiveItemTypeID = EntityCountTypeID FROM dbo.EntityCountType WHERE FullName = 'Active Items'
SELECT @ActivePageTypeID = EntityCountTypeID FROM dbo.EntityCountType WHERE FullName = 'Active Pages'
SELECT @ActiveSegmentTypeID = EntityCountTypeID FROM dbo.EntityCountType WHERE FullName = 'Active Segments'
SELECT @ActiveNameTypeID = EntityCountTypeID FROM dbo.EntityCountType WHERE FullName = 'Active Names'

CREATE TABLE #Stats
	(
	TitleCount int NOT NULL,
	VolumeCount int NOT NULL,
	[PageCount] int NOT NULL,
	TitleTotal int NOT NULL,
	VolumeTotal int NOT NULL,
	PageTotal int NOT NULL,
	SegmentCount int NOT NULL,
	SegmentTotal int NOT NULL,
	ItemSegmentCount int NOT NULL,
	ItemSegmentTotal int NOT NULL,
	NameCount int NOT NULL,
	NameTotal int NOT NULL,
	UniqueNameCount int NOT NULL,
	UniqueNameTotal int NOT NULL,
	VerifiedNameCount int NOT NULL,
	VerifiedNameTotal int NOT NULL,
	EOLNameCount int NOT NULL,
	EOLNameTotal int NOT NULL,
	EOLPageCount int NOT NULL,
	EOLPageTotal int NOT NULL
	)

DECLARE @TitleCount int
DECLARE @ItemCount int
DECLARE @PageCount int
DECLARE @SegmentCount int
DECLARE @NameCount int

DECLARE @CreationDate datetime
SELECT @CreationDate = GETDATE()

-- Get the title, item, volume, and segment counts
INSERT #Stats exec StatsSelect

SELECT	@TitleCount = TitleCount, 
		@ItemCount = VolumeCount, 
		@PageCount = [PageCount], 
		@SegmentCount = SegmentCount 
FROM	#Stats

-- Get the name counts
TRUNCATE TABLE #Stats
INSERT #Stats exec StatsSelect 0, 1

SELECT	@NameCount = NameCount
FROM	#Stats

-- Insert the counts into the table
BEGIN TRY
	BEGIN TRAN

	INSERT dbo.EntityCount (EntityCountTypeID, CountValue, CreationDate) VALUES (@ActiveTitleTypeID, @TitleCount, @CreationDate)
	INSERT dbo.EntityCount (EntityCountTypeID, CountValue, CreationDate) VALUES (@ActiveItemTypeID, @ItemCount, @CreationDate)
	INSERT dbo.EntityCount (EntityCountTypeID, CountValue, CreationDate) VALUES (@ActivePageTypeID, @PageCount, @CreationDate)
	INSERT dbo.EntityCount (EntityCountTypeID, CountValue, CreationDate) VALUES (@ActiveSegmentTypeID, @SegmentCount, @CreationDate)
	INSERT dbo.EntityCount (EntityCountTypeID, CountValue, CreationDate) VALUES (@ActiveNameTypeID, @NameCount, @CreationDate)

	COMMIT TRAN
END TRY
BEGIN CATCH
	DECLARE @ErrorMessage nvarchar(4000)
    DECLARE @ErrorSeverity int
    DECLARE @ErrorState int

    SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();

	IF @@TRANCOUNT > 0 ROLLBACK TRAN

	RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
END CATCH

END
