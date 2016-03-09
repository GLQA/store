using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Store.Demoqa
{
    class Config
    {
        /// <summary>
        /// Gets the site.
        /// </summary>
        /// <returns></returns>
        public static string GetSite()
        {
            return ConfigurationManager.AppSettings["DemoSiteURL"].ToString();
        }

        /// <summary>
        /// Gets the browser path.
        /// </summary>
        /// <returns></returns>
        public static string GetBrowserPath()
        {
            return ConfigurationManager.AppSettings["driver"];
        }

        /// <summary>
        /// Gets the browser.
        /// </summary>
        /// <returns></returns>
        public static string GetBrowser()
        {
            return ConfigurationManager.AppSettings["browser"].ToString();   
        }
    }
}
