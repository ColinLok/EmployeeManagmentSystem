using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using CS_HR_System.Models;
using Microsoft.AspNet.Identity;

namespace CS_HR_System.HelperMethods
{
    public static class HelperMethods
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static bool authenticateManager(string userId)
        {
            /**
             * Returns TRUE if the current logged in user is a Manager.
             */
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == userId);
            return (currentUser.JobTitle != 0);
        }
    }
}