
-- MarcDataFieldSelectAuto PROCEDURE
-- Generated 4/15/2009 3:34:26 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for MarcDataField

CREATE PROCEDURE MarcDataFieldSelectAuto

@MarcDataFieldID INT

AS 

SET NOCOUNT ON

SELECT 

	[MarcDataFieldID],
	[MarcID],
	[Tag],
	[Indicator1],
	[Indicator2],
	[CreationDate],
	[LastModifiedDate]

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

