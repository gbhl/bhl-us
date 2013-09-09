
-- IAPageSelectAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for IAPage

CREATE PROCEDURE IAPageSelectAuto

@PageID INT

AS 

SET NOCOUNT ON

SELECT 

	[PageID],
	[ItemID],
	[LocalFileName],
	[Sequence],
	[ExternalUrl],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IAPage]

WHERE
	[PageID] = @PageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAPageSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

