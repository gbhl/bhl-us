
CREATE VIEW [dbo].[vwIAMarcDataField]
AS
SELECT	m.ItemID,
		m.MarcID,
		d.MarcDataFieldID,
		s.MarcSubFieldID,
		d.Tag AS DataFieldTag,
		d.Indicator1,
		d.Indicator2,
		s.Code,
		s.[Value] AS SubFieldValue
FROM	IAMarc m LEFT JOIN IAMarcDataField d
			ON m.marcid = d.marcid
		LEFT JOIN IAMarcSubField s
			ON d.marcdatafieldid = s.marcdatafieldid
