using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using Npgsql;
using OneStopShop.API.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OneStopShop.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace OneStopShop.API.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/UserController")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private IOneStopShopRepository _oneStopShopRepository;
        private IHttpContextAccessor _accessor;
        private ILogger<UserController> _logger;

        public UserController(IConfiguration configuration,
            IWebHostEnvironment env,
            IOneStopShopRepository oneStopShopRepository,
            IHttpContextAccessor accessor,
            ILogger<UserController> logger)
        {
            _configuration = configuration;
            _env = env;
            _oneStopShopRepository = oneStopShopRepository;
            _accessor = accessor;
            _logger = logger;
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            List<Entities.User> users = new List<Entities.User>();

            try
            {
                users = _oneStopShopRepository.GetUsers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in the get users method in the UserController.");
            }

            return Ok(users);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO user)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                response = await _oneStopShopRepository.CreateUser(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in the create user method in the UserController.");
            }

            return Ok(response);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO user)
        {
            int response = 0;

            try
            {
                response = await _oneStopShopRepository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in the update users method in the UserController.");
            }

            return Ok(response);
        }

        [HttpDelete("DeleteUser/{userid}")]
        public IActionResult DeleteUser(int userid)
        {
            bool response = false;

            try
            {
                response = _oneStopShopRepository.DeleteUser(userid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in the delete users method in the UserController.");
            }

            return Ok(response);
        }
    }
}
