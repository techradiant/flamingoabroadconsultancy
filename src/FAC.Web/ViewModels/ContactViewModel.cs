using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FAC.Web.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }

        public string Phone { get; set; }

        [Required]
        public string Message { get; set; }
    }
}