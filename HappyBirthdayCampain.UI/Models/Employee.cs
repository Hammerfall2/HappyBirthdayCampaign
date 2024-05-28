using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HappyBirthdayCampain.UI.Models
{

    public partial class Employee
    {
        public int Id { get; set; }

        public string FullName { get; set; }
	}
    

	public partial class EmployeeLogin
    {
        [StringLength(50)]
        [DisplayName("User Name")]
        [Required(ErrorMessage ="User name is required")]
        public string UserName { get; set; }

        [StringLength(50)]
        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }

}