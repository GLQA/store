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
    class Driver
    {
        private const string SITEURL = "http://store.demoqa.com/";
        public IWebDriver driver { get; set; }

        private static Driver driverInstance;
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
        private Driver() { }

        public IWebDriver Start()
        {
            this.driver = new FirefoxDriver();
            this.driver.Navigate().GoToUrl(SITEURL);
            this.driver.Manage().Window.Maximize();
            return driver;
        }

        public void Close()
        {
            this.driver.Quit();
        }

        public void InitPageElements()
        {
            PageFactory.InitElements(this.driver, this);
        }

    }
}
