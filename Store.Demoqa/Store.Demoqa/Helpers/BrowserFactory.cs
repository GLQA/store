using System;
using OpenQA.Selenium.Remote;

namespace Store.Helpers
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
            capability.SetCapability("jenkins.nodeName", "(master)");
            capability.SetCapability("version", "45");
            return new RemoteWebDriver(new Uri(Config.GRID_HUB_PATH), capability);
        }
    }
}
