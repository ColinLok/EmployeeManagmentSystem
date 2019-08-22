using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CS_HR_System.Models
{
    public class PayHistory
    {
        public int Id { get; set; }

        public string EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public ApplicationUser Employee { get; set; }

        [Display(Name = "Salary Before")]
        public int SalaryBefore { get; set; }

        [Display(Name = "Salary After")]
        public int SalaryAfter { get; set; }

        [Display(Name = "Hourly Before")]
        public decimal HourlyBefore { get; set; }

        [Display(Name = "Hourly After")]
        public decimal HourlyAfter { get; set; }

        [Display(Name = "Date Applied")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateApplied { get; set; }
    }
}