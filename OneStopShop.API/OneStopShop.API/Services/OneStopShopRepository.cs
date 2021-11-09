using Microsoft.EntityFrameworkCore;
using OneStopShop.API.Entities;
using OneStopShop.API.Models;
using OneStopShop.API.Models.Security;
using OneStopShop.API.Repositries.OneStopShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneStopShop.API.Services
{
    public class OneStopShopRepository : IOneStopShopRepository
    {
        private OneStopShopContext _context;

        public OneStopShopRepository(OneStopShopContext context)
        {
            _context = context;
        }

        #region Users

        public async Task<User> Login(CredentialDTO cred)
        {
            return await _context.users.Where(u => u.username == cred.UserName && u.password == cred.Password).FirstOrDefaultAsync();
        }

        public List<User> GetUsers()
        {
            List<User> usersList = new List<User>();

            usersList = _context.users.ToList();

            return usersList;
        }

        public User ListUserDetails(int userID)
        {
            return _context.users.Where(u => u.userid == userID).SingleOrDefault();
        }

        public async Task<ResponseDTO> CreateUser(UserDTO user)
        {
            ResponseDTO response = new ResponseDTO();

            User userData = new User();
            userData.username = user.UserName;
            userData.emailaddress = user.EmailAddress;
            userData.password = user.Password;

            _context.users.Add(userData);
            _context.SaveChanges();

            response.Message = "Success";
            response.Success = true;

            return response;
        }

        public async Task<int> UpdateUser(UserDTO user)
        {
            User userData = _context.users.Where(u => u.userid == user.UserID).FirstOrDefault();

            userData.emailaddress = user.EmailAddress;
            userData.username = user.UserName;
            userData.password = user.Password;

            _context.users.Update(userData);
            _context.SaveChanges();

            return userData.userid;
        }

        public bool DeleteUser(int userid)
        {
            User userData = _context.users.Where(u => u.userid == userid).FirstOrDefault();

            _context.users.Remove(userData);
            _context.SaveChanges();

            return true;
        }

        #endregion

        #region Items

        public List<Item> GetItems()
        {
            List<Item> itemsList = new List<Item>();

            itemsList = _context.items.ToList();

            return itemsList;
        }

        public async Task<int> CreateShoppingListItem(ItemDTO shoppingListItem, int userid)
        {
            ShoppingListItem newItem = new ShoppingListItem();

            newItem.shoppinglistitemdescription = shoppingListItem.shoppinglistitemdescription;
            newItem.shoppinglistid = shoppingListItem.shoppinglistid;
            newItem.quantitydescription = shoppingListItem.quantitydescription;
            newItem.insertiondate = DateTime.Now;
            newItem.itemchecked = shoppingListItem.itemchecked;

            _context.shoppinglistitems.Add(newItem);
            _context.SaveChanges();

            return newItem.id;
        }

        public async Task<int> ToggleShoppingListItemChecked(ItemCheckDTO itemCheck)
        {
            ShoppingListItem currentItem = _context.shoppinglistitems.Where(i => i.id == itemCheck.id).FirstOrDefault();

            currentItem.itemchecked = itemCheck.ItemChecked;

            _context.shoppinglistitems.Update(currentItem);
            _context.SaveChanges();

            return currentItem.id;
        }

        public async Task<ShoppingListItemDTO> UpdateShoppingListItem(ShoppingListItemDTO shoppingListItem)
        {
            ShoppingListItem shoppingListData = _context.shoppinglistitems.Where(s => s.id == shoppingListItem.id).FirstOrDefault();

            shoppingListData.shoppinglistitemdescription = shoppingListItem.ShoppingListItemDescription;
            shoppingListData.quantitydescription = shoppingListItem.QuantityDescription;

            _context.shoppinglistitems.Update(shoppingListData);
            _context.SaveChanges();

            return shoppingListItem;
        }

        #endregion

        #region Shopping List

        public List<ShoppingList> GetShoppingLists(int userid)
        {
            List<ShoppingList> shoppingLists = new List<ShoppingList>();

            shoppingLists = _context.shoppinglists.Where(s => s.userid == userid).ToList();

            List<ShoppingList> shoppingListsSorted = new List<ShoppingList>();
            shoppingListsSorted = shoppingLists.OrderBy(l => l.id).ToList();

            return shoppingLists;
        }

        public List<ShoppingListItemDTO> GetShoppingListItems(int shoppinglistid)
        {
            List<ShoppingListItem> shoppingListItems = new List<ShoppingListItem>();
            List<ShoppingListItemDTO> shoppingListItemsData = new List<ShoppingListItemDTO>();

            shoppingListItems = _context.shoppinglistitems.Where(s => s.shoppinglistid == shoppinglistid).ToList();

            foreach(ShoppingListItem item in shoppingListItems)
            {
                ShoppingListItemDTO itemDTO = new ShoppingListItemDTO();

                itemDTO.id = item.id;
                itemDTO.ShoppingListID = shoppinglistid;
                itemDTO.ShoppingListItemDescription = item.shoppinglistitemdescription;
                itemDTO.QuantityDescription = item.quantitydescription;
                itemDTO.ItemChecked = item.itemchecked;

                shoppingListItemsData.Add(itemDTO);
            }

            List<ShoppingListItemDTO> shoppingListItemsDataSorted = new List<ShoppingListItemDTO>();
            shoppingListItemsDataSorted = shoppingListItemsData.OrderBy(s => s.id).ToList();

            return shoppingListItemsDataSorted;
        }

        public async Task<int> CreateShoppingList(ShoppingListDTO shoppinglist, int userid)
        {
            ShoppingList shoppingListData = new ShoppingList();

            shoppingListData.name = shoppinglist.shoppinglistname;
            shoppingListData.description = shoppinglist.shoppinglistdescription;
            shoppingListData.insertiondate = DateTime.Now;
            shoppingListData.userid = userid;

            _context.shoppinglists.Add(shoppingListData);
            _context.SaveChanges();

            return shoppingListData.id;
        }

        public async Task<int> UpdateShoppingList(ShoppingListDTO shoppinglist)
        {
            ShoppingList shoppingListData = _context.shoppinglists.Where(s => s.id == shoppinglist.id).FirstOrDefault();

            shoppingListData.name = shoppinglist.shoppinglistname;
            shoppingListData.description = shoppinglist.shoppinglistdescription;

            _context.shoppinglists.Update(shoppingListData);
            _context.SaveChanges();

            return shoppingListData.id;
        }

        public bool DeleteShoppingList(int shoppinglistid)
        {
            List<ShoppingListItem> shoppingListItems = _context.shoppinglistitems.Where(i => i.shoppinglistid == shoppinglistid).ToList();

            foreach(ShoppingListItem item in shoppingListItems)
            {
                _context.shoppinglistitems.Remove(item);
            }

            ShoppingList shoppingListData = _context.shoppinglists.Where(s => s.id == shoppinglistid).FirstOrDefault();

            _context.shoppinglists.Remove(shoppingListData);
            _context.SaveChanges();

            return true;
        }

        public bool DeleteShoppingListItem(int shoppinglistitemid)
        {
            ShoppingListItem itemData = _context.shoppinglistitems.Where(s => s.id == shoppinglistitemid).FirstOrDefault();

            _context.shoppinglistitems.Remove(itemData);
            _context.SaveChanges();

            return true;
        }

        #endregion

        #region Security

        public void AddRefreshToken(RefreshToken refreshToken)
        {
            _context.refreshtokens.Add(refreshToken);
            _context.SaveChanges();
        }

        public RefreshToken ListRefreshTokenDetails(string token)
        {
            return _context.refreshtokens.Where(r => r.token == token).FirstOrDefault();
        }

        public void RevokeRefreshToken(RefreshToken token)
        {
            _context.refreshtokens.Update(token);
            _context.SaveChanges();
        }

        #endregion
    }
}
