
-- MarcSubFieldSelectAuto PROCEDURE
-- Generated 4/15/2009 3:34:26 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for MarcSubField

CREATE PROCEDURE MarcSubFieldSelectAuto

@MarcSubFieldID INT

AS 

SET NOCOUNT ON

SELECT 

	[MarcSubFieldID],
	[MarcDataFieldID],
	[Code],
	[Value],
	[CreationDate],
	[LastModifiedDate]

FROM [dbo].[MarcSubField]

WHERE
	[MarcSubFieldID] = @MarcSubFieldID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MarcSubFieldSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

