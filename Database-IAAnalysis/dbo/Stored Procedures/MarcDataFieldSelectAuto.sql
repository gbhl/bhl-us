
-- MarcDataFieldSelectAuto PROCEDURE
-- Generated 11/12/2008 3:12:29 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for MarcDataField

CREATE PROCEDURE MarcDataFieldSelectAuto

@MarcDataFieldID INT

AS 

SET NOCOUNT ON

SELECT 

	[MarcDataFieldID],
	[ItemID],
	[Tag],
	[Indicator1],
	[Indicator2],
	[CreationDate]

FROM [dbo].[MarcDataField]

WHERE
	[MarcDataFieldID] = @MarcDataFieldID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MarcDataFieldSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

