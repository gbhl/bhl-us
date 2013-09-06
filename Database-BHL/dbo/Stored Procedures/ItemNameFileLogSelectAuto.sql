
-- ItemNameFileLogSelectAuto PROCEDURE
-- Generated 11/19/2009 2:21:40 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for ItemNameFileLog

CREATE PROCEDURE ItemNameFileLogSelectAuto

@LogID INT

AS 

SET NOCOUNT ON

SELECT 

	[LogID],
	[ItemID],
	[DoCreate],
	[DoUpload],
	[LastCreateDate],
	[LastUploadDate],
	[CreationDate],
	[LastModifiedDate]

FROM [dbo].[ItemNameFileLog]

WHERE
	[LogID] = @LogID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemNameFileLogSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

