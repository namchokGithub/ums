/*
 * Name: ErrorViewModel.cs
 * Author: System
 * Description: Model of Edit view model
 */

namespace UMS.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    } // End ErrorViewModel
}