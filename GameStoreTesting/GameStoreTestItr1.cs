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

    public class GameStoreTest_Iteration1
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
        // Iteration 1 tests
        //login tests
        [Test]
        public void SignUpFail_NoRecaptcha()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Register";

            String expected = "Please check recaptcha.";

            //Act
            driver.FindElement(By.Id("Input_UserName")).SendKeys("Alan");
            driver.FindElement(By.Id("Input_Email")).SendKeys("tim@gmail.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Alan123");
            driver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys("Alan123");

            driver.FindElement(By.XPath("/html/body/div[1]/div/div/form/button")).Click();

            String actual = driver.FindElement(By.XPath("/html/body/div[1]/div/div/form/div[1]/ul/li")).GetAttribute(innerHtml);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LoginSuccess()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "Mission"; //less about the text more about the fact that they successfully logged in and got to the home page

            //Act
            driver.FindElement(By.Id("Input_Email")).SendKeys("bealsman@gmail.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Admin123!");

            driver.FindElement(By.XPath("/html/body/div/div/div/section/form/div[5]/button")).Click();

            String actual = driver.FindElement(By.XPath("/html/body/div/div[2]/div[2]/h2")).GetAttribute(innerHtml);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        //profile tests
        [Test]
        public void EditProfile()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "JohnArbuckle";

            //Act
            login();

            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/form/ul/li[1]/a")).GetAttribute("href");

            driver.Url = driver.FindElement(By.XPath("/html/body/div/div[2]/a[1]")).GetAttribute("href");

            driver.FindElement(By.Id("first_name")).Clear();
            driver.FindElement(By.Id("first_name")).SendKeys("John");
            driver.FindElement(By.Id("last_name")).Clear();
            driver.FindElement(By.Id("last_name")).SendKeys("Arbuckle");
            new SelectElement(driver.FindElement(By.Id("gender"))).SelectByIndex(1);

            driver.FindElement(By.XPath("/html/body/div/div/div/form/div[20]/input")).Click();

            String actual = driver.FindElement(By.XPath("/html/body/div/div[1]/dl/dd[3]")).GetAttribute(innerText);
            actual += driver.FindElement(By.XPath("/html/body/div/div[1]/dl/dd[4]")).GetAttribute(innerText);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void EditProfileFail_NoLastName()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "The Last Name field is required.";

            //Act
            login();

            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/form/ul/li[1]/a")).GetAttribute("href");

            driver.Url = driver.FindElement(By.XPath("/html/body/div/div[2]/a[1]")).GetAttribute("href");

            driver.FindElement(By.Id("first_name")).Clear();
            driver.FindElement(By.Id("first_name")).SendKeys("John");
            driver.FindElement(By.Id("last_name")).Clear();
            new SelectElement(driver.FindElement(By.Id("gender"))).SelectByIndex(1);

            driver.FindElement(By.XPath("/html/body/div/div/div/form/div[20]/input")).Click();

            String actual = driver.FindElement(By.Id("last_name-error")).GetAttribute(innerText);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Address tests
        [Test]
        public void AddAddressSuccess()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "My Street Address 123";

            //Act
            login();

            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul/li[6]/a")).GetAttribute("href");

            driver.FindElement(By.Id("StreetAddress")).Clear();
            driver.FindElement(By.Id("StreetAddress")).SendKeys("My Street Address 123");
            driver.FindElement(By.Id("City")).Clear();
            driver.FindElement(By.Id("City")).SendKeys("My City");
            driver.FindElement(By.Id("PostalCode")).Clear();
            driver.FindElement(By.Id("PostalCode")).SendKeys("N0G 1C0");
            new SelectElement(driver.FindElement(By.Id("Province"))).SelectByIndex(1);

            driver.FindElement(By.Id("btnSubmit")).Click();

            String actual = driver.FindElement(By.XPath("/html/body/div/div/div[1]/div/div[2]/dl/dd[1]")).GetAttribute(innerText);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddAddressFail_NoEmail()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "The Postal Code field is required.";

            //Act
            login();

            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul/li[6]/a")).GetAttribute("href");

            driver.FindElement(By.Id("StreetAddress")).Clear();
            driver.FindElement(By.Id("StreetAddress")).SendKeys("My Street Address 123");
            driver.FindElement(By.Id("City")).Clear();
            driver.FindElement(By.Id("City")).SendKeys("My City");
            driver.FindElement(By.Id("PostalCode")).Clear();
            new SelectElement(driver.FindElement(By.Id("Province"))).SelectByIndex(1);

            driver.FindElement(By.Id("btnSubmit")).Click();

            String actual = driver.FindElement(By.Id("PostalCode-error")).GetAttribute(innerText);

            //Assert
            Assert.AreEqual(expected, actual);
        }
        //Credit Card tests
        [Test]
        public void AddCreditCard()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "4761-7390-0101-0010";

            //Act
            login();

            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul/li[4]/a")).GetAttribute("href");
            driver.FindElement(By.XPath("/html/body/div/a")).Click();

            driver.FindElement(By.Id("Number")).Clear();
            driver.FindElement(By.Id("Number")).SendKeys("4761-7390-0101-0010");
            driver.FindElement(By.Id("ExpDate")).Clear();
            driver.FindElement(By.Id("ExpDate")).SendKeys("12/22");
            driver.FindElement(By.Id("Ccc")).Clear();
            driver.FindElement(By.Id("Ccc")).SendKeys("201");

            driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[5]/input")).Click();

            String actual = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]")).GetAttribute(innerText);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddCreditCardFail_PastExpiryDate()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "Expiry date cannot be in past!!";

            //Act
            login();

            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul/li[4]/a")).GetAttribute("href");
            driver.FindElement(By.XPath("/html/body/div/a")).Click();

            driver.FindElement(By.Id("Number")).Clear();
            driver.FindElement(By.Id("Number")).SendKeys("4761-7390-0101-0010");
            driver.FindElement(By.Id("ExpDate")).Clear();
            driver.FindElement(By.Id("ExpDate")).SendKeys("12/20");
            driver.FindElement(By.Id("Ccc")).Clear();
            driver.FindElement(By.Id("Ccc")).SendKeys("201");

            driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[5]/input")).Click();

            String actual = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[3]/span")).GetAttribute(innerText);

            //Assert
            Assert.AreEqual(expected, actual);
        }
        //Preferences tests
        [Test]
        public void AddFavouritePlatform()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "XBOX";

            //Act
            login();

            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul/li[2]/a")).GetAttribute("href");

            SelectElement sel = new SelectElement(driver.FindElement(By.Id("PlatformId")));
            sel.SelectByIndex(1);

            String actual = driver.FindElement(By.XPath("/html/body/div/div/div[1]/table/tbody/tr/td[1]")).GetAttribute(innerText);

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void AddFavouriteCategory()
        {
            //Arrange
            driver.Url = "https://localhost:44329/Identity/Account/Login";

            String expected = "FPS";

            //Act
            login();

            driver.Url = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul/li[2]/a")).GetAttribute("href");

            SelectElement sel = new SelectElement(driver.FindElement(By.Id("CategoryId")));
            sel.SelectByIndex(1);

            String actual = driver.FindElement(By.XPath("/html/body/div/div/div[2]/table/tbody/tr/td[1]")).GetAttribute(innerText);

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
