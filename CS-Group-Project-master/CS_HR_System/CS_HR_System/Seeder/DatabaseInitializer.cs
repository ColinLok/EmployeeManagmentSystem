using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CS_HR_System.Models;

namespace CS_HR_System.Seeder
{
    public class DatabaseInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        /**
         * SEEDER
         * - ALWAYS DELETE DATABASE FIRST TO SEE CHANGES
         */
        protected override void Seed(ApplicationDbContext context)
        {

            /**
             * 
             * EMPLOYEE SETUP 
             *
             */
            var employees = new List<ApplicationUser>
            {
                // Manager 1
                new ApplicationUser{
                    Id = "1",
                    Name ="Test Manager 1",
                    JobTitle = JobTitle.Manager,
                    EmploymentType = EmploymentType.FullTime,
                    Salary = 100000,
                    HourlyRate = 30.50m,
                    StartDate = DateTime.Parse("01/01/2019 00:00:00"),
                    EndDate = DateTime.Parse("01/01/2020 00:00:00")
                },

                // Employee 2
                new ApplicationUser{
                    Id = "2",
                    Name ="Test Employee 2",
                    JobTitle = JobTitle.Employee,
                    EmploymentType = EmploymentType.FullTime,
                    Salary = 70000,
                    HourlyRate = 21m,
                    StartDate = DateTime.Parse("01/01/2019 00:00:00"),
                    EndDate = DateTime.Parse("01/01/2020 00:00:00")
                },
            };

            employees.ForEach(
                s => context.Users.Add(s)
            );
            context.SaveChanges();


            /**
             * 
             * SCHEDULE SETUP 
             *
             */
            var schedules = new List<Schedule>
             {
                 // Manager 1 - Schedule
                 new Schedule
                 {
                     EmployeeId = "1",
                     StartDate = DateTime.Now,
                     EndDate = DateTime.Now,
                     StartTime = DateTime.Parse("09:00:00"),
                     EndTime = DateTime.Parse("09:00:00"),
                     WorkingDays = new List<DaysOfWeek> {
                         DaysOfWeek.Mon,
                         DaysOfWeek.Tue,
                         DaysOfWeek.Wed,
                         DaysOfWeek.Thu,
                         DaysOfWeek.Fri,
                     }
                 },

                 // Employee 2 - Schedule
                 new Schedule
                 {
                     EmployeeId = "2",
                     StartDate = DateTime.Now.Date,
                     EndDate = DateTime.Now.Date,
                     StartTime = DateTime.Parse("09:00:00"),
                     EndTime = DateTime.Parse("09:00:00"),
                     WorkingDays = new List<DaysOfWeek> {
                         DaysOfWeek.Mon,
                         DaysOfWeek.Tue,
                         DaysOfWeek.Wed,
                         DaysOfWeek.Thu,
                         DaysOfWeek.Fri,
                     }
                 }
             };

            schedules.ForEach(
                s => context.Schedules.Add(s)
            );
            context.SaveChanges();


            /**
             * 
             * SHIFT SETUP 
             *
             */
            var shifts = new List<Shift>
            {
                // Manager 1 - Ontime
                new Shift {
                    EmployeeId = "1",
                    RequestedOff = false,
                    StartTime = DateTime.Parse("01/02/2019 09:00:00"),
                    EndTime = DateTime.Parse("01/02/2019 17:00:00"),
                    Date = DateTime.Now,
                    PunchIn = DateTime.Parse("01/02/2019 09:00:00"),
                    PunchOut = DateTime.Parse("01/02/2019 17:00:00")
                },

                // Manager 1 - Overtime (+4 hours)
                new Shift {
                    EmployeeId = "1",
                    RequestedOff = false,
                    StartTime = DateTime.Parse("01/03/2019 09:00:00"),
                    EndTime = DateTime.Parse("01/03/2019 17:00:00"),
                    Date = DateTime.Now,
                    PunchIn = DateTime.Parse("01/03/2019 09:00:00"),
                    PunchOut = DateTime.Parse("01/03/2019 21:00:00")
                },

                // Employee 2 - Ontime
                new Shift {
                    EmployeeId = "2",
                    RequestedOff = false,
                    StartTime = DateTime.Parse("01/02/2019 09:00:00"),
                    EndTime = DateTime.Parse("01/02/2019 17:00:00"),
                    Date = DateTime.Now,
                    PunchIn = DateTime.Parse("01/02/2019 09:00:00"),
                    PunchOut = DateTime.Parse("01/02/2019 17:00:00")
                },

                // Employee 2 - Overtime (+4 hours)
                new Shift {
                    EmployeeId = "2",
                    RequestedOff = false,
                    StartTime = DateTime.Parse("01/03/2019 09:00:00"),
                    EndTime = DateTime.Parse("01/03/2019 17:00:00"),
                    Date = DateTime.Now,
                    PunchIn = DateTime.Parse("01/03/2019 09:00:00"),
                    PunchOut = DateTime.Parse("01/03/2019 21:00:00")
                },

                // Employee 2 - Late (+4 hours)
                new Shift {
                    EmployeeId = "2",
                    RequestedOff = false,
                    StartTime = DateTime.Parse("01/04/2019 09:00:00"),
                    EndTime = DateTime.Parse("01/04/2019 17:00:00"),
                    Date = DateTime.Now,
                    PunchIn = DateTime.Parse("01/04/2019 13:00:00"),
                    PunchOut = DateTime.Parse("01/04/2019 17:00:00")
                },

                // Employee 2 - Request Time Off - Approved
                new Shift {
                    EmployeeId = "2",
                    RequestedOff = true,
                    StartTime = DateTime.Parse("01/05/2019 09:00:00"),
                    EndTime = DateTime.Parse("01/05/2019 17:00:00"),
                    Date = DateTime.Now,
                    PunchIn = DateTime.Parse("01/04/2019 13:00:00"),
                    PunchOut = DateTime.Parse("01/04/2019 17:00:00")
                },

                // Employee 2 - Request Time Off - Pending
                new Shift {
                    EmployeeId = "2",
                    RequestedOff = true,
                    StartTime = DateTime.Parse("01/06/2019 09:00:00"),
                    EndTime = DateTime.Parse("01/06/2019 17:00:00"),
                    Date = DateTime.Now,
                    PunchIn = DateTime.Parse("01/04/2019 13:00:00"),
                    PunchOut = DateTime.Parse("01/04/2019 17:00:00")
                },

                // Employee 2 - Request Time Off - Denied
                new Shift {
                    EmployeeId = "2",
                    RequestedOff = true,
                    StartTime = DateTime.Parse("01/07/2019 09:00:00"),
                    EndTime = DateTime.Parse("01/07/2019 17:00:00"),
                    Date = DateTime.Now,
                    PunchIn = DateTime.Parse("01/04/2019 13:00:00"),
                    PunchOut = DateTime.Parse("01/04/2019 17:00:00")
                },

            };

            shifts.ForEach(
                s => context.Shifts.Add(s)
            );
            context.SaveChanges();


            /**
             * 
             * REQUEST TIME OFF SETUP 
             *
             */
            var requestTimeOffs = new List<RequestTimeOff> {

                // Employee 2 - Request Time Off - Approved
                new RequestTimeOff {
                    EmployeeId = "2",
                    Type = RequestType.Vacation,
                    Status = RequestStatus.Approved,
                    Date = DateTime.Parse("01/05/2019")
                },

                // Employee 2 - Request Time Off - Pending
                new RequestTimeOff {
                    EmployeeId = "2",
                    Type = RequestType.Vacation,
                    Status = RequestStatus.Pending,
                    Date = DateTime.Parse("01/06/2019")
                },

                // Employee 2 - Request Time Off - Denied
                new RequestTimeOff {
                    EmployeeId = "2",
                    Type = RequestType.Vacation,
                    Status = RequestStatus.Denied,
                    Date = DateTime.Parse("01/07/2019")
                },
            };

            requestTimeOffs.ForEach(
                s => context.RequestTimeOffs.Add(s)
            );
            context.SaveChanges();

            /**
             * 
             * TODO: HOLIDAY SETUP - LOOK INTO NAGER DATE
             * - https://github.com/tinohager/Nager.Date
             *
             */

        }
    }
}