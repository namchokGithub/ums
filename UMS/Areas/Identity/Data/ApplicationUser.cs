using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace UMS.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        [Column(TypeName = "nvarchar(256)")]
        public string acc_Salt { set; get; }

        [Column(TypeName = "nvarchar(256)")]
        public string acc_User { set; get; }

        [PersonalData]
        [Column(TypeName = "nvarchar(256)")]
        public string acc_Firstname { set; get; }

        [PersonalData]
        [Column(TypeName = "nvarchar(256)")]
        public string acc_Lastname { set; get; }

        [Column(TypeName = "char(10)")]
        public char acc_IsActive { set; get; }

        [Column(TypeName = "Int")]
        public int acc_ro_Id { set; get; }

        [Column(TypeName = "Int")]
        public int acc_ta_Id { set; get; }
    }
}
