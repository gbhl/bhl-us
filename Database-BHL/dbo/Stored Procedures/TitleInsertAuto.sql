
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[TitleInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[TitleInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Insert Procedure for dbo.Title
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:32:28 AM

CREATE PROCEDURE dbo.TitleInsertAuto

@TitleID INT OUTPUT,
@MARCBibID NVARCHAR(50),
@MARCLeader NVARCHAR(24) = null,
@TropicosTitleID INT = null,
@RedirectTitleID INT = null,
@FullTitle NVARCHAR(2000),
@ShortTitle NVARCHAR(255) = null,
@UniformTitle NVARCHAR(255) = null,
@SortTitle NVARCHAR(60) = null,
@CallNumber NVARCHAR(100) = null,
@PublicationDetails NVARCHAR(255) = null,
@StartYear SMALLINT = null,
@EndYear SMALLINT = null,
@Datafield_260_a NVARCHAR(150) = null,
@Datafield_260_b NVARCHAR(255) = null,
@Datafield_260_c NVARCHAR(100) = null,
@LanguageCode NVARCHAR(10) = null,
@TitleDescription NTEXT = null,
@TL2Author NVARCHAR(100) = null,
@PublishReady BIT,
@RareBooks BIT,
@Note NVARCHAR(255) = null,
@CreationUserID INT = null,
@LastModifiedUserID INT = null,
@OriginalCatalogingSource NVARCHAR(100) = null,
@EditionStatement NVARCHAR(450) = null,
@CurrentPublicationFrequency NVARCHAR(100) = null,
@PartNumber NVARCHAR(255) = null,
@PartName NVARCHAR(255) = null,
@BibliographicLevelID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Title]
( 	[MARCBibID],
	[MARCLeader],
	[TropicosTitleID],
	[RedirectTitleID],
	[FullTitle],
	[ShortTitle],
	[UniformTitle],
	[SortTitle],
	[CallNumber],
	[PublicationDetails],
	[StartYear],
	[EndYear],
	[Datafield_260_a],
	[Datafield_260_b],
	[Datafield_260_c],
	[LanguageCode],
	[TitleDescription],
	[TL2Author],
	[PublishReady],
	[RareBooks],
	[Note],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID],
	[OriginalCatalogingSource],
	[EditionStatement],
	[CurrentPublicationFrequency],
	[PartNumber],
	[PartName],
	[BibliographicLevelID] )
VALUES
( 	@MARCBibID,
	@MARCLeader,
	@TropicosTitleID,
	@RedirectTitleID,
	@FullTitle,
	@ShortTitle,
	@UniformTitle,
	@SortTitle,
	@CallNumber,
	@PublicationDetails,
	@StartYear,
	@EndYear,
	@Datafield_260_a,
	@Datafield_260_b,
	@Datafield_260_c,
	@LanguageCode,
	@TitleDescription,
	@TL2Author,
	@PublishReady,
	@RareBooks,
	@Note,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID,
	@OriginalCatalogingSource,
	@EditionStatement,
	@CurrentPublicationFrequency,
	@PartNumber,
	@PartName,
	@BibliographicLevelID )

SET @TitleID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.TitleInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[TitleID],
		[MARCBibID],
		[MARCLeader],
		[TropicosTitleID],
		[RedirectTitleID],
		[FullTitle],
		[ShortTitle],
		[UniformTitle],
		[SortTitle],
		[CallNumber],
		[PublicationDetails],
		[StartYear],
		[EndYear],
		[Datafield_260_a],
		[Datafield_260_b],
		[Datafield_260_c],
		[LanguageCode],
		[TitleDescription],
		[TL2Author],
		[PublishReady],
		[RareBooks],
		[Note],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID],
		[OriginalCatalogingSource],
		[EditionStatement],
		[CurrentPublicationFrequency],
		[PartNumber],
		[PartName],
		[BibliographicLevelID]	
	FROM [dbo].[Title]
	WHERE
		[TitleID] = @TitleID
	
	RETURN -- insert successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

