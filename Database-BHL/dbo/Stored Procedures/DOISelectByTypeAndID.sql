CREATE PROCEDURE [dbo].[DOISelectByTypeAndID]

@DOIEntityTypeName nvarchar(50),
@EntityID int

AS

BEGIN

SET NOCOUNT ON

SELECT	d.DOIID,
		d.DOIEntityTypeID,
		t.DOIEntityTypeName,
		d.EntityID,
		d.DOIStatusID,
		d.DOIBatchID,
		d.DOIName,
		d.StatusMessage,
		d.StatusDate,
		u.FirstName + ' ' + u.LastName AS CreationUserName
FROM	dbo.DOI d
		INNER JOIN dbo.DOIEntityType t ON d.DOIEntityTypeID = t.DOIEntityTypeID
		INNER JOIN dbo.AspNetUsers u ON d.CreationUserID = u.Id
WHERE	t.DOIEntityTypeName = @DOIEntityTypeName
AND		d.EntityID = @EntityID

END

GO

