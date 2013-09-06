
-- AnnotatedItemSelectAuto PROCEDURE
-- Generated 7/14/2010 1:25:28 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for AnnotatedItem

CREATE PROCEDURE [annotation].AnnotatedItemSelectAuto

@AnnotatedItemID INT

AS 

SET NOCOUNT ON

SELECT 

	[AnnotatedItemID],
	[AnnotatedTitleID],
	[ItemID],
	[ExternalIdentifier],
	[Volume],
	[CreationDate],
	[LastModifiedDate]

FROM [annotation].[AnnotatedItem]

WHERE
	[AnnotatedItemID] = @AnnotatedItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedItemSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

