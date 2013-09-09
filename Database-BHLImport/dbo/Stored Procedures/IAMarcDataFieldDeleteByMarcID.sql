
CREATE PROCEDURE [dbo].[IAMarcDataFieldDeleteByMarcID]

@MarcID INT

AS
DELETE FROM [dbo].[IAMarcSubField]

WHERE
	[MarcDataFieldID] IN (SELECT [MarcDataFieldID] FROM [dbo].[IAMarcDataField]
							WHERE [MarcID] = @MarcID)
							

DELETE FROM [dbo].[IAMarcDataField]

WHERE

	[MarcID] = @MarcID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAMarcDataFieldDeleteByMarcID. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END
