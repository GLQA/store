using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Store.Demoqa.Pages;

namespace Store.Demoqa
{
    /// <summary>
    /// Class contains all controls from the central part of the site
    /// </summary>
   public class ContentContainer
    {
        IWebDriver driver;

        [FindsBy(How = How.XPath, Using = ".//*[@id='content']//header/h1")]
        public IWebElement PageHeader;

        [FindsBy(How = How.XPath, Using = ".//*[@class='input-button-buy']/span/input")]
        public IWebElement AddToCartButton;

        [FindsBy(How = How.XPath, Using = ".//*[@class='product_views group']/div/a[2]")]
        public IWebElement defaultListView;

        [FindsBy(How = How.XPath, Using = ".//*[@id='default_products_page_container']/div[3]")]
        public IWebElement firstProductInTheList;

        [FindsBy(How = How.XPath, Using = ".//*[@class='prodtitle']/a")]
        public IWebElement firstProdTitle;

        [FindsBy(How = How.XPath, Using = ".//*[@id='default_products_page_container']")]
        public IWebElement content;


        public ContentContainer(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public AddToCartPopUp AddProductToTheCart(int index)
        {
            var product = GetProduct(index);
            product.FindElement(By.ClassName("wpsc_buy_button")).Click();
            //firstProductInTheList.FindElement(By.ClassName("wpsc_buy_button")).Click();
            return new AddToCartPopUp(this.driver);
        }

        public string GetProductTitle(int index)
        {
            var product = GetProduct(index);
            return product.FindElement(By.XPath(".//*[@class='prodtitle']/a")).Text;
        }

        public IWebElement GetProduct(int index)
        {
           return content.FindElements(By.ClassName("productcol")).ElementAt(index);
        }
    }
}
