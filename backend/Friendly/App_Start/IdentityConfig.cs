using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Friendly.Models;
using Friendly.Context;

namespace Friendly
{
    public class FriendlyUserManager : UserManager<FriendlyUser>
    {
        public FriendlyUserManager(IUserStore<FriendlyUser> store) : base(store)
        {
        }

        public static FriendlyUserManager Create(IdentityFactoryOptions<FriendlyUserManager> options, IOwinContext context)
        {
            var manager = new FriendlyUserManager(new UserStore<FriendlyUser>(context.Get<FriendlyContext>()));
            manager.UserValidator = new UserValidator<FriendlyUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<FriendlyUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
