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

        #region Users Section

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

            try
            {
                userData.username = user.UserName;
                userData.emailaddress = user.EmailAddress;
                userData.password = user.Password;

                User userCheck = _context.users.Where(u => u.username == user.UserName).FirstOrDefault();

                if (userCheck == null)
                {
                    _context.users.Add(userData);
                    _context.SaveChanges();

                    response.Message = "Success";
                    response.Success = true;
                }
                else
                {
                    response.Message = "User exists!";
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Failure";
                response.Success = false;
            }
                
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

        #region Shopping Lists Section

        public List<ShoppingList> GetShoppingLists(int userid)
        {
            List<ShoppingList> shoppingLists = new List<ShoppingList>();

            shoppingLists = _context.shoppinglists.Where(s => s.userid == userid).ToList();

            List<ShoppingList> shoppingListsSorted = new List<ShoppingList>();
            shoppingListsSorted = shoppingLists.OrderBy(l => l.shoppinglistid).ToList();

            return shoppingLists;
        }

        public async Task<ResponseDTO> CreateShoppingList(ShoppingListDTO shoppinglist, int userid)
        {
            ResponseDTO response = new ResponseDTO();
            ShoppingList shoppingListData = new ShoppingList();

            try
            {
                shoppingListData.name = shoppinglist.shoppinglistname;
                shoppingListData.description = shoppinglist.shoppinglistdescription;
                shoppingListData.insertiondate = DateTime.Now;
                shoppingListData.userid = userid;

                _context.shoppinglists.Add(shoppingListData);
                _context.SaveChanges();

                response.Message = "Success";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Failure";
                response.Success = false;
            }          
           
            return response;
        }

        public async Task<ResponseDTO> UpdateShoppingList(ShoppingListDTO shoppinglist)
        {
            ResponseDTO response = new ResponseDTO();
            ShoppingList shoppingListData = _context.shoppinglists.Where(s => s.shoppinglistid == shoppinglist.shoppinglistid).FirstOrDefault();

            try
            {
                shoppingListData.name = shoppinglist.shoppinglistname;
                shoppingListData.description = shoppinglist.shoppinglistdescription;

                _context.shoppinglists.Update(shoppingListData);
                _context.SaveChanges();

                response.Message = "Success";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Failure";
                response.Success = false;
            }
           
            return response;
        }

        public ResponseDTO DeleteShoppingList(int shoppinglistid)
        {
            ResponseDTO response = new ResponseDTO();
            List<ShoppingListItem> shoppingListItems = _context.shoppinglistitems.Where(i => i.shoppinglistid == shoppinglistid).ToList();

            foreach (ShoppingListItem item in shoppingListItems)
            {
                _context.shoppinglistitems.Remove(item);
            }

            ShoppingList shoppingListData = _context.shoppinglists.Where(s => s.shoppinglistid == shoppinglistid).FirstOrDefault();

            _context.shoppinglists.Remove(shoppingListData);
            _context.SaveChanges();

            response.Message = "Success";
            response.Success = true;

            return response;
        }

        #endregion

        #region Shopping List Items Section

        public List<ShoppingListItemDTO> GetShoppingListItems(int shoppinglistid)
        {
            List<ShoppingListItem> shoppingListItems = new List<ShoppingListItem>();
            List<ShoppingListItemDTO> shoppingListItemsData = new List<ShoppingListItemDTO>();

            shoppingListItems = _context.shoppinglistitems.Where(s => s.shoppinglistid == shoppinglistid).ToList();

            foreach (ShoppingListItem item in shoppingListItems)
            {
                ShoppingListItemDTO itemDTO = new ShoppingListItemDTO();

                itemDTO.itemid = item.itemid;
                itemDTO.ShoppingListID = shoppinglistid;
                itemDTO.ShoppingListItemDescription = item.shoppinglistitemdescription;
                itemDTO.QuantityDescription = item.quantitydescription;
                itemDTO.ItemChecked = item.itemchecked;

                shoppingListItemsData.Add(itemDTO);
            }

            List<ShoppingListItemDTO> shoppingListItemsDataSorted = new List<ShoppingListItemDTO>();
            shoppingListItemsDataSorted = shoppingListItemsData.OrderBy(s => s.itemid).ToList();

            return shoppingListItemsDataSorted;
        }

        public async Task<ResponseDTO> CreateShoppingListItem(ItemDTO shoppingListItem, int userid)
        {
            ResponseDTO response = new ResponseDTO();
            ShoppingListItem newItem = new ShoppingListItem();

            try
            {
                newItem.shoppinglistitemdescription = shoppingListItem.shoppinglistitemdescription;
                newItem.shoppinglistid = shoppingListItem.shoppinglistid;
                newItem.quantitydescription = shoppingListItem.quantitydescription;
                newItem.insertiondate = DateTime.Now;
                newItem.itemchecked = shoppingListItem.itemchecked;

                _context.shoppinglistitems.Add(newItem);
                _context.SaveChanges();

                response.Message = "Success";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Failure";
                response.Success = false;
            }        

            return response;
        }

        public async Task<ResponseDTO> UpdateShoppingListItem(ShoppingListItemDTO shoppingListItem)
        {
            ResponseDTO response = new ResponseDTO();
            ShoppingListItem shoppingListData = _context.shoppinglistitems.Where(s => s.itemid == shoppingListItem.itemid).FirstOrDefault();

            try
            {
                shoppingListData.shoppinglistitemdescription = shoppingListItem.ShoppingListItemDescription;
                shoppingListData.quantitydescription = shoppingListItem.QuantityDescription;

                _context.shoppinglistitems.Update(shoppingListData);
                _context.SaveChanges();

                response.Message = "Success";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Failure";
                response.Success = false;
            }
         
            return response;
        }

        public async Task<ResponseDTO> ToggleShoppingListItemChecked(ItemCheckDTO itemCheck)
        {
            ResponseDTO response = new ResponseDTO();
            ShoppingListItem currentItem = _context.shoppinglistitems.Where(i => i.itemid == itemCheck.itemid).FirstOrDefault();

            currentItem.itemchecked = itemCheck.ItemChecked;

            _context.shoppinglistitems.Update(currentItem);
            _context.SaveChanges();

            response.Message = "Success";
            response.Success = true;

            return response;
        }

        public ResponseDTO DeleteShoppingListItem(int shoppinglistitemid)
        {
            ResponseDTO response = new ResponseDTO();
            ShoppingListItem itemData = _context.shoppinglistitems.Where(s => s.itemid == shoppinglistitemid).FirstOrDefault();

            _context.shoppinglistitems.Remove(itemData);
            _context.SaveChanges();

            response.Message = "Success";
            response.Success = true;

            return response;
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
