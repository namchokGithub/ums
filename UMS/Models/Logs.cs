using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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
    public class Logs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int log_Id { set; get; }

        [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string log_datetime { set; get; }

        [AllowNull]
        public string log_date { set; get; }

        [AllowNull]
        public string log_time { set; get; }

        [AllowNull]
        [Column(TypeName = "nvarchar(256)")]
        public string log_level { set; get; }

        [AllowNull]
        [Column(TypeName = "nvarchar(256)")]
        public string log_logger { set; get; }

        [AllowNull]
        [Column(TypeName = "nvarchar(450)")]
        public string log_message { set; get; }

        [AllowNull]
        [Column(TypeName = "nvarchar(450)")]
        public string log_exception { set; get; }

        [AllowNull]
        [Column(TypeName = "nvarchar(256)")]
        public string log_user_identity { set; get; }

        [AllowNull]
        [Column(TypeName = "nvarchar(256)")]
        public string log_mvc_action { set; get; }

        //[AllowNull]
        //[Column(TypeName = "nvarchar(256)")]
        //public string log_mvc_controller { set; get; }

        [AllowNull]
        [Column(TypeName = "nvarchar(256)")]
        public string log_filename { set; get; }

        [AllowNull]
        [Column(TypeName = "nvarchar(256)")]
        public string log_linenumber { set; get; }

    } // End Log
}
