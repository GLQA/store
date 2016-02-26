using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.PageObjects;

namespace Store.Demoqa
{
    /// <summary>
    /// Login Page class
    /// </summary>
   public class LoginPage
    {
        [FindsBy(How = How.Id, Using = "login")]
        public IWebElement LoginButton;

        [FindsBy(How = How.Id, Using = "log")]
        private IWebElement userNameField;

        [FindsBy(How = How.Id, Using = "pwd")]
        private IWebElement passwordField;

        public LoginPage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public void SetUserName(string userName)
        {
            this.userNameField.SendKeys(userName);
        }

        public void SetPassword(string password)
        {
            this.passwordField.SendKeys(password);
        }
    }
}
