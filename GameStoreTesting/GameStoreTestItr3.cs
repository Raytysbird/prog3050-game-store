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

namespace GameStoreTesting
{
    public class GameStoreTestItr3
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

        // Iteration 3 tests
        //Merch tests
        [Test]
        public void AdminAddMerch()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "Mario Shirt";

            //Act
            loginAsAdmin();

            GetHref("adminPanel");
            GetHref("manage_merch");
            GetHref("create_merch");

            SendKeys("name", "Mario Shirt");
            SendKeys("description", "a cool Mario T-Shirt");
            //default chooses first item
            //new SelectElement(driver.FindElement(By.Id("game_select"))).SelectByIndex(0);
            SendKeys("price", "9");

            Click("submit_btn");

            String actual = GetInnerText("Mario Shirt");
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void UserAddMerchToCart()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "Mario Shirt";

            //Act
            login();

            GetHref("nav_merch");
            GetHref("Mario Shirt_details");
            GetHref("addtocart");

            String actual = GetInnerText("Mario Shirt");
            //Assert
            Assert.AreEqual(expected, actual);
        }

        //cart tests
        [Test]
        public void RemoveFromCart()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "Your cart is empty right now. Checkout our games and merchandise!!View gamesView Merchandise";

            //Act
            login();

            GetHref("nav_cart");
            driver.Url = driver.FindElement(By.ClassName("btn-danger")).GetAttribute("href");

            String actual = GetInnerText("empty_cart");
            //Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void AddGameToCart()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "Mario";

            //Act
            login();

            GetHref("nav_games");
            driver.Url = driver.FindElement(By.ClassName("game_details")).GetAttribute("href");
            GetHref("addtocart");

            String actual = GetInnerText("Mario");
            //Assert
            Assert.AreEqual(expected, actual);
        }




        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }

        private void GetHref(string id)
        {
            driver.Url = driver.FindElement(By.Id(id)).GetAttribute("href");
        }

        private void SendKeys(String id, String value)
        {
            driver.FindElement(By.Id(id)).SendKeys(value);
        }

        private String GetInnerText(String id)
        {
            return driver.FindElement(By.Id(id)).GetAttribute(innerText);
        }

        private String GetInnerHtml(String id)
        {
            return driver.FindElement(By.Id(id)).GetAttribute(innerHtml);
        }

        private void Click(String id)
        {
            driver.FindElement(By.Id(id)).Click();
        }

        private void login()
        {
            driver.FindElement(By.Id("Input_Email")).SendKeys("bealsman@gmail.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Admin123!");
            driver.FindElement(By.Id("logIn")).Click();
        }

        private void loginAsAdmin()
        {
            driver.FindElement(By.Id("Input_Email")).SendKeys("Abeals8171@conestogac.on.ca");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Admin123!");
            driver.FindElement(By.Id("logIn")).Click();
        }
    }
}
