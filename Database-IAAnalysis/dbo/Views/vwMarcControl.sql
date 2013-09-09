CREATE VIEW [dbo].[vwMarcControl] with schemabinding
AS
SELECT	i.ItemID, i.MARCLeader, c.MarcControlID, c.Tag, c.Value 
FROM	dbo.Item i INNER JOIN dbo.MarcControl c
			ON i.ItemID = c.ItemID

GO
CREATE UNIQUE CLUSTERED INDEX [IX_vwMarcControl_]
    ON [dbo].[vwMarcControl]([MarcControlID] ASC);

