using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using System.Drawing;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework.Interfaces;

namespace GameStoreTesting
{

    public class GameStoreTest_Iteration2
    {
        IWebDriver driver;
        FirefoxOptions options;
        const String innerHtml = "innerHTML";
        const String innerText = "innerText";

        [SetUp]
        public void startBrowser()
        {
            options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;
            driver = new FirefoxDriver(options);
        }
        // Iteration 2 tests
        //Game tests
        [Test]
        public void ViewGameDetail()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "Mario";

            //Act
            login();

            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul/li[1]/a")).GetAttribute("href");
            driver.Url = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr[1]/td[5]/a")).GetAttribute("href");

            String actual = driver.FindElement(By.XPath("/html/body/div/div[1]/dl/dd[1]")).GetAttribute(innerText);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SearchGame()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "Mario";

            //Act
            login();

            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul/li[1]/a")).GetAttribute("href");
            driver.FindElement(By.XPath("/html/body/div/form/div/input")).SendKeys("Mario");

            String actual = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr[1]/td[2]")).GetAttribute(innerText);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Wishlist tests
        [Test]
        public void AddToWishlist()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "Mario";

            //Act
            login();

            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul/li[1]/a")).GetAttribute("href");
            driver.Url = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr[1]/td[5]/a")).GetAttribute("href");
            driver.Url = driver.FindElement(By.XPath("/html/body/div/div[2]/a[3]")).GetAttribute("href");

            String actual = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]")).GetAttribute(innerText);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RemoveFromWishlist()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "You don't have games in your wish list. Checkout our game list to view games and add to wish list. View games";

            //Act
            login();

            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul/li[1]/a")).GetAttribute("href");
            driver.Url = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr[1]/td[5]/a")).GetAttribute("href");
            driver.Url = driver.FindElement(By.XPath("/html/body/div/div[2]/a[3]")).GetAttribute("href");
            driver.Url = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[2]/a")).GetAttribute("href");

            String actual = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td")).GetAttribute(innerText);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Rate tests
        [Test]
        public void AddRating()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "Alan";

            //Act
            login();
            //games nav
            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul/li[1]/a")).GetAttribute("href");
            //details btn
            driver.Url = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr[1]/td[5]/a")).GetAttribute("href");
            //add rev btn
            driver.Url = driver.FindElement(By.XPath("/html/body/div/div[2]/a[2]")).GetAttribute("href");

            driver.FindElement(By.XPath("/html/body/div/div/div/form/div[4]/span[1]/i[4]")).Click();

            driver.FindElement(By.XPath("/html/body/div/div/div/form/div[5]/input")).Click();

            String actual = driver.FindElement(By.XPath("/html/body/div/div[3]/div[2]/div/div[1]/span[2]")).GetAttribute(innerText);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ViewSummaryOfRating()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            int expected = 4;

            //Act
            login();
            //games nav
            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul/li[1]/a")).GetAttribute("href");
            //details btn
            driver.Url = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr[1]/td[5]/a")).GetAttribute("href");
            //add rev btn
            driver.Url = driver.FindElement(By.XPath("/html/body/div/div[2]/a[2]")).GetAttribute("href");

            driver.FindElement(By.XPath("/html/body/div/div/div/form/div[4]/span[1]/i[4]")).Click();

            driver.FindElement(By.XPath("/html/body/div/div/div/form/div[5]/input")).Click();

            IWebElement sumarryRating = driver.FindElement(By.XPath("/html/body/div/div[1]/dl/dd[5]/span"));

            int actual = sumarryRating.FindElements(By.ClassName("fas")).Count;

            //Assert
            Assert.AreEqual(expected, actual);
        }
        //Review tests
        [Test]
        public void AddReview()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "Alan";

            //Act
            login();
            //games nav
            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul/li[1]/a")).GetAttribute("href");
            //details btn
            driver.Url = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr[1]/td[5]/a")).GetAttribute("href");
            //add rev btn
            driver.Url = driver.FindElement(By.XPath("/html/body/div/div[2]/a[2]")).GetAttribute("href");

            driver.FindElement(By.XPath("/html/body/div/div/div/form/div[4]/span[1]/i[4]")).Click();

            driver.FindElement(By.Id("Title")).SendKeys("Test Title");
            driver.FindElement(By.Id("Review1")).SendKeys("Test Review");
            driver.FindElement(By.XPath("/html/body/div/div/div/form/div[5]/input")).Click();

            String actual = driver.FindElement(By.XPath("/html/body/div/div[3]/div[2]/div/div[1]/span[2]")).GetAttribute(innerText);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddReview_PendingCheck()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "Pending";

            //Act
            login();
            //games nav
            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul/li[1]/a")).GetAttribute("href");
            //details btn
            driver.Url = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr[1]/td[5]/a")).GetAttribute("href");
            //add rev btn
            driver.Url = driver.FindElement(By.XPath("/html/body/div/div[2]/a[2]")).GetAttribute("href");

            driver.FindElement(By.XPath("/html/body/div/div/div/form/div[4]/span[1]/i[4]")).Click();

            driver.FindElement(By.Id("Title")).SendKeys("Test Title");
            driver.FindElement(By.Id("Review1")).SendKeys("Test Review");
            driver.FindElement(By.XPath("/html/body/div/div/div/form/div[5]/input")).Click();

            String actual = driver.FindElement(By.XPath("/html/body/div/div[3]/div[2]/div/div[1]/span[2]/span")).GetAttribute(innerText);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        //Friends tests
        [Test]
        public void SendFriendRequest()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "Alan1";

            //Act
            login();
            //friend nav
            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul/li[5]/a")).GetAttribute("href");
            
            driver.FindElement(By.XPath("/html/body/div/div[1]/div[2]/form/div/input")).SendKeys("Alan1");
            
            driver.FindElement(By.XPath("/html/body/div/div[1]/div[2]/form/button")).Click();

            driver.Url = driver.FindElement(By.XPath("/html/body/div/div[1]/div[2]/table/tbody/tr/td[2]/a")).GetAttribute("href");

            String actual = driver.FindElement(By.XPath("/html/body/div/div[2]/div[2]/table/tbody/tr/td[1]/a")).GetAttribute(innerText);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AcceptFriendRequest()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "Alan";

            //Act
            driver.FindElement(By.Id("Input_Email")).SendKeys("bealsman1@gmail.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Admin123!");
            driver.FindElement(By.XPath("/html/body/div/div/div/section/form/div[5]/button")).Click();

            //friend nav
            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul/li[5]/a")).GetAttribute("href");

            driver.Url = driver.FindElement(By.XPath("/html/body/div/div[2]/div[1]/table/tbody/tr/td[2]/a")).GetAttribute("href");

            String actual = driver.FindElement(By.XPath("/html/body/div/div[1]/div[1]/table/tbody/tr/td[1]/a")).GetAttribute(innerText);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }

        private void login()
        {
            driver.FindElement(By.Id("Input_Email")).SendKeys("bealsman@gmail.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Admin123!");
            driver.FindElement(By.XPath("/html/body/div/div/div/section/form/div[5]/button")).Click();
        }

    }
}
