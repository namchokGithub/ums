using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace UMS.Models
{
    public class EditAccount
    {
        [Key]
        public string acc_Id { set; get; } // ไอดี

        public string acc_User { set; get; } // ชื่อผู้ใช้งานที่เป็นอีเมล หรือ เบอร์โทรศัพท์

        public string acc_Email { set; get; } // อีเมล

        public string acc_Firstname { set; get; } // ชื่อจริง

        public string acc_Lastname { set; get; } // นามสกุล

        public char acc_IsActive { set; get; } // สถานะของบัญชีผู้ใช้

        public string acc_Rolename { set; get; } // ชื่อตำแหน่งของผู้ใช้งาน
    }
}
