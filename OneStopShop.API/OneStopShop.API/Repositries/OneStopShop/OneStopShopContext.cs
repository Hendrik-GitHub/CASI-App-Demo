using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OneStopShop.API.Entities;

namespace OneStopShop.API.Repositries.OneStopShop
{
    public class OneStopShopContext : DbContext
    {
        #region Constructor

        public OneStopShopContext(DbContextOptions<OneStopShopContext> options)
            : base(options)
        {

        }

        #endregion

        #region DB Sets

        public DbSet<User> users { get; set; }

        public DbSet<ShoppingList> shoppinglists { get; set; }

        public DbSet<Item> items { get; set; }

        public DbSet<RefreshToken> refreshtokens { get; set; }

        public DbSet<ShoppingListItem> shoppinglistitems { get; set; }

        public DbSet<ItemQuantityType> itemquantitytypes { get; set; }

        #endregion
    }
}
