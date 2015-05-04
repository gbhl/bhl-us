CREATE PROCEDURE [reqlog].[RequestTypeSelectByApplication]

@ApplicationID INT

AS 

SET NOCOUNT ON

SELECT 	[RequestTypeID],
		[RequestTypeName]
FROM	[reqlog].[RequestType]
WHERE	[ApplicationID] = @ApplicationID
ORDER BY RequestTypeName
