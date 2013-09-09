CREATE VIEW [dbo].[vwMarcDataField] with schemabinding
AS 
SELECT	i.[ItemID],
		m.[MarcDataFieldID],
		m.[Tag],
		m.[Indicator1],
		m.[Indicator2]
FROM	[dbo].[MarcDataField] m INNER JOIN [dbo].[Item] i
			ON m.[ItemID] = i.[ItemID]  


GO
CREATE UNIQUE CLUSTERED INDEX [IX_vwMarcDataField_MarcDataFieldID]
    ON [dbo].[vwMarcDataField]([MarcDataFieldID] ASC);

