using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Store.Demoqa
{
    /// <summary>
    /// Describes controls and methods on Login Page
    /// </summary>
    public class Login
    {
        /// <summary>
        /// The login button
        /// </summary>
        [FindsBy(How = How.Id, Using = "login")]
        public IWebElement LoginButton;

        /// <summary>
        /// The user name field
        /// </summary>
        [FindsBy(How = How.Id, Using = "log")]
        private IWebElement userNameField;

        /// <summary>
        /// The password field
        /// </summary>
        [FindsBy(How = How.Id, Using = "pwd")]
        private IWebElement passwordField;

        private IWebDriver driver;
        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public Login()
        {
            Driver.Instance.InitPageElements();
        }

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
    }
}
