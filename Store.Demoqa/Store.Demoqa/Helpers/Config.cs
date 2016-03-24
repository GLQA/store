using System.Configuration;
using System.IO;

namespace Store.Demoqa.Helpers
{
    class Config
    {
        public const int TIME_TO_WAIT = 20;

        public const string GRID_HUB_PATH = "http://localhost:4444/wd/hub";

        public static string SchemaFilePath
        {
            get
            {
                return Path.GetFullPath("../../Helpers/ReportConveringSchema.xslt");
            }
        }
        
        public static string TestResultsXMLFilePath
        {
            get
            {
                return Path.GetFullPath("TestResult.xml");
            }
        }

        public static string TestResultsHTMLFilePath 
        {
            get
            {
                return Path.GetFullPath("TestResultHTML.html");
            }
        }
        
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
