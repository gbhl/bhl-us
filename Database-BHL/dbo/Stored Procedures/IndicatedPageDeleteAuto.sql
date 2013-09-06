
-- IndicatedPageDeleteAuto PROCEDURE
-- Generated 5/17/2010 4:03:17 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for IndicatedPage

CREATE PROCEDURE IndicatedPageDeleteAuto

@PageID INT,
@Sequence SMALLINT

AS 

DELETE FROM [dbo].[IndicatedPage]

WHERE

	[PageID] = @PageID AND
	[Sequence] = @Sequence

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IndicatedPageDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

