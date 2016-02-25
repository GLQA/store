using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;

namespace Store.Demoqa
{
    class Header
    {
        [FindsBy(How = How.XPath, Using = ".//*[@id='account']/a")]
        public IWebElement myAccountButton;

        [FindsBy(How = How.XPath, Using = ".//*[@id='header_cart']/a")]
        public IWebElement checkoutButton;

        [FindsBy(How = How.XPath, Using = ".//*[@id='account_logout'")]
        public IWebElement logoutButton;

        [FindsBy(How = How.XPath, Using = ".//*[@id='wp-admin-bar-my-account']/a")]
        public IWebElement homePageUserName;

        [FindsBy(How = How.XPath, Using = "//*[@id='menu-item-33']/a")]
        public IWebElement ProductCategoryTab;

        [FindsBy(How = How.XPath, Using = "//*[@id='menu-item-34']/a")]
        public IWebElement accessoriesMenuItem;

        [FindsBy(How = How.XPath, Using = "//*[@id='menu-item-35']/a")]
        public IWebElement iMacMenuItem;

        [FindsBy(How = How.XPath, Using = "//*[@id='menu-item-36']/a")]
        public IWebElement iPadMenuItem;

        [FindsBy(How = How.XPath, Using = "//*[@id='menu-item-37']/a")]
        public IWebElement iPhonesMenuItem;

        [FindsBy(How = How.XPath, Using = ".//*[@id='main-nav']/form")]
        public IWebElement searchField;

        public Header(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
            
        }

    }
}
