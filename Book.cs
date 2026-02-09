using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace DeleteKindleBookmarks
{
    internal class Book
    {
        public IWebElement notebook { get; set; }

        public Book(IWebDriver driver)
        {
            notebook = driver.FindElement(By.XPath("//*[@title='Notebook']"));
        }
    }
}
