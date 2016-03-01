using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;

namespace Store.Demoqa
{
    /// <summary>
    /// The upper part of the site
    /// </summary>
    public class Header
    {
        /// <summary>
        /// My account button
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@id='account']/a")]
        public IWebElement MyAccountButton;

        /// <summary>
        /// The checkout button
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@id='header_cart']/a/span[1]")]
        public IWebElement CheckoutButton;

        /// <summary>
        /// The items button
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@class='count']")]
        public IWebElement ItemsButton;

        /// <summary>
        /// The logout button
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@id='account_logout'")]
        public IWebElement LogoutButton;

        /// <summary>
        /// The home page user name
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@id='wp-admin-bar-my-account']/a")]
        public IWebElement HomePageUserName;

        /// <summary>
        /// The product category tab
        /// </summary>
        [FindsBy(How = How.XPath, Using = "//*[@id='menu-item-33']/a")]
        public IWebElement ProductCategoryTab;

        /// <summary>
        /// The accessories menu item
        /// </summary>
        [FindsBy(How = How.XPath, Using = "//*[@id='menu-item-34']/a")]
        public IWebElement AccessoriesMenuItem;

        /// <summary>
        /// The imac menu item
        /// </summary>
        [FindsBy(How = How.XPath, Using = "//*[@id='menu-item-35']/a")]
        public IWebElement IMacMenuItem;

        /// <summary>
        /// The ipad menu item
        /// </summary>
        [FindsBy(How = How.XPath, Using = "//*[@id='menu-item-36']/a")]
        public IWebElement IPadMenuItem;

        /// <summary>
        /// The iphones menu item
        /// </summary>
        [FindsBy(How = How.XPath, Using = "//*[@id='menu-item-37']/a")]
        public IWebElement IPhonesMenuItem;

        [FindsBy(How = How.CssSelector, Using = "#menu-item-15>a")]
        public IWebElement HomeButton;

        /// <summary>
        /// The search field
        /// </summary>
        [FindsBy(How = How.Name, Using = "s")]
        public IWebElement SearchField;

        /// <summary>
        /// The driver
        /// </summary>
        private IWebDriver driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="Header"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public Header(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);   
        }

        /// <summary>
        /// Selecting of product category using Product Category tab
        /// </summary>
        /// <param name="product"></param>
        public ContentContainer SelectProductCategory(string product)
        {
            Actions action = new Actions(this.driver);
            action.MoveToElement(this.ProductCategoryTab).Perform();
            this.GetProduct(product).Click();
            return new ContentContainer(this.driver);
        }

        /// <summary>
        /// Converts the product to enum.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public PRODUCTS ConvertProductToEnum(string product)
        {
            return (PRODUCTS)Enum.Parse(typeof(PRODUCTS), product, true);
        }

        /// <summary>
        /// Goes to cart.
        /// </summary>
        /// <returns></returns>
        /// 
        /*
        public Cart GoToCart()
        {
            this.CheckoutButton.Click();
            return new Cart(this.driver);
        }
        */
        /// <summary>
        /// Gets the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        private IWebElement GetProduct(string product)
        {
            switch (this.ConvertProductToEnum(product))
            {
                case PRODUCTS.Accessories:
                    return this.AccessoriesMenuItem;
                case PRODUCTS.iMacs:
                    return this.IMacMenuItem;
                case PRODUCTS.iPads:
                    return this.IPadMenuItem;
                case PRODUCTS.iPhones:
                    return this.IPhonesMenuItem;
                default:
                    return null;
            }
        }

        public ContentContainer SetSearchValueAndSubmit(string searchValue)
        {
            SearchField.SendKeys(searchValue);
            SearchField.SendKeys(Keys.Enter);
            return new ContentContainer(driver);
        }

        public ContentContainer GoToHomePage()
        {
            HomeButton.Click();
            return new ContentContainer(driver);
        }
    }
}
