
-- Title_IdentifierSelectAuto PROCEDURE
-- Generated 5/1/2012 2:41:41 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Title_Identifier

CREATE PROCEDURE Title_IdentifierSelectAuto

@TitleIdentifierID INT

AS 

SET NOCOUNT ON

SELECT 

	[TitleIdentifierID],
	[TitleID],
	[IdentifierID],
	[IdentifierValue],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[Title_Identifier]

WHERE
	[TitleIdentifierID] = @TitleIdentifierID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Title_IdentifierSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


