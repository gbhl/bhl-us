using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
	public enum IndicatedPageStyle
	{
		Integer = 1,
		LowerRomanIV = 2,
		UpperRomanIV = 3,
		LowerRomanIIII = 4,
		UpperRomanIIII = 5,
		FreeForm = 6
	}

	public partial class BHLProvider
	{
		private void IndicatedPageSave( int pageID, string pagePrefix,	string pageNumber, bool implied, int userID, 
			TransactionController transactionController )
		{
			IndicatedPageDAL dal = new IndicatedPageDAL();
			dal.IndicatedPageInsertNext( transactionController.Connection, transactionController.Transaction,
				pageID, pagePrefix, pageNumber, implied, userID );
			dal = null;
			PageSetPaginationInfo( pageID, userID, transactionController );
		}

		public void IndicatedPageSave( int[] pageIDs, string pagePrefix, IndicatedPageStyle style, string start, 
			int increment, bool implied, int userID )
		{
			int pageNumber;
			string pageNumberRoman;
			TransactionController transactionController = new TransactionController();
			try
			{
				transactionController.BeginTransaction();
				switch ( style )
				{
					case IndicatedPageStyle.Integer:
						pageNumber = int.Parse( start );
						for ( int index = 0; index < pageIDs.Length; index++ )
						{
							this.IndicatedPageSave( pageIDs[ index ], pagePrefix, pageNumber.ToString(), implied, userID, 
								transactionController );
							pageNumber = pageNumber + increment;
						}
						break;
					case IndicatedPageStyle.LowerRomanIV:
						pageNumber = RomanNumerals.FromRomanNumeral( start );
						pageNumberRoman = "";
						for ( int index = 0; index < pageIDs.Length; index++ )
						{
							pageNumberRoman = RomanNumerals.ToRomanNumeral( pageNumber, true ).ToLower();
							this.IndicatedPageSave( pageIDs[ index ], pagePrefix, pageNumberRoman, implied, userID, transactionController );
							pageNumber = pageNumber + increment;
						}
						break;
					case IndicatedPageStyle.UpperRomanIV:
						pageNumber = RomanNumerals.FromRomanNumeral( start );
						pageNumberRoman = "";
						for ( int index = 0; index < pageIDs.Length; index++ )
						{
							pageNumberRoman = RomanNumerals.ToRomanNumeral( pageNumber, true );
							this.IndicatedPageSave( pageIDs[ index ], pagePrefix, pageNumberRoman, implied, userID, transactionController );
							pageNumber = pageNumber + increment;
						}
						break;
					case IndicatedPageStyle.LowerRomanIIII:
						pageNumber = RomanNumerals.FromRomanNumeral( start );
						pageNumberRoman = "";
						for ( int index = 0; index < pageIDs.Length; index++ )
						{
							pageNumberRoman = RomanNumerals.ToRomanNumeral( pageNumber, false ).ToLower();
							this.IndicatedPageSave( pageIDs[ index ], pagePrefix, pageNumberRoman, implied, userID, transactionController );
							pageNumber = pageNumber + increment;
						}
						break;
					case IndicatedPageStyle.UpperRomanIIII:
						pageNumber = RomanNumerals.FromRomanNumeral( start );
						pageNumberRoman = "";
						for ( int index = 0; index < pageIDs.Length; index++ )
						{
							pageNumberRoman = RomanNumerals.ToRomanNumeral( pageNumber, false );
							this.IndicatedPageSave( pageIDs[ index ], pagePrefix, pageNumberRoman, implied, userID, transactionController );
							pageNumber = pageNumber + increment;
						}
						break;
					case IndicatedPageStyle.FreeForm:
						for ( int index = 0; index < pageIDs.Length; index++ )
						{
							this.IndicatedPageSave( pageIDs[ index ], pagePrefix, start, implied, userID, transactionController );
						}
						break;
				}
				transactionController.CommitTransaction();
			}
			catch
			{
				transactionController.RollbackTransaction();
			}
			finally
			{
				transactionController.Dispose();
			}

		}

		private void IndicatedPageDeleteAllForPage( int pageID, int userID, TransactionController transactionController )
		{
			new IndicatedPageDAL().IndicatedPageDeleteAllForPage( transactionController.Connection, transactionController.Transaction, pageID );
			PageSetPaginationInfo( pageID, userID, transactionController );
		}

		public void IndicatedPageDeleteAllForPage( int[] pageIDs, int userID )
		{
			int index = 0;
			TransactionController transactionController = new TransactionController();
			try
			{
				transactionController.BeginTransaction();
				for ( index = 0; index < pageIDs.Length; index++ )
				{
					this.IndicatedPageDeleteAllForPage( pageIDs[ index ], userID, transactionController );
				}
				transactionController.CommitTransaction();
			}
			catch
			{
				transactionController.RollbackTransaction();
			}
			finally
			{
				transactionController.Dispose();
			}
		}

        public List<IndicatedPage> IndicatedPageSelectByPageID(int pageID)
        {
            return new IndicatedPageDAL().IndicatedPageSelectByPageID(null, null, pageID);
        }

    }
}
