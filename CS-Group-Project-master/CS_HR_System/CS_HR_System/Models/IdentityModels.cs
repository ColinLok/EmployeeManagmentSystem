using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CS_HR_System.Models
{
    public enum JobTitle
    {
        [Display(Name = "Manager")]
        Manager = 0,
        [Display(Name = "Employee")]
        Employee = 1
    }

    public enum EmploymentType
    {
        [Display(Name = "Full-Time")]
        FullTime = 0,
        [Display(Name = "Part-Time")]
        PartTime = 1,
        [Display(Name = "On-Call")]
        OnCall = 2,
    }
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Emergency Contact Name")]
        public string EmergencyContactName { get; set; }

        [Display(Name = "Emergency Contact Phone")]
        public string EmergencyContactPhone { get; set; }

        [Display(Name = "Job Title")]
        public JobTitle JobTitle { get; set; }

        [Display(Name = "Employment Type")]
        public EmploymentType EmploymentType { get; set; }

        [Display(Name = "Salary")]
        public int Salary { get; set; }

        [Display(Name = "Hourly Rate")]
        public decimal HourlyRate { get; set; }

        [Display(Name = "Schedule")]
        public List<Schedule> Schedule { get; set; }

        [Display(Name = "Shifts")]
        public List<Shift> Shifts { get; set; }

        [Display(Name = "Request Time Off")]
        public List<RequestTimeOff> RequestTimeOff { get; set; }

        [Display(Name = "Payroll")]
        public List<PayRoll> Payroll { get; set; }

        [Display(Name = "Payroll History")]
        public List<PayHistory> PayHistory { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
    }
}