using System.Collections.Generic;
using System.Configuration;

namespace BHL.Export
{
    public class ConfigParms
    {
        public string SMTPHost { get; set; }
        public string EmailFromAddress { get; set; }
        public string EmailToAddress { get; set; }
        public Dictionary<string, ExportProcessor> Processors { get; set; }
        public string ProcessorToRun { get; set; }

        public ConfigParms()
        {
            SMTPHost = string.Empty;
            EmailFromAddress = string.Empty;
            EmailToAddress = string.Empty;
            Processors = new Dictionary<string, ExportProcessor>();
            ProcessorToRun = string.Empty;
        }

        public void LoadAppConfig()
        {
            this.SMTPHost = ConfigurationManager.AppSettings["SMTPHost"] ?? string.Empty;
            this.EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"] ?? string.Empty;
            this.EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"] ?? string.Empty;
            InitializeProcessorList(ConfigurationManager.AppSettings["Processors"]);
            this.ProcessorToRun = (ConfigurationManager.AppSettings["ProcessorToRun"] ?? string.Empty).ToUpper();
        }

        private void InitializeProcessorList(string processors)
        {
            // Set up the metadata formats
            string[] processorStrings = processors.Split('\n');

            foreach (string processorString in processorStrings)
            {
                string[] processorSpecs = processorString.Split('|');

                ExportProcessor processor = new ExportProcessor();
                processor.Name = processorSpecs[0].Trim();
                processor.Handler = processorSpecs[1].Trim();
                Processors.Add(processor.Name.ToUpper(), processor);
            }
        }
    }
}
