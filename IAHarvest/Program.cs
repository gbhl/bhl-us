[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace IAHarvest
{
    class Program
    {
        static void Main()
        {
            IAHarvestProcessor processor = new();
            processor.Process();
        }
    }
}
