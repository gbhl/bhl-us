﻿using System.Collections.Generic;
using System.Configuration;

namespace BHL.Export
{
    public class ConfigParms
    {
        public string SMTPHost { get; set; }
        public string EmailFromAddress { get; set; }
        public string EmailToAddress { get; set; }
        public bool LogToFile { get; set;}
        public bool LogToConsole { get; set; }
        public Dictionary<string, ExportProcessor> Processors { get; set; }
        public string ProcessorToRun { get; set; }

        public ConfigParms()
        {
            SMTPHost = string.Empty;
            EmailFromAddress = string.Empty;
            EmailToAddress = string.Empty;
            LogToFile = true;
            LogToConsole = true;
            Processors = new Dictionary<string, ExportProcessor>();
            ProcessorToRun = string.Empty;
        }

        public void LoadAppConfig()
        {
            SMTPHost = ConfigurationManager.AppSettings["SMTPHost"] ?? string.Empty;
            EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"] ?? string.Empty;
            EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"] ?? string.Empty;
            LogToFile = !((ConfigurationManager.AppSettings["LogToFile"] ?? string.Empty).ToLower() == "false");
            LogToConsole = !((ConfigurationManager.AppSettings["LogToConsole"] ?? string.Empty).ToLower() == "false");
            InitializeProcessorList(ConfigurationManager.AppSettings["Processors"]);
            ProcessorToRun = (ConfigurationManager.AppSettings["ProcessorToRun"] ?? string.Empty).ToUpper();
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
