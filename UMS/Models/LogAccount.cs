using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

/*
 * Name: LogAccount.cs
 * Namespace: Models
 * Author: Namchok Singhachai
 * Description: Model of log account
 */

namespace UMS.Models
{
    public class LogAccount
    {
        [Key]
        [Required]
        public int la_Id {set; get;} // ID

        [Required]
        public int la_acc_Id {set; get;} // Account ID

        [Required]
        public int la_log_Id {set; get;} // Log ID
    }// End LogAccount
}
