namespace Authentication.Service.Controllers
{
    [Route("api/Client")]
    [ApiController]
    [Authorize]

    public class GameController : ControllerBase
    {
        private IClient _iclient;
        private ILogger<ClientModule> _logger;

        public ClientController(IClient client, ILogger<ClientModule> logger)
        {
            this._iclient = client;
            _logger = logger;
        }
    }
}
