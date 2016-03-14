using NUnit.Framework;
using OpenQA.Selenium;
using System.Drawing;

namespace Store.Demoqa.Tests
{
    [TestFixture]
    public class ProductPageTestSuite : BaseTest
    {
        private string productToCheckImageEnlargement = "Skullcandy";

        private string expectedFacebookPageTitle = "Facebook";

        /// <summary>
        /// Verification of availability of 'People who bought this item' section and product description 
        /// </summary>
        [Test]
        public void ProductContentVerification()
        {
            string TrimmedRandProductTitle = homePage.Footer.TrimmedRandProductTitle;
            ProductDescriptionPage prodPage = homePage.Footer.GoToRandomProduct();
            CheckOpenedProdTitleContainsRequired(TrimmedRandProductTitle, prodPage.ProductTitleText);
            CheckDescriptionSectionIsNotEmpty(prodPage.ProductDescriptionText);
            CheckPeopleBoughtSectionIsNotEmpty(prodPage.PeopleBoughtSectionText);
        }

        /// <summary>
        /// Checks the product title equals to opened.
        /// </summary>
        /// <param name="randomProductTitle">The random product title.</param>
        /// <param name="pageProductTitle">The page product title.</param>
        private void CheckOpenedProdTitleContainsRequired(string randomProductTitle, string pageProductTitle)
        {
            StringAssert.StartsWith(randomProductTitle, pageProductTitle);
        }

        /// <summary>
        /// Checks the description section is not empty.
        /// </summary>
        /// <param name="productDescription">The product description.</param>
        private void CheckDescriptionSectionIsNotEmpty(string productDescription)
        {
            Assert.IsNotEmpty(productDescription);
        }

        /// <summary>
        /// Checks the people bought section is not empty.
        /// </summary>
        /// <param name="sectionText">The section text.</param>
        private void CheckPeopleBoughtSectionIsNotEmpty(string sectionText)
        {
            Assert.IsNotEmpty(sectionText);
        }

        /// <summary>
        /// Verification of the picture availability and functionality on the product's description page
        /// </summary>
        [Test]
        public void PictureEnlargementVerification()
        {
            //TODO: check image md5 - Maryna
            ProductDescriptionPage product = homePage.Header.FindProductAndGoToTheFirst(productToCheckImageEnlargement);
            CheckRequiredProductWasOpened(product.ProductTitleText, productToCheckImageEnlargement);


            string a = DriverSingleton.Instance.Driver.FindElement(By.CssSelector(".imagecol>a[href]")).GetAttribute("href");
            DriverSingleton.Instance.Driver.Navigate().GoToUrl(a);
            //DriverSingleton.Instance.Driver.SwitchTo().Window(DriverSingleton.Instance.Driver.WindowHandles[1]);
            //save image from page
            //check md5
            product.OpenImage();
            CheckImageIsDisplayed(product.OpenedImage);
            //save image
            //check md5
            Size regularSize = product.OpenedImage.Size;
            product.EnlargeImage();
            Size enlargedSize = product.OpenedImage.Size;
            Assert.Greater(enlargedSize.Height, regularSize.Height);
            Assert.Greater(enlargedSize.Width, regularSize.Width);
            product.NextImageArrow.Click();
            Assert.IsTrue(product.OpenedImage.Displayed);
        }

        /// <summary>
        /// Checks that image is displayed on page
        /// </summary>
        /// <param name="productImage"></param>
        private void CheckImageIsDisplayed(IWebElement productImage)
        {
            Assert.IsTrue(productImage.Displayed);
        }

        /// <summary>
        /// Checks that required product's description page was opened
        /// </summary>
        /// <param name="actualTitle"></param>
        /// <param name="expectedTitle"></param>
        private void CheckRequiredProductWasOpened(string actualTitle, string expectedTitle)
        {
            Assert.That(actualTitle, Contains.Substring(expectedTitle).IgnoreCase);
        }

        /// <summary>
        /// Verification of the Facebook 'Like' button
        /// </summary>
        [Test]
        public void LikeFunctionalityVerification()
        {
            ProductDescriptionPage product = homePage.GoToProductFromCarousel();
            product.ClickFaceBookLikeButton();
            FaceBookLoginPage faceBookLoginPage = product.SwitchToFaceBookWindow();
            CheckOpenedPageIsFaceBookLoginPage(faceBookLoginPage.TitleText, expectedFacebookPageTitle);
            faceBookLoginPage.Close();
            CheckFaceBookPageIsClosed();
        }

        /// <summary>
        /// Checks that opened FaceBook login page is closed now
        /// </summary>
        private void CheckFaceBookPageIsClosed()
        {
            Assert.LessOrEqual(DriverSingleton.Instance.Driver.WindowHandles.Count, 1);
        }

        /// <summary>
        /// Checks that FaceBook login page has been opened
        /// </summary>
        /// <param name="actualTitle"></param>
        /// <param name="expectedTitle"></param>
        private void CheckOpenedPageIsFaceBookLoginPage(string actualTitle, string expectedTitle)
        {
            StringAssert.AreEqualIgnoringCase(actualTitle, expectedTitle);
            Assert.That(DriverSingleton.Instance.Driver.Url, Contains.Substring(expectedTitle).IgnoreCase);
        }
    }
}
