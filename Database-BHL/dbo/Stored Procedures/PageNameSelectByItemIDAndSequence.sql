


CREATE PROCEDURE [dbo].[PageNameSelectByItemIDAndSequence]

@ItemID INT,
@SequenceOrder INT

AS 

SET NOCOUNT ON

SELECT 

	pn.[PageNameID],
	pn.[PageID],
	pn.[Source],
	pn.[NameFound],
	pn.[NameConfirmed],
	pn.[NameBankID],
	pn.[CreateDate],
	pn.[LastUpdateDate]

FROM [dbo].[PageName] pn
JOIN [dbo].[Page] p ON pn.PageID = p.PageID

WHERE
	p.ItemID = @ItemID
	AND p.SequenceOrder = @SequenceOrder

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageNameSelectByItemIDAndSequence. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


