using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace Store.Demoqa
{
    /// <summary>
    /// Class describes 
    /// </summary>
    public class Header
    {
        /// <summary>
        /// My account button
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#account>a")]
        public IWebElement MyAccountButton { get; set; }

        /// <summary>
        /// The checkout button
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@id='header_cart']/a/span[1]")]
        public IWebElement CheckoutButton { get; set; }

        /// <summary>
        /// The items button
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".count")]
        public IWebElement ItemsButton { get; set; }

        /// <summary>
        /// The logout button
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#account_logout")]
        public IWebElement LogoutButton { get; set; }

        /// <summary>
        /// The home page user name
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#wp-admin-bar-my-account>a")]
        public IWebElement HomePageUserName { get; set; }

        /// <summary>
        /// The product category tab
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#menu-item-33>a")]
        public IWebElement ProductCategoryTab { get; set; }

        /// <summary>
        /// The accessories menu item
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#menu-item-34>a")]
        public IWebElement AccessoriesMenuItem { get; set; }

        /// <summary>
        /// The imac menu item
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#menu-item-35>a")]
        public IWebElement IMacMenuItem { get; set; }

        /// <summary>
        /// The ipad menu item
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#menu-item-36>a")]
        public IWebElement IPadMenuItem { get; set; }

        /// <summary>
        /// The iphones menu item
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#menu-item-37>a")]
        public IWebElement IPhonesMenuItem { get; set; }

        /// <summary>
        /// Button 'Home' in the header
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#menu-item-15>a")]
        public IWebElement HomeButton { get; set; }

        /// <summary>
        /// Button 'Log out' in the header
        /// </summary>
        [FindsBy(How = How.Id, Using = "account_logout")]
        private IWebElement logoutButton { get; set; }

        /// <summary>
        /// The search field in the header
        /// </summary>
        [FindsBy(How = How.Name, Using = "s")]
        public IWebElement SearchField { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Header"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public Header()
        {
            PageFactory.InitElements(DriverSingleton.Instance.Driver, this);
        }

        /// <summary>
        /// Selecting of product category using Product Category tab
        /// </summary>
        /// <param name="product"></param>
        public CategoryProductPage SelectProductCategory(string product)
        {
            Actions action = new Actions(DriverSingleton.Instance.Driver);
            action.MoveToElement(this.ProductCategoryTab).Perform();
            this.GetProduct(product).Click();
            return new CategoryProductPage();
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
        public CartPage GoToCart()
        {
            this.CheckoutButton.Click();
            return new CartPage();
        }

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

        /// <summary>
        /// Types name of product that need to be searched in the search field and pushes 'Enter'
        /// </summary>
        public SearchResultsPage TypeSearchValueAndSubmit(string searchValue)
        {
            SearchField.SendKeys(searchValue);
            SearchField.SendKeys(Keys.Enter);
            return new SearchResultsPage();
        }

        /// <summary>
        /// Finds the product and go to the first.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        /// <exception cref="InvalidInputValueForSearchException">Search value {0} can't be found, there is no such product</exception>
        public ProductDescriptionPage FindProductAndGoToTheFirst(string title)
        {
            SearchResultsPage searchResults = TypeSearchValueAndSubmit(title);
            if (searchResults.FoundProducts.Count == 0)
                throw new InvalidInputValueForSearchException("Search value {0} can't be found, there is no such product", title);
            else
                searchResults.FoundProducts[0].Click();
            return new ProductDescriptionPage();
        }

        /// <summary>
        /// Clicks 'Home' button in the header in order to go to home page
        /// </summary>
        public HomePage GoToHomePage()
        {
            HomeButton.Click();
            return new HomePage();
        }
    }
}
