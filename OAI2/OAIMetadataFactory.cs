using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace MOBOT.BHL.OAI2
{
    internal class OAIMetadataFactory
    {
        Assembly _formatAssembly = null;
        String _handler = String.Empty;

        public OAIMetadataFactory(String metadataFormat, List<OAIMetadataFormat> metadataFormats)
        {
            // Find the handler assemby for the specified format
            foreach (OAIMetadataFormat format in metadataFormats)
            {
                if (format.MetadataFormat == metadataFormat)
                {
                    _handler = format.MetadataHandler;
                    break;
                }
            }

            _formatAssembly = System.Reflection.Assembly.Load(_handler);
        }

        public String GetMetadata(OAIRecord oaiRecord)
        {
            String metadata;

            Type formatType = _formatAssembly.GetType(_handler + ".Convert");
            object[] args = new object[1];
            args[0] = oaiRecord;
            object formatInstance = Activator.CreateInstance(formatType, args);
            metadata = formatType.InvokeMember("ToString", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public,
                null, formatInstance, null).ToString();

            return metadata;
        }
    }
}
