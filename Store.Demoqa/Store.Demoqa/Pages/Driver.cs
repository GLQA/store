using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Demoqa
{
    /// <summary>
    /// WebDriver Singleton
    /// </summary>
    public class Driver
    {
        /// <summary>
        /// The siteurl
        /// </summary>
        private const string SITEURL = "http://store.demoqa.com/";

        /// <summary>
        /// Gets the driver.
        /// </summary>
        /// <value>
        /// The driver.
        /// </value>
        public IWebDriver driver { get; }

        /// <summary>
        /// The driver instance
        /// </summary>
        private static Driver driverInstance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static Driver Instance
        {
            get
            {
                if (driverInstance == null)
                {
                    driverInstance = new Driver();
                }
                return driverInstance;
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Driver"/> class from being created.
        /// </summary>
        private Driver() {
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl(SITEURL);
            driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            this.driver.Quit();
            driverInstance = null;
        }
    }
}
