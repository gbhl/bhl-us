﻿
-- IAMarcControlSelectAuto PROCEDURE
-- Generated 7/8/2013 2:53:08 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for IAMarcControl

CREATE PROCEDURE IAMarcControlSelectAuto

@MarcControlID INT

AS 

SET NOCOUNT ON

SELECT 

	[MarcControlID],
	[MarcID],
	[Tag],
	[Value],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IAMarcControl]

WHERE
	[MarcControlID] = @MarcControlID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAMarcControlSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

