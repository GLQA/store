using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Store.Demoqa.Helpers;
using Store.Demoqa.PageBaseComponents;
using Store.Demoqa.PopUps;
using Store.Demoqa.Tests;
using System;
using System.Collections.Generic;

namespace Store.Demoqa.Pages
{
    /// <summary>
    /// Class describes controls and methods from the central part of the site called Container
    /// </summary>
    public class CategoryProductPage : PageFrame
    {
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
        /// List of product titles
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
        public CategoryProductPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public CategoryProductPage() : base() { }

        /// <summary>
        /// Adds the product to the cart.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        /// 
        public AddToCartPopUp AddProductToTheCart(int index)
        {
            var product = this.GetProductByIndex(index);
            product.FindElement(By.ClassName("wpsc_buy_button")).Click();
            return BaseTest.repository.Get<AddToCartPopUp>();
        }

        /// <summary>
        /// Random product from footer that will be used it tests
        /// </summary>
        public int RandNumberOfProductInCategory()
        {
            var productsCount = Products.Count;
            return (new Random()).Next(0, productsCount);
        }

        /// <summary>
        /// Gets the product title.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public string GetProductTitleByIndex(int index)
        {
            return ProductTitle[index].Text;
        }

        /// <summary>
        /// Gets the product.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public IWebElement GetProductByIndex(int index)
        {
            return Products[index];
        }
    }
}
