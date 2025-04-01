using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<Identifier> IdentifierSelectAll()
        {
            return (new IdentifierDAL().IdentifierSelectAll(null, null));
        }

        public List<Identifier> IdentifierSelectByIDType(string idTypeName)
        {
            return (new IdentifierDAL().IdentifierSelectByIDType(null, null, idTypeName));
        }

        public Identifier IdentifierSelectByGNFinderDataSource(int gnDataSourceID)
        {
            return (new IdentifierDAL().IdentifierSelectByGNFinderDataSource(null, null, gnDataSourceID));
        }

        public IdentifierValidationResult ValidateIdentifiers(object identifiers)
        {
            IdentifierValidationResult result = new IdentifierValidationResult();

            List<Identifier> allIdentifiers = new BHLProvider().IdentifierSelectAll();
            Dictionary<string, int> identifierCount = new Dictionary<string, int>();

            foreach(object identifier in (IEnumerable<object>)identifiers)
            {
                // Get the name and value of each identifier in the list being evaluated
                PropertyInfo nameProp = identifier.GetType().GetProperty("IdentifierName");
                PropertyInfo valueProp = identifier.GetType().GetProperty("IdentifierValue");
                PropertyInfo isDeletedProp = identifier.GetType().GetProperty("IsDeleted");
                PropertyInfo isDirtyProp = identifier.GetType().GetProperty("IsDirty");
                PropertyInfo isNewProp = identifier.GetType().GetProperty("IsNew");

                string identifierName = (string)nameProp.GetValue(identifier);
                string identifierValue = (string)valueProp.GetValue(identifier);
                bool isDeleted = (bool)isDeletedProp.GetValue(identifier);
                bool isDirty = (bool)isDirtyProp.GetValue(identifier);
                bool isNew = (bool)isNewProp.GetValue(identifier);

                if (!isDeleted)  // Ignore deleted identifiers
                {
                    // Update the count of each type of identifier being evaulated
                    if (identifierCount.ContainsKey(identifierName))
                        identifierCount[identifierName]++;
                    else
                        identifierCount.Add(identifierName, 1);

                    // If the identifier is new or updated, make sure the value is valid
                    if (isDirty || isNew)
                    {
                        var identifierDefinition = allIdentifiers.Where(i => i.IdentifierName == identifierName).Single();
                        IdentifierValidationResult idResult = ValidateIdentifier(identifierValue, isNew, identifierDefinition);
                        if (idResult.IncludesNewBHLDOI) result.IncludesNewBHLDOI = true;
                        if (!idResult.IsValid)
                        {
                            result.IsValid = false;
                            foreach (string message in idResult.Messages) result.Messages.Add(message);
                        }
                    }
                }
            }

            // Make sure the maximum number of each type of identifier has not been exceeded
            foreach(KeyValuePair<string, int> idCount in identifierCount)
            {
                var identifierDefinition = allIdentifiers.Where(i => i.IdentifierName == idCount.Key).Single();
                if (idCount.Value > identifierDefinition.MaximumPerEntity && 
                    identifierDefinition.MaximumPerEntity > 0)  // MaximumPerEntity = 0 means an unlimited number are allowed
                {
                    result.IsValid = false;
                    result.Messages.Add(string.Format("{0} {1} values have been assigned, which is more than the maximum of {2}.",
                        idCount.Value, idCount.Key, identifierDefinition.MaximumPerEntity));
                }
            }

            return result;
        }

        public IdentifierValidationResult ValidateIdentifier(string identifierValue, bool IsNew, Identifier identifierTemplate)
        {
            IdentifierValidationResult result = new IdentifierValidationResult();

            string identifierPattern = identifierTemplate.PatternExpression;
            if (!string.IsNullOrWhiteSpace(identifierPattern))
            {
                Regex regex = new Regex(identifierPattern);
                if (!regex.IsMatch(identifierValue))
                {
                    result.IsValid = false;
                    result.Messages.Add(string.Format("{0} value {1} is not in the required format: {2}",
                        identifierTemplate.IdentifierName, identifierValue, identifierTemplate.PatternDescription));
                }
            }

            // Set the indicator that specifies if this is a new BHL DOI
            if (string.Compare(identifierTemplate.IdentifierName, "DOI", true, CultureInfo.CurrentCulture) == 0)
            {
                List<DOIPrefix> prefixes = new DOIDAL().DOIPrefixSelectAll(null, null);
                var prefixMatch = prefixes.Where(x => identifierValue.StartsWith(x.Prefix));
                if (prefixMatch.Count() > 0 && IsNew) result.IncludesNewBHLDOI = true;
            }

            return result;
        }
    }

    public class IdentifierValidationResult
    {
        /// <summary>
        /// Indicates whether all identifiers passed validation.  True or False.
        /// </summary>
        public bool IsValid { get; set; } = true;

        /// <summary>
        /// Indicates if any of the identifiers are new BHL DOIs.  True or False.  Calling procedure must 
        /// evaluate to determine if this indicates an error condition.  In some cases it may, and in other
        /// cases not.
        /// </summary>
        public bool IncludesNewBHLDOI { get; set; } = false;

        /// <summary>
        /// Messages generated by the validation process.  Typically these are error messages.
        /// </summary>
        public List<string> Messages { get; set; } = new List<string>();
    }
}
