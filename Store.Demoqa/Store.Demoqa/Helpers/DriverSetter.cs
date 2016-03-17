using OpenQA.Selenium;

namespace Store.Demoqa.Helpers
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

        public Browser(){}
    }
}
