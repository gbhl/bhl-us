CREATE PROCEDURE dbo.NamePageDelete

@NamePageID int

AS
/*
 *	Use the specified values to delete NamePage records and (if necessary) 
 *  Name, NameResolved, and NameIdentifier records.
 */
BEGIN

SET NOCOUNT ON

DECLARE @NameID int
DECLARE @NameResolvedID int

-- Get some IDs for use later
SELECT	@NameID = np.NameID, @NameResolvedID = n.NameResolvedID 
FROM	dbo.NamePage np INNER JOIN dbo.Name n ON np.NameID = n.NameID
WHERE	np.NamePageID = @NamePageID

-- Delete the NamePage record
DELETE dbo.NamePage WHERE NamePageID = @NamePageID

-- Delete the Name record if it is no longer in use
IF NOT EXISTS(SELECT NamePageID FROM dbo.NamePage WHERE NameID = @NameID)
BEGIN
	DELETE dbo.Name WHERE NameID = @NameID
END

-- If the resolved name referenced by the Name record is no longer used by ANY Name record,
-- remove it and its associated NameIdentifier records
IF (@NameResolvedID IS NOT NULL AND 
	NOT EXISTS(SELECT NameID FROM dbo.Name WHERE NameResolvedID = @NameResolvedID))
BEGIN
	DELETE dbo.NameIdentifier WHERE NameResolvedID = @NameResolvedID
	DELETE dbo.NameResolved WHERE NameResolvedID = @NameResolvedID
END

END

