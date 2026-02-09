using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;


namespace DeleteKindleBookmarks
{
    [TestFixture]
    public class DeleteKindleBookmarks
    {
        public static IWebDriver driver = new ChromeDriver();
        public static WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
        public static Actions actions = new Actions(driver);


        [TestFixtureSetUp]
        public void Startup()
        {
            driver.Navigate().GoToUrl("https://read.amazon.com");
            //Sign In
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("top-sign-in-btn")));
            driver.FindElement(By.Id("top-sign-in-btn")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("signInSubmit")));
            SignIn signIn = new SignIn(driver);
            signIn.emailInput.SendKeys(signIn.email);
            signIn.passwordInput.SendKeys(signIn.password);
            signIn.signInButton.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("web-library-root")));
        }

        [SetUp]
        public void SetTest()
        {
            driver.Navigate().GoToUrl("https://read.amazon.com/kindle-library");
        }

        //Books to run
        [Test]
        public void DeleteThemBookMarks() => DeleteBookmarks(""); 
        //[Test]
        //public void DeleteThemBookMarks2() => DeleteBookmarks(""); 
        //[Test]
        //public void DeleteThemBookMarks3() => DeleteBookmarks(""); 
        //[Test]
        //public void DeleteThemBookMarks4() => DeleteBookmarks(""); 
        //[Test]
        //public void DeleteThemBookMarks5() => DeleteBookmarks(""); 
        //[Test]
        //public void DeleteThemBookMarks6() => DeleteBookmarks(""); 
        //[Test
        //public void DeleteThemBookMarks7() => DeleteBookmarks("");


        public void DeleteBookmarks(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                Console.WriteLine("No Title ID provided");
                Assert.True(true);
            }


            string bookID = "title-" + input;
            //Locate Book
            actions = new Actions(driver);
            IWebElement identifiedBook = driver.FindElement(By.Id(bookID));
            actions.MoveToElement(identifiedBook).Perform();
            Console.WriteLine("Title to be scrubbed: " + identifiedBook.Text);
            identifiedBook.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@title='Notebook']")));

            Book book = new Book(driver);
            actions = new Actions(driver);
            book.notebook.Click();

            if(!ElementCheck(By.ClassName("notebook-delete")))
            {
                Console.WriteLine("No bookmarks found");
                Assert.IsTrue(true);
            }
            else
            {
                do
                {
                    driver.FindElement(By.ClassName("notebook-delete")).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@class='alert-button ion-focusable ion-activatable sc-ion-alert-ios']")));
                    driver.FindElement(By.XPath("//*[@class='alert-button ion-focusable ion-activatable sc-ion-alert-ios']")).Click();
                    Thread.Sleep(100);
                }
                while (ElementCheck(By.ClassName("notebook-delete")) == true);
                Assert.IsTrue(true);
            }

        }

        [TestFixtureTearDown]
        public void Close()
        {
            driver.Close();
        }





        public static bool ElementCheck(By locator)
        {
            WebDriverWait elementWait = new WebDriverWait(driver, new TimeSpan(0, 0, 1));
            //try { driver.FindElement(locator); }
            try { elementWait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator)); }
            catch (NoSuchElementException) { return false; }
            catch (StaleElementReferenceException) { return false; }
            catch (WebDriverTimeoutException) { return false; }
            return true;
        }
    }

}
