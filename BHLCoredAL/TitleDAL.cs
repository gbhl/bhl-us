using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.DAL
{
	public partial class TitleDAL
	{
		#region Select methods

		public Title TitleSelectExtended( SqlConnection sqlConnection, SqlTransaction sqlTransaction, int titleId )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(
				CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			Title title = new TitleDAL().TitleSelectAuto( connection, transaction, titleId );

			if ( title != null )
			{
				title.TitleAuthors = new TitleAuthorDAL().TitleAuthorSelectByTitle( connection, transaction, titleId );
                if (title.TitleAuthors != null && title.TitleAuthors.Count > 0)
				{
					AuthorDAL authorDAL = new AuthorDAL();
                    foreach (TitleAuthor titleAuthor in title.TitleAuthors)
					{
						titleAuthor.Author = authorDAL.AuthorSelectAuto( connection, transaction, titleAuthor.AuthorID);
					}
				}

                title.TitleIdentifiers = new Title_IdentifierDAL().Title_IdentifierSelectByTitleID(connection, transaction, titleId, null);

				title.TitleCollections = new TitleCollectionDAL().SelectByTitle( connection, transaction, titleId );

				title.Items = new ItemDAL().ItemSelectByTitleID( connection, transaction, titleId );

                title.TitleItems = new TitleItemDAL().TitleItemSelectByTitle(connection, transaction, titleId);

                title.TitleKeywords = new TitleKeywordDAL().TitleKeywordSelectByTitleID(connection, transaction, titleId);

                title.TitleAssociations = new TitleAssociationDAL().TitleAssociationSelectExtendedForTitle(connection, transaction, titleId);

                title.TitleVariants = new TitleVariantDAL().TitleVariantSelectByTitleID(connection, transaction, titleId);

                title.TitleLanguages = new TitleLanguageDAL().TitleLanguageSelectByTitleID(connection, transaction, titleId);
			}

			return title;
		}

		/// <summary>
		/// Select all values from Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <returns>Object of type Title.</returns>
		public CustomGenericList<Title> TitleSelectAll(
						SqlConnection sqlConnection,
						SqlTransaction sqlTransaction )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;
			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "TitleSelectAll", connection, transaction ) )
			{
				using ( CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>() )
				{
					CustomGenericList<Title> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

		/// <summary>
		/// Select all values from Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <returns>Object of type Title.</returns>
		public CustomGenericList<Title> TitleSelectAllPublished(
						SqlConnection sqlConnection,
						SqlTransaction sqlTransaction )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;
			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "TitleSelectAll", connection, transaction,
					 CustomSqlHelper.CreateInputParameter( "IsPublished", SqlDbType.Bit, 1, false, true ) ) )
			{
				using ( CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>() )
				{
					CustomGenericList<Title> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

		/// <summary>
		/// Select all values from Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <returns>Object of type Title.</returns>
		public CustomGenericList<Title> TitleSelectAllNonPublished(
						SqlConnection sqlConnection,
						SqlTransaction sqlTransaction )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;
			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "TitleSelectAll", connection, transaction,
					 CustomSqlHelper.CreateInputParameter( "IsPublished", SqlDbType.Bit, 1, false, false ) ) )
			{
				using ( CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>() )
				{
					CustomGenericList<Title> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

		/// <summary>
		/// Select all values from Title like a particular string.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <returns>Object of type Title.</returns>
		public CustomGenericList<Title> TitleSelectSearchName(
						SqlConnection sqlConnection,
						SqlTransaction sqlTransaction,
                        string name, 
                        string languageCode, 
                        int returnCount)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "TitleSelectSearchName", connection, transaction,
							CustomSqlHelper.CreateInputParameter( "Name", SqlDbType.VarChar, 1000, false, name ),
                            CustomSqlHelper.CreateInputParameter( "LanguageCode", SqlDbType.NVarChar, 10, false, languageCode),
                            CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount)))
			{
				using ( CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>() )
				{
					CustomGenericList<Title> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

		#endregion

		public CustomGenericList<Title> TitleSearch( SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			TitleSearchCriteria tsc )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(
				CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "TitleSearchPaging", connection, transaction,
				CustomSqlHelper.CreateInputParameter( "TitleID", SqlDbType.Int, null, true, tsc.TitleID ),
				CustomSqlHelper.CreateInputParameter( "MARCBibID", SqlDbType.NVarChar, 50, true, tsc.MARCBibID ),
				CustomSqlHelper.CreateInputParameter( "Title", SqlDbType.NVarChar, 255, true, tsc.Title ),
				CustomSqlHelper.CreateInputParameter( "StartRow", SqlDbType.BigInt, null, false, tsc.StartRow ),
				CustomSqlHelper.CreateInputParameter( "PageSize", SqlDbType.Int, null, false, tsc.PageSize ),
				CustomSqlHelper.CreateInputParameter( "OrderBy", SqlDbType.Int, null, false,
				(int)tsc.OrderBy * ( tsc.SortOrder == SortOrder.Ascending ? 1 : -1 ) ) ) )
			{
				using ( CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>() )
				{
					return helper.ExecuteReader( command );
				}
			}
		}

		public int TitleSearchCount( SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			TitleSearchCriteria tsc )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(
				CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "TitleSearchCount", connection, transaction,
				CustomSqlHelper.CreateInputParameter( "TitleID", SqlDbType.Int, null, true, tsc.TitleID ),
				CustomSqlHelper.CreateInputParameter( "MARCBibID", SqlDbType.NVarChar, 50, true, tsc.MARCBibID ),
				CustomSqlHelper.CreateInputParameter( "Title", SqlDbType.NVarChar, 255, true, tsc.Title ) ) )
			{
				using ( CustomSqlHelper<int> helper = new CustomSqlHelper<int>() )
				{
					CustomGenericList<int> k = helper.ExecuteReader( command );

					return k[ 0 ];
				}
			}
		}

        public CustomGenericList<CreatorTitle> TitleSimpleSelectByAuthor(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int authorId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSimpleSelectByAuthor", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AuthorId", SqlDbType.Int, null, false, authorId)))
            {
                using (CustomSqlHelper<CreatorTitle> helper = new CustomSqlHelper<CreatorTitle>())
                {
                    CustomGenericList<CreatorTitle> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }

        /// <summary>
        /// Select all Titles for the specified Item.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="itemID">Identifier of the item for which to get titles</param>
        /// <returns>Object of type Title.</returns>
        public CustomGenericList<Title> TitleSelectByItem(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction,
                        int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectByItem", connection, transaction,
                     CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
                {
                    CustomGenericList<Title> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select data for BibTex item citations for all Titles.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of type TitleBibTeX.</returns>
        public CustomGenericList<TitleBibTeX> TitleBibTeXSelectAllItemCitations(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleBibTeXSelectAllItemCitations", connection, transaction))
            {
                using (CustomSqlHelper<TitleBibTeX> helper = new CustomSqlHelper<TitleBibTeX>())
                {
                    CustomGenericList<TitleBibTeX> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select data for BibTex all title citations.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of type TitleBibTeX.</returns>
        public CustomGenericList<TitleBibTeX> TitleBibTeXSelectAllTitleCitations(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleBibTeXSelectAllTitleCitations", connection, transaction))
            {
                using (CustomSqlHelper<TitleBibTeX> helper = new CustomSqlHelper<TitleBibTeX>())
                {
                    CustomGenericList<TitleBibTeX> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select data for EndNote item citations for all Titles.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of type TitleEndNote.</returns>
        public CustomGenericList<TitleEndNote> TitleEndNoteSelectAllItemCitations(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleEndNoteSelectAllItemCitations", connection, transaction))
            {
                using (CustomSqlHelper<TitleEndNote> helper = new CustomSqlHelper<TitleEndNote>())
                {
                    CustomGenericList<TitleEndNote> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select data for EndNote all title citations.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of type TitleEndNote.</returns>
        public CustomGenericList<TitleEndNote> TitleEndNoteSelectAllTitleCitations(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleEndNoteSelectAllTitleCitations", connection, transaction))
            {
                using (CustomSqlHelper<TitleEndNote> helper = new CustomSqlHelper<TitleEndNote>())
                {
                    CustomGenericList<TitleEndNote> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select data for BibTex references for the specified Title.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="titleId">Title identifier for which to get BibTex data</param>
        /// <returns>List of type TitleBibTeX.</returns>
        public CustomGenericList<TitleBibTeX> TitleBibTeXSelectForTitleID(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction,
                        int titleId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleBibTeXSelectForTitleID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleId)))
            {
                using (CustomSqlHelper<TitleBibTeX> helper = new CustomSqlHelper<TitleBibTeX>())
                {
                    CustomGenericList<TitleBibTeX> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select data for EndNote reference for the specified Title.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="titleId">Title identifier for which to get EndNote data</param>
        /// <returns>List of type TitleEndNote.</returns>
        public CustomGenericList<TitleEndNote> TitleEndNoteSelectForTitleID(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction,
                        int titleId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleEndNoteSelectForTitleID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleId)))
            {
                using (CustomSqlHelper<TitleEndNote> helper = new CustomSqlHelper<TitleEndNote>())
                {
                    CustomGenericList<TitleEndNote> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public void Save(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Title title, int userId)
		{
			SqlConnection connection = sqlConnection;
			SqlTransaction transaction = sqlTransaction;

			if ( connection == null )
			{
				connection =
					CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ) );
			}

			bool isTransactionCoordinator = CustomSqlHelper.IsTransactionCoordinator( transaction );

			try
			{
				transaction = CustomSqlHelper.BeginTransaction( connection, transaction, isTransactionCoordinator );

                CustomDataAccessStatus<Title> updatedTitle = 
				    new TitleDAL().TitleManageAuto( connection, transaction, title, userId );

				if ( title.TitleAuthors.Count > 0 )
				{
					TitleAuthorDAL titleAuthorDAL = new TitleAuthorDAL();
					foreach ( TitleAuthor titleAuthor in title.TitleAuthors )
					{
                        if (titleAuthor.TitleID == 0) titleAuthor.TitleID = updatedTitle.ReturnObject.TitleID;
                        titleAuthorDAL.TitleAuthorManageAuto(connection, transaction, titleAuthor, userId);
					}
				}

                if (title.TitleKeywords.Count > 0)
                {
                    TitleKeywordDAL titleKeywordDAL = new TitleKeywordDAL();
                    KeywordDAL keywordDAL = new KeywordDAL();
                    foreach (TitleKeyword titleKeyword in title.TitleKeywords)
                    {
                        // If we have a newly entered keyword, insert it and/or get its ID
                        if (titleKeyword.KeywordID == 0)
                        {
                            Keyword keyword = keywordDAL.KeywordSelectByKeyword(connection, transaction, titleKeyword.Keyword);
                            if (keyword == null)
                            {
                                // Keyword not found, so insert a new one
                                keyword = new Keyword();
                                keyword.Keyword = titleKeyword.Keyword;
                                keyword.CreationUserID = userId;
                                keyword.CreationDate = DateTime.Now;
                                keyword.LastModifiedUserID = userId;
                                keyword.LastModifiedDate = DateTime.Now;
                                keyword.IsNew = true;
                                keyword = keywordDAL.KeywordInsertAuto(connection, transaction, keyword);
                            }
                            titleKeyword.KeywordID = keyword.KeywordID;
                        }

                        // Insert/Update the TitleKeyword record
                        if (titleKeyword.TitleID == 0) titleKeyword.TitleID = updatedTitle.ReturnObject.TitleID;
                        titleKeywordDAL.TitleKeywordManageAuto(connection, transaction, titleKeyword, userId);
                    }
                }

                if (title.TitleIdentifiers.Count > 0)
                {
                    Title_IdentifierDAL titleIdentifierDAL = new Title_IdentifierDAL();
                    foreach (Title_Identifier titleIdentifier in title.TitleIdentifiers)
                    {
                        if (titleIdentifier.TitleID == 0) titleIdentifier.TitleID = updatedTitle.ReturnObject.TitleID;
                        titleIdentifierDAL.Title_IdentifierManageAuto(connection, transaction, titleIdentifier, userId);
                    }
                }

                if (title.TitleAssociations.Count > 0)
                {
                    TitleAssociationDAL titleAssociationDAL = new TitleAssociationDAL();
                    foreach (TitleAssociation titleAssociation in title.TitleAssociations)
                    {
                        if (titleAssociation.TitleID == 0) titleAssociation.TitleID = updatedTitle.ReturnObject.TitleID;
                        new TitleAssociationDAL().Save(connection, transaction, titleAssociation, userId);
                    }
                }

                if (title.TitleVariants.Count > 0)
                {
                    TitleVariantDAL titleVariantDAL = new TitleVariantDAL();
                    foreach (TitleVariant titleVariant in title.TitleVariants)
                    {
                        if (titleVariant.TitleID == 0) titleVariant.TitleID = updatedTitle.ReturnObject.TitleID;
                        titleVariantDAL.TitleVariantManageAuto(connection, transaction, titleVariant, userId);
                    }
                }

                if (title.TitleLanguages.Count > 0)
                {
                    TitleLanguageDAL titleLanguageDAL = new TitleLanguageDAL();
                    foreach (TitleLanguage titleLanguage in title.TitleLanguages)
                    {
                        if (titleLanguage.TitleID == 0) titleLanguage.TitleID = updatedTitle.ReturnObject.TitleID;
                        titleLanguageDAL.TitleLanguageManageAuto(connection, transaction, titleLanguage, userId);
                    }
                }

				if ( title.TitleCollections.Count > 0 )
				{
					TitleCollectionDAL titleCollectionDAL = new TitleCollectionDAL();
					foreach ( TitleCollection titleCollection in title.TitleCollections )
					{
                        if (titleCollection.TitleID == 0) titleCollection.TitleID = updatedTitle.ReturnObject.TitleID;
						titleCollectionDAL.TitleCollectionManageAuto( connection, transaction, titleCollection );
					}
				}

				if ( title.TitleItems.Count > 0 )
				{
					ItemDAL itemDAL = new ItemDAL();
                    TitleItemDAL titleItemDAL = new TitleItemDAL();
					foreach ( TitleItem titleItem in title.TitleItems )
					{
                        // Update the item
                        if (titleItem.TitleID == 0) titleItem.TitleID = updatedTitle.ReturnObject.TitleID;
						titleItemDAL.TitleItemManageAuto( connection, transaction, titleItem, userId );
                        // Update the primary title id (stored on the Item table)
                        itemDAL.ItemUpdatePrimaryTitleID(connection, transaction, titleItem.ItemID, titleItem.PrimaryTitleID);
					}
				}

				CustomSqlHelper.CommitTransaction( transaction, isTransactionCoordinator );
			}
			catch ( Exception ex )
			{
				CustomSqlHelper.RollbackTransaction( transaction, isTransactionCoordinator );

                throw;
			}
			finally
			{
				CustomSqlHelper.CloseConnection( connection, isTransactionCoordinator );
			}

		}

        /// <summary>
        /// Returns a list of titles that have suspected character encoding problems.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="institutionCode">Institution for which to return titles</param>
        /// <param name="maxAge">Age in days of titles to consider (i.e. titles new in the last 30 days)</param>
        /// <returns></returns>
        public CustomGenericList<TitleSuspectCharacter> TitleSelectWithSuspectCharacters(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                String institutionCode,
                int maxAge)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectWithSuspectCharacters", connection, transaction,
                CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
                CustomSqlHelper.CreateInputParameter("MaxAge", SqlDbType.Int, null, false, maxAge)))
            {
                using (CustomSqlHelper<TitleSuspectCharacter> helper = new CustomSqlHelper<TitleSuspectCharacter>())
                {
                    CustomGenericList<TitleSuspectCharacter> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select titles associated with the specified collection
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="collectionID"></param>
        /// <returns></returns>
        public CustomGenericList<Title> TitleSelectByCollection(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                int collectionID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectByCollection", 
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID)))
            {
                using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
                {
                    CustomGenericList<Title> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<DOI> TitleSelectWithoutSubmittedDOI(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int numberToReturn)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectWithoutSubmittedDOI",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("NumberToReturn", SqlDbType.Int, null, false, numberToReturn)))
            {
                using (CustomSqlHelper<DOI> helper = new CustomSqlHelper<DOI>())
                {
                    CustomGenericList<DOI> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}
