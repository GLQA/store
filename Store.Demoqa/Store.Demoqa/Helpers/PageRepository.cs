using OpenQA.Selenium;
using Store.PageBaseComponents;
using System;
using System.Collections.Generic;

namespace Store.Helpers
{
    public class PageRepository
    {
        private IWebDriver driver;
        /// <summary>
        /// Page Repository constructor
        /// </summary>
        public PageRepository(IWebDriver Driver)
        {
            this.driver = Driver;
        }

        Dictionary<string, PageFrame> PageKeeper = new Dictionary<string, PageFrame>();

        public T Get<T>()
            where T : PageFrame, new()
        {
            if (!PageKeeper.ContainsKey(typeof(T).ToString()))
            {
                PageKeeper.Add(typeof(T).ToString(), (T)Activator.CreateInstance(typeof(T), this.driver));
            }
            return (T)PageKeeper[typeof(T).ToString()];
        }
    }
}
