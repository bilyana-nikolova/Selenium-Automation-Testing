//Inside SeleniumTest.cs

using NUnit.Framework;

using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V131.FedCm;
using OpenQA.Selenium.Firefox;

using System;

using System.Collections.ObjectModel;

using System.IO;
using static System.Net.WebRequestMethods;

namespace SeleniumCsharp

{

    public class Tests
    {
        private IWebDriver driver;

        private string BaseUrl = "https://practice.expandtesting.com/";

        [OneTimeSetUp]

        public void Setup()

        {
            // TODO: how to default base url in selenium ********************************
            //Below code is to get the drivers folder path dynamically.

            //You can also specify chromedriver.exe path dircly ex: C:/MyProject/Project/drivers

            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            //Creates the ChomeDriver object, Executes tests on Google Chrome

            driver = new ChromeDriver(path + @"\drivers\");

            //If you want to Execute Tests on Firefox uncomment the below code

            // Specify Correct location of geckodriver.exe folder path. Ex: C:/Project/drivers

            //driver= new FirefoxDriver(path + @"\drivers\");

        }

        [Test]

        public void verifyLogin()

        {

            driver.Navigate().GoToUrl("https://practice.expandtesting.com/login");
            

            Assert.IsTrue(driver.FindElement(By.Id("login")).Displayed);

        }

        [Test]

        public void successfulLogin()
        {

            driver.Navigate().GoToUrl(BaseUrl + "login");


            IWebElement usernameTextBox = driver.FindElement(By.Id("username"));
            IWebElement passwordTextBox = driver.FindElement(By.Id("password"));


            passwordTextBox.Clear();
            passwordTextBox.SendKeys("SuperSecretPassword!");

            usernameTextBox.Clear();
            usernameTextBox.SendKeys("practice");

            IWebElement form = driver.FindElement(By.Id("login"));

            form.Submit();

            String currentURL = driver.Url;

            Assert.IsTrue("https://practice.expandtesting.com/secure" == currentURL);


            WebElement alert = (WebElement)driver.FindElement(By.XPath("//*[contains(text(),'You logged into a secure area!')]"));

            Assert.IsTrue(alert.Displayed);

            WebElement Logout = (WebElement)driver.FindElement(By.XPath("//*[contains(text(),'Logout')]"));

            Assert.IsTrue(Logout.Displayed);

           }
            

        [OneTimeTearDown]

        public void TearDown()

        {

            driver.Quit();

        }

    }

}
