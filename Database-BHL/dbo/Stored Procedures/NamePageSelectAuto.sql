
-- NamePageSelectAuto PROCEDURE
-- Generated 10/29/2012 3:17:36 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for NamePage

CREATE PROCEDURE NamePageSelectAuto

@NamePageID INT

AS 

SET NOCOUNT ON

SELECT 

	[NamePageID],
	[NameID],
	[PageID],
	[NameSourceID],
	[IsFirstOccurrence],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[NamePage]

WHERE
	[NamePageID] = @NamePageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NamePageSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

