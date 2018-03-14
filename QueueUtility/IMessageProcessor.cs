namespace BHL.QueueUtility
{
    public interface IMessageProcessor
    {
        bool ProcessMessage(string message);
    }
}
