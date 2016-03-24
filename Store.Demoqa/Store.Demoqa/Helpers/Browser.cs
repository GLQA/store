using OpenQA.Selenium;
using System;

namespace Store.Helpers
{
    public class Browser
    {
        private IWebDriver driver;

        public IWebDriver GetDriver()
        {
            if (driver == null)
            {
                driver = BrowserFactory.CreateDriverInstance();
            }
            return driver;
        }

        private string SITEURL
        {
            get
            {
                return Config.GetSite();
            }
        }

        public void SetDriver()
        {
            MaximizeWindow();
            SetImplicitWaitFromConfig();
        }

        public void MaximizeWindow()
        {
            driver.Manage().Window.Maximize();
        }

        public void SetImplicitWaitFromConfig()
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.TIME_TO_WAIT));
        }

        public void GoToSiteFromConfig()
        {
            driver.Navigate().GoToUrl(Config.GetSite());
        }

        public Browser(){}
    }
}
