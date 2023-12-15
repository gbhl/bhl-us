using System;

namespace MOBOT.BHL.DataObjects
{
    public class IAHarvestItem
    {
        private int _itemId = 0;
        private string _iaIdentifier = string.Empty;
        private string _externalStatus = string.Empty;
        private string _status = string.Empty;
        private string _holdingInstitution = string.Empty;
        private DateTime? _iaAddedDate = null;
        private string _scanDate = string.Empty;
        private DateTime? _iaDateStamp = null;
        private DateTime? _lastXMLDataHarvestDate = null;
        private DateTime? _lastProductionDate = null;
        private DateTime _createdDate;
        private DateTime _lastModifiedDate;
        private string _createdUser = string.Empty;
        private string _lastmodifiedUser = string.Empty;
        private int _totalItems = 0;

        public int ItemId { get => _itemId; set => _itemId = value; }
        public string IAIdentifier { get => _iaIdentifier; set => _iaIdentifier = value; }
        public string ExternalStatus { get => _externalStatus; set => _externalStatus = value; }
        public string Status { get => _status; set => _status = value; }
        public string HoldingInstitution { get => _holdingInstitution; set => _holdingInstitution = value; }
        public DateTime? IAAddedDate { get => _iaAddedDate; set => _iaAddedDate = value; }
        public string ScanDate { get => _scanDate; set => _scanDate = value; }
        public DateTime? IADateStamp { get => _iaDateStamp; set => _iaDateStamp = value; }
        public DateTime? LastXMLDataHarvestDate { get => _lastXMLDataHarvestDate; set => _lastXMLDataHarvestDate = value; }
        public DateTime? LastProductionDate { get => _lastProductionDate; set => _lastProductionDate = value; }
        public DateTime CreatedDate { get => _createdDate; set => _createdDate = value; }
        public DateTime LastModifiedDate { get => _lastModifiedDate; set => _lastModifiedDate = value; }
        public string CreatedUser { get => _createdUser; set => _createdUser = value; }
        public string LastModifiedUser { get => _lastmodifiedUser; set => _lastmodifiedUser = value; }
        public int TotalItems { get => _totalItems; set => _totalItems = value; }
    }
}
