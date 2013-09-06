using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public PageAnnotation PageAnnotationSave(int annotatedPageId, int annotationId, string pageColumn)
        {
            PageAnnotationDAL dal = new PageAnnotationDAL();
            PageAnnotation pageAnnotation = dal.PageAnnotationSelectAuto(null, null, annotatedPageId, annotationId);

            if (pageAnnotation != null)
            {
                pageAnnotation.PageColumn = pageColumn;
                pageAnnotation = dal.PageAnnotationUpdateAuto(null, null, pageAnnotation);
            }
            else
            {
                pageAnnotation = new PageAnnotation();
                pageAnnotation.AnnotatedPageID = annotatedPageId;
                pageAnnotation.AnnotationID = annotationId;
                pageAnnotation.PageColumn = pageColumn;
                pageAnnotation = dal.PageAnnotationInsertAuto(null, null, pageAnnotation);
            }
            return pageAnnotation;
        }
    }
}
