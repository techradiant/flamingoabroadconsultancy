using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FAC.Web.Models
{
    public class Employee
    {        

        [Key]
        public int EmployeeId { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name = "Designation")]
        public string EmployeeDesignation { get; set; }
        public Department Department { get; set; }
        [Display(Name = "Address")]
        public string EmployeeAddress { get; set; }
      
        [Display(Name = "Phone")]
        public string EmployeePhone { get; set; }
        [Display(Name = "Gender")]
        public string EmployeeGender { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
       
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Pin Code")]
        public int PinCode { get; set; }
        [Display(Name = "Description")]
        public string EmployeeDescription { get; set; }
        [Display(Name = "Dept Name")]

        public int DepartmentId { get; set; }

        [Display(Name = "Profile Photo")]
        public string ProfilePhotoUrl { get; set; }

        [Display(Name = "Include In Team List")]
        public bool IncludeInTeamList { get; set; }

        [Display(Name = "Team List View Order")]
        public int TeamListViewOrder { get; set; }


        [Display(Name = "Twitter Url")]
        public string TwitterUrl { get; set; }


        [Display(Name = "Facebook Url")]
        public string FacebookUrl { get; set; }


        [Display(Name = "Instagram Url")]
        public int InstagramUrl { get; set; }
       
    }
}