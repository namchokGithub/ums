using System;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 * Name: LogAccount.cs
 * Author: Namchok Singhachai
 * Description: Model of table log.
 */

namespace User_Management_System.Models
{
    public class Logs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int log_Id { set; get; }

        [DataType(DataType.Date)]
        [Column(TypeName = "datetime")]
        public DateTime log_datetime { set; get; }

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

        [AllowNull]
        [Column(TypeName = "nvarchar(256)")]
        public string log_filename { set; get; }

        [AllowNull]
        [Column(TypeName = "nvarchar(256)")]
        public string log_linenumber { set; get; }
    } // End Logs
}