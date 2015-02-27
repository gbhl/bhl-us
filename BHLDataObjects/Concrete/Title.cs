
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Title : __Title
	{
		#region Properties

        private CustomGenericList<TitleAuthor> _titleAuthors = new CustomGenericList<TitleAuthor>();
        private CustomGenericList<Title_Identifier> _titleIdentifiers = new CustomGenericList<Title_Identifier>();
		private CustomGenericList<TitleCollection> _titleCollections = new CustomGenericList<TitleCollection>();
		private CustomGenericList<Item> _items = new CustomGenericList<Item>();
        private CustomGenericList<TitleItem> _titleItems = new CustomGenericList<TitleItem>();
        private CustomGenericList<TitleKeyword> _titleKeywords = new CustomGenericList<TitleKeyword>();
        private CustomGenericList<TitleAssociation> _titleAssociations = new CustomGenericList<TitleAssociation>();
        private CustomGenericList<TitleVariant> _titleVariants = new CustomGenericList<TitleVariant>();
        private CustomGenericList<TitleLanguage> _titleLanguages = new CustomGenericList<TitleLanguage>();
        private CustomGenericList<TitleNote> _titleNotes = new CustomGenericList<TitleNote>();
		private long _rowNum;
		private string _institutionName;

        public CustomGenericList<TitleAuthor> TitleAuthors
		{
			get { return _titleAuthors; }
			set { _titleAuthors = value; }
		}

        public CustomGenericList<Title_Identifier> TitleIdentifiers
        {
            get { return _titleIdentifiers; }
            set { _titleIdentifiers = value; }
        }

		public CustomGenericList<TitleCollection> TitleCollections
		{
			get { return this._titleCollections; }
			set { this._titleCollections = value; }
		}

		public CustomGenericList<Item> Items
		{
			get { return this._items; }
			set { this._items = value; }
		}

        public CustomGenericList<TitleItem> TitleItems
        {
            get { return this._titleItems; }
            set { this._titleItems = value; }
        }
        
        public CustomGenericList<TitleKeyword> TitleKeywords
        {
            get { return _titleKeywords; }
            set { _titleKeywords = value; }
        }

        public CustomGenericList<TitleAssociation> TitleAssociations
        {
            get { return _titleAssociations; }
            set { _titleAssociations = value; }
        }

        public CustomGenericList<TitleVariant> TitleVariants
        {
            get { return _titleVariants; }
            set { _titleVariants = value; }
        }

        public CustomGenericList<TitleLanguage> TitleLanguages
        {
            get { return _titleLanguages; }
            set { _titleLanguages = value; }
        }

        public CustomGenericList<TitleNote> TitleNotes
        {
            get { return _titleNotes; }
            set { _titleNotes = value; }
        }

        public string InstitutionName
		{
			get { return this._institutionName; }
			set { this._institutionName = value; }
		}

		public long RowNum
		{
			get { return this._rowNum; }
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
				}
			}

			base.SetValues( row );

		}

		#endregion

	}
}
