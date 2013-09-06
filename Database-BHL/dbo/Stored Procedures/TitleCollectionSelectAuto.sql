
-- TitleCollectionSelectAuto PROCEDURE
-- Generated 7/30/2010 2:09:29 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for TitleCollection

CREATE PROCEDURE TitleCollectionSelectAuto

@TitleCollectionID INT

AS 

SET NOCOUNT ON

SELECT 

	[TitleCollectionID],
	[TitleID],
	[CollectionID],
	[CreationDate]

FROM [dbo].[TitleCollection]

WHERE
	[TitleCollectionID] = @TitleCollectionID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleCollectionSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

