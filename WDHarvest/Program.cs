[assembly: log4net.Config.XmlConfigurator(Watch = true)]

WDHarvestProcessor processor = new();
processor.Process();
