﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Store.Demoqa
{
    /// <summary>
    /// Class describes controls and methods on Cart page 
    /// </summary>
    public class CartPage : PageFrame
    {
        /// <summary>
        /// The cart page header
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#post-29>header>h1")]
        public IWebElement CartPageHeader { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartPage"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public CartPage() : base()
        {
            PageFactory.InitElements(DriverSingleton.Instance.Driver, this);
        }

        /// <summary>
        /// Gets product from Cart
        /// </summary>
        /// <param name="prodTitle"></param>
        /// <returns></returns>
        public string GetProductFromCart(string prodTitle)
        {
            var listOfProductsInCart = DriverSingleton.Instance.Driver.FindElements(By.XPath(".//table/tbody//td[2]/a"));
            foreach (IWebElement element in listOfProductsInCart)
            {
                if (element.Text == prodTitle)
                {
                    return element.Text;
                }
            }
            return null;
        }
    }
}

