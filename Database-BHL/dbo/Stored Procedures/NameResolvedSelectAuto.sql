
-- NameResolvedSelectAuto PROCEDURE
-- Generated 12/10/2012 3:05:47 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for NameResolved

CREATE PROCEDURE NameResolvedSelectAuto

@NameResolvedID INT

AS 

SET NOCOUNT ON

SELECT 

	[NameResolvedID],
	[ResolvedNameString],
	[CanonicalNameString],
	[IsPreferred],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[NameResolved]

WHERE
	[NameResolvedID] = @NameResolvedID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameResolvedSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

