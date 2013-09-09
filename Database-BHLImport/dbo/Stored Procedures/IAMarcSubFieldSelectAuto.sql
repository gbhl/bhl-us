
-- IAMarcSubFieldSelectAuto PROCEDURE
-- Generated 7/8/2013 2:53:08 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for IAMarcSubField

CREATE PROCEDURE IAMarcSubFieldSelectAuto

@MarcSubFieldID INT

AS 

SET NOCOUNT ON

SELECT 

	[MarcSubFieldID],
	[MarcDataFieldID],
	[Code],
	[Value],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IAMarcSubField]

WHERE
	[MarcSubFieldID] = @MarcSubFieldID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAMarcSubFieldSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

