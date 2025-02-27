using Microsoft.AspNetCore.Identity;

namespace SolarWatch.Data.Seeder
{

    public class AuthenticationSeeder
    {

        private RoleManager<IdentityRole> roleManager;
        private UserManager<IdentityUser> userManager;

        private readonly string _adminRole;
        private readonly string _userRole;

        public AuthenticationSeeder(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _adminRole = configuration.GetValue<string>("Roles:Admin") ?? "Admin";
            _userRole = configuration.GetValue<string>("Roles:User") ?? "User";
        }
        public void AddRoles()
        {
            var tAdmin = CreateAdminRole(roleManager);
            tAdmin.Wait();

            var tUser = CreateUserRole(roleManager);
            tUser.Wait();
        }

        public void AddAdmin()
        {
            var tAdmin = CreateAdminIfNotExists();
            tAdmin.Wait();
        }


        private async Task CreateAdminRole(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(_adminRole));
        }

        async Task CreateUserRole(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(_userRole));
        }

        private async Task CreateAdminIfNotExists()
        {
            var adminInDb = await userManager.FindByEmailAsync("admin@admin.com");
            if (adminInDb == null)
            {
                var admin = new IdentityUser { UserName = "admin", Email = "admin@admin.com" };
                var adminCreated = await userManager.CreateAsync(admin, "admin123");

                if (adminCreated.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, _adminRole);
                }
            }
        }

    }

}
