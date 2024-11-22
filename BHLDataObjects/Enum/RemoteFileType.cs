using System.Runtime.Serialization;

namespace MOBOT.BHL.DataObjects.Enum
{
    [DataContract]
    public enum RemoteFileType
    {
        ImageJpg,
        ImageWebp,
        ImageZip,
        Pdf,
        Scandata,
        Djvu
    }
}
