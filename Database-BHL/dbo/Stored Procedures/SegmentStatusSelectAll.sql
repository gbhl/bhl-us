SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SegmentStatusSelectAll]

AS

BEGIN

SELECT	ItemStatusID,
		ItemStatusName,
		ItemStatusDescription,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	dbo.ItemStatus
ORDER BY
		ItemStatusName

END



GO
