
-- NameSelectAuto PROCEDURE
-- Generated 12/10/2012 3:05:47 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Name

CREATE PROCEDURE NameSelectAuto

@NameID INT

AS 

SET NOCOUNT ON

SELECT 

	[NameID],
	[NameSourceID],
	[NameString],
	[IsActive],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID],
	[NameResolvedID]

FROM [dbo].[Name]

WHERE
	[NameID] = @NameID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

