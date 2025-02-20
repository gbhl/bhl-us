﻿
CREATE PROCEDURE [dbo].[IADCMetadataSelectByItemElementNameAndSource]

@ItemID INT,
@DCElementName NVARCHAR(15),
@Source NVARCHAR(10)

AS 

SET NOCOUNT ON

SELECT 

	[DCMetadataID],
	[ItemID],
	[DCElementName],
	[DCElementValue],
	[Source],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IADCMetadata]

WHERE
	[ItemID] = @ItemID
AND	[DCElementName] = @DCElementName
AND	[Source] = @Source
	
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IADCMetadataSelectByItemElementNameAndSource. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END



