using Shop.Application.Interfaces;
using Shop.Domain.Interfaces;
using Shop.Domain.Models.Wallet;
using Shop.Domain.ViewModels.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{
    public class WalletService: IWalletService
    {
        #region constractor
        private readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;

        public WalletService(IWalletRepository walletRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
        }

        #endregion

        #region chargewallet
        public async Task<long> ChargeWallet(long userId, ChargeWalletViewModel chargeWallet, string description)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null) return 0; 
            

            var vallet = new UserWallet
            {
                UserId = userId,
                Amount = chargeWallet.Amount,
                Description = description,
                IsPay = false,
                WalletType = WalletType.Variz,
            };
            await _walletRepository.CreateWallet(vallet);
            await _walletRepository.SaveChange();

            return vallet.Id;

        }

        public async  Task<FilterWalletViewModel> FilterWallets(FilterWalletViewModel filter)
        {
            return await _walletRepository.FilterWallets(filter);
        }

        public async  Task<int> GetUserWalletAmount(long userId)
        {
            return await  _walletRepository.GetUserWalletAmount(userId);
        }

        public  async Task<UserWallet> GetUserWalletById(long WalletId)
        {
            return await _walletRepository.GetUserWalletById(WalletId);
        }

        public async Task<bool> UpdateWalletForCharge(UserWallet wallet)
        {
            if (wallet != null)
            {
                wallet.IsPay= true;
                _walletRepository.UpdateWallet(wallet);
                await _walletRepository.SaveChange();
                return true;
            }
            return false;
            
        }
        #endregion
    }
}
