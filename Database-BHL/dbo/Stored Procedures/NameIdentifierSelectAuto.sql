
-- NameIdentifierSelectAuto PROCEDURE
-- Generated 9/18/2012 11:59:15 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for NameIdentifier

CREATE PROCEDURE NameIdentifierSelectAuto

@NameIdentifierID INT

AS 

SET NOCOUNT ON

SELECT 

	[NameIdentifierID],
	[NameResolvedID],
	[IdentifierID],
	[IdentifierValue],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[NameIdentifier]

WHERE
	[NameIdentifierID] = @NameIdentifierID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameIdentifierSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

