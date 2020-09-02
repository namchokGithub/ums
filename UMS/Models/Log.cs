using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

/*
 * Name: LogAccount.cs
 * Namespace: Models
 * Author: Namchok Singhachai
 * Description: Model of log account
 */

namespace UMS.Models
{
    public class Log
    {
        [Key]
        [Required]
        public int log_Id { set; get; }

        [Required]
        [Display(Name = "Date")]
        public string log_datetime { set; get; }
    } // End Log
}
