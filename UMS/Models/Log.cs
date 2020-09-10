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
    public class Log
    {        
        [MaybeNull]
        public string log_datetime { set; get; }

        [MaybeNull]
        public string log_date { set; get; }

        [MaybeNull]
        public string log_time { set; get; }

        [MaybeNull]
        public string log_level { set; get; }

        [MaybeNull]
        public string log_logger { set; get; }

        [MaybeNull]
        public string log_user_identity { set; get; }

        [MaybeNull]
        public string log_mvc_action { set; get; }

        [MaybeNull]
        public string log_mvc_controller { set; get; }

        [MaybeNull]
        public string log_filename { set; get; }

        [MaybeNull]
        public string log_linenumber { set; get; }

        [MaybeNull]
        public string log_message { set; get; }

        [MaybeNull]
        public string log_exception { set; get; }

    } // End Log
}
