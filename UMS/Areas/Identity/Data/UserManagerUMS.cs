using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using UMS.Data;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading;



/*
 * Name: UMS.Areas.Identity.Data.UserManagerUMS.cs
 * Author: Namchok Singhachai
 * Descriptions: For custom manager user.
 */

namespace UMS.Areas.Identity.Data
{
    public class UserManagerUMS : UserManager<ApplicationUser>
    {
        private readonly AuthDbContext _authDbContext;
        private readonly ILogger<UserManager<ApplicationUser>> _logger;
        public UserManagerUMS(AuthDbContext authDbContext, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            try
            {
                _authDbContext = authDbContext;
                _logger = logger;
                _logger.LogTrace("Starting Interface user manager UMS.");
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message.ToString());
                _logger.LogTrace("End Interface user manager UMS.");
            } // End try catch
        } // End constructor

        public async Task<IdentityResult> NewCreateAsync(ApplicationUser user, string password)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            var result = await UpdatePassword(passwordStore, user, password);
            if (!result.Succeeded)
            {
                return result;
            }
            return await NewCreateAsync(user);
        }

        public async Task<IdentityResult> NewCreateAsync(ApplicationUser user)
        {
            ThrowIfDisposed();
            await GetSecurityStore().SetSecurityStampAsync(user, NewSecurityStamp(), new CancellationToken(false));
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var errors = new List<string>();
            await ValidateUserName(user, errors);
            if (errors.Count > 0)
            {
                return IdentityResult.Failed(new IdentityError { Description= string.Join(",",errors.ToArray()) });
            }
            await Store.CreateAsync(user, new CancellationToken(false));
            return IdentityResult.Success;
        }

        private IUserPasswordStore<ApplicationUser> GetPasswordStore()
        {
            var cast = Store as IUserPasswordStore<ApplicationUser>;
            if (cast == null)
            {
                throw new NotSupportedException("Store Not IUserPasswordStore.");
            }
            return cast;
        }

        protected virtual async Task<IdentityResult> UpdatePassword(IUserPasswordStore<ApplicationUser> passwordStore, ApplicationUser user, string newPassword)
        {
            var RegExPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[#$!%@*?&])[A-Za-z0-9#$!%@*?&]+$";
            if (!Regex.IsMatch(newPassword, RegExPassword))
            {
                return IdentityResult.Failed(new IdentityError { Description= "The password must contain at least 1 uppercase, 1 lowercase, 1 digit and 1 special character." });
            }
            await passwordStore.SetPasswordHashAsync(user, PasswordHasher.HashPassword(user, newPassword), new CancellationToken(false));
            await GetSecurityStore().SetSecurityStampAsync(user, NewSecurityStamp(), new CancellationToken(false));
            return IdentityResult.Success;
        }

        private static string NewSecurityStamp()
        {
            return Guid.NewGuid().ToString();
        }

        private IUserSecurityStampStore<ApplicationUser> GetSecurityStore()
        {
            var cast = Store as IUserSecurityStampStore<ApplicationUser>;
            if (cast == null)
            {
                throw new NotSupportedException("Store Not IUserSecurityStampStore.");
            }
            return cast;
        }

        // New Add login
        public async Task<IdentityResult> NewAddLoginAsync(ApplicationUser userParam, UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            var user = await FindByIdAsync(userParam.Id);
            if (user == null)
            {
                throw new InvalidOperationException("User Not Found.");
            }
            // Check existing user
            var existingUser = await FindByLoginAsync(login.LoginProvider, login.ProviderKey);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "ExternalLogin Exists." });
            }
            await AddLoginNewAsync(user, login);
            return await UpdateAsync(user);
        } // End AddLoginAsync

        override public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var result = await ValidateAsync(user);
            if (!result.Succeeded)
            {
                return result;
            }
            // ADD Normalize
            _authDbContext.Update(user);
            await _authDbContext.SaveChangesAsync();
            return IdentityResult.Success;
        } // End overried UpdateAsync

        public virtual async Task<IdentityResult> ValidateAsync(ApplicationUser item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var errors = new List<string>();
            await ValidateUserName(item, errors);
            if (errors.Count > 0)
            {
                IdentityError err = new IdentityError
                {
                    Description = string.Join(",", errors.ToArray())
                };
                return IdentityResult.Failed(err);
            }
            return IdentityResult.Success;
        } // End ValidateAsync

        /*
         * Name
         */
        // ------------------------------------------------------**************************************************--------------------------------------------
        private async Task ValidateUserName(ApplicationUser user, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                errors.Add("Username too short.");
            }
            else if (!Regex.IsMatch(user.UserName, @"^[A-Za-z0-9@_\.]+$"))
            {
                // If any characters are not letters or digits, its an illegal user name
                errors.Add("Invalid Username.");
            }
            else
            {
                // Create store procedure find active user status
                var owner = await FindByNameAsync(user.UserName);
                _logger.LogTrace("User ID: " + user.Id);
                _logger.LogTrace("Owner ID: " + owner.Id);
                _logger.LogTrace("Owner Status: " + owner.acc_IsActive);
                _logger.LogTrace("Check ID: " + string.Equals(owner.Id, user.Id));
                var checkStatus = EqualityComparer<string>.Default.Equals(owner.Id, user.Id);
                if (owner != null && checkStatus && owner.acc_IsActive!= 'Y')
                {
                    errors.Add($"User name {user.UserName} has been taken.");
                }
            }
        } // End ValidateUserName !EqualityComparer<string>.Default.Equals(owner.Id, user.Id)
        // ------------------------------------------------------**************************************************--------------------------------------------

        public Task AddLoginNewAsync(ApplicationUser user, UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            string sqlAddUserLogin = @$"ums_Add_user_login '{login.LoginProvider}', '{login.ProviderDisplayName}', '{login.ProviderKey}', '{user.Id}'";
            _authDbContext.Database.ExecuteSqlRawAsync(sqlAddUserLogin);
            _authDbContext.SaveChanges();
            return Task.FromResult(0);
        } // End AddLoginNewAsync
    } // End UserManagerUMS
}
