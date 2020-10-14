using System;

/*
 * Name: LogAccount.cs
 * Namespace: Models
 * Author: System
 * Description: Model of Edit view model
 */

namespace UMS.Models
{
    public class ErrorViewModel
    {
        public string RequestId
        {
            get; set;
        } // Activity current ID

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    } // End ErrorViewModel
}
