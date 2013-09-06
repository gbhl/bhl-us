
CREATE PROCEDURE dbo.MarcUpdateStatusError

@MarcID int

AS
BEGIN

SET NOCOUNT ON

UPDATE	dbo.Marc
SET		MarcImportStatusID = 99
WHERE	MarcID = @MarcID

END
