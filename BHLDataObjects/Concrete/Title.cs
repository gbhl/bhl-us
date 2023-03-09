
#region Using

using System;
using System.Collections.Generic;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Title : __Title
	{
		#region Properties

        private List<TitleAuthor> _titleAuthors = new List<TitleAuthor>();
        private List<Title_Identifier> _titleIdentifiers = new List<Title_Identifier>();
		private List<TitleCollection> _titleCollections = new List<TitleCollection>();
		private List<Book> _books = new List<Book>();
        private List<ItemTitle> _itemTitles = new List<ItemTitle>();
        private List<TitleKeyword> _titleKeywords = new List<TitleKeyword>();
        private List<TitleExternalResource> _titleExternalResources = new List<TitleExternalResource>();
        private List<TitleAssociation> _titleAssociations = new List<TitleAssociation>();
        private List<TitleVariant> _titleVariants = new List<TitleVariant>();
        private List<TitleLanguage> _titleLanguages = new List<TitleLanguage>();
        private List<TitleNote> _titleNotes = new List<TitleNote>();
        private List<Institution> _titleInstitutions = new List<Institution>();
		private long _rowNum;
		private string _institutionName;
        private bool _hasLocalContent = true;
        private bool _hasExternalContent = false;

        public List<TitleAuthor> TitleAuthors
		{
			get { return _titleAuthors; }
			set { _titleAuthors = value; }
		}

        public List<Title_Identifier> TitleIdentifiers
        {
            get { return _titleIdentifiers; }
            set { _titleIdentifiers = value; }
        }

		public List<TitleCollection> TitleCollections
		{
			get { return this._titleCollections; }
			set { this._titleCollections = value; }
		}

		public List<Book> Books
		{
			get { return this._books; }
			set { this._books = value; }
		}

        public List<ItemTitle> ItemTitles
        {
            get { return this._itemTitles; }
            set { this._itemTitles = value; }
        }
        
        public List<TitleKeyword> TitleKeywords
        {
            get { return _titleKeywords; }
            set { _titleKeywords = value; }
        }

        public List<TitleExternalResource> TitleExternalResources
        {
            get { return _titleExternalResources; }
            set { _titleExternalResources = value; }
        }

        public List<TitleAssociation> TitleAssociations
        {
            get { return _titleAssociations; }
            set { _titleAssociations = value; }
        }

        public List<TitleVariant> TitleVariants
        {
            get { return _titleVariants; }
            set { _titleVariants = value; }
        }

        public List<TitleLanguage> TitleLanguages
        {
            get { return _titleLanguages; }
            set { _titleLanguages = value; }
        }

        public List<TitleNote> TitleNotes
        {
            get { return _titleNotes; }
            set { _titleNotes = value; }
        }

        public List<Institution> TitleInstitutions
        {
            get { return _titleInstitutions; }
            set { _titleInstitutions = value; }
        }

        // Title institution name is rolled up from the Items associated with the Title.
        public string InstitutionName
		{
			get { return this._institutionName; }
			set { this._institutionName = value; }
		}

        public long RowNum
		{
			get { return this._rowNum; }
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

        #endregion

        #region Helper methods

        public string DisplayedShortTitle
		{
			get
			{
				if ( FullTitle.Length > 60 )
				{
					return ( FullTitle.Substring( 0, 60 ) + "..." );
				}
				else
				{
					return ( FullTitle );
				}
			}
		}

        #endregion

        #region ISet override

        public override void SetValues( CustomDataRow row )
		{
			foreach ( CustomDataColumn column in row )
			{
				switch ( column.Name )
				{
					case "InstitutionName":
						{
							_institutionName = Utility.EmptyIfNull( column.Value );
							break;
						}
					case "RowNum":
						{
							_rowNum = (long)column.Value;
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
                }
            }

			base.SetValues( row );

		}

		#endregion

	}
}
