
-- TitleItemSelectAuto PROCEDURE
-- Generated 2/4/2011 3:25:21 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for TitleItem

CREATE PROCEDURE TitleItemSelectAuto

@TitleItemID INT

AS 

SET NOCOUNT ON

SELECT 

	[TitleItemID],
	[TitleID],
	[ItemID],
	[ItemSequence],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[TitleItem]

WHERE
	[TitleItemID] = @TitleItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleItemSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

