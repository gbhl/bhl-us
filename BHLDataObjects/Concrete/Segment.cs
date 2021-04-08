
#region Using

using System;
using System.Collections.Generic;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Segment : __Segment
	{
        private int? _titleId = null;

        public int? TitleId
        {
            get { return _titleId; }
            set { _titleId = value; }
        }

        private int? _bookID = null;

        public int? BookID
        {
            get { return _bookID; }
            set { _bookID = value; }
        }

        private byte? _bookIsVirtual = null;

        public byte? BookIsVirtual
        {
            get { return _bookIsVirtual; }
            set { _bookIsVirtual = value; }
        }

        private int? _segmentClusterId = null;

        public int? SegmentClusterId
        {
            get { return _segmentClusterId; }
            set { _segmentClusterId = value; }
        }

        private int? _segmentClusterTypeId = null;

        public int? SegmentClusterTypeId
        {
            get { return _segmentClusterTypeId; }
            set { _segmentClusterTypeId = value; }
        }

        private string _segmentClusterTypeLabel = string.Empty;

        public string SegmentClusterTypeLabel
        {
            get { return _segmentClusterTypeLabel; }
            set { _segmentClusterTypeLabel = value; }
        }

        private string _titleFullTitle = string.Empty;

        public string TitleFullTitle
        {
            get { return _titleFullTitle; }
            set { _titleFullTitle = value; }
        }

        private string _titleShortTitle = string.Empty;

        public string TitleShortTitle
        {
            get { return _titleShortTitle; }
            set { _titleShortTitle = value; }
        }

        private string _titlePublisherName = string.Empty;

        public string TitlePublisherName
        {
            get { return _titlePublisherName; }
            set { _titlePublisherName = value; }
        }

        private string _titlePublicationPlace = string.Empty;

        public string TitlePublicationPlace
        {
            get { return _titlePublicationPlace; }
            set { _titlePublicationPlace = value; }
        }

        private string _titlePublicationDate = string.Empty;

        public string TitlePublicationDate
        {
            get { return _titlePublicationDate; }
            set { _titlePublicationDate = value; }
        }

        private string _itemVolume = string.Empty;

        public string ItemVolume
        {
            get { return _itemVolume; }
            set { _itemVolume = value; }
        }

        private string _itemYear = string.Empty;

        public string ItemYear
        {
            get { return _itemYear;  }
            set { _itemYear = value; }
        }

        private string _contributorName = string.Empty;

        public string ContributorName
        {
            get { return _contributorName; }
            set { _contributorName = value; }
        }

        private string _genreName = string.Empty;

        public string GenreName
        {
            get { return _genreName; }
            set { _genreName = value; }
        }

        private string _statusName = string.Empty;

        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        private string _languageName = string.Empty;

        public string LanguageName
        {
            get { return _languageName; }
            set { _languageName = value; }
        }

        private string _doiName = string.Empty;

        public string DOIName
        {
            get { return _doiName; }
            set { _doiName = value; }
        }

        private string _authors = string.Empty;

        public string Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        private string _keywords = string.Empty;

        public string Keywords
        {
            get { return _keywords; }
            set { _keywords = value; }
        }

        private short _isPrimary = 0;

        public short IsPrimary
        {
            get { return _isPrimary; }
            set
            {
                if (_isPrimary != value)
                {
                    _isPrimary = value; _IsDirty = true;
                }
            }
        }

        private bool _hasLocalContent = true;

        public bool HasLocalContent
        {
            get { return _hasLocalContent; }
            set { _hasLocalContent = value; }
        }

        private bool _hasExternalContent = false;

        public bool HasExternalContent
        {
            get { return _hasExternalContent; }
            set { _hasExternalContent = value; }
        }

        private int? _sequenceOrder = null;

        public int? SequenceOrder
        {
            get { return _sequenceOrder; }
            set { _sequenceOrder = value; }
        }

        private int _segmentStatusID;

        public int SegmentStatusID
        { 
            get { return _segmentStatusID; }
            set { _segmentStatusID = value; }
        }

        public string _notes = string.Empty;
        
        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

        private Item _item = null;

        public Item Item
        {
            get { return _item; }
            set { _item = value; }
        }

        private List<ItemAuthor> _authorList = new List<ItemAuthor>();

        public List<ItemAuthor> AuthorList
        {
            get { return _authorList; }
            set { _authorList = value; }
        }

        private List<ItemKeyword> _keywordList = new List<ItemKeyword>();

        public List<ItemKeyword> KeywordList
        {
            get { return _keywordList; }
            set { _keywordList = value; }
        }

        private List<ItemIdentifier> _identifierList = new List<ItemIdentifier>();

        public List<ItemIdentifier> IdentifierList
        {
            get { return _identifierList; }
            set { _identifierList = value; }
        }

        private List<Institution> _contributorList = new List<Institution>();

        public List<Institution> ContributorList
        {
            get { return this._contributorList; }
            set { this._contributorList = value; }
        }

        private List<ItemPage> _pageList = new List<ItemPage>();

        public List<ItemPage> PageList
        {
            get { return _pageList; }
            set { _pageList = value; }
        }

        private List<Name> _nameList = new List<Name>();

        public List<Name> NameList
        {
            get { return _nameList; }
            set { _nameList = value; }
        }

        private List<Segment> _relatedSegmentList = new List<Segment>();

        public List<Segment> RelatedSegmentList
        {
            get { return _relatedSegmentList; }
            set { _relatedSegmentList = value; }
        }

        private List<ItemRelationship> _relationshipList = new List<ItemRelationship>();

        public List<ItemRelationship> RelationshipList
        {
            get { return _relationshipList; }
            set { _relationshipList = value; }
        }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                if (column.Name == "BookID")
                {
                    _bookID = (int?)(column.Value);
                }
				if (column.Name == "BookIsVirtual")
				{
                    _bookIsVirtual = (byte?)column.Value;
                }
                if (column.Name == "TitleID")
                {
                    _titleId = Utility.ZeroIfNull(column.Value);
                }
                if (column.Name == "SegmentClusterID")
                {
                    _segmentClusterId = Utility.ZeroIfNull(column.Value);
                }
                if (column.Name == "SegmentClusterTypeID")
                {
                    _segmentClusterTypeId = Utility.ZeroIfNull(column.Value);
                }
                if (column.Name == "SegmentClusterTypeLabel")
                {
                    _segmentClusterTypeLabel = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "TitleShortTitle")
                {
                    _titleShortTitle = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "ItemVolume")
                {
                    _itemVolume = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "ContributorName")
                {
                    _contributorName = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "GenreName")
                {
                    _genreName = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "StatusName")
                {
                    _statusName = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "LanguageName")
                {
                    _languageName = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "DOIName")
                {
                    _doiName = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "Authors")
                {
                    _authors = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "Subjects")
                {
                    _keywords = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "IsPrimary")
                {
                    _isPrimary = (short)Utility.ZeroIfNull(Convert.ToInt32(column.Value));
                }
                if (column.Name == "HasLocalContent")
                {
                    _hasLocalContent = Convert.ToInt16(column.Value) == 1;
                }
                if (column.Name == "HasExternalContent")
                {
                    _hasExternalContent = Convert.ToInt16(column.Value) == 1;
                }
                if (column.Name == "SequenceOrder")
                {
                    _sequenceOrder = Convert.ToInt32(column.Value);
                }
                if (column.Name == "SegmentStatusID")
                {
                    _segmentStatusID = (int)column.Value;
                }
                if (column.Name == "Notes")
                {
                    _notes = Utility.EmptyIfNull(column.Value);
                }
            }
            base.SetValues(row);
        }
	}
}
