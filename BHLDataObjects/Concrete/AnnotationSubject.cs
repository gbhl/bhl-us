using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class AnnotationSubject : __AnnotationSubject
    {
        #region Properties

        private int? _annotationSubjectCategoryID;
        public int? AnnotationSubjectCategoryID1
        {
            get { return _annotationSubjectCategoryID; }
            set { _annotationSubjectCategoryID = value; }
        }

        private string _subjectCategoryName = string.Empty;
        public string SubjectCategoryName
        {
            get { return _subjectCategoryName; }
            set { _subjectCategoryName = value; }
        }

        #endregion Properties

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "AnnotationSubjectCategoryID":
                        {
                            _annotationSubjectCategoryID = (int?)column.Value;
                            break;
                        }
                    case "SubjectCategoryName":
                        {
                            _subjectCategoryName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

        #endregion

    }
}
