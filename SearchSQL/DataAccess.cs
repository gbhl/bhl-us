using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHL.Search.SQL
{
    internal class DataAccess
    {
        // Look at this article for paging SQL result sets
        // https://sqlperformance.com/2015/01/t-sql-queries/pagination-with-offset-fetch


        private readonly string _connectionString = string.Empty;

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool IsOnline()
        {
            bool online = true;

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "dbo.SearchCatalogCheckStatus";
                    SqlParameter retval = sqlCommand.Parameters.Add("@Status", SqlDbType.Int);
                    retval.Direction = ParameterDirection.ReturnValue;

                    sqlCommand.ExecuteNonQuery();
                    online = (Int32)sqlCommand.Parameters["@Status"].Value == 1;
                }
            }
            catch (Exception ex)
            {
                ProcessError(ex);
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed) sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return online;
        }

        public List<IHit> SearchItem(string title, string author, string volume, string year, string subject,
            string languageCode, string collectionID, string notes, out long totalHits, int startPage = 1, int pageSize = 10)
        {
            List<IHit> pubs = new List<IHit>();
            totalHits = 0;

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {

                    Int32.TryParse(year, out int yearNum);
                    Int32.TryParse(collectionID, out int collectionNum);

                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "srchindex.SearchPublicationAdvanced";
                    SqlParameter paramTitle = sqlCommand.Parameters.Add("@Title", SqlDbType.NVarChar, 2000);
                    paramTitle.Value = title;
                    SqlParameter paramAuthor = sqlCommand.Parameters.Add("@AuthorLastName", SqlDbType.NVarChar, 255);
                    paramAuthor.Value = author;
                    SqlParameter paramVolume = sqlCommand.Parameters.Add("@Volume", SqlDbType.NVarChar, 100);
                    paramVolume.Value = volume;
                    SqlParameter paramYear = sqlCommand.Parameters.Add("@Year", SqlDbType.SmallInt);
                    if (yearNum == 0)
                        paramYear.Value = null;
                    else
                        paramYear.Value = yearNum;
                    SqlParameter paramSubject = sqlCommand.Parameters.Add("@Subject", SqlDbType.NVarChar, 50);
                    paramSubject.Value = subject;
                    SqlParameter paramLanguage = sqlCommand.Parameters.Add("@LanguageCode", SqlDbType.NVarChar, 10);
                    paramLanguage.Value = languageCode;
                    SqlParameter paramCollection = sqlCommand.Parameters.Add("@CollectionID", SqlDbType.Int);
                    if (collectionNum == 0)
                        paramCollection.Value = null;
                    else
                        paramCollection.Value = collectionNum;
                    SqlParameter paramNotes = sqlCommand.Parameters.Add("@NoteText", SqlDbType.NVarChar, 1073741823);
                    paramNotes.Value = notes;
                    SqlParameter paramStartPage = sqlCommand.Parameters.Add("@StartPage", SqlDbType.Int);
                    paramStartPage.Value = startPage;
                    SqlParameter paramPageSize = sqlCommand.Parameters.Add("@PageSize", SqlDbType.Int);
                    paramPageSize.Value = pageSize;

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ItemHit hit = new ItemHit();
                            totalHits = reader.GetInt32(reader.GetOrdinal(SQLField.TOTALHITS));
                            hit.Id = (reader.IsDBNull(reader.GetOrdinal(SQLField.ITEMID)) ? 
                                reader.GetInt32(reader.GetOrdinal(SQLField.SEGMENTID)) : 
                                reader.GetInt32(reader.GetOrdinal(SQLField.ITEMID))).ToString();
                            if (!reader.IsDBNull(reader.GetOrdinal(SQLField.TITLEID))) hit.TitleId = reader.GetInt32(reader.GetOrdinal(SQLField.TITLEID));
                            if (!reader.IsDBNull(reader.GetOrdinal(SQLField.ITEMID))) hit.ItemId = reader.GetInt32(reader.GetOrdinal(SQLField.ITEMID));
                            if (!reader.IsDBNull(reader.GetOrdinal(SQLField.SEGMENTID))) hit.SegmentId = reader.GetInt32(reader.GetOrdinal(SQLField.SEGMENTID));
                            if (!reader.IsDBNull(reader.GetOrdinal(SQLField.BOOKISVIRTUAL))) hit.BookIsVirtual = (reader.GetByte(reader.GetOrdinal(SQLField.BOOKISVIRTUAL)) == 1);
                            hit.Title = reader.GetString(reader.GetOrdinal(SQLField.TITLE));
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.AUTHORS))))
                            {
                                hit.Authors = reader.GetString(reader.GetOrdinal(SQLField.AUTHORS)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.SEARCHAUTHORS))))
                            {
                                hit.SearchAuthors = reader.GetString(reader.GetOrdinal(SQLField.SEARCHAUTHORS)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.KEYWORDS))))
                            {
                                hit.Keywords = reader.GetString(reader.GetOrdinal(SQLField.KEYWORDS)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.ASSOCIATIONS))))
                            {
                                hit.Associations = reader.GetString(reader.GetOrdinal(SQLField.ASSOCIATIONS)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.VARIANTS))))
                            {
                                hit.Variants = reader.GetString(reader.GetOrdinal(SQLField.VARIANTS)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.CONTRIBUTORS))))
                            {
                                hit.Contributors = reader.GetString(reader.GetOrdinal(SQLField.CONTRIBUTORS)).Split('|').ToList();
                            }
                            hit.Volume = reader.GetString(reader.GetOrdinal(SQLField.VOLUME));
                            hit.Publisher = reader.GetString(reader.GetOrdinal(SQLField.PUBLISHER));
                            hit.PublicationPlace = reader.GetString(reader.GetOrdinal(SQLField.PUBLICATIONPLACE));
                            hit.HasSegments = reader.GetInt16(reader.GetOrdinal(SQLField.HASSEGMENTS)) == 1;
                            hit.HasLocalContent = reader.GetInt16(reader.GetOrdinal(SQLField.HASLOCALCONTENT)) == 1;
                            hit.HasExternalContent = reader.GetInt16(reader.GetOrdinal(SQLField.HASEXTERNALCONTENT)) == 1;
                            hit.UniformTitle = reader.GetString(reader.GetOrdinal(SQLField.UNIFORMTITLE));
                            hit.SortTitle = reader.GetString(reader.GetOrdinal(SQLField.SORTTITLE));
                            hit.Language = reader.GetString(reader.GetOrdinal(SQLField.LANGUAGE));
                            hit.Genre = reader.GetString(reader.GetOrdinal(SQLField.GENRE));
                            hit.MaterialType = reader.GetString(reader.GetOrdinal(SQLField.MATERIALTYPE));
                            hit.Doi = reader.GetString(reader.GetOrdinal(SQLField.DOI));
                            hit.Url = reader.GetString(reader.GetOrdinal(SQLField.URL));
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.OCLC))))
                            {
                                hit.Oclc = reader.GetString(reader.GetOrdinal(SQLField.OCLC)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.ISSN))))
                            {
                                hit.Issn = reader.GetString(reader.GetOrdinal(SQLField.ISSN)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.ISBN))))
                            {
                                hit.Isbn = reader.GetString(reader.GetOrdinal(SQLField.ISBN)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.COLLECTIONS))))
                            {
                                hit.Collections = reader.GetString(reader.GetOrdinal(SQLField.COLLECTIONS)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.DATES))))
                            {
                                hit.Dates = reader.GetString(reader.GetOrdinal(SQLField.DATES)).Split('|').ToList();
                            }
                            hit.Container = reader.GetString(reader.GetOrdinal(SQLField.CONTAINER));
                            if (!reader.IsDBNull(reader.GetOrdinal(SQLField.STARTPAGEID))) hit.StartPageId = reader.GetInt32(reader.GetOrdinal(SQLField.STARTPAGEID));
                            hit.PageRange = reader.GetString(reader.GetOrdinal(SQLField.PAGERANGE));
                            hit.Score = reader.GetInt32(reader.GetOrdinal(SQLField.SCORE));
                            pubs.Add(hit);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessError(ex);
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed) sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return pubs;
        }

        public List<IHit> SearchItem(string searchTerm, out long totalHits, int startPage = 1, int pageSize = 10)
        {
            List<IHit> pubs = new List<IHit>();
            totalHits = 0;

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {

                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "srchindex.SearchPublication";
                    SqlParameter paramText = sqlCommand.Parameters.Add("@SearchText", SqlDbType.NVarChar, 2000);
                    paramText.Value = searchTerm;
                    SqlParameter paramStartPage = sqlCommand.Parameters.Add("@StartPage", SqlDbType.Int);
                    paramStartPage.Value = startPage;
                    SqlParameter paramPageSize = sqlCommand.Parameters.Add("@PageSize", SqlDbType.Int);
                    paramPageSize.Value = pageSize;

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ItemHit hit = new ItemHit();
                            totalHits = reader.GetInt32(reader.GetOrdinal(SQLField.TOTALHITS));
                            hit.Id = (reader.IsDBNull(reader.GetOrdinal(SQLField.ITEMID)) ?
                                reader.GetInt32(reader.GetOrdinal(SQLField.SEGMENTID)) :
                                reader.GetInt32(reader.GetOrdinal(SQLField.ITEMID))).ToString();
                            if (!reader.IsDBNull(reader.GetOrdinal(SQLField.TITLEID))) hit.TitleId = reader.GetInt32(reader.GetOrdinal(SQLField.TITLEID));
                            if (!reader.IsDBNull(reader.GetOrdinal(SQLField.ITEMID))) hit.ItemId = reader.GetInt32(reader.GetOrdinal(SQLField.ITEMID));
                            if (!reader.IsDBNull(reader.GetOrdinal(SQLField.SEGMENTID))) hit.SegmentId = reader.GetInt32(reader.GetOrdinal(SQLField.SEGMENTID));
                            if (!reader.IsDBNull(reader.GetOrdinal(SQLField.BOOKISVIRTUAL))) hit.BookIsVirtual = (reader.GetByte(reader.GetOrdinal(SQLField.BOOKISVIRTUAL)) == 1);
                            hit.Title = reader.GetString(reader.GetOrdinal(SQLField.TITLE));
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.AUTHORS))))
                            {
                                hit.Authors = reader.GetString(reader.GetOrdinal(SQLField.AUTHORS)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.SEARCHAUTHORS))))
                            {
                                hit.SearchAuthors = reader.GetString(reader.GetOrdinal(SQLField.SEARCHAUTHORS)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.KEYWORDS))))
                            {
                                hit.Keywords = reader.GetString(reader.GetOrdinal(SQLField.KEYWORDS)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.ASSOCIATIONS))))
                            {
                                hit.Associations = reader.GetString(reader.GetOrdinal(SQLField.ASSOCIATIONS)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.VARIANTS))))
                            {
                                hit.Variants = reader.GetString(reader.GetOrdinal(SQLField.VARIANTS)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.CONTRIBUTORS))))
                            {
                                hit.Contributors = reader.GetString(reader.GetOrdinal(SQLField.CONTRIBUTORS)).Split('|').ToList();
                            }
                            hit.Volume = reader.GetString(reader.GetOrdinal(SQLField.VOLUME));
                            hit.Publisher = reader.GetString(reader.GetOrdinal(SQLField.PUBLISHER));
                            hit.PublicationPlace = reader.GetString(reader.GetOrdinal(SQLField.PUBLICATIONPLACE));
                            hit.HasSegments = reader.GetInt16(reader.GetOrdinal(SQLField.HASSEGMENTS)) == 1;
                            hit.HasLocalContent = reader.GetInt16(reader.GetOrdinal(SQLField.HASLOCALCONTENT)) == 1;
                            hit.HasExternalContent = reader.GetInt16(reader.GetOrdinal(SQLField.HASEXTERNALCONTENT)) == 1;
                            hit.UniformTitle = reader.GetString(reader.GetOrdinal(SQLField.UNIFORMTITLE));
                            hit.SortTitle = reader.GetString(reader.GetOrdinal(SQLField.SORTTITLE));
                            hit.Language = reader.GetString(reader.GetOrdinal(SQLField.LANGUAGE));
                            hit.Genre = reader.GetString(reader.GetOrdinal(SQLField.GENRE));
                            hit.MaterialType = reader.GetString(reader.GetOrdinal(SQLField.MATERIALTYPE));
                            hit.Doi = reader.GetString(reader.GetOrdinal(SQLField.DOI));
                            hit.Url = reader.GetString(reader.GetOrdinal(SQLField.URL));
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.OCLC))))
                            {
                                hit.Oclc = reader.GetString(reader.GetOrdinal(SQLField.OCLC)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.ISSN))))
                            {
                                hit.Issn = reader.GetString(reader.GetOrdinal(SQLField.ISSN)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.ISBN))))
                            {
                                hit.Isbn = reader.GetString(reader.GetOrdinal(SQLField.ISBN)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.COLLECTIONS))))
                            {
                                hit.Collections = reader.GetString(reader.GetOrdinal(SQLField.COLLECTIONS)).Split('|').ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal(SQLField.DATES))))
                            {
                                hit.Dates = reader.GetString(reader.GetOrdinal(SQLField.DATES)).Split('|').ToList();
                            }
                            hit.Container = reader.GetString(reader.GetOrdinal(SQLField.CONTAINER));
                            if (!reader.IsDBNull(reader.GetOrdinal(SQLField.STARTPAGEID))) hit.StartPageId = reader.GetInt32(reader.GetOrdinal(SQLField.STARTPAGEID));
                            hit.PageRange = reader.GetString(reader.GetOrdinal(SQLField.PAGERANGE));
                            hit.Score = reader.GetInt32(reader.GetOrdinal(SQLField.SCORE));
                            pubs.Add(hit);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessError(ex);
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed) sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return pubs;
        }

        public List<IHit> SearchAuthor(string searchTerm, out long totalHits, int startPage = 1, int pageSize = 10)
        {
            List<IHit> authors = new List<IHit>();
            totalHits = 0;

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "srchindex.SearchAuthor";
                    SqlParameter paramAuthorName = sqlCommand.Parameters.Add("@AuthorName", SqlDbType.NVarChar, 300);
                    paramAuthorName.Value = searchTerm;
                    SqlParameter paramStartPage = sqlCommand.Parameters.Add("@StartPage", SqlDbType.Int);
                    paramStartPage.Value = startPage;
                    SqlParameter paramPageSize = sqlCommand.Parameters.Add("@PageSize", SqlDbType.Int);
                    paramPageSize.Value = pageSize;

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AuthorHit hit = new AuthorHit();
                            totalHits = reader.GetInt32(reader.GetOrdinal(SQLField.TOTALHITS));
                            hit.Id = reader.GetInt32(reader.GetOrdinal(SQLField.AUTHORID)).ToString();
                            hit.PrimaryAuthorName = reader.GetString(reader.GetOrdinal(SQLField.PRIMARYAUTHORNAME));
                            authors.Add(hit);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ProcessError(ex);
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed) sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return authors;
        }

        public List<IHit> SearchKeyword(string searchTerm, out long totalHits, int startPage = 1, int pageSize = 10)
        {
            List<IHit> keywords = new List<IHit>();
            totalHits = 0;

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "srchindex.SearchKeyword";
                    SqlParameter paramKeyword = sqlCommand.Parameters.Add("@Keyword", SqlDbType.NVarChar, 300);
                    paramKeyword.Value = searchTerm;
                    SqlParameter paramStartPage = sqlCommand.Parameters.Add("@StartPage", SqlDbType.Int);
                    paramStartPage.Value = startPage;
                    SqlParameter paramPageSize = sqlCommand.Parameters.Add("@PageSize", SqlDbType.Int);
                    paramPageSize.Value = pageSize;

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            KeywordHit hit = new KeywordHit();
                            totalHits = reader.GetInt32(reader.GetOrdinal(SQLField.TOTALHITS));
                            hit.Id = reader.GetInt32(reader.GetOrdinal(SQLField.KEYWORDID)).ToString();
                            hit.Keyword = reader.GetString(reader.GetOrdinal(SQLField.KEYWORD));
                            keywords.Add(hit);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessError(ex);
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed) sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return keywords;
        }

        public List<IHit> SearchName(string searchTerm, out long totalHits, int startPage = 1, int pageSize = 10)
        {
            List<IHit> names = new List<IHit>();
            totalHits = 0;

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "srchindex.SearchName";
                    SqlParameter paramName = sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 100);
                    paramName.Value = searchTerm;
                    SqlParameter paramStartPage = sqlCommand.Parameters.Add("@StartPage", SqlDbType.Int);
                    paramStartPage.Value = startPage;
                    SqlParameter paramPageSize = sqlCommand.Parameters.Add("@PageSize", SqlDbType.Int);
                    paramPageSize.Value = pageSize;

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NameHit hit = new NameHit();
                            totalHits = reader.GetInt32(reader.GetOrdinal(SQLField.TOTALHITS));
                            hit.Id = reader.GetInt32(reader.GetOrdinal(SQLField.NAMERESOLVEDID)).ToString();
                            hit.Name = reader.GetString(reader.GetOrdinal(SQLField.RESOLVEDNAMESTRING));
                            hit.Count = reader.GetInt32(reader.GetOrdinal(SQLField.NAMECOUNT));
                            names.Add(hit);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessError(ex);
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed) sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return names;
        }

        private void ProcessError(Exception ex)
        {
            throw new SearchException(string.Format("{0}\r{1}", ex.Message, ex.StackTrace));
        }
    }
}
