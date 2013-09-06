using System;
using System.Web.Services;
using System.ComponentModel;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.WebService
{
    public partial class BHLWS : System.Web.Services.WebService
    {
        [WebMethod]
        public AnnotatedTitle AnnotatedTitleSave(int annotationSourceID, string externalIdentifier,
            string author, string title, string edition, string volume, string publicationDetails, 
            string date, string location, string isBeagleEra, string inscription)
        {
            return new BHLProvider().AnnotatedTitleSave(annotationSourceID, externalIdentifier, author, title,
                edition, volume, publicationDetails, date, location, isBeagleEra, inscription);
        }

        [WebMethod]
        public AnnotatedItem AnnotatedItemSave(int annotatedTitleId, string externalIdentifier, string volume)
        {
            return new BHLProvider().AnnotatedItemSave(annotatedTitleId, externalIdentifier, volume);
        }

        [WebMethod]
        public AnnotatedPage AnnotatedPageSave(int annotatedItemId, string externalIdentifier,
            int annotatedPageTypeId, string pageNumber)
        {
            return new BHLProvider().AnnotatedPageSave(annotatedItemId, externalIdentifier, 
                annotatedPageTypeId, pageNumber);
        }

        [WebMethod]
        public bool AnnotatedPageCharacteristicDeleteByPageID(int annotatedPageId)
        {
            return new BHLProvider().AnnotatedPageCharacteristicDeleteByPageID(annotatedPageId);
        }

        [WebMethod]
        public AnnotatedPageCharacteristic AnnotatedPageCharacteristicSave(int annotatedPageId,
            string characteristicDetail)
        {
            return new BHLProvider().AnnotatedPageCharactersticSave(annotatedPageId, characteristicDetail);
        }

        [WebMethod]
        public Annotation AnnotationSelectAuto(int annotationID)
        {
            return new BHLProvider().AnnotationSelectAuto(annotationID);
        }

        [WebMethod]
        public Annotation AnnotationSave(int annotationSourceID, int annotatedPageId, string pageColumn,
            string externalIdentifier, int sequenceNumber, string comment, bool dataLoadEdit)
        {
            return new BHLProvider().AnnotationSave(annotationSourceID, annotatedPageId, pageColumn, 
                externalIdentifier, sequenceNumber, comment, dataLoadEdit);
        }

        [WebMethod]
        public Annotation AnnotationSaveText(int annotationSourceID, string externalIdentifer,
            string textDescription, string text, string textCorrected, bool dataLoadEdit)
        {
            return new BHLProvider().AnnotationSaveText(annotationSourceID, externalIdentifer, textDescription,
                text, textCorrected, dataLoadEdit);
        }

        [WebMethod]
        public void AnnotationClear(int annotationId)
        {
            new BHLProvider().AnnotationClear(annotationId);
        }

        [WebMethod]
        public AnnotationRelation AnnotationRelationSave(int annotationId, string relatedExternalIdentifier,
            string note)
        {
            return new BHLProvider().AnnotationRelationSave(annotationId, relatedExternalIdentifier, note);
        }

        [WebMethod]
        public AnnotationNote AnnotationNoteSave(int annotationId, string noteText, byte isAlternate)
        {
            return new BHLProvider().AnnotationNoteSave(annotationId, noteText, isAlternate);
        }

        [WebMethod]
        public Annotation_AnnotationConcept Annotation_AnnotationConceptSave(int annotationId, string conceptCode,
            int keywordTargetId)
        {
            return new BHLProvider().Annotation_AnnotationConceptSave(annotationId, conceptCode, keywordTargetId);
        }

        [WebMethod]
        public AnnotationSubject AnnotationSubjectSave(int annotationId, int sourceId, string categoryName,
            int keywordTargetId, string subjectText)
        {
            return new BHLProvider().AnnotationSubjectSave(annotationId, sourceId, categoryName, keywordTargetId,
                subjectText);
        }
    }
}
