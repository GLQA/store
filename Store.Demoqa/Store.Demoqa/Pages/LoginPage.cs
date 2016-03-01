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

        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public Login(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        //todo: clear and type
        /// <summary>
        /// Sets the name of the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        public void SetUserName(string userName)
        {
            this.userNameField.SendKeys(userName);
        }

        /// <summary>
        /// Sets the password.
        /// </summary>
        /// <param name="password">The password.</param>
        public void SetPassword(string password)
        {
            this.passwordField.SendKeys(password);
        }
    }
}
