using System;
using System.Collections.Generic;
using System.Reflection;

namespace BHL.Export
{
    public class ExportProcessorFactory
    {
        Assembly _processorAssembly = null;
        String _handler = String.Empty;

        public ExportProcessorFactory(String processorToRun, Dictionary<string, ExportProcessor> processors)
        {
            // Find the handler assembly for the specified processor
            ExportProcessor processor = processors[processorToRun];
            _handler = processor.Handler;

            _processorAssembly = Assembly.Load(_handler);
        }

        public IBHLExport New()
        {
            Type processorType = _processorAssembly.GetType(_handler + ".ExportProcessor");
            IBHLExport processorInstance = (IBHLExport)Activator.CreateInstance(processorType);
            return processorInstance;
        }
    }
}
