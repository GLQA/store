using System.Configuration;

namespace Store.Demoqa.Helpers
{
    class Config
    {
        public const int TIMETOWAIT = 10;
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
