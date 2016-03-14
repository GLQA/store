using NUnit.Framework;

namespace Store.Demoqa.Tests
{
    [TestFixture]
    public class BaseTest
    {
        public HomePage homePage;

        /// <summary>
        /// Sets up creation of new HomePage before each test in suite
        /// </summary>
        [SetUp]
        public virtual void Init()
        {
            homePage = new HomePage();
        }

        /// <summary>
        /// Closes the browser
        /// </summary>
        [TearDown]
        public virtual void CloseBrowser()
        {
            DriverSingleton.Instance.Close();
        }
    }
}
