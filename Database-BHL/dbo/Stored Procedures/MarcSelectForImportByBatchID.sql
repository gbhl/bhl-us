CREATE PROCEDURE [dbo].[MarcSelectForImportByBatchID]

@MarcImportBatchID int

AS
BEGIN

SET NOCOUNT ON

SELECT	MarcID, MarcFileLocation, InstitutionCode, Leader, TitleID AS BHLTitleID
FROM	dbo.Marc
WHERE	MarcImportBatchID = @MarcImportBatchID
AND		MarcImportStatusID = 20

END
