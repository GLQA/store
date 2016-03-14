using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;


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
                    FirefoxProfile profile = new FirefoxProfile();
                    profile.SetPreference("browser.download.dir", Environment.GetEnvironmentVariable("TEMP"));
                    profile.SetPreference("browser.helperApps.alwaysAsk.force", false);
                    profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "image/png");
                    return new FirefoxDriver();  
                default:
                    return null;
            }
        }
    }
}
