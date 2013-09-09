
-- ItemSelectAuto PROCEDURE
-- Generated 3/24/2009 1:57:09 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Item

CREATE PROCEDURE ItemSelectAuto

@ItemID INT

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

