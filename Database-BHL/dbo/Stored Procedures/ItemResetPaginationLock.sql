SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemResetPaginationLock]

@MaxAge int

AS

BEGIN

-- Resets 'In Progress' items to 'Incomplete' if they have been 'In Progress' longer than @MaxAge.
-- 'In Progress' items are 'locked', and prevent any other user from paginating them.

SET NOCOUNT ON

DECLARE @Incomplete int
DECLARE @InProgress int
SELECT @Incomplete = PaginationStatusID FROM PaginationStatus WHERE PaginationStatusName = 'Incomplete'
SELECT @InProgress = PaginationStatusID FROM PaginationStatus WHERE PaginationStatusName = 'In Progress'

UPDATE	dbo.Book
SET		PaginationStatusID = @Incomplete,
		PaginationStatusDate = GETDATE()
WHERE	PaginationStatusID = @InProgress
AND		DATEDIFF(day, PaginationStatusDate, GETDATE()) > @MaxAge

UPDATE	dbo.Segment
SET		PaginationStatusID = @Incomplete,
		PaginationStatusDate = GETDATE()
WHERE	PaginationStatusID = @InProgress
AND		DATEDIFF(day, PaginationStatusDate, GETDATE()) > @MaxAge

END


GO
