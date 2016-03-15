using NUnit.Framework;
using OpenQA.Selenium;
using System.Drawing;
using Store.Demoqa.Helpers;

namespace Store.Demoqa.Tests
{
    [TestFixture]
    public class ProductPageTestSuite : BaseTest
    {
        private string productToCheckImageEnlargement = "Skullcandy";

        private string expectedFacebookPageTitle = "Facebook";

        private string fileLocationPath = @"D:\\Skullcandy.png";

        private string expectedImageMD5 = "\vòÊa®ü—*Šé\0””\u000f¦w";

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
            ProductDescriptionPage product = homePage.Header.FindProductAndGoToTheFirst(productToCheckImageEnlargement);
            CheckRequiredProductWasOpened(product.ProductTitleText, productToCheckImageEnlargement);
            product.SaveImage(fileLocationPath);
            MD5Checker checker = new MD5Checker();
            CheckMD5AreTheSame(expectedImageMD5, checker.CheckMD5(fileLocationPath));
            product.OpenImage();
            CheckImageIsDisplayed(product.OpenedImage);
            Size regularSize = product.OpenedImage.Size;
            product.EnlargeImage();
            ChechEnlargedImageIsGreater(product.OpenedImage.Size, regularSize);
            product.NextImageArrow.Click();
            CheckImageIsDisplayed(product.OpenedImage);
        }

        /// <summary>
        /// Verifies that MD5 of product main image is the same as expected
        /// </summary>
        /// <param name="expectedMD5"></param>
        /// <param name="actualMD5"></param>
        private void CheckMD5AreTheSame(string expectedMD5, string actualMD5)
        {
            StringAssert.AreEqualIgnoringCase(expectedImageMD5, actualMD5);
        }

        /// <summary>
        /// Verifies that enlarged image sizes are bigger that sizes of non-enlarged picture
        /// </summary>
        private void ChechEnlargedImageIsGreater(Size enlargedSize, Size regularSize)
        {
            Assert.Greater(enlargedSize.Height, regularSize.Height);
            Assert.Greater(enlargedSize.Width, regularSize.Width);
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

        //ToDO: install selenium grid; two processes(two singletons) similar parts(two solutions: thread local, delete singleton) - Maryna
        //TODO: create screen repository - Maryna
    }
}
