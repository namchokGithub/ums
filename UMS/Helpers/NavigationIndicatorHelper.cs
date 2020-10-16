using System;
using Microsoft.AspNetCore.Mvc;

/*
 * Namspace: ~/Helpers
 * Author: Namchok Singhachai
 * Description: Setting active class in nav menu.
 */

namespace User_Management_System.Helpers
{
    public static class NavigationIndicatorHelper
    {
        /*
         * Name: MakeActiveClass
         * Parametor: urlHelper(IurlHelper), controller(string), action(string)
         * Author: Namchok Singhachai
         * Description: Setting active class in nav menu.
         */
        public static string MakeActiveClass(this IUrlHelper urlHelper, string controller, string action)
        {
            try
            {
                string result = "active";
                string controllerName = urlHelper.ActionContext.RouteData.Values["controller"].ToString();
                string methodName = urlHelper.ActionContext.RouteData.Values["action"].ToString();

                // This controller name is empty then return null
                if (string.IsNullOrEmpty(controllerName)) return null;

                // When this controller name is equals controller name (parametor)
                if (controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
                {
                    // When method name is equals the action (parametor)
                    if (methodName.Equals(action, StringComparison.OrdinalIgnoreCase))
                    {
                        return result; // will return "active" to class in menu
                    }
                } // End check equals string

                return null;
            }
            catch (Exception)
            {
                return null;
            } // End try catch
        } // End MakeActiveClass
    } // End NavigationIndicatorHelper
}
