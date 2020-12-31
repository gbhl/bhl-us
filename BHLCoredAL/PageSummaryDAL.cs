#region using

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
  public partial class PageSummaryDAL
  {
    public List<PageSummaryView> PageSummarySelectByItemId( SqlConnection sqlConnection, SqlTransaction sqlTransaction,
     int itemId)
    {
      SqlConnection connection = CustomSqlHelper.CreateConnection( 
        CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
      SqlTransaction transaction = sqlTransaction;

      using ( SqlCommand command = CustomSqlHelper.CreateCommand( "PageSummarySelectByItemID", connection, transaction,
          CustomSqlHelper.CreateInputParameter( "ItemID", SqlDbType.Int, null, false, itemId ) ) )
      {
        using ( CustomSqlHelper<PageSummaryView> helper = new CustomSqlHelper<PageSummaryView>() )
        {
          List<PageSummaryView> list = helper.ExecuteReader( command );
          if ( list.Count > 0 )
          {
              return list;
          }
          else
          {
            return null;
          }
        }
      }
    }

    public List<PageSummaryView> PageSummarySelectAllByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
        int itemId)
    {
        SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
        SqlTransaction transaction = sqlTransaction;

        using (SqlCommand command = CustomSqlHelper.CreateCommand("PageSummarySelectAllByItemID", connection, transaction,
            CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemId)))
        {
            using (CustomSqlHelper<PageSummaryView> helper = new CustomSqlHelper<PageSummaryView>())
            {
                return helper.ExecuteReader(command);
            }
        }
    }

    public PageSummaryView PageSummarySelectByItemIdAndTitleId(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
        int itemId, int titleId)
    {
        SqlConnection connection = CustomSqlHelper.CreateConnection(
          CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
        SqlTransaction transaction = sqlTransaction;

        using (SqlCommand command = CustomSqlHelper.CreateCommand("PageSummarySelectByItemIDAndTitleID", 
            connection, transaction,
            CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemId),
            CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleId)))
        {
            using (CustomSqlHelper<PageSummaryView> helper = new CustomSqlHelper<PageSummaryView>())
            {
                List<PageSummaryView> list = helper.ExecuteReader(command);
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

    public List<PageSummaryView> PageSummarySelectForViewerByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
        int itemId)
    {
        SqlConnection connection = CustomSqlHelper.CreateConnection(
          CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
        SqlTransaction transaction = sqlTransaction;

        using (SqlCommand command = CustomSqlHelper.CreateCommand("PageSummarySelectForViewerByItemID", connection, transaction,
            CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemId)))
        {
            using (CustomSqlHelper<PageSummaryView> helper = new CustomSqlHelper<PageSummaryView>())
            {
                List<PageSummaryView> list = helper.ExecuteReader(command);
                if (list.Count > 0)
                {
                    return list;
                }
                else
                {
                    return null;
                }
            }
        }
    }

    /// <summary>
    /// Select values from PageSummaryView by Barcode.
    /// </summary>
    /// <param name="sqlConnection">Sql connection or null.</param>
    /// <param name="sqlTransaction">Sql transaction or null.</param>
    /// <param name="barcode"></param>
    /// <returns>Object of type Title.</returns>
    public List<PageSummaryView> PageSummarySelectByBarcode(
        SqlConnection sqlConnection,
        SqlTransaction sqlTransaction,
        string barcode)
    {
      SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
      SqlTransaction transaction = sqlTransaction;

      using ( SqlCommand command = CustomSqlHelper.CreateCommand( "PageSummarySelectByBarcode", connection, transaction,
          CustomSqlHelper.CreateInputParameter( "Barcode", SqlDbType.VarChar, 40, false, barcode ) ) )
      {
        using ( CustomSqlHelper<PageSummaryView> helper = new CustomSqlHelper<PageSummaryView>() )
        {
          List<PageSummaryView> list = helper.ExecuteReader( command );
          if ( list.Count > 0 )
          {
              return list;
          }
          else
          {
            return null;
          }
        }
      }
    }

    public PageSummaryView PageSummarySelectByTitleId(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
     int titleId)
    {
        SqlConnection connection = CustomSqlHelper.CreateConnection(
          CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
        SqlTransaction transaction = sqlTransaction;

        using (SqlCommand command = CustomSqlHelper.CreateCommand("PageSummarySelectByTitleId", connection, transaction,
            CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleId)))
        {
            using (CustomSqlHelper<PageSummaryView> helper = new CustomSqlHelper<PageSummaryView>())
            {
                List<PageSummaryView> list = helper.ExecuteReader(command);
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

    /// <summary>
    /// Select values from PageSummaryView by Page Id.
    /// </summary>
    /// <param name="sqlConnection">Sql connection or null.</param>
    /// <param name="sqlTransaction">Sql transaction or null.</param>
    /// <param name="pageId"></param>
    /// <returns>Object of type Title.</returns>
    public List<PageSummaryView> PageSummarySelectByPageId(
        SqlConnection sqlConnection,
        SqlTransaction sqlTransaction,
        int pageId )
    {
      SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
      SqlTransaction transaction = sqlTransaction;

      using ( SqlCommand command = CustomSqlHelper.CreateCommand( "PageSummarySelectByPageId", connection, transaction,
          CustomSqlHelper.CreateInputParameter( "PageId", SqlDbType.Int, null, false, pageId ) ) )
      {
        using ( CustomSqlHelper<PageSummaryView> helper = new CustomSqlHelper<PageSummaryView>() )
        {
          List<PageSummaryView> list = helper.ExecuteReader( command );
          if ( list.Count > 0 )
          {
            return list;
          }
          else
          {
            return null;
          }
        }
      }
    }

    /// <summary>
    /// Select values from PageSummaryView by Item and Sequence
    /// </summary>
    /// <param name="sqlConnection">Sql connection or null.</param>
    /// <param name="sqlTransaction">Sql transaction or null.</param>
    /// <param name="itemID"></param>
    /// <param name="sequence"></param>
    /// <returns>Object of type Title.</returns>
    public PageSummaryView PageSummarySelectByItemAndSequence(
        SqlConnection sqlConnection,
        SqlTransaction sqlTransaction,
        int itemID,
        int sequence )
    {
      SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
      SqlTransaction transaction = sqlTransaction;

      using ( SqlCommand command = CustomSqlHelper.CreateCommand( "PageSummarySelectByItemAndSequence", connection, transaction,
          CustomSqlHelper.CreateInputParameter( "ItemID", SqlDbType.Int, null, false, itemID ),
          CustomSqlHelper.CreateInputParameter( "Sequence", SqlDbType.Int, null, false, sequence ) ) )
      {
        using ( CustomSqlHelper<PageSummaryView> helper = new CustomSqlHelper<PageSummaryView>() )
        {
          List<PageSummaryView> list = helper.ExecuteReader( command );
          if ( list.Count > 0 )
          {
            return list[ 0 ];
          }
          else
          {
            return null;
          }
        }
      }
    }

    public List<PageSummaryView> PageSummarySelectBarcodeForTitleID(
        SqlConnection sqlConnection,
        SqlTransaction sqlTransaction,
        int titleID)
    {
        SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
        SqlTransaction transaction = sqlTransaction;

        using (SqlCommand command = CustomSqlHelper.CreateCommand("PageSummarySelectBarcodeForTitleID", connection, transaction,
            CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
        {
            using (CustomSqlHelper<PageSummaryView> helper = new CustomSqlHelper<PageSummaryView>())
            {
                List<PageSummaryView> list = helper.ExecuteReader(command);
                return list;
            }
        }
    }

    public List<PageSummaryView> PageSummarySelectFoldersForTitleID(
        SqlConnection sqlConnection,
        SqlTransaction sqlTransaction,
        int titleID)
    {
        SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
        SqlTransaction transaction = sqlTransaction;

        using (SqlCommand command = CustomSqlHelper.CreateCommand("PageSummarySelectFoldersForTitleID", connection, transaction,
            CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
        {
            using (CustomSqlHelper<PageSummaryView> helper = new CustomSqlHelper<PageSummaryView>())
            {
                List<PageSummaryView> list = helper.ExecuteReader(command);
                return list;
            }
        }
    }
  }
}