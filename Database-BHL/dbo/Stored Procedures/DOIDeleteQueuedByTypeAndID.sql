CREATE PROCEDURE [dbo].[DOIDeleteQueuedByTypeAndID]

@DOIEntityTypeID int,
@EntityID int

AS 

BEGIN

SET NOCOUNT ON

DECLARE @DOIStatusQueuedID int
SELECT @DOIStatusQueuedID = DOIStatusID FROM dbo.DOIStatus WHERE DOIStatusName = 'Queued'

DELETE 
FROM	dbo.DOI
WHERE	DOIEntityTypeID = @DOIEntityTypeID
AND		EntityID = @EntityID
AND		DOIStatusID = @DOIStatusQueuedID

END

GO
