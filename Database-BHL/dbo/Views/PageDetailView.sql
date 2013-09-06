
CREATE VIEW [dbo].[PageDetailView]  
 AS 
SELECT  [dbo].[Page].[PageID],  
		[dbo].[Item].[ItemStatusID],  
		[dbo].[Page].[Active],  
		[dbo].[Page].[SequenceOrder],  
		[dbo].[Item].[ItemID],  
		[dbo].[Item].[Volume],
		[dbo].[Title].[TitleID],
		[dbo].[Title].[MARCBibID],
		[dbo].[Title].[ShortTitle],
		[dbo].[Title].[SortTitle],
		count_big(*) AS NumPages
FROM	[dbo].[Title],  
		[dbo].[Item],  
		[dbo].[Page]   
WHERE	[dbo].[Title].[TitleID] = [dbo].[Item].[PrimaryTitleID]  
AND		[dbo].[Item].[ItemID] = [dbo].[Page].[ItemID]  
GROUP BY  
		[dbo].[Page].[PageID],  
		[dbo].[Item].[ItemStatusID],  
		[dbo].[Page].[Active],  
		[dbo].[Page].[SequenceOrder],  
		[dbo].[Item].[ItemID],  
		[dbo].[Item].[Volume],  
		[dbo].[Title].[TitleID],  
		[dbo].[Title].[MARCBibID],  
		[dbo].[Title].[ShortTitle],  
		[dbo].[Title].[SortTitle]
