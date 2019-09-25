using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services
{
    public static class WebHostExtensions
    {
        public static IWebHost SomeExtension(this IWebHost webHost)
        {
           

            // Your initialisation code here.
            // ...

            return webHost; 
        }
    }
}
