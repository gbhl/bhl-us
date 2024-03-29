CREATE PROCEDURE [import].[ImportFileStatusSelectAuto]

@ImportFileStatusID INT

AS 

SET NOCOUNT ON

SELECT 

	[ImportFileStatusID],
	[StatusName],
	[StatusDescription],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [import].[ImportFileStatus]

WHERE
	[ImportFileStatusID] = @ImportFileStatusID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ImportFileStatusSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


GO
