using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public AnnotatedTitle AnnotatedTitleSave(int annotationSourceID, string externalIdentifier,
            string author, string title, string edition, string volume, string publicationDetails,
            string date, string location, string isBeagleEra, string inscription)
        {
            AnnotatedTitleDAL dal = new AnnotatedTitleDAL();
            AnnotatedTitle annotatedTitle = dal.AnnotatedTitleSelectByExternalIdentifer(null, null,
                externalIdentifier, annotationSourceID);

            if (annotatedTitle != null)
            {
                annotatedTitle.Author = author;
                annotatedTitle.Title = title;
                annotatedTitle.Edition = edition;
                annotatedTitle.Volume = volume;
                annotatedTitle.PublicationDetails = publicationDetails;
                annotatedTitle.Date = date;
                annotatedTitle.Location = location;
                annotatedTitle.IsBeagleEra = isBeagleEra;
                annotatedTitle.Inscription = inscription;
                annotatedTitle = dal.AnnotatedTitleUpdateAuto(null, null, annotatedTitle);
            }
            else
            {
                annotatedTitle = new AnnotatedTitle();
                annotatedTitle.AnnotationSourceID = annotationSourceID;
                annotatedTitle.ExternalIdentifier = externalIdentifier;
                annotatedTitle.Author = author;
                annotatedTitle.Title = title;
                annotatedTitle.Edition = edition;
                annotatedTitle.Volume = volume;
                annotatedTitle.PublicationDetails = publicationDetails;
                annotatedTitle.Date = date;
                annotatedTitle.Location = location;
                annotatedTitle.IsBeagleEra = isBeagleEra;
                annotatedTitle.Inscription = inscription;
                annotatedTitle = dal.AnnotatedTitleInsertAuto(null, null, annotatedTitle);
            }
            return annotatedTitle;
        }
    }
}
