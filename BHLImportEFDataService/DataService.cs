using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using MOBOT.BHLImport.BHLImportEFDataModel;

namespace MOBOT.BHLImport.BHLImportEFDataService
{
    public partial class DataService
    {
        private BHLImportEntities context = null;

        public DataService()
        {
            //context = new BHLImportEntities();
        }

        /// <summary>
        /// Get a data context.  By default, use any existing context.  If no context exists, a new one is
        /// created.  You can also force a new context to be created by setting forceNew to true. By default 
        /// no newly created contexts are persisted.  Override this by setting persist to true.
        /// </summary>
        /// <param name="forceNew"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        private BHLImportEntities GetDataContext()
        {
            return GetDataContext(false, false);
        }

        /// <summary>
        /// Get a data context.  By default, use any existing context.  If no context exists, a new one is
        /// created.  You can also force a new context to be created by setting forceNew to true. By default 
        /// no newly created contexts are persisted.  Override this by setting persist to true.
        /// </summary>
        /// <param name="forceNew"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        private BHLImportEntities GetDataContext(bool forceNew)
        {
            return GetDataContext(forceNew, false);
        }

        /// <summary>
        /// Get a data context.  By default, use any existing context.  If no context exists, a new one is
        /// created.  You can also force a new context to be created by setting forceNew to true. By default 
        /// no newly created contexts are persisted.  Override this by setting persist to true.
        /// </summary>
        /// <param name="forceNew"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        private BHLImportEntities GetDataContext(bool forceNew, bool persist)
        {
            if (context == null || forceNew)
            {
                BHLImportEntities newContext = new BHLImportEntities();
                newContext.Configuration.AutoDetectChangesEnabled = false;
                ((IObjectContextAdapter)newContext).ObjectContext.CommandTimeout = 600;
                if (persist) context = newContext;
                return newContext;
            }
            else
            {
                return context;
            }
        }
    }
}
