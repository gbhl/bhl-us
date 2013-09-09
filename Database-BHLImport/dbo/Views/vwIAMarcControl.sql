
CREATE VIEW [dbo].[vwIAMarcControl]
AS
SELECT	m.ItemID, m.MarcID, m.Leader, c.MarcControlID, c.Tag, c.Value 
FROM	IAMarc m LEFT JOIN IAMarcControl c
			ON m.marcid = c.marcid
