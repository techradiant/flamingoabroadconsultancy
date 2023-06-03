using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FAC.Web.Models
{
    public class Department1
    {
        [Key]
        public int DepartmentId { get; set; }



        public string DepartmentName { get; set; }

        internal IEnumerable<Department1> ToList()
        {
            throw new NotImplementedException();
        }
    }
}