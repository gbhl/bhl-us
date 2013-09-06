CREATE PROCEDURE dbo.MarcImportBatchSelectStatsByInstitution

@InstitutionCode nvarchar(10) = ''

AS

SET NOCOUNT ON

SELECT	b.MarcImportBatchID,
		b.FileName,
		i.InstitutionName,
		b.CreationDate,
		COUNT(m.MarcID) AS NumberOfRecords,
		SUM(CASE WHEN m.MarcImportStatusID = 10 THEN 1 ELSE 0 END) AS New,
		SUM(CASE WHEN m.MarcImportStatusID = 20 THEN 1 ELSE 0 END) AS PendingImport,
		SUM(CASE WHEN m.MarcImportStatusID = 30 THEN 1 ELSE 0 END) AS Complete,
		SUM(CASE WHEN m.MarcImportStatusID = 99 THEN 1 ELSE 0 END) AS Error
FROM	dbo.MarcImportBatch b INNER JOIN dbo.Institution i
			ON b.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.Marc m
			ON b.MarcImportBatchID = m.MarcImportBatchID
WHERE	b.InstitutionCode = @InstitutionCode OR @InstitutionCode = ''
GROUP BY
		b.MarcImportBatchID,
		b.FileName,
		i.InstitutionName,
		b.CreationDate
ORDER BY
		b.CreationDate DESC
