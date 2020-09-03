using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

/*
 * Namspace: ~/Models/EditAccount
 * Author: Namchok Singhachai
 * Description: Model for Edit accout in Manage user
 */

namespace UMS.Models
{
    public class EditAccount
    {
        [Key]
        public string acc_Id { set; get; } // ไอดี

        public string acc_User { set; get; } // ชื่อผู้ใช้งานที่เป็นอีเมล หรือ เบอร์โทรศัพท์

        public string acc_Email { set; get; } // อีเมล

        [Required(ErrorMessage = "Please enter firstname")]
        [DataType(DataType.Text, ErrorMessage = "The firstname have only text. No number, digit and special charactor")]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z]+(([a-zA-Z])?[a-zA-Z]*)*$"
                , ErrorMessage = "The first name have only text. No number no space digit and special charactor")]
        public string acc_Firstname { set; get; } // ชื่อจริง

        [Required(ErrorMessage = "Please enter lastname")]
        [DataType(DataType.Text, ErrorMessage = "The lastname have only text. No number no space, digit and special charactor")]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+(([a-zA-Z])?[a-zA-Z]*)*$"
                , ErrorMessage = "The last name have only text. No number no space digit and special charactor")]
        public string acc_Lastname { set; get; } // นามสกุล

        public char acc_IsActive { set; get; } // สถานะของบัญชีผู้ใช้

        public string acc_Rolename { set; get; } // ชื่อตำแหน่งของผู้ใช้งาน
    } // End EditAccount
}
