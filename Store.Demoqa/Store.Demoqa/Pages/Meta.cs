using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Demoqa.Pages
{
    class Meta : PageFrame
    {
        /// <summary>
        /// Gets or sets the logout button.
        /// </summary>
        /// <value>
        /// The logout button.
        /// </value>
        [FindsBy(How = How.CssSelector, Using = "#meta>ul>li:nth-of-type(2)>a")]
        public IWebElement LogInLogOutLink { get; set; }

        public Meta() : base()
        {
            PageFactory.InitElements(DriverSingleton.Instance.Driver, this);
        }


    }
}
