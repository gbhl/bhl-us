
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[TitleSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[TitleSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Select Procedure for dbo.Title
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:32:28 AM

CREATE PROCEDURE [dbo].[TitleSelectAuto]

@TitleID INT

AS 

SET NOCOUNT ON

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
FROM	
	[dbo].[Title]
WHERE	
	[TitleID] = @TitleID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.TitleSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

