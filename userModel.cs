using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPNETMVC_BootstrapCustomThemeDemo.Models
{
    public class userModel
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name="UserName")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Picture")]
        public string Picture  { get; set; }
        //public int RoleId {  get; set; }



    }
}