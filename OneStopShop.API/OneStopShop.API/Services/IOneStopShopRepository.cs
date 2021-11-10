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
        #region Users Section

        List<User> GetUsers();

        Task<User> Login(CredentialDTO cred);

        User ListUserDetails(int userID);

        Task<ResponseDTO> CreateUser(UserDTO user);

        Task<int> UpdateUser(UserDTO user);

        bool DeleteUser(int userid);

        #endregion

        #region Shopping Lists Section

        List<ShoppingList> GetShoppingLists(int userid);

        Task<ResponseDTO> CreateShoppingList(ShoppingListDTO shoppinglist, int userid);

        Task<ResponseDTO> UpdateShoppingList(ShoppingListDTO shoppinglist);

        ResponseDTO DeleteShoppingList(int shoppinglistid);

        #endregion

        #region Shopping List Items Section

        List<ShoppingListItemDTO> GetShoppingListItems(int shoppinglistid);

        Task<ResponseDTO> CreateShoppingListItem(ItemDTO shoppingListItem, int userid);

        Task<ResponseDTO> UpdateShoppingListItem(ShoppingListItemDTO shoppingListItem);

        Task<ResponseDTO> ToggleShoppingListItemChecked(ItemCheckDTO itemCheck);

        ResponseDTO DeleteShoppingListItem(int shoppinglistitemid);

        #endregion

        #region Security

        void AddRefreshToken(RefreshToken refreshToken);

        RefreshToken ListRefreshTokenDetails(string token);

        void RevokeRefreshToken(RefreshToken token);

        #endregion    
    }
}
