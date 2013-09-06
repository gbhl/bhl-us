CREATE VIEW [dbo].[vwMarcDataField]
AS
SELECT	m.MarcImportBatchID,
		m.MarcFileLocation,
		m.InstitutionCode,
		m.MarcID,
		d.MarcDataFieldID,
		s.MarcSubFieldID,
		d.Tag AS DataFieldTag,
		d.Indicator1,
		d.Indicator2,
		s.Code,
		s.[Value] AS SubFieldValue
FROM	Marc m LEFT JOIN MarcDataField d
			ON m.marcid = d.marcid
		LEFT JOIN MarcSubField s
			ON d.marcdatafieldid = s.marcdatafieldid
