using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public Marc MarcInsertAuto(int marcImportBatchID, String marcFileLocation, String institutionCode,
            String leader, int? titleID)
        {
            return new MarcDAL().MarcInsertAuto(null, null, 10, marcImportBatchID, 
                marcFileLocation, institutionCode, leader, titleID);
        }

        public List<Marc> MarcSelectPendingImport(int batchID)
        {
            return new MarcDAL().MarcSelectPendingImport(null, null, batchID);
        }

        public List<vwMarcDataField> MarcSelectFullDetailsForMarcID(int marcID)
        {
            return new MarcDAL().MarcSelectFullDetailsForMarcID(null, null, marcID);
        }

        public List<Marc> MarcSelectForImportByBatchID(int batchID)
        {
            return new MarcDAL().MarcSelectForImportByBatchID(null, null, batchID);
        }

        public bool MarcDeleteAuto(int marcID)
        {
            return new MarcDAL().MarcDeleteAuto(null, null, marcID);
        }

        public Title MarcSelectTitleDetailsByMarcID(int marcID)
        {
            return new MarcDAL().MarcSelectTitleDetailsByMarcID(null, null, marcID);
        }

        public List<TitleKeyword> MarcSelectTitleKeywordsByMarcID(int marcID)
        {
            return new MarcDAL().MarcSelectTitleKeywordsByMarcID(null, null, marcID);
        }

        public List<TitleNote> MarcSelectTitleNotesByMarcID(int marcID)
        {
            return new MarcDAL().MarcSelectTitleNotesByMarcID(null, null, marcID);
        }

        public List<TitleLanguage> MarcSelectTitleLanguagesByMarcID(int marcID)
        {
            return new MarcDAL().MarcSelectTitleLanguagesByMarcID(null, null, marcID);
        }

        public List<TitleAuthor> MarcSelectAuthorsByMarcID(int marcID)
        {
            return new MarcDAL().MarcSelectAuthorsByMarcID(null, null, marcID);
        }

        public List<Title_Identifier> MarcSelectTitleIdentifiersByMarcID(int marcID)
        {
            return new MarcDAL().MarcSelectTitleIdentifiersByMarcID(null, null, marcID);
        }

        public List<TitleAssociation> MarcSelectAssociationsByMarcID(int marcID)
        {
            return new MarcDAL().MarcSelectAssociationsByMarcID(null, null, marcID);
        }

        public List<TitleAssociation_TitleIdentifier> MarcSelectAssociationIdsByMarcDataFieldID(int marcDataFieldID)
        {
            return new MarcDAL().MarcSelectAssociationIdsByMarcDataFieldID(null, null, marcDataFieldID);
        }

        public List<TitleVariant> MarcSelectVariantsByMarcID(int marcID)
        {
            return new MarcDAL().MarcSelectVariantsByMarcID(null, null, marcID);
        }

        public bool MarcResolveTitles(int batchId)
        {
            return new MarcDAL().MarcResolveTitles(null, null, batchId);
        }

        public void MarcUpdateStatusImported(int marcID)
        {
            new MarcDAL().MarcUpdateStatusImported(null, null, marcID);
        }

        public void MarcUpdateStatusError(int marcID)
        {
            new MarcDAL().MarcUpdateStatusError(null, null, marcID);
        }
    }
}
