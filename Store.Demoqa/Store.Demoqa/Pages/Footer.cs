using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;

namespace Store.Demoqa.Pages
{
    public class Footer
    {
        /// <summary>
        /// WebDriver
        /// </summary>
        private IWebDriver driver;

        /// <summary>
        /// Number of products in footer
        /// </summary>
        private int NumberOfProducts
        {
            get
            {
                return ProductsInFooter.Count;
            }
        }

        /// <summary>
        /// Random product from footer that will be used it tests
        /// </summary>
        private int RandNumberOfProduct
        {
            get
            {
                return (new Random()).Next(0, NumberOfProducts);
            }
        }

        /// <summary>
        /// Returns a title of random product from footer
        /// </summary>
        public string TrimmedRandProductTitle
        {
            get
            {
                return ProductsInFooter[RandNumberOfProduct].Text.TrimEnd('-', '.');
            }
        }
        /// <summary>
        /// All products in footer
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".group>li>a[title]:first-of-type")]
        public IList<IWebElement> ProductsInFooter;

        /// <summary>
        /// Creates new instance of footer
        /// </summary>
        public Footer(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver = driver;
        }

        /// <summary>
        /// Goes to random product in footer
        /// </summary>
        public ProductDescriptionPage GoToRandomProduct()
        {
            ProductsInFooter[RandNumberOfProduct].Click();
            return new ProductDescriptionPage(driver);
        }
    }
}
