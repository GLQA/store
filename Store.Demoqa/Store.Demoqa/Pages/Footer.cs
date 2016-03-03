using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;

namespace Store.Demoqa
{
    public class Footer
    {
        /// <summary>
        /// All products in footer
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".group>li>a[title]:first-of-type")]
        public static IList<IWebElement> ProductsInFooter { get; set; }

        /// <summary>
        /// Random index number of product from footer that will be used it tests
        /// </summary>
        public int randNumberOfProduct = new Random().Next(0, productsQuantityInFooter);

        /// <summary>
        /// Number of products in footer
        /// </summary>
        public static int productsQuantityInFooter = ProductsInFooter.Count; 

        /// <summary>
        /// Returns a title of random product from footer
        /// </summary>
        public string TrimmedRandProductTitle
        {
            get
            {
                return ProductsInFooter[randNumberOfProduct].Text.TrimEnd('-', '.');
            }
        }

        /// <summary>
        /// Creates new instance of footer
        /// </summary>
        public Footer()
        {
            PageFactory.InitElements(DriverSingleton.Instance.Driver, this);
        }

        /// <summary>
        /// Goes to random product in footer
        /// </summary>
        public ProductDescriptionPage GoToRandomProduct()
        {
            ProductsInFooter[randNumberOfProduct].Click();
            return new ProductDescriptionPage();
        }
    }
}
