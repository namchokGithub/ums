using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

/*
 * Namspace: ~/Models/Account
 * Author: Namchok Singhachai
 * Description: Model for table dbo.Account
 */

namespace UMS.Models
{
    public class Account
    {
        [Key]
        [Required]
        public string acc_Id { set; get; } // ไอดี

        [Required]
        public string acc_User { set; get; } // ชื่อผู้ใช้งานที่เป็นอีเมล หรือ เบอร์โทรศัพท์

        [Required]
        public string acc_NormalizedUserName { set; get; } // ชื่อผู้ใช้งานที่เป็นอีเมล หรือ เบอร์โทรศัพท์

        [Required]
        public string acc_Email { set; get; } // อีเมล

        [Required]
        public string acc_NormalizedEmail { set; get; } // อีเมลแบบธรรมดา

        [AllowNull]
        public string acc_PasswordHash { set; get; } // รหัสผ่านแบบแฮช

        [Required]
        public string acc_SecurityStamp { set; get; } // ---

        [Required]
        public string acc_ConcurrencyStamp { set; get; } // ---

        [Required]
        public string acc_Firstname { set; get; } // ชื่อจริง

        [Required]
        public string acc_Lastname { set; get; } // นามสกุล

        [Required]
        [NotNull]
        public char acc_IsActive { set; get; } // สถานะของบัญชีผู้ใช้

    }
}
