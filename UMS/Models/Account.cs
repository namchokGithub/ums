using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 * Namspace: ~/Models/Account
 * Author: Namchok Singhachai
 * Description: Model for table dbo.Account.
 */

namespace User_Management_System.Models
{
    public class Account
    {
        [Key]
        public string acc_Id { set; get; } // ไอดี
        [AllowNull]
        [Column(TypeName = "nvarchar(256)")]
        public string acc_User { set; get; } // ชื่อผู้ใช้งานที่เป็นอีเมล หรือ เบอร์โทรศัพท์
        [AllowNull]
        [Column(TypeName = "nvarchar(256)")]
        public string acc_NormalizedUserName { set; get; } // ชื่อผู้ใช้งานที่เป็นอีเมล หรือ เบอร์โทรศัพท์
        [AllowNull]
        [Column(TypeName = "nvarchar(256)")]
        public string acc_Email { set; get; } // อีเมล
        [AllowNull]
        [Column(TypeName = "nvarchar(256)")]
        public string acc_NormalizedEmail { set; get; } // อีเมลแบบธรรมดา
        [AllowNull]
        [Column(TypeName = "nvarchar(MAX)")]
        public string acc_PasswordHash { set; get; } // รหัสผ่านแบบแฮช
        [AllowNull]
        [Column(TypeName = "nvarchar(MAX)")]
        public string acc_SecurityStamp { set; get; } // ---
        [AllowNull]
        [Column(TypeName = "nvarchar(MAX)")]
        public string acc_ConcurrencyStamp { set; get; } // ตรวจสอบการแก้ไขว่าแก้ไขล่าสุดเมื่อใด

        [Required(ErrorMessage = "Please enter first name")]
        [PersonalData]
        [Column(TypeName = "nvarchar(256)")]
        [DataType(DataType.Text, ErrorMessage = "The first name have only text. No number no space, digit and special charactor")]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z]+(([a-zA-Z])?[a-zA-Z]*)*$"
                , ErrorMessage = "The first name have only text. No number no space digit and special charactor")]
        public string acc_Firstname { set; get; } // ชื่อจริง

        [Required(ErrorMessage = "Please enter last name")]
        [PersonalData]
        [Column(TypeName = "nvarchar(256)")]
        [DataType(DataType.Text, ErrorMessage = "The last name have only text. No number no space, digit and special charactor")]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+(([a-zA-Z])?[a-zA-Z]*)*$"
                , ErrorMessage = "The last name have only text. No number no space digit and special charactor")]
        public string acc_Lastname { set; get; } // นามสกุล
        [AllowNull]
        [Column(TypeName = "char(1)")]
        public char acc_IsActive { set; get; } // สถานะของบัญชีผู้ใช้
        [AllowNull]
        [NotMapped]
        public string acc_Rolename { set; get; } // ชื่อตำแหน่งของผู้ใช้งาน
        [AllowNull]
        [NotMapped]
        public string acc_TypeAccoutname { set; get; } // ชื่อประเภทของผู้ใช้งาน

    } // End Account 
}