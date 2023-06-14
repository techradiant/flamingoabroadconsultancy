using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FAC.Web.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }        
    }
}