using System;
using System.Runtime.Remoting;
using MOBOT.FileAccess;
using System.Collections;


namespace MOBOT.FileAccess.RemotingUtilities
{
    /// <summary>
    /// Provides access to an instance of the MOBOT.FileAccess.FileAccessProvider class 
    /// running as a service on a remote server.  Such a configuration allows for all
    /// file access operations to be maintained/controlled on a single remote server.
    /// </summary>
    public class RemotingHelper
    {
        private static bool _isInit;
        private static IDictionary _wellKnownTypes;

        public static object GetObject(Type type)
        {
            if (!_isInit)
            {
                InitTypeCache();
            }
            WellKnownClientTypeEntry entry = (WellKnownClientTypeEntry)_wellKnownTypes[type];
            if (entry == null)
            {
                throw new RemotingException(string.Format("Type '{0}' not found.", type.Name));
            }

            return RemotingServices.Connect(entry.ObjectType, entry.ObjectUrl);
        }

        public static void InitTypeCache()
        {
            try
            {
                Configure(AppDomain.CurrentDomain.BaseDirectory.EndsWith(@"\") ? AppDomain.CurrentDomain.BaseDirectory + "Remoting.Config" : AppDomain.CurrentDomain.BaseDirectory + @"\Remoting.Config");
            }
            catch (RemotingException)
            {
                //adding this for web apps because the base directory is not the bin folder
                string path = AppDomain.CurrentDomain.BaseDirectory;
                if (path.EndsWith(@"\"))
                    path += @"\";
                Configure(path + @"bin\Remoting.config");
            }
            _wellKnownTypes = new Hashtable();

            WellKnownClientTypeEntry[] entries = RemotingConfiguration.GetRegisteredWellKnownClientTypes();
            foreach (WellKnownClientTypeEntry entry in entries)
            {
                try
                {
                    _wellKnownTypes.Add(entry.ObjectType, entry);
                }
                catch
                {
                    throw new RemotingException(string.Format("Error occurred processing registered well known client types. The following configuration entry is not well formed: {0}", entry.ToString()));
                }
            }
            _isInit = true;
        }

        public static void Configure(string fileName)
        {
            if (!_isInit)
            {
                RemotingConfiguration.Configure(fileName, false);
            }
        }

        public static IFileAccessProvider GetRemotedFileAccessProvider()
        {
            IFileAccessProvider provider = null;
            try
            {
                provider = (IFileAccessProvider)RemotingHelper.GetObject(typeof(IFileAccessProvider));
            }
            catch (RemotingException e)
            {
                throw new RemotingException(e.Message, e);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            return provider;
        }
    }
}
