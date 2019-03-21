using System;
using System.Collections.Generic;
using System.Reflection;

namespace BHL.Export
{
    public class ExportProcessorFactory
    {
        Dictionary<string, ExportProcessor> _processors;

        public ExportProcessorFactory(Dictionary<string, ExportProcessor> processors)
        {
            _processors = processors;
        }

        /// <summary>
        /// Get an instance of the specified processor
        /// </summary>
        /// <param name="processorToRun"></param>
        /// <returns></returns>
        public IBHLExport New(string processorName)
        {
            // Find the handler assembly for the specified processor
            ExportProcessor processor = _processors[processorName];
            string handler = processor.Handler;

            // Instatiate an instance of the specified processor
            Assembly processorAssembly = Assembly.Load(handler);
            Type processorType = processorAssembly.GetType(handler + ".ExportProcessor");
            IBHLExport processorInstance = (IBHLExport)Activator.CreateInstance(processorType);

            return processorInstance;
        }
    }
}
