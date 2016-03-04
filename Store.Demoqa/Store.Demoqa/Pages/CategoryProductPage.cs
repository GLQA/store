using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Store.Demoqa.PopUps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Demoqa
{
    /// <summary>
    /// Class describes controls and methods from the central part of the site called Container
    /// </summary>
    public class CategoryProductPage : PageFrame
    {
        int index;

        /// <summary>
        /// The category title
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#content header>h1")]
        public IWebElement CategoryTitle { get; set; }

        /// <summary>
        /// List of Products in specific category
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".productcol")]
        public IList<IWebElement> Products { get; set; }

        /// <summary>
        /// The default ListView
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@class='product_views group']/div/a[2]")]
        public IWebElement DefaultListView { get; set; }

        /// <summary>
        /// The first product title
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".prodtitle>a")]
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
            PageFactory.InitElements(DriverSingleton.Instance.Driver, this);
        }

        /// <summary>
        /// Adds the product to the cart.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        /// 
         public AddToCartPopUp AddProductToTheCart()
        {
            var product = this.GetProduct();
            product.FindElement(By.ClassName("wpsc_buy_button")).Click();
            return new AddToCartPopUp();
        }
        /// Number of products in footer
        /// </summary>
        private int NumberOfProductsInCategory
        {
            get
            {
                return Products.Count;
            }
        }

        /// <summary>
        /// Random product from footer that will be used it tests
        /// </summary>
        private int RandNumberOfProductInCategory
        {
            get
            {
                return (new Random()).Next(0, NumberOfProductsInCategory);
            }
        }

        /// <summary>
        /// Gets the product title.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public string GetProductTitle()
        {
            index = RandNumberOfProductInCategory;
            return ProductTitle[index].Text.TrimEnd('-', '.'); ;
        }

        /// <summary>
        /// Gets the product.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public IWebElement GetProduct()
        {
            return Products[index];
        }
    }
}
