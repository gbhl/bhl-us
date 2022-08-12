using BHL.SiteServicesREST.v1.Models;

namespace BHL.SiteServicesREST.v1.Services
{
    public interface IQueueService
    {
        Task AddQueueMessages(string queueName, List<string> messages);
        Task<uint> GetQueueMessageCount(string queueName);
    }
}
