using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UMS.Models
{
    public class LogAccount
    {
        [Key]
        [Required]
        public int la_Id {set; get;}

        [Required]
        public int la_acc_Id {set; get;}

        [Required]
        public int la_log_Id {set; get;}
    }
}
