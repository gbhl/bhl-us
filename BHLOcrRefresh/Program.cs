[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace MOBOT.BHL.BHLOcrRefresh
{
    class Program
    {
        static void Main()
        {
            OcrProcessor processor = new();
            processor.Process();
        }
    }
}
