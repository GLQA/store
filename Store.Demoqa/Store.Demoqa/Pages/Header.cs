using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using Store.Demoqa.Pages;

namespace Store.Demoqa
{
    /// <summary>
    /// The upper part of the site
    /// </summary>
   public class Header
    {
        private IWebDriver driver;

        [FindsBy(How = How.XPath, Using = ".//*[@id='account']/a")]
        public IWebElement MyAccountButton;

        [FindsBy(How = How.XPath, Using = ".//*[@id='header_cart']/a/span[1]")]
        public IWebElement CheckoutButton;

        [FindsBy(How = How.XPath, Using = ".//*[@id='account_logout'")]
        public IWebElement LogoutButton;

        [FindsBy(How = How.XPath, Using = ".//*[@id='wp-admin-bar-my-account']/a")]
        public IWebElement HomePageUserName;

        [FindsBy(How = How.XPath, Using = "//*[@id='menu-item-33']/a")]
        public IWebElement ProductCategoryTab;

        [FindsBy(How = How.XPath, Using = "//*[@id='menu-item-34']/a")]
        public IWebElement AccessoriesMenuItem;

        [FindsBy(How = How.XPath, Using = "//*[@id='menu-item-35']/a")]
        public IWebElement IMacMenuItem;

        [FindsBy(How = How.XPath, Using = "//*[@id='menu-item-36']/a")]
        public IWebElement IPadMenuItem;

        [FindsBy(How = How.XPath, Using = "//*[@id='menu-item-37']/a")]
        public IWebElement IPhonesMenuItem;

        [FindsBy(How = How.XPath, Using = ".//*[@id='main-nav']/form")]
        public IWebElement SearchField;

        public Header(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);   
        }

        /// <summary>
        /// Selecting of product category using Product Category tab
        /// </summary>
        /// <param name="product"></param>
        public ContentContainer SelectProductCategory(String product)
        {
            Actions action = new Actions(this.driver);
            action.MoveToElement(ProductCategoryTab).Perform();
            GetProduct(product).Click();
            return new ContentContainer(driver);
        }

        /// <summary>
        /// Parsing 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public virtual PRODUCTS ConvertProductToEnum(string product)
        {
            return (PRODUCTS)Enum.Parse(typeof(PRODUCTS), product, true);
        }

        /// <summary>
        /// Get control according to enum
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private IWebElement GetProduct(string product)
        {
            switch (ConvertProductToEnum(product))
            {
                case PRODUCTS.Accessories:
                    return AccessoriesMenuItem;
                case PRODUCTS.iMacs:
                    return IMacMenuItem;
                case PRODUCTS.iPads:
                    return IPadMenuItem;
                case PRODUCTS.iPhones:
                    return IPhonesMenuItem;
                default:
                    return null;
            }
        }

        public Cart GoToCart()
        {
            CheckoutButton.Click();
            //Actions action = new Actions(driver);
            //action.MoveToElement(CheckoutButton).Perform();
            return new Cart(driver);
        }

    }
}
