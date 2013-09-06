
-- MarcControlSelectAuto PROCEDURE
-- Generated 4/15/2009 3:34:26 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for MarcControl

CREATE PROCEDURE MarcControlSelectAuto

@MarcControlID INT

AS 

SET NOCOUNT ON

SELECT 

	[MarcControlID],
	[MarcID],
	[Tag],
	[Value],
	[CreationDate],
	[LastModifiedDate]

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

