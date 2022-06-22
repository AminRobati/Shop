using Microsoft.AspNetCore.Http;
using Shop.Domain.Models.Account;
using Shop.Domain.ViewModels.Account;
using Shop.Domain.ViewModels.Admin.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces
{
    public interface IUserService
    {
        #region account
        Task<RegisterUserResult> RegisterUser(RegisterUserViewModel register);
        Task<LoginUserResult> loginUser (LoginUserViewModel login);
        
        Task<User> GetUserByPhoneNumber(string phoneNumber);
        Task<ActiveAccountResult> ActiveAccount (ActiveAccountViewModel activeAccount);
        Task<User> GetUserById(long userId);
        bool CheckPermission(long permissionId, string phoneNumber);
        #endregion

        #region profile
        Task<EditUserProfileViewModel> GetEditUserProfile(long userId); 
        Task<EditUserProfileResult> EditProfile(long UserId, IFormFile userAvatar, EditUserProfileViewModel editUserProfile);
        Task<ChangePasswordResult> ChangePassword(long UserId, ChangePasswordViewModel changePassword);
        Task<bool> AddProductToFavorite(long productId , long userId);
        Task<bool> AddProductToCompare(long productId, long userId);
        Task<List<UserCompare>> GetUserCompare(long userId);
        Task<int> UserFavoriteCount(long userId);
        Task<List<UserFavorite>> GetUserFavorite(long userId);
        Task<bool> RemoveAllUserCompare(long userId);
        Task<bool> RemoveUserCompare(long Id);

        Task<UserComparesViewModel> UserCompares(UserComparesViewModel userCompares);
        Task<UserFavoritsViewModel> UserFavorits(UserFavoritsViewModel userFavorits);
        #endregion

        #region admin
        Task<FilterUserViewModel> FilterUsers(FilterUserViewModel filter);
        Task<EditUserFromAdmin> GetEditUserFromAdmin(long userId);

        Task<EditUserFromAdminResult> EditUserFromAdmin(EditUserFromAdmin editUser);
        Task<CreateOrEditRoleViewModel> GetEditRoleById(long roleId);
        Task<CreateOrEditRoleResult> CreateOrEditRole(CreateOrEditRoleViewModel createOrEditRole);
        Task<FilterRolesViewModel> FilterRoles(FilterRolesViewModel filter);
        Task<List<Permission>> GetAllActivePermission();
        Task<List<Role>> GetAllActiveRoles();
        #endregion

    }
}
