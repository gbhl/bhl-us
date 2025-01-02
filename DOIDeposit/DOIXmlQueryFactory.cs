using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.DOIDeposit
{
    public class DOIXmlQueryFactory
    {
        private DOIDepositData data = null;
        public DOIDepositData Data
        {
            get { return data; }
            set { data = value; }
        }

        public enum DOIQueryType
        {
            Monograph,
            Journal,
            Article
        }

        #region Constructors

        public DOIXmlQueryFactory()
        {
        }

        public DOIXmlQueryFactory(DOIDepositData data)
        {
            Data = data;
        }

        #endregion Constructors

        public DOIQuery GetDOIQuery(DOIQueryType type)
        {
            return GetDOIQuery(type, Data);
        }

        public DOIQuery GetDOIQuery(DOIQueryType type, DOIDepositData data)
        {
            DOIQuery deposit = null;

            switch (type)
            {
                case DOIQueryType.Monograph:
                    deposit = new DOIMonographXmlQuery(data);
                    break;
                case DOIQueryType.Journal:
                    throw new NotImplementedException();
                case DOIQueryType.Article:
                    deposit = new DOIArticleXmlQuery(data);
                    break;
                default:
                    deposit = new DOIMonographXmlQuery(data);
                    break;
            }

            return deposit;
        }
    }
}
