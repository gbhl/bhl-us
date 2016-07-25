﻿using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.API.BHLApiDataObjects2;

namespace MOBOT.BHL.API.BHLApiDAL
{
    public class Api2DAL : IApi2DAL
    {
        #region Page methods

        public CustomGenericList<Name> NamePageSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NamePageSelectByPageID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
            {
                using (CustomSqlHelper<Name> helper = new CustomSqlHelper<Name>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public Page PageSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageSelectAuto", connection, transaction,
                CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
            {
                using (CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>())
                {
                    CustomGenericList<Page> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public CustomGenericList<PageNumber> IndicatedPageSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IndicatedPageSelectByPageID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
            {
                using (CustomSqlHelper<PageNumber> helper = new CustomSqlHelper<PageNumber>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<PageType> PageTypeSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTypeSelectByPageID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
            {
                using (CustomSqlHelper<PageType> helper = new CustomSqlHelper<PageType>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        #endregion Page methods

        #region Item methods

        public Item ItemSelectByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemID)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiItemSelectByItemID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    CustomGenericList<Item> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public Item ItemSelectByBarcode(SqlConnection sqlConnection, SqlTransaction sqlTransaction, String barcode)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null) connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiItemSelectByBarcode", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Barcode", SqlDbType.NVarChar, 40, false, barcode)))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    CustomGenericList<Item> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public CustomGenericList<PageDetail> PageSelectByItemID(SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction, int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiPageSelectByItemID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<PageDetail> helper = new CustomSqlHelper<PageDetail>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<Item> ItemSelectUnpublished(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiItemSelectUnpublished", connection, transaction))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    CustomGenericList<Item> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<Part> SegmentSelectByItemID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSegmentSelectByItemID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        #endregion Item methods

        #region Title methods

        public Title TitleSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiTitleSelectAuto", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
                {
                    CustomGenericList<Title> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public CustomGenericList<Item> ItemSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiItemSelectByTitleID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleId", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<Creator> AuthorSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiAuthorSelectByTitleID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<Creator> helper = new CustomSqlHelper<Creator>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<TitleVariant> TitleVariantSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiTitleVariantSelectByTitleID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<TitleVariant> helper = new CustomSqlHelper<TitleVariant>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<TitleIdentifier> TitleIdentifierSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiTitleIdentifierSelectByTitleID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<TitleIdentifier> helper = new CustomSqlHelper<TitleIdentifier>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<TitleNote> TitleNoteSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleNoteSelectByTitleID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<TitleNote> helper = new CustomSqlHelper<TitleNote>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<Subject> SubjectSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleKeywordSelectByTitleID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<Subject> helper = new CustomSqlHelper<Subject>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<Title> TitleSelectByIdentifier(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            string identifierName, string identifierValue)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiTitleSelectByIdentifier", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("IdentifierName", SqlDbType.NVarChar, 40, false, identifierName),
                    CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 145, false, identifierValue)))
            {
                using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<Title> TitleSelectByDOI(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string doi)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiTitleSelectByDOI", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("DOIName", SqlDbType.NVarChar, 50, false, doi)))
            {
                using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<Title> TitleSelectSearchSimple(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                string title)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiTitleSelectSearchSimple", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("FullTitle", SqlDbType.VarChar, 1000, false, title)))
            {
                using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
                {
                    CustomGenericList<Title> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Execute a simple full-text search of titles (only the FullTitle field is searched)
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public CustomGenericList<Title> SearchTitleSimple(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                string title)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSearchTitleSimple", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("FullTitle", SqlDbType.VarChar, 4000, false, title)))
            {
                using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
                {
                    CustomGenericList<Title> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<Title> TitleSelectUnpublished(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiTitleSelectUnpublished", connection, transaction))
            {
                using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
                {
                    CustomGenericList<Title> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        #endregion Title methods

        #region Segment methods

        public Part SegmentSelectForSegmentID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int segmentId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectForSegmentID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentId)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    CustomGenericList<Part> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return (list[0]);
                    else
                        return null;
                }
            }
        }

        public CustomGenericList<Name> NameSegmentSelectBySegmentID(SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiNameSegmentSelectBySegmentID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<Name> helper = new CustomSqlHelper<Name>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<Creator> AuthorSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiAuthorSelectBySegmentID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<Creator> helper = new CustomSqlHelper<Creator>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<PartIdentifier> SegmentIdentifierSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSegmentIdentifierSelectBySegmentID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<PartIdentifier> helper = new CustomSqlHelper<PartIdentifier>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<Subject> SubjectSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentKeywordSelectBySegmentID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<Subject> helper = new CustomSqlHelper<Subject>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<PageDetail> PageSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiPageSelectBySegmentID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<PageDetail> helper = new CustomSqlHelper<PageDetail>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<Part> SegmentSelectRelated(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSegmentSelectRelated", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<Part> SegmentSelectUnpublished(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSegmentSelectUnpublished", connection, transaction))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    CustomGenericList<Part> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<Part> SegmentSelectByIdentifier(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string identifierName, string identifierValue)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSegmentSelectByIdentifier", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("IdentifierName", SqlDbType.NVarChar, 40, false, identifierName),
                    CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<Part> SegmentSelectByDOI(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string doi)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSegmentSelectByDOI", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("DOIName", SqlDbType.NVarChar, 50, false, doi)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        #endregion Segment methods

        #region Subject methods

        public CustomGenericList<Subject> TitleKeywordSelectLikeTag(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string subject
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleKeywordSelectLikeTag", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("Tag", SqlDbType.NVarChar, 50, false, subject),
                    CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, ""),
                    CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, 1000000)))
            {
                using (CustomSqlHelper<Subject> helper = new CustomSqlHelper<Subject>())
                {
                    CustomGenericList<Subject> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Full-text search for the specified title keyword.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="tag"></param>
        /// <param name="languageCode"></param>
        /// <param name="returnCount"></param>
        /// <returns></returns>
        public CustomGenericList<Subject> SearchTitleKeyword(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string tag)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchTitleKeyword", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 300, false, tag),
                    CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, ""),
                    CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, 1000000)))
            {
                using (CustomSqlHelper<Subject> helper = new CustomSqlHelper<Subject>())
                {
                    CustomGenericList<Subject> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<Title> TitleSelectByKeyword(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string subject
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiTitleSelectByKeyword", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 50, false, subject)))
            {
                using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
                {
                    CustomGenericList<Title> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<Part> SegmentSelectByKeyword(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string subject
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSegmentSelectByKeyword", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 50, false, subject)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    CustomGenericList<Part> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        #endregion Subject methods

        #region Author methods

        /// <summary>
        /// Select all Creators starting with a certain letter (or letters).
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of objects of type Creator.</returns>
        public CustomGenericList<Creator> AuthorSelectNameStartsWith(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string name)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiAuthorSelectNameStartsWith", connection, transaction,
                CustomSqlHelper.CreateInputParameter("FullName", SqlDbType.NVarChar, 255, false, name)))
            {
                using (CustomSqlHelper<Creator> helper = new CustomSqlHelper<Creator>())
                {
                    CustomGenericList<Creator> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Execute a full-text search of creators.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of objects of type Creator.</returns>
        public CustomGenericList<Creator> SearchAuthor(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string name)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSearchAuthor", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AuthorName", SqlDbType.NVarChar, 4000, false, name)))
            {
                using (CustomSqlHelper<Creator> helper = new CustomSqlHelper<Creator>())
                {
                    CustomGenericList<Creator> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<Title> TitleSelectByAuthor(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int authorID
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiTitleSelectByAuthor", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID)))
            {
                using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
                {
                    CustomGenericList<Title> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<Part> SegmentSelectByAuthor(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int authorID
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSegmentSelectByAuthor", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    CustomGenericList<Part> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        #endregion Author methods

        #region Name methods

        public int NameResolvedCountUnique(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiNameResolvedCountUnique", connection, transaction))
            {
                using (CustomSqlHelper<int> helper = new CustomSqlHelper<int>())
                {
                    CustomGenericList<int> list = helper.ExecuteReader(command);

                    if (list.Count == 0)
                    {
                        return default(int);
                    }
                    else
                    {
                        return list[0];
                    }
                }
            }
        }

        public int NameResolvedCountUniqueBetweenDates(
            SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction,
            DateTime startDate, 
            DateTime endDate)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiNameResolvedCountUniqueBetweenDates", connection, transaction,
                CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.DateTime, null, false, startDate),
                CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.DateTime, null, false, endDate)))
            {
                using (CustomSqlHelper<int> helper = new CustomSqlHelper<int>())
                {
                    CustomGenericList<int> list = helper.ExecuteReader(command);

                    if (list.Count == 0)
                    {
                        return default(int);
                    }
                    else
                    {
                        return list[0];
                    }
                }
            }
        }

        public CustomGenericList<Name> NameResolvedListActive(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int startRow, int batchSize)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiNameResolvedListActive", connection, transaction,
                CustomSqlHelper.CreateInputParameter("StartRow", SqlDbType.Int, null, false, startRow),
                CustomSqlHelper.CreateInputParameter("BatchSize", SqlDbType.Int, null, false, batchSize)))
            {
                using (CustomSqlHelper<Name> helper = new CustomSqlHelper<Name>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<Name> NameResolvedListActiveBetweenDates(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int startRow, int batchSize, DateTime startDate, DateTime endDate)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiNameResolvedListActiveBetweenDates", connection, transaction,
                CustomSqlHelper.CreateInputParameter("StartRow", SqlDbType.Int, null, false, startRow),
                CustomSqlHelper.CreateInputParameter("BatchSize", SqlDbType.Int, null, false, batchSize),
                CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.DateTime, null, false, startDate),
                CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.DateTime, null, false, endDate)))
            {
                using (CustomSqlHelper<Name> helper = new CustomSqlHelper<Name>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<Name> NameResolvedSelectByNameLike(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string name)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiNameResolvedSelectByNameLike", connection, transaction,
                CustomSqlHelper.CreateInputParameter("NameConfirmed", SqlDbType.NVarChar, 100, false, name),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, 1000000)))
            {
                using (CustomSqlHelper<Name> helper = new CustomSqlHelper<Name>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<PageDetail> PageSelectByNameBankID(SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction, string nameBankID)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiPageSelectByNameBankID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("NameBankID", SqlDbType.NVarChar, 100, false, nameBankID)))
            {
                using (CustomSqlHelper<PageDetail> helper = new CustomSqlHelper<PageDetail>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<PageDetail> PageSelectByNameConfirmed(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string nameConfirmed)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null) connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiPageSelectByNameConfirmed", connection, transaction,
                CustomSqlHelper.CreateInputParameter("NameConfirmed", SqlDbType.NVarChar, 100, false, nameConfirmed)))
            {
                using (CustomSqlHelper<PageDetail> helper = new CustomSqlHelper<PageDetail>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        #endregion Name methods

        #region Language methods

        public CustomGenericList<Language> LanguageSelectWithPublishedItems(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("LanguageSelectWithPublishedItems", connection, transaction))
            {
                using (CustomSqlHelper<Language> helper = new CustomSqlHelper<Language>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        #endregion Language methods

        #region Collection methods

        public CustomGenericList<Collection> CollectionSelectActive(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectActive", connection, transaction))
            {
                using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<Collection> CollectionSelectByTitleID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectByTitleID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<Collection> CollectionSelectByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectByItemID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        #endregion Collection methods

        #region Search methods

        public CustomGenericList<SearchBookResult> SearchBook(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string authorLastName, string volume, string edition, int? year, string subject, string languageCode, 
            int? collectionID, int returnCount)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchBook", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
                CustomSqlHelper.CreateInputParameter("AuthorLastName", SqlDbType.NVarChar, 255, false, authorLastName),
                CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
                CustomSqlHelper.CreateInputParameter("Edition", SqlDbType.NVarChar, 450, false, edition),
                CustomSqlHelper.CreateInputParameter("Year", SqlDbType.SmallInt, null, true, year),
                CustomSqlHelper.CreateInputParameter("Subject", SqlDbType.NVarChar, 50, false, subject),
                CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode),
                CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, true, collectionID),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    CustomGenericList<SearchBookResult> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Call the database procedure that implements the multiple-field full-text book search.
        /// </summary>
        public CustomGenericList<SearchBookResult> SearchBookFullText(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string authorLastName, string volume, string edition, int? year, string subject, string languageCode, 
            int? collectionID, int returnCount)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchBookFT", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
                CustomSqlHelper.CreateInputParameter("AuthorLastName", SqlDbType.NVarChar, 255, false, authorLastName),
                CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
                CustomSqlHelper.CreateInputParameter("Edition", SqlDbType.NVarChar, 450, false, edition),
                CustomSqlHelper.CreateInputParameter("Year", SqlDbType.SmallInt, null, true, year),
                CustomSqlHelper.CreateInputParameter("Subject", SqlDbType.NVarChar, 50, false, subject),
                CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode),
                CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, true, collectionID),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    CustomGenericList<SearchBookResult> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<Part> SearchSegment(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string containerTitle, string author, string date, string volume, string series, string issue, 
            int returnCount, string sortBy)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchSegment", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
                CustomSqlHelper.CreateInputParameter("ContainerTitle", SqlDbType.NVarChar, 2000, false, containerTitle),
                CustomSqlHelper.CreateInputParameter("Author", SqlDbType.NVarChar, 2000, false, author),
                CustomSqlHelper.CreateInputParameter("Date", SqlDbType.NVarChar, 20, false, date),
                CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
                CustomSqlHelper.CreateInputParameter("Series", SqlDbType.NVarChar, 100, false, series),
                CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 100, true, issue),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount),
                CustomSqlHelper.CreateInputParameter("SortBy", SqlDbType.NVarChar, 50, true, sortBy),
                CustomSqlHelper.CreateInputParameter("IncludeNoContent", SqlDbType.SmallInt, null, false, 1)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    CustomGenericList<Part> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Call the database procedure that implements the multiple-field full-text book search.
        /// </summary>
        public CustomGenericList<Part> SearchSegmentFullText(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string containerTitle, string author, string date, string volume, string series, string issue,
            int returnCount, string sortBy)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchSegmentAdvancedFT", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
                CustomSqlHelper.CreateInputParameter("ContainerTitle", SqlDbType.NVarChar, 2000, false, containerTitle),
                CustomSqlHelper.CreateInputParameter("Author", SqlDbType.NVarChar, 2000, false, author),
                CustomSqlHelper.CreateInputParameter("Date", SqlDbType.NVarChar, 20, false, date),
                CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
                CustomSqlHelper.CreateInputParameter("Series", SqlDbType.NVarChar, 100, false, series),
                CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 100, true, issue),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount),
                CustomSqlHelper.CreateInputParameter("SortBy", SqlDbType.NVarChar, 50, true, sortBy),
                CustomSqlHelper.CreateInputParameter("IncludeNoContent", SqlDbType.SmallInt, null, true, 1)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    CustomGenericList<Part> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        #endregion Search methods

        #region Validation methods

        public ApiKey ApiKeySelectByKey(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            Guid keyValue)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiKeySelectByKey", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ApiKeyValue", SqlDbType.UniqueIdentifier, null, false, keyValue)))
            {
                using (CustomSqlHelper<ApiKey> helper = new CustomSqlHelper<ApiKey>())
                {
                    CustomGenericList<ApiKey> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        #endregion Validation methods

        #region Stats methods

        public Stats StatsSelect(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiStatsSelect", connection, transaction))
            {
                CustomGenericList<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                if (list.Count > 0)
                {
                    CustomDataRow row = list[0];
                    Stats stats = new Stats();
                    stats.TitleCount = (int)row["TitleCount"].Value;
                    stats.ItemCount = (int)row["ItemCount"].Value;
                    stats.PageCount = (int)row["PageCount"].Value;
                    stats.PartCount = (int)row["PartCount"].Value;
                    return stats;
                }
                else
                {
                    return null;
                }

            }
        }

        #endregion Stats methods

        #region Institution methods

        public CustomGenericList<Contributor> InstitutionSelectBySegmentIDAndRole(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int segmentID,
            string institutionRoleName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionSelectBySegmentIDAndRole", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
                CustomSqlHelper.CreateInputParameter("InstitutionRoleName", SqlDbType.NVarChar, 100, false, institutionRoleName)))
            {
                using (CustomSqlHelper<Contributor> helper = new CustomSqlHelper<Contributor>())
                {
                    CustomGenericList<Contributor> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<Contributor> InstitutionSelectByItemIDAndRole(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int itemID,
            string institutionRoleName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionSelectByItemIDAndRole", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                CustomSqlHelper.CreateInputParameter("InstitutionRoleName", SqlDbType.NVarChar, 100, false, institutionRoleName)))
            {
                using (CustomSqlHelper<Contributor> helper = new CustomSqlHelper<Contributor>())
                {
                    CustomGenericList<Contributor> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        #endregion Institution methods
    }
}
