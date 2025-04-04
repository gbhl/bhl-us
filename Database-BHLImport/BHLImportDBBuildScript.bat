@ECHO OFF

IF "%1" == "" GOTO MISSINGPARAM
IF "%2" == "" GOTO MISSINGPARAM
IF "%3" == "" GOTO MISSINGPARAM

@ECHO ON

IF "%3" == "structure" GOTO CREATEDB
IF "%3" == "all" GOTO CREATEDB

GOTO DATALOAD

:CREATEDB

REM --------------------------------------
REM  Create Database
REM --------------------------------------
sqlcmd -E -S %1 -Q "CREATE DATABASE [%2]"
sqlcmd -E -S %1 -Q "ALTER DATABASE [%2] MODIFY FILE ( NAME = N'%2', SIZE = 512000KB , FILEGROWTH = 10%)"

REM --------------------------------------
REM  Build Tables
REM --------------------------------------
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\BSItemStatus.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\BSSegmentStatus.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\DOIHarvestLog.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAAnalysisHarvestLog.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAFileFormat.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAItemStatus.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAScanCenterInstitution.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAScandataPageType.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IASet.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\ImportSource.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\ImportLog.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\ImportError.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\MarcCountryCode.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\ImportSourceItemSource.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\ImportStatus.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIFormat.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIRecordLanguage.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIRecordStatus.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIRecordRelatedTitleTypeAssociation.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIRecordRelatedTitleTypeVariant.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIPublishError.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIPublishLog.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\PageFlickrTag.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\PageFlickrNote.sql"

sqlcmd -E -S %1 -d %2 -i "dbo\Tables\BSItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\BSSegment.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\BSSegmentPage.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\BSSegmentAuthor.sql"

sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IABHLCreator.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IABHLCreatorIdentifier.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IADCMetadata.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAFile.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAItemError.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAItemIdentifier.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAItemSet.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAMarc.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAMarcControl.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAMarcDataField.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAMarcSubField.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAPage.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAScandata.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAScandataAltPageNumber.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IAScandataAltPageType.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IASegment.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IASegmentAuthor.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IASegmentPage.sql"

sqlcmd -E -S %1 -d %2 -i "dbo\Tables\Creator.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\Title.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\TitleAssociation.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\TitleAssociation_TitleIdentifier.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\TitleLanguage.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\TitleTag.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\TitleVariant.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\TitleNote.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\Title_Creator.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\Title_TitleIdentifier.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\Item.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\ItemLanguage.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\ItemIdentifier.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\ItemKeyword.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\ItemCreator.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\ItemCreatorIdentifier.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\Page.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\PageName.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\Page_PageType.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\IndicatedPage.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\Segment.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\SegmentPage.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\SegmentIdentifier.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\SegmentAuthor.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\SegmentAuthorIdentifier.sql"

sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIRepository.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIRepositoryFormat.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAISet.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIHarvestSet.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIHarvestLog.sql"

sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIRecord.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIRecordCreator.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIRecordCreatorIdentifier.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIRecordDCType.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIRecordRelatedTitle.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIRecordRight.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\OAIRecordSubject.sql"

REM --------------------------------------
REM  Add Synonyms
REM --------------------------------------
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLAspNetUsers.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLAuthor.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLAuthorIdentifier.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLAuthorName.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLAuthorRole.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLAuthorType.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLBibliographicLevel.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLBook.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLCollection.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLConfiguration.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLDOI.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLDOIDelete.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLDOIEntityType.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLDOIInsert.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLDOIPrefix.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLDOIStatus.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLDOIUpdate.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLfnAddAuthorNameSpaces.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLfnConvertToTitleCase.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLfnGetISSNID.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLfnGetISSNName.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLfnGetISSNValue.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLfnGetLCCNValue.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLfnRemoveTrailingPunctuation.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLIdentifier.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLIndicatedPage.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLInstitution.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLInstitutionRole.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLItemAuthor.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLItemIdentifier.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLItemInstitution.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLItemKeyword.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLItemCollection.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLItemLanguage.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLItemPage.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLItemRelationship.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLKeyword.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLLanguage.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLMaterialType.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLNoteType.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLPage.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLPageName.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLPagePageType.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLSegment.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLSegmentGenre.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLTitle.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLTitleAssociation.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLTitleAssociationType.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLTitleAssociation_TitleIdentifier.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLTitleAuthor.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLTitleCollection.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLTitleItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLTitleKeyword.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLTitleLanguage.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLTitleNote.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLTitleVariant.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLTitleVariantType.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLTitle_Identifier.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLVault.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLvwAuthorName.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\IAAnalysisCollection.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\IAAnalysisItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\IAAnalysisItemCollection.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\IAAnalysisrptCombined.sql"

REM --------------------------------------
REM  Build Functions
REM --------------------------------------
sqlcmd -E -S %1 -d %2 -i "dbo\Functions\fnBSIsDate.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Functions\fnFilterString.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Functions\fnGetDatesFromString.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Functions\fnGetSortString.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Functions\fnRemoveNonAlphaNumericCharacters.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Functions\fnRemoveNonNumericCharacters.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Functions\fnReverseAuthorName.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Functions\vwSplitLanguage.sql"

REM --------------------------------------
REM  Build Views
REM --------------------------------------
sqlcmd -E -S %1 -d %2 -i "dbo\Views\vwIAMarcControl.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Views\vwIAMarcDataField.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Views\vwIAPage.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Views\vwOAIHarvestSet.sql"

:DATALOAD

IF "%3" == "data" GOTO INSERTDATA
IF "%3" == "all" GOTO INSERTDATA

GOTO RESUMESTRUCTURE

:INSERTDATA

REM --------------------------------------
REM  Insert Data
REM --------------------------------------
sqlcmd -E -S %1 -d %2 -i "data\dbo.BSItemStatus.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.BSSegmentStatus.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.IAFileFormat.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.IAItemStatus.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.IAScanCenterInstitution.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.IAScanDataPageType.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.IASet.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.ImportSource.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.MarcCountryCode.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.ImportSourceItemSource.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.ImportStatus.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.OAIFormat.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.OAIRecordLanguage.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.OAIRecordRelatedTitleTypeAssociation.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.OAIRecordRelatedTitleTypeVariant.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.OAIRepository.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.OAIRepositoryFormat.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.OAISet.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.OAIRecordStatus.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.OAIHarvestSet.Table.sql"

:RESUMESTRUCTURE

IF "%3" == "structure" GOTO CREATESP
IF "%3" == "all" GOTO CREATESP

GOTO DONE

:CREATESP

REM --------------------------------------
REM  Build Stored Procedures
REM --------------------------------------
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BHLItemCollectionInsertSeedCatalogItems.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSItemAvailabilityCheck.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSItemDeleteAllSegments.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSItemDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSItemInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSItemSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSItemSelectByStatus.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSItemSetStatus.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSItemStatusDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSItemStatusInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSItemStatusSelectAll.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSItemStatusSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSItemStatusUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSItemUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSSegmentDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSSegmentInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSSegmentPublishToProduction.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSSegmentResolveAuthors.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSSegmentSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSSegmentSelectByItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BSSegmentUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\BuildSynonyms.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\CreatorDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\CreatorInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\CreatorSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\CreatorSelectNewByCreatorNameAndSource.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\CreatorUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IABHLCreatorDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IABHLCreatorDeleteByItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IABHLCreatorInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IABHLCreatorSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IABHLCreatorUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IABHLCreatorIdentifierDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IABHLCreatorIdentifierInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IABHLCreatorIdentifierSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IABHLCreatorIdentifierUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IADCMetadataDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IADCMetadataDeleteForItemAndSource.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IADCMetadataInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IADCMetadataSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IADCMetadataSelectByItemElementNameAndSource.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IADCMetadataUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAFileDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAFileInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAFileSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAFileSelectByItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAFileSelectByItemAndFormat.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAFileSelectByItemAndRemoteFileName.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAFileSelectForDownload.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAFileUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemErrorDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemErrorInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemErrorSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemErrorSelectRecent.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemErrorUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemIdentifierDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemIdentifierDeleteByItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemIdentifierInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemIdentifierSelect.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemIdentifierSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemIdentifierUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemInsertFromIAAnalysis.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemPublishToImportTables.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemResetForDownload.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemSelectByIAIdentifier.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemSelectByStatus.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemSelectForPublishToImportTables.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemSelectForXMLDownload.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemSelectOKToPublish.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemSelectPendingApproval.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemSetDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemSetDeleteByItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemSetInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemSetSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemSetUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemStatusSelectAll.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAItemUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcControlDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcControlDeleteByMarcID.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcControlInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcControlSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcControlUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcDataFieldDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcDataFieldDeleteByMarcID.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcDataFieldInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcDataFieldSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcDataFieldUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcSelectByItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcSubFieldDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcSubFieldInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcSubFieldSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcSubFieldUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAMarcUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAPageDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAPageDeleteByItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAPageInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAPageSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAPageUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataAltPageNumberDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataAltPageNumberDeleteByScandataID.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataAltPageNumberInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataAltPageNumberSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataAltPageNumberSelectByScandataAndSequence.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataAltPageNumberUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataAltPageTypeDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataAltPageTypeDeleteByScandataID.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataAltPageTypeInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataAltPageTypeSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataAltPageTypeSelectByScandataAndPageType.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataAltPageTypeUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataDeleteByItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataSelectByItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataSelectByItemAndSequence.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IAScandataUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASegmentDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASegmentDeleteByItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASegmentInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASegmentSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASegmentSelectByItemAndSequence.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASegmentUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASegmentAuthorDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASegmentAuthorInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASegmentAuthorSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASegmentAuthorSelectBySegmentAndSequence.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASegmentAuthorUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASegmentPageDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASegmentPageInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASegmentPageSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASegmentPageSelectBySegmentAndSequence.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASegmentPageUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASetDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASetInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASetSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASetSelectBySetSpecification.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASetSelectForDownload.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IASetUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ImportErrorSelectRecent.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ImportLogSelectRecent.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IndicatedPageDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IndicatedPageInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IndicatedPageSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IndicatedPageSelectNewByKeyValuesAndSource.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\IndicatedPageUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemPublishToProductionIA.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemSelectNewByBarCodeAndSource.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemUpdateThumbnailPageID.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIHarvestLogDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIHarvestLogInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIHarvestLogSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIHarvestLogSelectLastDateForHarvestSet.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIHarvestLogUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIHarvestSetSelectAll.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordCreatorDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordCreatorInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordCreatorSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordCreatorUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordCreatorIdentifierDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordCreatorIdentifierInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordCreatorIdentifierSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordCreatorIdentifierUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordDCTypeDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordDCTypeInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordDCTypeSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordDCTypeUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordDeleteForHarvestLogID.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordDeleteForOAIRecordID.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordPublishDeleteSegment.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordPublishDeleteTitleAndItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordPublishInsertItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordPublishInsertSegment.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordPublishInsertTitle.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordPublishInsertTitleAndItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordPublishSetProductionIDs.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordPublishToProduction.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordPublishUpdateItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordPublishUpdateSegment.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordPublishUpdateTitle.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordPublishUpdateTitleAndItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordRelatedTitleDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordRelatedTitleInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordRelatedTitleSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordRelatedTitleUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordRightDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordRightInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordRightSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordRightUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordSelectForOAIIdentifierAndDateStamp.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordSelectForOAIIdentifierAndStatus.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordStatusDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordStatusInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordStatusSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordStatusUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordSubjectDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordSubjectInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordSubjectSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordSubjectUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\OAIRecordUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageFlickrNoteDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageFlickrNoteInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageFlickrNoteSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageFlickrNoteSelectForPageID.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageFlickrNoteUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageFlickrTagDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageFlickrTagInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageFlickrTagSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageFlickrTagSelectForPageID.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageFlickrTagUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageNameDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageNameInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageNameSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageNameSelectNewByKeyValuesAndSource.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageNameUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageSelectNewByKeyValuesAndSource.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PageUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\Page_PageTypeDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\Page_PageTypeInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\Page_PageTypeSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\Page_PageTypeSelectNewByKeyValuesAndSource.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\Page_PageTypeUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\PublishToProduction.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\StatsCountIAItemPendingApproval.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\StatsSelectBSItemGroupByStatus.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\StatsSelectIAItemGroupByStatus.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\StatsSelectReadyForProductionBySource.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleAssociationDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleAssociationInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleAssociationSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleAssociationSelectByKey.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleAssociationUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleAssociation_TitleIdentifierDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleAssociation_TitleIdentifierInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleAssociation_TitleIdentifierSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleAssociation_TitleIdentifierSelectByKey.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleAssociation_TitleIdentifierUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleSelectNewByKeyAndSource.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleTagDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleTagInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleTagSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleTagSelectNewByKeyTagTextAndSource.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleTagUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\TitleUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\Title_TitleIdentifierDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\Title_TitleIdentifierInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\Title_TitleIdentifierSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\Title_TitleIdentifierSelectByKeyNameAndValue.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\Title_TitleIdentifierUpdateAuto.sql"

GOTO DONE

:MISSINGPARAM

@ECHO OFF

ECHO.
ECHO Usage:
ECHO. 
ECHO BHLImportDBBuildScript SERVERNAME DATABASENAME DATAORSTRUCTURE
ECHO.
ECHO SERVERNAME is the name of the database server
ECHO DATABASENAME is the name of the database to be created
ECHO DATAORSTRUCTURE is "structure" to build the empty database (no data), "data" to add data to an existing database, or "all" to build the struture and add the data.
ECHO.
ECHO Example: BHLImportDBBuildScript localhost BHLImport all
ECHO.
ECHO It is highly recommended that "BHLImport" be used as the database name.
ECHO.

@ECHO ON

:DONE

