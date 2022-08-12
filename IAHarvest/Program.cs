[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace IAHarvest
{
    class Program
    {
        static void Main(string[] args)
        {
            IAHarvestProcessor processor = new IAHarvestProcessor();
            processor.Process();
        }
    }
}
