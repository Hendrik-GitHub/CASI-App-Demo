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
using System.Security.Claims;
using Microsoft.Extensions.Logging;


namespace OneStopShop.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/ShoppingListController")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private IOneStopShopRepository _oneStopShopRepository;
        private IHttpContextAccessor _accessor;
        private ILogger<ShoppingListController> _logger;

        public ShoppingListController(IConfiguration configuration,
            IWebHostEnvironment env,
            IOneStopShopRepository oneStopShopRepository,
            IHttpContextAccessor accessor,
            ILogger<ShoppingListController> logger)
        {
            _configuration = configuration;
            _env = env;
            _oneStopShopRepository = oneStopShopRepository;
            _accessor = accessor;
            _logger = logger;
        }

        [HttpGet("GetShoppingLists")]
        public IActionResult GetShoppingLists()
        {
            int userid = Convert.ToInt32(this.User.FindFirstValue("userid"));

            List<Entities.ShoppingList> shoppingLists = new List<Entities.ShoppingList>();

            try
            {
                shoppingLists = _oneStopShopRepository.GetShoppingLists(userid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in the get shopping lists method in the shoppinglistcontroller.");
            }

            return Ok(shoppingLists);
        }

        [HttpPost("CreateShoppingList")]
        public async Task<IActionResult> CreateShoppingList([FromBody] ShoppingListDTO shoppingList)
        {
            int response = 0;

            int userid = Convert.ToInt32(this.User.FindFirstValue("userid"));

            try
            {
                response = await _oneStopShopRepository.CreateShoppingList(shoppingList, userid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in the create shopping list method in the shoppinglistcontroller.");
            }

            return Ok(response);
        }

        [HttpPut("UpdateShoppingList")]
        public async Task<IActionResult> UpdateShoppingList([FromBody] ShoppingListDTO shoppingList)
        {
            int response = 0;

            try
            {
                response = await _oneStopShopRepository.UpdateShoppingList(shoppingList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in the update shopping list method in the shoppinglistcontroller.");
            }

            return Ok(response);
        }

        [HttpDelete("DeleteShoppingList/{listId}")]
        public IActionResult DeleteShoppingList(int listId)
        {
            bool response = false;

            try
            {
                response = _oneStopShopRepository.DeleteShoppingList(listId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in the delete shopping list method in the shoppinglistcontroller.");
            }

            return Ok(response);
        }

        [HttpGet("GetShoppingListItems/{id}")]
        public IActionResult GetShoppingListItems(int id)
        {
            List<ShoppingListItemDTO> shoppingListItems = new List<ShoppingListItemDTO>();

            try
            {
                shoppingListItems = _oneStopShopRepository.GetShoppingListItems(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in the get shopping list items method in the shoppinglistcontroller.");
            }

            return Ok(shoppingListItems);
        }

        [HttpPost("CreateShoppingListItem")]
        public async Task<IActionResult> CreateShoppingListItem([FromBody] ItemDTO newShoppingListItem)
        {
            int response = 0;

            int userid = Convert.ToInt32(this.User.FindFirstValue("userid"));

            try
            {
                response = await _oneStopShopRepository.CreateShoppingListItem(newShoppingListItem, userid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in the create shopping list item method in the shoppinglistcontroller.");
            }

            return Ok(newShoppingListItem);
        }

        [HttpPost("ToggleShoppingListItemChecked")]
        public async Task<IActionResult> ToggleShoppingListItemChecked([FromBody] ItemCheckDTO itemCheck)
        {
            int response = 0;

            try
            {
                response = await _oneStopShopRepository.ToggleShoppingListItemChecked(itemCheck);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in the toggle shopping list item method in the shoppinglistcontroller.");
            }

            return Ok(itemCheck);
        }

        [HttpDelete("DeleteShoppingListItem/{id}")]
        public IActionResult DeleteShoppingListItem(int id)
        {
            bool response = false;

            try
            {
                response = _oneStopShopRepository.DeleteShoppingListItem(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in the delete shopping list item method in the shoppinglistcontroller.");
            }

            return Ok(response);
        }

        [HttpPut("UpdateShoppingListItem")]
        public async Task<IActionResult> UpdateShoppingListItem([FromBody] ShoppingListItemDTO shoppingListItemDTO)
        {
            ShoppingListItemDTO response = null;

            try
            {
                response = await _oneStopShopRepository.UpdateShoppingListItem(shoppingListItemDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in the update shopping list item method in the shoppinglistcontroller.");
            }

            return Ok(response);
        }
    }
}
