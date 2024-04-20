using PokeApiV2.Server.Models;

namespace PokeApiV2.Server.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(Message message);
    }
}
