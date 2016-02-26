using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Store.Demoqa.Pages
{
    public class AddToCartPopUp
    {
        [FindsBy(How = How.XPath, Using = ".//*[@id='fancy_notification_content']")]
        public IWebElement addToCartPopUp;

        [FindsBy(How = How.XPath, Using = ".//*[@id='fancy_notification_content']/span")]
        public IWebElement popUpTextField;

        [FindsBy(How = How.XPath, Using = ".//*[@id='fancy_notification_content']/a[1]")]
        public IWebElement checkOutButton;


        [FindsBy(How = How.XPath, Using = ".//*[@id='fancy_notification_content']/a[2]")]
        public IWebElement continueShoppingButton;

        public AddToCartPopUp(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }
    }
}
