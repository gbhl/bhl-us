
-- ItemUpdateAuto PROCEDURE
-- Generated 3/24/2009 1:57:09 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Item

CREATE PROCEDURE ItemUpdateAuto

@ItemID INT,
@Identifier NVARCHAR(50),
@MARCLeader NVARCHAR(200),
@Sponsor NVARCHAR(50),
@Contributor NVARCHAR(100),
@ScanningCenter NVARCHAR(50),
@CollectionLibrary NVARCHAR(20),
@CallNumber NVARCHAR(50),
@ImageCount INT,
@CurationState NVARCHAR(50),
@PossibleCopyrightStatus NVARCHAR(50),
@Volume NVARCHAR(200),
@ScanDate NVARCHAR(50),
@AddedDate DATETIME,
@PublicDate DATETIME,
@UpdateDate DATETIME,
@SponsorDate NVARCHAR(50),
@ItemStatusID INT,
@MetaGetStatus NVARCHAR(30),
@MarcGetStatus NVARCHAR(30)

AS 

SET NOCOUNT ON

UPDATE [dbo].[Item]

SET

	[Identifier] = @Identifier,
	[MARCLeader] = @MARCLeader,
	[Sponsor] = @Sponsor,
	[Contributor] = @Contributor,
	[ScanningCenter] = @ScanningCenter,
	[CollectionLibrary] = @CollectionLibrary,
	[CallNumber] = @CallNumber,
	[ImageCount] = @ImageCount,
	[CurationState] = @CurationState,
	[PossibleCopyrightStatus] = @PossibleCopyrightStatus,
	[Volume] = @Volume,
	[ScanDate] = @ScanDate,
	[AddedDate] = @AddedDate,
	[PublicDate] = @PublicDate,
	[UpdateDate] = @UpdateDate,
	[SponsorDate] = @SponsorDate,
	[ItemStatusID] = @ItemStatusID,
	[MetaGetStatus] = @MetaGetStatus,
	[MarcGetStatus] = @MarcGetStatus

WHERE
	[ItemID] = @ItemID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

