using PokeApi.Server.Models;

namespace PokeApi.Server.Services.Interfaces
{
    public interface IMailService
    {
        Task SendMessageAsync(Message message);
    }
}
