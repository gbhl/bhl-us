
-- IdentifierSelectAuto PROCEDURE
-- Generated 5/1/2012 2:41:41 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Identifier

CREATE PROCEDURE IdentifierSelectAuto

@IdentifierID INT

AS 

SET NOCOUNT ON

SELECT 

	[IdentifierID],
	[IdentifierName],
	[IdentifierLabel],
	[Display],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[Identifier]

WHERE
	[IdentifierID] = @IdentifierID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IdentifierSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


