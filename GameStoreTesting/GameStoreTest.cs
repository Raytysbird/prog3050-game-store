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

namespace GameStoreTesting
{
    internal class GameStoreTest
    {
        IWebDriver driver;
        FirefoxOptions options;

        [SetUp]
        public void startBrowser()
        {
            options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;
            driver = new FirefoxDriver(options);
        }

        [Test]
        public void SelectGamesTest()
        {
            //Arrange
            driver.Url = "https://localhost:44329/";

            //int expected = 1;

            //Act


            //Assert
            //Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DisplayDetails()
        {
            //Arrange
            driver.Url = "https://localhost:44329/";

            //int expected = 1;

            //Act


            //Assert
            //Assert.AreEqual(expected, actual);
        }

        [Test]
        public void WishListAdd()
        {
            //Arrange
            driver.Url = "https://localhost:44329/";

            //int expected = 1;

            //Act


            //Assert
            //Assert.AreEqual(expected, actual);
        }

        [Test]
        public void WishlistRemove()
        {
            //Arrange
            driver.Url = "https://localhost:44329/";

            //int expected = 1;

            //Act


            //Assert
            //Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RateGame()
        {
            //Arrange
            driver.Url = "https://localhost:44329/";

            //int expected = 1;

            //Act


            //Assert
            //Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DisplayOverAllResults()
        {
            //Arrange
            driver.Url = "https://localhost:44329/";

            //int expected = 1;

            //Act


            //Assert
            //Assert.AreEqual(expected, actual);
        }

        [Test]
        public void WriteReview()
        {
            //Arrange
            driver.Url = "https://localhost:44329/";

            //int expected = 1;

            //Act


            //Assert
            //Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ApproveReview()
        {
            //Arrange
            driver.Url = "https://localhost:44329/";

            //int expected = 1;

            //Act


            //Assert
            //Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddMemberToFamily()
        {
            //Arrange
            driver.Url = "https://localhost:44329/";

            //int expected = 1;

            //Act


            //Assert
            //Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddMemberToFriends()
        {
            //Arrange
            driver.Url = "https://localhost:44329/";

            //int expected = 1;

            //Act


            //Assert
            //Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ViewEvent()
        {
            //Arrange
            driver.Url = "https://localhost:44329/";

            //int expected = 1;

            //Act


            //Assert
            //Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RegisterForEvent()
        {
            //Arrange
            driver.Url = "https://localhost:44329/";

            //int expected = 1;

            //Act


            //Assert
            //Assert.AreEqual(expected, actual);
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}
