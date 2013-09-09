CREATE VIEW vwMarcDetail
AS
SELECT	d.ItemID,
		d.MarcDataFieldID,
		s.MarcSubFieldID,
		d.Tag AS DataFieldTag,
		d.Indicator1,
		d.Indicator2,
		s.Code,
		s.[Value] AS SubFieldValue
FROM	vwMarcDataField d INNER JOIN MarcSubField s
			ON d.MarcDataFieldID = s.MarcDataFieldID
