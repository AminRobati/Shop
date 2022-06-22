using Microsoft.EntityFrameworkCore;
using Shop.Domain.Models.Account;
using Shop.Domain.Models.Orders;
using Shop.Domain.Models.ProductEntity;
using Shop.Domain.Models.Site;
using Shop.Domain.Models.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infra.Data.Context
{
    public class ShopDbContext : DbContext
    {

        #region constractor
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {

        }
        #endregion

        #region user
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserFavorite> UserFavorites { get; set; }
        public DbSet<UserCompare> UserCompares { get; set; } 
        #endregion

        #region wallet
        public DbSet<UserWallet> UserWallets { get; set; }
        #endregion

        #region products
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSelectedCategories> ProductSelectedCategories { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductGalleries> ProductGalleries { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        #endregion

        #region site
        public DbSet<Slider> Sliders { get; set; }
        #endregion

        #region order
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        #endregion

    }
}
