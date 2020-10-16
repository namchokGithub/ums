using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 * Name: ApplicationUser (Extend: IdentityUser)
 * Author: Namchok Singhachai
 * Description: User identity model for this application.
 */

namespace User_Management_System.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Please enter first name")]
        [PersonalData]
        [Column(TypeName = "nvarchar(256)")]
        public string acc_Firstname { set; get; }
        [Required(ErrorMessage = "Please enter first name")]
        [PersonalData]
        [Column(TypeName = "nvarchar(256)")]
        public string acc_Lastname { set; get; }
        [AllowNull]
        [Column(TypeName = "char(1)")]
        public char acc_IsActive { set; get; } // For check status user
    } // End ApplicationUser
}