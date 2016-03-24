using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Store.PageBaseComponents;
using Store.Tests;

namespace Store.Pages
{
    /// <summary>
    /// Describes controls and methods on Login Page
    /// </summary>
    public class LoginPage : PageFrame
    {
        /// <summary>
        /// The login button
        /// </summary>
        [FindsBy(How = How.Id, Using = "login")]
        public IWebElement LoginButton { get; set; }

        /// <summary>
        /// The user name field
        /// </summary>
        [FindsBy(How = How.Id, Using = "log")]
        private IWebElement userNameField { get; set; }

        internal void LogInWithCredentials(string userName, string password)
        {
            ClearAndTypeUserName(userName);
            ClearAndTypePassword(password);
            LoginButton.Click();
        }

        /// <summary>
        /// The password field
        /// </summary>
        [FindsBy(How = How.Id, Using = "pwd")]
        private IWebElement passwordField { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public LoginPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public LoginPage() : base() { }

        /// <summary>
        /// Sets the name of the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        public void ClearAndTypeUserName(string userName)
        {
            this.userNameField.Clear();
            this.userNameField.SendKeys(userName);
        }

        /// <summary>
        /// Sets the password.
        /// </summary>
        /// <param name="password">The password.</param>
        public void ClearAndTypePassword(string password)
        {
            this.passwordField.Clear();
            this.passwordField.SendKeys(password);
        }

        public Meta GetMetaSection()
        {
            return BaseTest.repository.Get<Meta>();
        }
    }
    //TODO: NUnit reports export
    //TODO: jenkins, set up jobs, send reports via email on schedule
}
