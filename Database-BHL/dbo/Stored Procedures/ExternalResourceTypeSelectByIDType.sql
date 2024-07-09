CREATE PROCEDURE dbo.ExternalResourceTypeSelectByIDType

@IDTypeName nvarchar(50)

AS

BEGIN

SET NOCOUNT ON

SELECT	t.ExternalResourceTypeID,
		t.ExternalResourceTypeName,
		t.ExternalResourceTypeLabel,
		t.CreationDate,
		t.LastModifiedDate,
		t.CreationUserID,
		t.LastModifiedUserID
FROM	ExternalResourceType t
		INNER JOIN dbo.ExternalResourceTypeIDType ert ON t.ExternalResourceTypeID = ert.ExternalResourceTypeID
		INNER JOIN dbo.IDType idt ON ert.IDTypeID = idt.IDTypeID
WHERE	idt.IDTypeName = @IDTypeName
ORDER BY
		ExternalResourceTypeLabel

END

GO
