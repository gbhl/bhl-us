CREATE PROCEDURE [dbo].[IAScandataAltPageNumberSelectByScandataAndSequence]

@ScandataID INT,
@Sequence INT

AS 

SET NOCOUNT ON

SELECT	[ScandataAltPageNumberID],
		[ScandataID],
		[Sequence],
		[PagePrefix],
		[PageNumber],
		[Implied],
		[CreatedDate],
		[LastModifiedDate]
FROM	[dbo].[IAScandataAltPageNumber]
WHERE	[ScandataID] = @ScandataID
AND		[Sequence] = @Sequence

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAScandataAltPageNumberSelectByScandataAndSequence. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

