
-- IAMarcDataFieldSelectAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for IAMarcDataField

CREATE PROCEDURE IAMarcDataFieldSelectAuto

@MarcDataFieldID INT

AS 

SET NOCOUNT ON

SELECT 

	[MarcDataFieldID],
	[MarcID],
	[Tag],
	[Indicator1],
	[Indicator2],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IAMarcDataField]

WHERE
	[MarcDataFieldID] = @MarcDataFieldID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAMarcDataFieldSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

