
CREATE VIEW [dbo].[vwMarcControl]
AS
SELECT	m.MarcImportBatchID,
		m.MarcFileLocation,
		m.InstitutionCode,
		m.MarcID, 
		m.Leader, 
		c.MarcControlID, 
		c.Tag, 
		c.Value 
FROM	Marc m LEFT JOIN MarcControl c
			ON m.marcid = c.marcid
