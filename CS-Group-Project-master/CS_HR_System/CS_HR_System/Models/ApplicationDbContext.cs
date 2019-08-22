using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CS_HR_System.Seeder;

namespace CS_HR_System.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //System.Data.Entity.Database.SetInitializer(new DatabaseInitializer());
            //Database.Initialize(true);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<CS_HR_System.Models.PayRoll> PayRolls { get; set; }

        public System.Data.Entity.DbSet<CS_HR_System.Models.PayHistory> PayHistories { get; set; }

        public System.Data.Entity.DbSet<CS_HR_System.Models.RequestTimeOff> RequestTimeOffs { get; set; }

        public System.Data.Entity.DbSet<CS_HR_System.Models.Schedule> Schedules { get; set; }

        public System.Data.Entity.DbSet<CS_HR_System.Models.Shift> Shifts { get; set; }

        public System.Data.Entity.DbSet<CS_HR_System.Models.HolidayModels> Holidays { get; set; }
    }
}