
-- TitleItemDeleteAuto PROCEDURE
-- Generated 2/4/2011 3:25:21 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for TitleItem

CREATE PROCEDURE TitleItemDeleteAuto

@TitleItemID INT

AS 

DELETE FROM [dbo].[TitleItem]

WHERE

	[TitleItemID] = @TitleItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleItemDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

