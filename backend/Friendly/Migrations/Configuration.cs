using Friendly.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;
using System.Linq;
using Friendly.Context;
using Microsoft.AspNet.Identity;

namespace Friendly.Migrations
{    
    internal sealed class Configuration : DbMigrationsConfiguration<FriendlyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Friendly.Context.FriendlyContext";
        }

        protected override void Seed(FriendlyContext context)
        {
            SeedFriendlyUsers(context);
        }

        private static void SeedFriendlyUsers(FriendlyContext context)
        {
            var userStore = new UserStore<FriendlyUser>(context);
            var userManager = new UserManager<FriendlyUser>(userStore);
            var firstUser = new FriendlyUser() { UserName = "chrisf", Email = "chris.i.fisher@gmail.com" };
            if (!(context.Users.Any(u => u.UserName == firstUser.UserName)))
                userManager.Create(firstUser, "Huzzah1982");
            var secondUser = new FriendlyUser() { UserName = "donaldw", Email = "donaldwong83@gmail.com" };
            if (!(context.Users.Any(u => u.UserName == secondUser.UserName)))
                userManager.Create(secondUser, "stlukes");
            var thirdUser = new FriendlyUser() { UserName = "robf", Email = "robertmandenofisher@gmail.com" };
            if (!(context.Users.Any(u => u.UserName == thirdUser.UserName)))
                userManager.Create(thirdUser, "63Princes");
        }
    }
}
