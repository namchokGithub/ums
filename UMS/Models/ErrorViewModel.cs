/*
 * Name: ErrorViewModel.cs
 * Author: System
 * Description: Model of Edit view model
 */

namespace User_Management_System.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    } // End ErrorViewModel
}