


CREATE PROCEDURE [dbo].[PageNameSelectByPageIDAndNameFound]

@PageID INT,
@NameFound NVARCHAR(100)

AS 

SET NOCOUNT ON

SELECT 

	[PageNameID],
	[PageID],
	[Source],
	[NameFound],
	[NameConfirmed],
	[NameBankID],
	[Active],
	[CreateDate],
	[LastUpdateDate]

FROM [dbo].[PageName]

WHERE
	[PageID] = @PageID AND
	[NameFound] = @NameFound

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageNameSelectByPageIDAndNameFound. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


