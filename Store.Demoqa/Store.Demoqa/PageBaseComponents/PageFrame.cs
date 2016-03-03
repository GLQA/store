using OpenQA.Selenium;

namespace Store.Demoqa
{
    public class PageFrame
    {
        /// <summary>
        /// Gets the footer.
        /// </summary>
        /// <value>
        /// The footer.
        /// </value>
        public Footer Footer
        {
            get
            {
                return new Footer();
            }
        }

        /// <summary>
        /// Gets the header.
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        public Header Header
        {
            get
            {
                return new Header();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageFrame"/> class.
        /// </summary>
        public PageFrame(){}
    }
}
      