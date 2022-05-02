using MOBOT.BHL.DataObjects.Enum;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public interface IBHLProvider
    {
        string GetItemText(ItemType itemType, int entityID);
        byte[] GetItemPdf(ItemType itemType, int entityID);
        List<BHLProvider.ViewerPage> PageGetImageDimensions(List<BHLProvider.ViewerPage> pages, ItemType itemType, int entityID);
        string DOIGetFileContents(string batchId, string type);
        string MarcGetFileContents(int id, string type);
        void MarcCreateFile(string marcBibID, string content);
        bool OcrJobExists(int itemID);
        void OcrCreateJob(int itemID);
        string GetOcrText(int pageID);
    }
}
