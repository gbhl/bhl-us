using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public Creator SaveCreator(int importSourceID, string creatorName, string firstNameFirst,
            string simpleName, string dob, string dod, string biography, string creatorNote,
            string marcDataFieldTag, string marcCreator_a, string marcCreator_b,
            string marcCreator_c, string marcCreator_d, string marcCreator_full,
            DateTime? externalCreationDate, DateTime? externalLastModifiedDate)
        {
            // Format the MARCCreator_Full value for BHL
            marcCreator_full = (marcCreator_a ?? "") + " " + (marcCreator_b ?? "") + " " + 
                                (marcCreator_c ?? "") + " " + (marcCreator_d ?? "");
            marcCreator_full = marcCreator_full.Substring(0, (marcCreator_full.Length > 450 ? 450 : marcCreator_full.Length));
            
            CreatorDAL dal = new CreatorDAL();
            Creator savedCreator = dal.CreatorSelectNewByCreatorNameAndSource(null, null, marcCreator_a, 
                                        marcCreator_b, marcCreator_c, marcCreator_d, importSourceID);

            if (savedCreator == null)
            {
                Creator newCreator = new Creator
                {
                    ImportStatusID = 10,
                    ImportSourceID = importSourceID,
                    CreatorName = creatorName,
                    FirstNameFirst = firstNameFirst,
                    SimpleName = simpleName,
                    DOB = dob,
                    DOD = dod,
                    Biography = biography,
                    CreatorNote = creatorNote,
                    MARCDataFieldTag = marcDataFieldTag,
                    MARCCreator_a = marcCreator_a,
                    MARCCreator_b = marcCreator_b,
                    MARCCreator_c = marcCreator_c,
                    MARCCreator_d = marcCreator_d,
                    MARCCreator_Full = marcCreator_full,
                    ExternalCreationDate = externalCreationDate,
                    ExternalLastModifiedDate = externalLastModifiedDate
                };
                savedCreator = dal.CreatorInsertAuto(null, null, newCreator);
            }
            else
            {
                if (savedCreator.FirstNameFirst != firstNameFirst || 
                    savedCreator.SimpleName != simpleName ||
                    savedCreator.DOB != dob || savedCreator.DOD != dod ||
                    savedCreator.Biography != biography || savedCreator.CreatorNote != creatorNote ||
                    savedCreator.MARCDataFieldTag != marcDataFieldTag ||
                    savedCreator.MARCCreator_a != marcCreator_a ||
                    savedCreator.MARCCreator_b != marcCreator_b ||
                    savedCreator.MARCCreator_c != marcCreator_c ||
                    savedCreator.MARCCreator_d != marcCreator_d ||
                    savedCreator.MARCCreator_Full != marcCreator_full) 
                {
                    savedCreator.FirstNameFirst = firstNameFirst;
                    savedCreator.SimpleName = simpleName;
                    savedCreator.DOB = dob;
                    savedCreator.DOD = dod;
                    savedCreator.Biography = biography;
                    savedCreator.CreatorNote = creatorNote;
                    savedCreator.MARCDataFieldTag = marcDataFieldTag;
                    savedCreator.MARCCreator_a = marcCreator_a;
                    savedCreator.MARCCreator_b = marcCreator_b;
                    savedCreator.MARCCreator_c = marcCreator_c;
                    savedCreator.MARCCreator_d = marcCreator_d;
                    savedCreator.MARCCreator_Full = marcCreator_full;
                    savedCreator.ExternalLastModifiedDate = externalLastModifiedDate;

                    dal.CreatorUpdateAuto(null, null, savedCreator);
                }
            }

            return savedCreator;
        }
    }
}
