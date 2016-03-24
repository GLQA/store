using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Store.Pages;
using Store.Tests;
using System;
using System.Collections.Generic;

namespace Store.PageBaseComponents
{
    public class Footer
    {
        /// <summary>
        /// All products in footer
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".group>li>a[title]:first-of-type")]
        public IList<IWebElement> ProductsInFooter { get; set; }


        private int randNumberOfProduct;
        /// <summary>
        /// Random index number of product from footer that will be used it tests
        /// </summary>
        /// //make as singleton
        public int GetRandValue()
        {
            if (randNumberOfProduct == null )
                {
                    randNumberOfProduct = new Random().Next(0, productsQuantityInFooter);
                }
            return randNumberOfProduct;
        }

        /// <summary>
        /// Number of products in footer
        /// </summary>
        public int productsQuantityInFooter
        {
            get
            {
                return ProductsInFooter.Count;
            }
        }

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
        public Footer(){}

        public Footer(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        /// <summary>
        /// Goes to random product in footer
        /// </summary>
        public ProductDescriptionPage GoToRandomProduct()
        {
            ProductsInFooter[randNumberOfProduct].Click();
            return BaseTest.repository.Get<ProductDescriptionPage>();
        }
    }
}
