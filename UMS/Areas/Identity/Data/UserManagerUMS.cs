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
        public UserManagerUMS(AuthDbContext authDbContext, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            try
            {
                _authDbContext = authDbContext;
                Logger.LogTrace("Starting Interface user manager UMS.");
            }
            catch (Exception e)
            {
                Logger.LogCritical(e.Message.ToString());
                Logger.LogTrace("End Interface user manager UMS.");
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
        public virtual async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            var user = await FindByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("UserId Not Found.");
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
            if (true)
            {
                await ValidateEmailAsync(item, errors);
            }
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
                if (owner != null && !EqualityComparer<string>.Default.Equals(owner.Id, user.Id))
                {
                    errors.Add($"User name {user.UserName} is already taken.");
                }
            }
        } // End ValidateUserName
        // ------------------------------------------------------**************************************************--------------------------------------------

        private async Task ValidateEmailAsync(ApplicationUser user, List<string> errors)
        {
            var email = await GetEmailAsync(user);
            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add("Email too short.");
                return;
            }
            try
            {
                var m = new MailAddress(email);
            }
            catch (FormatException)
            {
                errors.Add($"The {email} is not valid.");
                return;
            }
            var owner = await FindByEmailAsync(email);
            if (owner != null && !EqualityComparer<string>.Default.Equals(owner.Id, user.Id))
            {
                errors.Add($"User name {email} is already taken.");
            }
        } // End ValidateEmailAsync

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
