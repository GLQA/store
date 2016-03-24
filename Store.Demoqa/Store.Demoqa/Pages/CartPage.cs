using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Store.PageBaseComponents;
using System.Collections.Generic;

namespace Store.Pages
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

        [FindsBy(How = How.CssSelector, Using = ".wpsc_product_name")]
        public IList<IWebElement> listOfProductsInCart;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartPage"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public CartPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public CartPage() : base() { }

        /// <summary>
        /// Gets product from Cart
        /// </summary>
        /// <param name="prodTitle"></param>
        /// <returns></returns>
        public List<string> GetProductTitlesInCart()
        {
            List<string> titlesList = new List<string>();
            foreach (IWebElement element in listOfProductsInCart)
            {
                titlesList.Add(element.Text);
            }
            return titlesList;
        }
    }
}

