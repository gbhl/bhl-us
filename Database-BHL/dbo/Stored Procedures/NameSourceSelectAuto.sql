
-- NameSourceSelectAuto PROCEDURE
-- Generated 9/18/2012 11:59:15 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for NameSource

CREATE PROCEDURE NameSourceSelectAuto

@NameSourceID INT

AS 

SET NOCOUNT ON

SELECT 

	[NameSourceID],
	[SourceName],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[NameSource]

WHERE
	[NameSourceID] = @NameSourceID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameSourceSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

