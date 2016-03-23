using System;
using OpenQA.Selenium.Remote;

namespace Store.Demoqa.Helpers
{
    class BrowserFactory
    {
        public static RemoteWebDriver CreateDriverInstance()
        {
            DesiredCapabilities capability;
            string browserName = Config.GetBrowser();

            switch (browserName)
            {
                case "chrome":
                    capability = DesiredCapabilities.Chrome();
                    break;
                case "firefox":
                    capability = DesiredCapabilities.Firefox();
                    break;
                default:
                    return null;
            }
            return new RemoteWebDriver(new Uri(Config.GRID_HUB_PATH), capability);
        }
    }
}
