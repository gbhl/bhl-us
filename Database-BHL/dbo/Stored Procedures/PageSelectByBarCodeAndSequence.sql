
CREATE PROCEDURE [dbo].[PageSelectByBarCodeAndSequence]

@BarCode nvarchar(40),
@SequenceOrder int

AS 

SET NOCOUNT ON

SELECT 

	p.[PageID],
	p.[ItemID],
	p.[FileNamePrefix],
	p.[SequenceOrder],
	p.[PageDescription],
	p.[Illustration],
	p.[Note],
	p.[FileSize_Temp],
	p.[FileExtension],
	p.[CreationDate],
	p.[LastModifiedDate],
	p.[CreationUserID],
	p.[LastModifiedUserID],
	p.[Active],
	p.[Year],
	p.[Series],
	p.[Volume],
	p.[Issue],
	p.[ExternalURL],
	p.[AltExternalURL],
	p.[IssuePrefix]

FROM [dbo].[Page] p
JOIN [dbo].[Item] i on p.ItemID = i.ItemID

WHERE
	i.BarCode = @BarCode AND
	p.SequenceOrder = @SequenceOrder

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSelectByBarCodeAndSequence. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


