using Microsoft.AspNetCore.Mvc;
using PokeApi.Server.Models;
using PokeApi.Server.Services.Interfaces;

namespace PokeApi.Server.Controllers
{
    [Route("mail")]
    [ApiController]
    public class MailController([FromServices] IMailService mailService, [FromServices] ILogger<MailController> logger) : ControllerBase
    {
        private ILogger<MailController> logger = logger;
        private IMailService mailService = mailService;

        [HttpPost]
        public async void SendMessageAsync([FromBody] Message message)
        {
            await mailService.SendMessageAsync(message);
        }
    }
}
