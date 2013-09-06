
CREATE PROCEDURE [dbo].[TitleAssociationTypeSelectAll]

AS 

SELECT 
	[TitleAssociationTypeID],
	[TitleAssociationName],
	[TitleAssociationLabel],
	[MARCTag],
	[MARCIndicator2]
FROM [dbo].[TitleAssociationType]
ORDER BY TitleAssociationName

