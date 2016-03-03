using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;
using System.Linq;

namespace Store.Demoqa
{
    /// <summary>
    /// Class describes controls and methods from the central part of the site called Container
    /// </summary>
    public class CategoryProductPage : PageFrame
    {
        /// <summary>
        /// The category title
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@id='content']//header/h1")]
        public IWebElement CategoryTitle { get; set; }

        /// <summary>
        /// The add to cart button
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@class='input-button-buy']/span/input")]
        public IWebElement AddToCartButton { get; set; }

        /// <summary>
        /// The default ListView
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@class='product_views group']/div/a[2]")]
        public IWebElement DefaultListView { get; set; }

        /// <summary>
        /// The first product title
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@class='prodtitle']/a")]
        public IList<IWebElement> ProductTitle { get; set; }

        /// <summary>
        /// The content
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#default_products_page_container")]
        public IWebElement Content { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryProductPage"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public CategoryProductPage() : base()
        {
            PageFactory.InitElements(Driver.Instance.driver, this);
        }

        /// <summary>
        /// Adds the product to the cart.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public AddToCartPopUp AddProductToTheCart(int index)
        {
            var product = this.GetProduct(index);
            product.FindElement(By.ClassName("wpsc_buy_button")).Click();
            return new AddToCartPopUp();
        }

        /// <summary>
        /// Gets the product title.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public string GetProductTitle(int index)
        {
            return ProductTitle[index].Text;
        }

        /// <summary>
        /// Gets the product.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public IWebElement GetProduct(int index)
        {
            return this.Content.FindElements(By.ClassName("productcol")).ElementAt(index);
        }
    }
}
