using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace Store.Demoqa
{
    public class ProductDescriptionPage
    {
        private IWebDriver driver;

        public ProductDescriptionPage()
        {
            Driver.Instance.InitPageElements();
        }

        [FindsBy(How = How.CssSelector, Using = ".prodtitle")]
        public IWebElement ProductTitle;

        [FindsBy(How = How.CssSelector, Using = ".product_description")]
        public IWebElement ProductDescription;

        [FindsBy(How = How.CssSelector, Using = ".product_image")]
        public IList <IWebElement> ProdPageImages;

        [FindsBy(How = How.CssSelector, Using = ".wpsc_also_bought.group")]
        public IWebElement PeopleWhoBoughtThisItemSection;

        [FindsBy(How = How.CssSelector, Using = "#fullResImage")]
        public IWebElement ProdOpenedImage;

        [FindsBy(How = How.CssSelector, Using = ".pp_next")]
        public IWebElement NextImageArrow;

        [FindsBy(How = How.CssSelector, Using = ".pp_expand")]
        public IWebElement ExpandImageButton;

        [FindsBy(How = How.CssSelector, Using = ".pluginCountTextDisconnected")]
        public IWebElement FBLikesCounter;

        [FindsBy(How = How.CssSelector, Using = ".pluginButtonLabel")]
        public IWebElement FBLikeButton;

        public IWebElement ProdRegularImage
        {
            get { return ProdPageImages[0]; }
        }

        public string GetTitleText()
        {
            return ProductTitle.Text;
        }

        public string GetDescriptionText()
        {
            return ProductDescription.Text;
        }

        public string GetTextOfPeopleWhoBoughtSection()
        {
            return PeopleWhoBoughtThisItemSection.Text;
        }

        public void EnlargeImage()
        {
            ExpandImageButton.Click();
        }


    }
}