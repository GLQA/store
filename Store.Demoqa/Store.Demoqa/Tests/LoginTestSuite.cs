using NUnit.Framework;
using Store.Demoqa.Pages;
using System;

namespace Store.Demoqa.Tests
{
    [TestFixture]
    public class LoginTestSuite : BaseTest
    {
        private string userNAme = "qa29";

        private string password = "W0fucGsTDnVS";

        private string expectedUserGreeting = "Howdy, Qa29";

        private string expectedLogInLogOutLinkName = "Log in";

        /// <summary>
        /// Login with valid data 
        /// </summary>
        [Test]
        public void LoginAndLogoutVerification()
        {
            LoginPage login = new LoginPage();
            homePage.Header.MyAccountButton.Click();
            login.ClearAndTypeUserName(userNAme);
            login.ClearAndTypePassword(password);
            login.LoginButton.Click();
            DriverSingleton.Instance.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            CheckThatUserIsLoggedIn(expectedUserGreeting, homePage);
            homePage.Header.LogoutButton.Click();
            DriverSingleton.Instance.Driver.Navigate().Refresh();
            Meta meta = new Meta();
            CheckThatUserLoggedOut(expectedLogInLogOutLinkName, meta);
        }

        /// <summary>
        /// Checks the that user logged out.
        /// </summary>
        /// <param name="meta">The meta.</param>
        private void CheckThatUserLoggedOut(string expectedLogInLogOutLinkName, Meta meta)
        {
            Assert.AreEqual(expectedLogInLogOutLinkName, meta.LogInLogOutLink.Text);
        }

        /// <summary>
        /// Checks the that user is logged in.
        /// </summary>
        /// <param name="expectedUserGreeting">The expected user greeting.</param>
        /// <param name="homePage">The home page.</param>
        private void CheckThatUserIsLoggedIn(string expectedUserGreeting, HomePage homePage)
        {
            Assert.AreEqual(expectedUserGreeting, homePage.Header.HomePageUserName.Text);
        }
    }
}
