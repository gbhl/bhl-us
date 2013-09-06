
CREATE PROCEDURE dbo.MarcUpdateStatusImported

@MarcID int

AS
BEGIN

SET NOCOUNT ON

UPDATE	dbo.Marc
SET		MarcImportStatusID = 30
WHERE	MarcID = @MarcID

END
