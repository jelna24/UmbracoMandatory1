using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AarhusWebDevCoop.ViewModels
{
    public class ContactForm
    {
       
            [Display(Name = "Name")]
            [Required]
            public string Name { get; set; }

            [Display(Name = "Email")]
            [Required]
            [EmailAddress(ErrorMessage = "This is not a valid email address")]
            public string Email { get; set; }

            [Display(Name = "Subject")]
            [Required]
            public string Subject { get; set; }
            [Display(Name = "Message")]
            [Required]
            public string Message { get; set; }
        }
    }
