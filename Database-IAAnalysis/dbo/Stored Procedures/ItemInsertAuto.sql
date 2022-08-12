CREATE PROCEDURE dbo.ItemInsertAuto

@ItemID INT OUTPUT,
@Identifier NVARCHAR(200),
@MARCLeader NVARCHAR(200),
@Sponsor NVARCHAR(50),
@Contributor NVARCHAR(100),
@ScanningCenter NVARCHAR(50),
@CollectionLibrary NVARCHAR(20),
@CallNumber NVARCHAR(50),
@ImageCount INT = null,
@CurationState NVARCHAR(50),
@PossibleCopyrightStatus NVARCHAR(50),
@Volume NVARCHAR(200),
@ScanDate NVARCHAR(50),
@AddedDate DATETIME = null,
@PublicDate DATETIME = null,
@UpdateDate DATETIME = null,
@SponsorDate NVARCHAR(50) = null,
@ItemStatusID INT,
@MetaGetStatus NVARCHAR(30),
@MarcGetStatus NVARCHAR(30)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Item]
( 	[Identifier],
	[MARCLeader],
	[Sponsor],
	[Contributor],
	[ScanningCenter],
	[CollectionLibrary],
	[CallNumber],
	[ImageCount],
	[CurationState],
	[PossibleCopyrightStatus],
	[Volume],
	[ScanDate],
	[AddedDate],
	[PublicDate],
	[UpdateDate],
	[SponsorDate],
	[ItemStatusID],
	[MetaGetStatus],
	[MarcGetStatus],
	[CreationDate] )
VALUES
( 	@Identifier,
	@MARCLeader,
	@Sponsor,
	@Contributor,
	@ScanningCenter,
	@CollectionLibrary,
	@CallNumber,
	@ImageCount,
	@CurationState,
	@PossibleCopyrightStatus,
	@Volume,
	@ScanDate,
	@AddedDate,
	@PublicDate,
	@UpdateDate,
	@SponsorDate,
	@ItemStatusID,
	@MetaGetStatus,
	@MarcGetStatus,
	getdate() )

SET @ItemID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
		[Volume],
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
		[ItemID] = @ItemID
	
	RETURN -- insert successful
END
GO
