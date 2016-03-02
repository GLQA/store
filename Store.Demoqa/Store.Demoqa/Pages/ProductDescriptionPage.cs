using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace Store.Demoqa.Pages
{
    public class ProductDescriptionPage : PageFrame
    {
        private IWebDriver driver;

        public ProductDescriptionPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = ".prodtitle")]
        public IWebElement ProductTitle;

        [FindsBy(How = How.CssSelector, Using = ".product_description")]
        public IWebElement ProductDescription;

        [FindsBy(How = How.CssSelector, Using = ".product_image")]
        public IList <IWebElement> ProdPageImages;

        [FindsBy(How = How.CssSelector, Using = ".wpsc_also_bought.group")]
        public IWebElement PeopleWhoBoughtSection;

        [FindsBy(How = How.CssSelector, Using = "#fullResImage")]
        public IWebElement ProductOpenedImage;

        [FindsBy(How = How.CssSelector, Using = ".pp_next")]
        public IWebElement NextImageArrow;

        [FindsBy(How = How.CssSelector, Using = ".pp_expand")]
        public IWebElement ExpandImageButton;

        [FindsBy(How = How.CssSelector, Using = ".pluginCountTextDisconnected")]
        public IWebElement FBLikesCounter;

        [FindsBy(How = How.CssSelector, Using = ".pluginButtonLabel")]
        public IWebElement FBLikeButton;

        public IWebElement ProductRegularImage
        {
            get { return ProdPageImages[0]; }
        }

        public string ProductTitleText
        {
            get { return ProductTitle.Text; }
        }

        public string ProductDescriptionText
        {
            get { return ProductDescription.Text; }
        }

        public string PeopleBoughtSectionText
        {
            get { return PeopleWhoBoughtSection.Text; }
        }

        public void EnlargeImage()
        {
            ExpandImageButton.Click();
        }

        public void CloseSecondaryWindow()
        {
            driver.SwitchTo().Window(driver.WindowHandles[1]).Close();
        }

    }
}