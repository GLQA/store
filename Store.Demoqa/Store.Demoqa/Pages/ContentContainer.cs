using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Store.Demoqa
{
    class ContentContainer
    {
        [FindsBy(How = How.XPath, Using = ".//*[@id='content']//header/h1")]
        public IWebElement pageHeader;

        [FindsBy(How = How.XPath, Using = ".//*[@class='input-button-buy']/span/input")]
        public IWebElement addToCartButton;

        public ContentContainer(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

    }
}
