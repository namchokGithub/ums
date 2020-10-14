using System.ComponentModel.DataAnnotations;

/*
 * Namespace: ~/Models/EditProfile.cs
 * Author: Wannapa Srijermtong
 * Description:  Model for table dbo.Account
 */

namespace UMS.Models
{
    public class EditProfile
    {
        [Key]
        public string acc_Id { set; get; } // ไอดี

        public string acc_Firstname { set; get; } // ชื่อจริง

        public string acc_Lastname { set; get; } // นามสกุล

        public string acc_TypeAccoutname { set; get; } // ชื่อประเภทของผู้ใช้งาน
    }
}
