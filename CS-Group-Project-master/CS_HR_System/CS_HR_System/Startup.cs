using System.Web.Security;
using CS_HR_System.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CS_HR_System.Startup))]
namespace CS_HR_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        // In this method we will create default User roles and Admin user for login   
        private void createRolesandUsers()
        {
            // creating Creating Manager role    
            if (!Roles.RoleExists("Manager"))
            {
                Roles.CreateRole("Manager");
            }

            // creating Creating Employee role    
            if (!Roles.RoleExists("Employee"))
            {
                Roles.CreateRole("Employee");
            }
        }
    }
}
