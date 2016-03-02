using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Store.Demoqa.Pages;
using System.Collections.Generic;
using System.Linq;

namespace Store.Demoqa
{
    /// <summary>
    /// Class describes controls and methods from the central part of the site called Container
    /// </summary>
    public class ContentContainer
    {
        /// <summary>
        /// The page header
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@id='content']//header/h1")]
        public IWebElement PageHeader;

        /// <summary>
        /// The add to cart button
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@class='input-button-buy']/span/input")]
        public IWebElement AddToCartButton;

        /// <summary>
        /// The default ListView
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@class='product_views group']/div/a[2]")]
        public IWebElement DefaultListView;

        /// <summary>
        /// The first product title
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@class='prodtitle']/a")]
        public IList <IWebElement> ProductTitle;

        /// <summary>
        /// The content
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#default_products_page_container")]
        public IWebElement Content;

        [FindsBy(How = How.CssSelector, Using = ".prodtitle")]
        public IList <IWebElement> FoundProducts;

        [FindsBy(How = How.CssSelector, Using = ".featured_image>a[href]")]
        public IWebElement HomeProdReference;

        [FindsBy(How = How.CssSelector, Using = ".product_description>h2")]
        public IWebElement HomeProdTitle;
        
        /// <summary>
        /// The driver
        /// </summary>
       // private IWebDriver driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentContainer"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public ContentContainer()
        {
            Driver.Instance.InitPageElements();
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

        public ProductDescriptionPage GoToProdFromHomePage()
        {
            HomeProdReference.Click();
            return new ProductDescriptionPage();
        }    
    }
}
