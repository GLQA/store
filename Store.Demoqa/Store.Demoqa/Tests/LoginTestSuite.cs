using NUnit.Framework;
using Store.Pages;

namespace Store.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]

    public class LoginTestSuite : BaseTest
    {
        private string userName = "qa111";

        private string password = "MQM8t%x67RX9XyVZ";

        private string expectedUserGreeting = "Howdy, qa111";

        private string expectedLogInLogOutLinkName = "Log in";

        /// <summary>
        /// Login with valid data 
        /// </summary>
        [Test]
        public void LoginAndLogoutVerification()
        {
            LoginPage loginPage = homePage.Header.GoToLoginPage();
            loginPage.LogInWithCredentials(userName, password);
            CheckThatUserIsLoggedIn(expectedUserGreeting, homePage);
            homePage.Header.LogOut();
            loginPage.RefreshPage();
            Meta metaSection = loginPage.GetMetaSection();
            CheckThatUserLoggedOut(expectedLogInLogOutLinkName, metaSection);
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
