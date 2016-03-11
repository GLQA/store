using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Demoqa
{
    class BrowserFactory
    {
        public static IWebDriver CreateDriverInstance()
        {
           string browserName = Config.GetBrowser();
            switch (browserName)
            {
                case "chrome":
                    return new ChromeDriver(Config.GetBrowserPath());
                case "firefox":
                    return new FirefoxDriver();
                default:
                    return null;
            }
        }
    }
}
