using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;


namespace Store.Demoqa.Helpers
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
