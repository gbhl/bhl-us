
-- ItemStatusSelectAuto PROCEDURE
-- Generated 1/18/2008 11:10:47 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for ItemStatus

CREATE PROCEDURE ItemStatusSelectAuto

@ItemStatusID INT

AS 

SET NOCOUNT ON

SELECT 

	[ItemStatusID],
	[ItemStatusName]

FROM [dbo].[ItemStatus]

WHERE
	[ItemStatusID] = @ItemStatusID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemStatusSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

