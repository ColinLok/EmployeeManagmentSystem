using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CS_HR_System.Models
{
    public class PayRoll
    {
        [Key]
        public int Id { get; set; }

        public string EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public ApplicationUser Employee { get; set; }

        [Display(Name = "Hourly Rate")]
        public decimal HourlyRate { get; set; }

        [Display(Name = "Hours Worked")]
        public decimal HoursWorked { get; set; }

        [Display(Name = "Overtime Hours Worked")]
        public decimal OvertimeHoursWorked { get; set; }

        [Display(Name = "TimeOff Hours")]
        public decimal TimeOffHours { get; set; }

        [Display(Name = "Total Paid")]
        public decimal TotalPaid { get; set; }

        [Display(Name = "Tax Rate")]
        public decimal TaxRate { get; set; }

        [Display(Name = "Net Paid")]
        public decimal NetPaid { get; set; }

        [Display(Name = "Payroll Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PayrollStartDate { get; set; }

        [Display(Name = "Payroll End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PayrollEndDate { get; set; }
    }
}