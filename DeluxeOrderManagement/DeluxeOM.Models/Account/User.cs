using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DeluxeOM.Models.Account
{
    public class User
    {
        public User()
        {
            this.Roles = new List<RoleMembership>();
            //this.SelectedRoles = new List<int>();
            this.Privs = new List<Privilleges>();
        }

        //[Required]
        public string Username { get; set; }            // email address

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int UserId { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [System.Web.Mvc.Remote("IsUserEmailAvailable", "Manage", ErrorMessage = "Email already exists!")]
        public string Email { get; set; }

        [Display(Name = "Phone No")]
        public string PhoneNumber { get; set; }
        
        public string Title { get; set; }
        
        public bool Active { get; set; }

        [StringLength(50), DataType(DataType.Password)]
        [RegularExpression("(^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#\\$&\\*\\?]).{12,}$)", ErrorMessage = "Passwords must be at least 12 characters, contain at least one uppercase and one lower case letter, one number, and one special character (!@#$&*?).")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public int LoginAttempts { get; set; }
        public DateTime CreateDatetime { get; set; }
        
        public Nullable<DateTime> PasswordDatetime { get; set; }
        
        public Nullable<DateTime> LastLoginDatetime { get; set; }
        
        public DateTime ModifyDatetime { get; set; }

        [Display(Name = "Role")]
        public int SelectedRoleId { get; set; }

        public virtual List<RoleMembership> Roles { get; set; }

        public virtual List<Privilleges> Privs { get; set; }
        public string Role { get; set; }
    }
}
