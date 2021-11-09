using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneStopShop.API.Entities;
using OneStopShop.API.Models;
using OneStopShop.API.Models.Security;

namespace OneStopShop.API.Services
{
    public interface IOneStopShopRepository
    {
        List<User> GetUsers();

        Task<User> Login(CredentialDTO cred);

        void AddRefreshToken(RefreshToken refreshToken);

        RefreshToken ListRefreshTokenDetails(string token);

        void RevokeRefreshToken(RefreshToken token);

        User ListUserDetails(int userID);

        Task<ResponseDTO> CreateUser(UserDTO user);

        Task<int> UpdateUser(UserDTO user);

        bool DeleteUser(int userid);

        List<ShoppingList> GetShoppingLists(int userid);

        List<ShoppingListItemDTO> GetShoppingListItems(int shoppinglistid);

        Task<int> CreateShoppingList(ShoppingListDTO shoppinglist, int userid);

        Task<int> UpdateShoppingList(ShoppingListDTO shoppinglist);

        bool DeleteShoppingList(int shoppinglistid);

        Task<int> CreateShoppingListItem(ItemDTO shoppingListItem, int userid);

        Task<int> ToggleShoppingListItemChecked(ItemCheckDTO itemCheck);

        bool DeleteShoppingListItem(int shoppinglistitemid);

        Task<ShoppingListItemDTO> UpdateShoppingListItem(ShoppingListItemDTO shoppingListItem);
    }
}
