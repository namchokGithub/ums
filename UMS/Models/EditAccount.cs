using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "The First name can not be blank and must only character.")]
        [DataType(DataType.Text, ErrorMessage = "The First name can not be blank and must only character.")]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z]+(([a-zA-Z])?[a-zA-Z]*)*$"
                , ErrorMessage = "The First name can not be blank and must only character.")]
        public string acc_Firstname { set; get; } // ชื่อจริง

        [Required(ErrorMessage = "The Last name can not be blank and must only character.")]
        [DataType(DataType.Text, ErrorMessage = "The Last name can not be blank and must only character.")]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+(([a-zA-Z])?[a-zA-Z]*)*$"
                , ErrorMessage = "The Last name can not be blank and must only character.")]
        public string acc_Lastname { set; get; } // นามสกุล

        public char acc_IsActive { set; get; } // สถานะของบัญชีผู้ใช้

        public string acc_Rolename { set; get; } // ชื่อตำแหน่งของผู้ใช้งาน
    } // End EditAccount
}
