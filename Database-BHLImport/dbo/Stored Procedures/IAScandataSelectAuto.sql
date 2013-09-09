
-- IAScandataSelectAuto PROCEDURE
-- Generated 11/24/2010 3:52:48 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for IAScandata

CREATE PROCEDURE IAScandataSelectAuto

@ScandataID INT

AS 

SET NOCOUNT ON

SELECT 

	[ScandataID],
	[ItemID],
	[Sequence],
	[PageType],
	[PageNumber],
	[Year],
	[Volume],
	[Issue],
	[IssuePrefix],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IAScandata]

WHERE
	[ScandataID] = @ScandataID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAScandataSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

