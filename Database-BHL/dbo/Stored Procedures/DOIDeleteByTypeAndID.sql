CREATE PROCEDURE [dbo].[DOIDeleteByTypeAndID]

@DOIEntityTypeID int,
@EntityID int

AS 

BEGIN

DELETE 
FROM	dbo.DOI
WHERE	DOIEntityTypeID = @DOIEntityTypeID
AND		EntityID = @EntityID

END

GO
