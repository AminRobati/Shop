using Microsoft.EntityFrameworkCore;
using Shop.Domain.Interfaces;
using Shop.Domain.Models.Wallet;
using Shop.Domain.ViewModels.Paging;
using Shop.Domain.ViewModels.Wallet;
using Shop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infra.Data.Repositories
{
    public class WalletRepository: IWalletRepository
    {
        #region constractor
        private readonly ShopDbContext _context;
        public WalletRepository(ShopDbContext context)
        {
            _context = context;
        }

        #endregion

        #region wallet
        public async Task CreateWallet(UserWallet userWallet)
        {
             await _context.UserWallets.AddAsync(userWallet);
        }

        public async Task<FilterWalletViewModel> FilterWallets(FilterWalletViewModel filter)
        {
            var query = _context.UserWallets.AsQueryable();

            #region filter
            if (filter.UserId != 0 && filter.UserId!=null)
            {
                query = query.Where(c => c.UserId == filter.UserId);
            }
            #endregion

            #region paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.CountForShowAfterAndBefor);
            var allData = await query.Paging(pager).ToListAsync();
            #endregion

            return filter.SetPaging(pager).SetWallets(allData);
        }

        public async  Task<int> GetUserWalletAmount(long userId)
        {
            var variz = await  _context.UserWallets.Where(c => c.UserId == userId && c.WalletType == WalletType.Variz && c.IsPay)
                .Select(c => c.Amount).ToListAsync();

            var bardasht = await  _context.UserWallets.Where(c => c.UserId == userId && c.WalletType == WalletType.Bardasht && c.IsPay)
                .Select(c => c.Amount).ToListAsync();

            return (variz.Sum() - bardasht.Sum());
        }

        public async Task<UserWallet> GetUserWalletById(long WalletId)
        {
            return await _context.UserWallets.AsQueryable()
                .SingleOrDefaultAsync(c => c.Id == WalletId);
        }

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateWallet(UserWallet wallet)
        {
            _context.UserWallets.Update(wallet);
        }
        #endregion

    }
}
