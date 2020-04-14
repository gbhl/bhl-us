using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Item : __Item
	{
		#region Properties

		private string _titleName;
        private string _fullTitle;
        private string _shortTitle;
        private string _bibliographicLevel;
        private string _partNumber;
        private string _partName;
		private string _paginationStatusName;
		private string _paginationUserName;
		private string _downloadUrl;
        private short? _itemSequence = null;
        private CustomGenericList<Institution> _institutions = new CustomGenericList<Institution>();
		private CustomGenericList<Page> _pages = new CustomGenericList<Page>();
        private CustomGenericList<Title> _titles = new CustomGenericList<Title>();
        private CustomGenericList<TitleItem> _titleItems = new CustomGenericList<TitleItem>();
        private CustomGenericList<ItemLanguage> _itemLanguages = new CustomGenericList<ItemLanguage>();
        private CustomGenericList<ItemCollection> _itemCollections = new CustomGenericList<ItemCollection>();
        private CustomGenericList<Segment> _segments = new CustomGenericList<Segment>();
        private string[] authorStrings = null;
        private string[] tagStrings = null;
        private string[] associationStrings = null;
        private string[] institutionStrings = null;
        private string[] rightsHolderStrings = null;
        private string[] scanningInstitutionStrings = null;
        private string _publicationDetails;
        private int _numberOfSegments = 0;
        private int _numberOfPages = 0;
        private int _totalItems = 0;
        private int? _firstPageID = null;
        private bool _hasFlickrImages = false;
        private bool _hasLocalContent = true;
        private bool _hasExternalContent = false;
        private string _textFilename;
        private string _pdfFilename;
        private string _imagesFilename;
        private string _djvuFilename;
        private string _scandataFilename;
        private string _ocrFolderShare = string.Empty;

        public string DisplayedShortVolume
		{
			get
			{
				if ( Volume.Length > 32 )
					return Volume.Substring( 0, 32 ) + "...";
				else
					return Volume;
			}
		}

        public CustomGenericList<Institution> Institutions
        {
            get { return this._institutions; }
            set { this._institutions = value; }
        }

		public CustomGenericList<Page> Pages
		{
			get { return this._pages; }
			set { this._pages = value; }
		}

        public CustomGenericList<Title> Titles
        {
            get { return this._titles; }
            set { this._titles = value; }
        }

        public CustomGenericList<TitleItem> TitleItems
        {
            get { return this._titleItems; }
            set { this._titleItems = value; }
        }

        public CustomGenericList<ItemLanguage> ItemLanguages
        {
            get { return this._itemLanguages; }
            set { this._itemLanguages = value; }
        }

        public CustomGenericList<ItemCollection> ItemCollections
        {
            get { return this._itemCollections; }
            set { this._itemCollections = value; }
        }

        public CustomGenericList<Segment> Segments
        {
            get { return this._segments; }
            set { this._segments = value; }
        }

        public string TitleName
		{
			get { return this._titleName; }
			set { this._titleName = value; }
		}

        public string FullTitle
        {
            get { return this._fullTitle; }
            set { this._fullTitle = value; }
        }

        public string ShortTitle
        {
            get { return this._shortTitle; }
            set { this._shortTitle = value; }
        }

        public string BibliographicLevel
        {
            get { return this._bibliographicLevel; }
            set { this._bibliographicLevel = value; }
        }
        public string PartNumber
        {
            get { return _partNumber; }
            set { _partNumber = value; }
        }

        public string PartName
        {
            get { return _partName; }
            set { _partName = value; }
        }

        public string PaginationStatusName
		{
			get { return this._paginationStatusName; }
			set { this._paginationStatusName = value; }
		}

		public string PaginationUserName
		{
			get { return this._paginationUserName; }
			set { this._paginationUserName = value; }
		}

		public string DownloadUrl
		{
			get { return this._downloadUrl; }
			set { this._downloadUrl = value; }
		}

        public short? ItemSequence
        {
            get { return this._itemSequence; }
            set { this._itemSequence = value; }
        }

        public string[] TagStrings
        {
            get { return tagStrings; }
        }

        public string[] AuthorStrings
        {
            get { return authorStrings; }
        }

        public string AuthorListString
        {
            get {
                if (authorStrings == null)
                    return string.Empty;
                else
                    return string.Join("; ", authorStrings); 
            }
        }

        public string[] AssociationStrings
        {
            get { return associationStrings; }
        }

        public string[] InstitutionStrings
        {
            get { return institutionStrings; }
        }

        public string[] RightsHolderStrings
        {
            get { return rightsHolderStrings; }
        }

        public string[] ScanningInstitutionStrings
        {
            get { return scanningInstitutionStrings; }
        }

        public string PublicationDetails
        {
            get { return _publicationDetails; }
            set { _publicationDetails = value; }
        }

        public int NumberOfSegments
        {
            get { return _numberOfSegments; }
            set { _numberOfSegments = value; }
        }

        public int NumberOfPages
        {
            get { return _numberOfPages; }
            set { _numberOfPages = value; }
        }

        public int TotalItems
        {
            get { return _totalItems; }
            set { _totalItems = value; }
        }

        public int? FirstPageID
        {
            get { return _firstPageID; }
            set { _firstPageID = value; }
        }

        public bool HasFlickrImages
        {
            get { return _hasFlickrImages; }
            set { _hasFlickrImages = value; }
        }

        public bool HasLocalContent
        {
            get { return _hasLocalContent; }
            set { _hasLocalContent = value; }
        }

        public bool HasExternalContent
        {
            get { return _hasExternalContent; }
            set { _hasExternalContent = value; }
        }

        public string TextFilename
        {
            get { return _textFilename; }
            set { _textFilename = value; }
        }

        public string PdfFilename
        {
            get { return _pdfFilename; }
            set { _pdfFilename = value; }
        }

        public string ImagesFilename
        {
            get { return _imagesFilename; }
            set { _imagesFilename = value; }
        }

        public string DjvuFilename
        {
            get { return _djvuFilename; }
            set { _djvuFilename = value; }
        }

        public string ScandataFilename
        {
            get { return _scandataFilename; }
            set { _scandataFilename = value; }
        }

        public string OcrFolderShare
        {
            get { return _ocrFolderShare; }
            set { _ocrFolderShare = value; }
        }

        #endregion

        private void ProcessTagTextString(string value)
        {
            string tagTextString = "";
            //strip off the trailing separator if necessary
            if (value != null && value.EndsWith("|"))
                value = value.Substring(0, value.Length - 1);

            if (value != null)
                tagTextString = value;

            tagStrings = tagTextString.Split('|');
        }

        private void ProcessAuthorTextString(string value)
        {
            string authorTextString = "";
            //strip off the trailing separator if necessary
            if (value != null && value.EndsWith("|"))
                value = value.Substring(0, value.Length - 1);

            if (value != null)
                authorTextString = value;

            authorStrings = authorTextString.Split('|');
        }

        private void ProcessAssociationTextString(string value)
        {
            string associationTextString = "";
            //strip off the trailing separator if necessary
            if (value != null && value.EndsWith("|"))
                value = value.Substring(0, value.Length - 1);

            if (value != null)
                associationTextString = value;

            associationStrings = associationTextString.Split('|');
        }

        private string[] ProcessInstitutionTextString(string value)
        {
            string institutionTextString = "";
            //strip off the trailing separator if necessary
            if (value != null && value.EndsWith("|"))
                value = value.Substring(0, value.Length - 1);

            if (value != null)
                institutionTextString = value;

            return institutionTextString.Split('|');
        }

        #region ISet override

        public override void SetValues( CustomDataRow row )
		{
			foreach ( CustomDataColumn column in row )
			{
				switch ( column.Name )
				{
                    case "KeywordString":
                        {
                            ProcessTagTextString(Utility.EmptyIfNull(column.Value));
                            break;
                        }
                    case "CreatorTextString":
                        {
                            ProcessAuthorTextString(Utility.EmptyIfNull(column.Value));
                            break;
                        }
                    case "AssociationTextString":
                        {
                            ProcessAssociationTextString(Utility.EmptyIfNull(column.Value));
                            break;
                        }
                    case "ContributorTextString":
                        {
                            institutionStrings = ProcessInstitutionTextString(Utility.EmptyIfNull(column.Value));
                            break;
                        }
                    case "RightsHolderTextString":
                        {
                            rightsHolderStrings = ProcessInstitutionTextString(Utility.EmptyIfNull(column.Value));
                            break;
                        }
                    case "ScanningInstitutionTextString":
                        {
                            scanningInstitutionStrings = ProcessInstitutionTextString(Utility.EmptyIfNull(column.Value));
                            break;
                        }
                    case "TitleName":
						{
							_titleName = Utility.EmptyIfNull( column.Value );
							break;
						}
                    case "FullTitle":
                        {
                            _fullTitle = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "ShortTitle":
                        {
                            _shortTitle = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "BibliographicLevel":
                        {
                            _bibliographicLevel = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PartNumber":
                        {
                            _partNumber = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PartName":
                        {
                            _partName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
					case "PaginationStatusName":
						{
							_paginationStatusName = Utility.EmptyIfNull( column.Value );
							break;
						}
					case "PaginationUserName":
						{
							_paginationUserName = Utility.EmptyIfNull( column.Value );
							break;
						}
					case "DownloadUrl":
						{
							_downloadUrl = Utility.EmptyIfNull( column.Value );
							break;
						}
                    case "ItemSequence":
                        {
                            _itemSequence = (short?)column.Value;
                            break;
                        }
                    case "PublicationDetails":
                        {
                            _publicationDetails = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "NumberOfSegments":
                        {
                            _numberOfSegments = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "NumberOfPages":
                        {
                            _numberOfPages = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "TotalItems":
                        {
                            _totalItems = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "FirstPageID":
                        {
                            _firstPageID = (int?)column.Value;
                            break;
                        }
                    case "HasFlickrImages":
                        {
                            _hasFlickrImages = (((int)column.Value) == 1);
                            break;
                        }
                    case "HasLocalContent":
                        {
                            _hasLocalContent = Convert.ToInt16(column.Value) == 1;
                            break;
                        }
                    case "HasExternalContent":
                        {
                            _hasExternalContent = Convert.ToInt16(column.Value) == 1;
                            break;
                        }
                    case "TextFilename":
                        {
                            _textFilename = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PdfFilename":
                        {
                            _pdfFilename = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "ImagesFilename":
                        {
                            _imagesFilename = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "DjvuFilename":
                        {
                            _djvuFilename = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "ScandataFilename":
                        {
                            _scandataFilename = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "OcrFolderShare":
                        {
                            _ocrFolderShare = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

			base.SetValues( row );
		}

		#endregion

	}
}
