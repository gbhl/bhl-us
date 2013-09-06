using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Marc : __Marc
	{
        #region Properties

        private string _titlePart1;

        public string TitlePart1
        {
            get { return _titlePart1; }
            set { _titlePart1 = value; }
        }

        private string _titlePart2;

        public string TitlePart2
        {
            get { return _titlePart2; }
            set { _titlePart2 = value; }
        }

        private string _responsible;

        public string Responsible
        {
            get { return _responsible; }
            set { _responsible = value; }
        }

        private string _number;

        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }

        private string _part;

        public string Part
        {
            get { return _part; }
            set { _part = value; }
        }

        private int _bhlTitleId;

        public int BhlTitleId
        {
            get { return _bhlTitleId; }
            set { _bhlTitleId = value; }
        }

        private String _bhlShortTitle;

        public String BhlShortTitle
        {
            get { return _bhlShortTitle; }
            set { _bhlShortTitle = value; }
        }
        
        #endregion

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "TitlePart1":
                        {
                            _titlePart1 = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "TitlePart2":
                        {
                            _titlePart2 = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Responsible":
                        {
                            _responsible = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Number":
                        {
                            _number = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Part":
                        {
                            _part = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "BHLTitleID":
                        {
                            _bhlTitleId = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "BHLShortTitle":
                        {
                            _bhlShortTitle = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

        #endregion



    }
}
