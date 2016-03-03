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
    public class DriverSingleton
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
        public IWebDriver Driver { get; }

        /// <summary>
        /// The driver instance
        /// </summary>
        private static DriverSingleton driverInstance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static DriverSingleton Instance
        {
            get
            {
                if (driverInstance == null)
                {
                    driverInstance = new DriverSingleton();
                }
                return driverInstance;
            }
        }
        //TODO: init driver with specific options (file directory) and move SITEURL to config - Yuliia
        //TODO: support of two browsers(+Chrome); move to config; add chrome driver to the project - Yuliia
        /// <summary>
        /// Prevents a default instance of the <see cref="DriverSingleton"/> class from being created.
        /// </summary>
        private DriverSingleton() {
            Driver = new FirefoxDriver();
            Driver.Navigate().GoToUrl(SITEURL);
            Driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            this.Driver.Quit();
            driverInstance = null;
        }
    }
}
