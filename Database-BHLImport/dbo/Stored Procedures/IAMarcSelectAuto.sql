
-- IAMarcSelectAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for IAMarc

CREATE PROCEDURE IAMarcSelectAuto

@MarcID INT

AS 

SET NOCOUNT ON

SELECT 

	[MarcID],
	[ItemID],
	[Leader],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IAMarc]

WHERE
	[MarcID] = @MarcID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAMarcSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

