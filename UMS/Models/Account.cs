using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UMS.Models
{
    public class Account
    {
        [Key]
        [Required]
        public string acc_Id { set; get; }

        [Required]
        public string acc_Firstname { set; get; }

        [Required]
        public string acc_Lastname { set; get; }
    }
}
