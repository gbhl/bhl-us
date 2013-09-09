

CREATE PROCEDURE [dbo].[IAScandataAltPageTypeDeleteByScandataID]

@ScandataID INT

AS

DELETE FROM [dbo].[IAScandataAltPageType]
WHERE [ScandataID] = @ScandataID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAScandataAltPageTypeDeleteByScandataID. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

