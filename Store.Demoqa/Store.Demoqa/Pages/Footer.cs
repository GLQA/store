using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Store.Demoqa
{
    public class Footer
    {
        private IWebDriver driver;
        private int numberOfProducts;
        private int randNumberOfProduct;

        [FindsBy(How = How.CssSelector, Using = ".group>li>a[title]:first-of-type")]
        public IList<IWebElement> productsInFooter;

        public Footer(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver = driver;
            //productsInFooter = driver.FindElements(products);
            int numberOfProducts = productsInFooter.Count;
            //TODO: move to property(return in get) - Maryna
            randNumberOfProduct = (new Random()).Next(0, numberOfProducts);
        }

        public ProductDescriptionPage GoToRandomProduct()
        {
            productsInFooter[randNumberOfProduct].Click();
            return new ProductDescriptionPage(driver);
        }

        public string GetRandomProdTitleText()
        {
            return productsInFooter[randNumberOfProduct].Text;
        }
    }
}