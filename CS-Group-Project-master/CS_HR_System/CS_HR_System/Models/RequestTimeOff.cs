using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CS_HR_System.Models
{
    public enum RequestType
    {
        Vacation = 0,
        Sick = 1
    }

    public enum RequestStatus
    {
        Pending = 0,
        Approved = 1,
        Denied = 2
    }

    public class RequestTimeOff
    {
        public int Id { get; set; }

        public string EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public ApplicationUser Employee { get; set; }

        [Display(Name = "Type")]
        public RequestType Type { get; set; }

        [Display(Name = "Status")]
        public RequestStatus Status { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}