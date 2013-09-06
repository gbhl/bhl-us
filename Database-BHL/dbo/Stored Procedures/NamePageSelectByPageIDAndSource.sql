CREATE PROCEDURE [dbo].[NamePageSelectByPageIDAndSource]

@PageID int,
@SourceName nvarchar(50)

AS 

SET NOCOUNT ON

--SET @NameSourceID = NULL
--SELECT @NameSourceID = NameSourceID FROM dbo.NameSource WHERE SourceName = @SourceName

SELECT	np.NamePageID,
		np.NameID,
		np.PageID,
		n.NameString
FROM	dbo.NamePage np 
		INNER JOIN dbo.Name n ON np.NameID = n.NameID
		INNER JOIN dbo.NameSource ns ON np.NameSourceID = ns.NameSourceID
WHERE	np.PageID = @PageID
AND		ns.SourceName = @SourceName

