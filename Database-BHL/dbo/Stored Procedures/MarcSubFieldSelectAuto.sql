CREATE PROCEDURE [dbo].[MarcSubFieldSelectAuto]

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
FROM	
	[dbo].[MarcSubField]
WHERE	
	[MarcSubFieldID] = @MarcSubFieldID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.MarcSubFieldSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
