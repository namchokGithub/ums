using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

/*
 * Name: Log.cs
 * Namespace: Models
 * Author: Namchok
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
    }
}
