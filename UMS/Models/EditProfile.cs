using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UMS.Models
{
    public class EditProfile
    {
        [Key]
        public string acc_Id { set; get; } // ไอดี

        public string acc_Firstname { set; get; } // ชื่อจริง

        public string acc_Lastname { set; get; } // นามสกุล

        public string acc_PasswordHash { set; get; } // รหัสผ่าน
    }
}
