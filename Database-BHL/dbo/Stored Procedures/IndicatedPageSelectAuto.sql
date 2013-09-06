
-- IndicatedPageSelectAuto PROCEDURE
-- Generated 5/17/2010 4:03:17 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for IndicatedPage

CREATE PROCEDURE IndicatedPageSelectAuto

@PageID INT,
@Sequence SMALLINT

AS 

SET NOCOUNT ON

SELECT 

	[PageID],
	[Sequence],
	[PagePrefix],
	[PageNumber],
	[Implied],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[IndicatedPage]

WHERE
	[PageID] = @PageID AND
	[Sequence] = @Sequence

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IndicatedPageSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

