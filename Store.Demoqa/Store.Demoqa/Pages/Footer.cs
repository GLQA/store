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
        private int randNumerOfProduct;

        [FindsBy(How = How.CssSelector, Using = ".group>li>a[title]:first-of-type")]
        public IList <IWebElement> productsInFooter;
        
        public Footer(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver = driver;
            //productsInFooter = driver.FindElements(products);
            int numberOfProducts = productsInFooter.Count;
            randNumerOfProduct = (new Random()).Next(0, numberOfProducts);
        }

        public ProductDescriptionPage GoToRandomProduct()
        {
            productsInFooter[randNumerOfProduct].Click();
            return new ProductDescriptionPage(driver);           
        }

        public string GetTitleText()
        {
            return productsInFooter[randNumerOfProduct].Text;
        }
    }
}
