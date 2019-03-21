using System.Threading.Tasks;
using Intelligent.API.Data;

namespace Intelligent.API.Framework
{
    public interface IMessagingService
    {
        Task<BaseResponse> PostMessageToConversationTask(string conversationId, Message message);
    }
}