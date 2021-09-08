using System.Runtime.Serialization;

namespace MOBOT.BHL.DataObjects.Enum
{
    [DataContract]
    public enum ItemType
    {
        Item = 1,
        Book = 10,
        Segment = 20
    }
}
