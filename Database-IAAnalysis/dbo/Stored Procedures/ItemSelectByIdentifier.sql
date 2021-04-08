CREATE PROCEDURE [dbo].[ItemSelectByIdentifier]

@Identifier NVARCHAR(200)

AS
SET NOCOUNT ON

SELECT 

	[ItemID],
	[Identifier],
	[MARCLeader],
	[Sponsor],
	[Contributor],
	[ScanningCenter],
	[CollectionLibrary],
	[CallNumber],
	[ImageCount],
	[CurationState],
	[PossibleCopyrightStatus],
	[ScanDate],
	[AddedDate],
	[PublicDate],
	[UpdateDate],
	[SponsorDate],
	[ItemStatusID],
	[MetaGetStatus],
	[MarcGetStatus],
	[CreationDate]

FROM [dbo].[Item]

WHERE
	[Identifier] = @Identifier

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemSelectByIdentifier. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
