using MOBOT.BHL.DataObjects;
using MOBOT.BHL.DataObjects.Enum;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public interface IBHLProvider
    {
        string GetItemText(ItemType itemType, int entityID);
        byte[] GetItemPdf(ItemType itemType, int entityID);
        string GetItemPdfPath(ItemType itemType, int entityID);
        List<BHLProvider.ViewerPage> PageGetImageDimensions(List<BHLProvider.ViewerPage> pages, ItemType itemType, int entityID);
        string DOIGetFileContents(string batchId, string type);
        string MarcGetFileContents(int id, string type);
        void MarcCreateFile(string marcBibID, string content);
        bool OcrJobExists(int itemID);
        void OcrCreateJob(int itemID);
        string GetOcrText(int pageID);
        Book BookSelectAuto(int bookID);
        Book BookSelectByItemID(int itemID);
        List<Book> BookSelectRecentlyChanged(string startDate);
        Item ItemSelectFilenames(ItemType itemType, int entityID);
        List<Page> PageMetadataSelectByItemID(int bookID);
        List<NameFinderResponse> GetNamesFromOcr(string nameFinderService, int pageID, bool usePreferredNameResults, int maxReadAttempts);
        bool ItemCheckForOcrText(int itemID, string ocrTextPath);
        bool PageCheckForOcrText(int pageID);
        List<PageFlickr> PageFlickrSelectRandom(int numberToReturn);
        List<PageSummaryView> PDFPageSummaryViewSelectByPdfID(int pdfId);
        Vault VaultSelect(int vaultID);
        Title TitleSelectAuto(int titleID);
        Title TitleSelectExtended(int titleID);
        List<Title_Identifier> Title_IdentifierSelectByTitleID(int titleID);
        List<Title_Identifier> DOISelectValidForTitle(int titleID);
        List<DOI> TitleSelectWithoutSubmittedDOI(int numberToReturn);
        List<Title> TitleSelectAllPublished();
    }
}
