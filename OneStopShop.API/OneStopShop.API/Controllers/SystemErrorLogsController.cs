using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OneStopShop.API.Services;

namespace OneStopShop.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/systemerrorlogscontroller")]
    public class SystemErrorLogsController : Controller
    {
        private ILogger<SystemErrorLogsController> _logger;
        //private IScoreOnlineRepository _scoreOnlineRepository;
        private IConfiguration _config;
        private IHttpContextAccessor _accessor;

        public SystemErrorLogsController(ILogger<SystemErrorLogsController> logger,
           //IScoreOnlineRepository scoreOnlineRepository,
           IHttpContextAccessor accessor,
           IConfiguration config)
        {
            _logger = logger;
            //_scoreOnlineRepository = scoreOnlineRepository;
            _config = config;
            _accessor = accessor;
        }
    }
}
