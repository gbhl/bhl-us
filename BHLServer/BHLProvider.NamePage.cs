using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<NamePage> NamePageSelectByPageID(int pageID)
        {
            return new NamePageDAL().NamePageSelectByPageID(null, null, pageID);
        }

        public CustomGenericList<NamePage> NamePageSelectByPageIDAndSource(int pageID, string sourceName)
        {
            return new NamePageDAL().NamePageSelectByPageIDAndSource(null, null, pageID, sourceName);
        }

        public NamePage NamePageSelectByPageIDAndNameString(int pageID, string nameString)
        {
            return new NamePageDAL().NamePageSelectByPageIDAndNameString(null, null, pageID, nameString);
        }

        public bool NamePageDeleteAuto(int namePageID)
        {
            return new NamePageDAL().NamePageDeleteAuto(null, null, namePageID);
        }

        public NamePage NamePageInsert(int PageID, string nameString, string resolvedNameString, string canonicalNameString,
            string identifierList, string sourceName)
        {
            return this.NamePageInsert(null, null, 
                PageID, nameString, resolvedNameString, canonicalNameString, identifierList, sourceName, 0, 1);
        }

        public NamePage NamePageInsert(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int PageID, 
            string nameString, string resolvedNameString, string canonicalNameString, string identifierList, 
            string sourceName, short isFirstOccurrence, int userId)
        {
            return new NamePageDAL().NamePageInsert(sqlConnection, sqlTransaction, 
                PageID, nameString, resolvedNameString, canonicalNameString, identifierList, sourceName, isFirstOccurrence, userId);
        }

        public void NamePageUpdate(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int namePageID, 
            string nameString, string resolvedNameString, string nameBankID, string eolID, short isFirstOccurrence, 
            int userID)
        {
            new NamePageDAL().NamePageUpdate(sqlConnection, sqlTransaction, namePageID, nameString, resolvedNameString, 
                nameBankID, eolID, isFirstOccurrence, userID);
        }

        public void NamePageDelete(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int namePageID)
        {
            new NamePageDAL().NamePageDelete(sqlConnection, sqlTransaction, namePageID);
        }

        public void NamePageDeleteByItemID(int itemID)
        {
            new NamePageDAL().NamePageDeleteByItemID(null, null, itemID);
        }

        public void NamePageSave(CustomGenericList<NamePage> namePages, int userId)
        {
            NameDAL nameDAL = new NameDAL();
            NamePageDAL namePageDAL = new NamePageDAL();
			TransactionController transactionController = new TransactionController();

            try
            {
                transactionController.BeginTransaction();

                foreach (NamePage namePage in namePages)
                {
                    if (namePage.IsNew && !namePage.IsDeleted)
                    {
                        // Insert a new record (and any necessary supporting name records)
                        string identifierList = string.Empty;
                        if (!string.IsNullOrEmpty(namePage.NameBankID)) identifierList = "NameBank|" + namePage.NameBankID;
                        if (!string.IsNullOrEmpty(namePage.EOLID)) identifierList += (identifierList.Length > 0 ? "^" : "") + "EOL|" + namePage.EOLID;

                        this.NamePageInsert(transactionController.Connection, transactionController.Transaction,
                            namePage.PageID, namePage.NameString, namePage.ResolvedNameString, namePage.ResolvedNameString,
                            identifierList, "User Reported", namePage.IsFirstOccurrence, userId);
                    }
                    else if (namePage.IsDirty && !namePage.IsDeleted)
                    {
                        // Update the existing name records
                        this.NamePageUpdate(transactionController.Connection, transactionController.Transaction,
                            namePage.NamePageID, namePage.NameString, namePage.ResolvedNameString, namePage.NameBankID,
                            namePage.EOLID, namePage.IsFirstOccurrence, userId);
                    }
                    else if (namePage.IsDeleted && !namePage.IsNew)
                    {
                        // Delete the existing NamePage record
                        this.NamePageDelete(transactionController.Connection, transactionController.Transaction,
                            namePage.NamePageID);
                    }
                }

                transactionController.CommitTransaction();
            }
            catch (Exception ex)
            {
                transactionController.RollbackTransaction();
                throw;
            }
            finally
            {
                transactionController.Dispose();
            }
        }

        /// <summary>
        /// For each page name in the list, it inserts a new row in the database or
        /// or updates an existing row.  Then, any previously-existing rows that
        /// no longer appear in the list are deleted.
        /// </summary>
        /// <param name="pageID">Identifier of the page whose page names are being updated</param>
        /// <param name="names">List of page names</param>
        public int[] PageNameUpdateList(int pageID, List<NameFinderResponse> names, string sourceName)
        {
            int[] updateStats = new int[4];

            foreach (NameFinderResponse name in names)
            {
                string identifierList = string.Empty;
                if (name.Identifiers.Count > 0) identifierList = string.Join("^", name.Identifiers.ToArray());

                NamePage newNamePage = this.NamePageInsert(
                    pageID, name.Name, name.NameResolved, name.CanonicalName, identifierList, sourceName);

                if (newNamePage != null)
                    updateStats[0]++; // number inserted
                else
                    updateStats[3]++; // number unchanged
            }

            // Deactivate any names that were previously contributed by this name source, 
            // but were not not returned by the just-completed source request (this means 
            // they've fallen out of the list of page names for this page)
            CustomGenericList<NamePage> namePages = this.NamePageSelectByPageIDAndSource(pageID, sourceName);
            foreach (NamePage namePage in namePages)
            {
                if (!this.NameIsInList(namePage.NameString, names))
                {
                    this.NamePageDeleteAuto(namePage.NamePageID);
                    updateStats[2]++; // number deleted
                }
            }

            return updateStats;
        }

        /// <summary>
        /// Determines if the specified name is in the supplied list
        /// </summary>
        /// <param name="pageName">Name for which to search</param>
        /// <param name="names">List in which to search</param>
        /// <returns>True if found, False if not</returns>
        private bool NameIsInList(string nameString, List<NameFinderResponse> names)
        {
            bool searchResult = false;
            foreach (NameFinderResponse name in names)
            {
                if (String.Compare(name.Name, nameString, true) == 0)
                    searchResult = true;
            }
            return searchResult;
        }
    }
}
