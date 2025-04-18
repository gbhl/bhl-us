using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.DOIDeposit
{
    public class DOIDepositFactory
    {
        private DOIDepositData data = null;
        public DOIDepositData Data
        {
            get { return data; }
            set { data = value; }
        }

        public enum DOIDepositType
        {
            Monograph,
            Chapter,
            Journal,
            Article
        }

        #region Constructors

        public DOIDepositFactory()
        {
        }

        public DOIDepositFactory(DOIDepositData data)
        {
            Data = data;
        }

        #endregion Constructors

        public DOIDeposit GetDOIDeposit(DOIDepositType type)
        {
            return GetDOIDeposit(type, Data);
        }

        public DOIDeposit GetDOIDeposit(DOIDepositType type, DOIDepositData data)
        {
            DOIDeposit deposit = null;

            switch (type)
            {
                case DOIDepositType.Monograph:
                    deposit = new DOIMonographDeposit(data);
                    break;
                case DOIDepositType.Chapter:
                    deposit = new DOIChapterDeposit(data);
                    break;
                case DOIDepositType.Journal:
                    deposit = new DOIJournalDeposit(data);
                    break;
                case DOIDepositType.Article:
                    deposit = new DOIArticleDeposit(data);
                    break;
                default:
                    deposit = new DOIMonographDeposit(data);
                    break;
            }

            return deposit;
        }
    }
}
