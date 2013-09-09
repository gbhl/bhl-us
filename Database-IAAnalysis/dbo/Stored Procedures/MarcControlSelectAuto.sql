
-- MarcControlSelectAuto PROCEDURE
-- Generated 11/12/2008 3:12:29 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for MarcControl

CREATE PROCEDURE MarcControlSelectAuto

@MarcControlID INT

AS 

SET NOCOUNT ON

SELECT 

	[MarcControlID],
	[ItemID],
	[Tag],
	[Value],
	[CreationDate]

FROM [dbo].[MarcControl]

WHERE
	[MarcControlID] = @MarcControlID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MarcControlSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

