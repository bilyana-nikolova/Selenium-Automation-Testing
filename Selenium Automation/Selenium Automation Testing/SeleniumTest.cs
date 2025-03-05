//Inside SeleniumTest.cs

using NUnit.Framework;

using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V131.FedCm;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

using System.Collections.ObjectModel;

using System.IO;
using static System.Net.WebRequestMethods;
using NUnit.Framework.Internal;
using OpenQA.Selenium.Support.Events;
using System.Reflection.PortableExecutable;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace SeleniumCsharp

{

    public class Tests
    {
        private IWebDriver driver;

        private string BaseUrl = "https://practice.expandtesting.com/";

        WebDriverWait wait;

        [OneTimeSetUp]

        public void Setup()

        {
            // TODO: how to default base url in selenium ********************************
            //Below code is to get the drivers folder path dynamically.

            //You can also specify chromedriver.exe path dircly ex: C:/MyProject/Project/drivers

            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            //Creates the ChomeDriver object, Executes tests on Google Chrome

            driver = new ChromeDriver(path + @"\drivers\");

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            //If you want to Execute Tests on Firefox uncomment the below code

            // Specify Correct location of geckodriver.exe folder path. Ex: C:/Project/drivers

            //driver= new FirefoxDriver(path + @"\drivers\");

        }
        [Ignore("skip")]
        [Test]

        public void verifyLogin()

        {

            driver.Navigate().GoToUrl(BaseUrl + "login");
            IWebElement login = driver.FindElement(By.Id("login"));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("login")));
            Assert.IsTrue(login.Displayed);

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


        [Test]

        public void InvalidUsername()

        {

            driver.Navigate().GoToUrl("https://practice.expandtesting.com/login");


            Assert.IsTrue(driver.FindElement(By.Id("login")).Displayed);

            IWebElement usernameTextBox = driver.FindElement(By.Id("username"));
            IWebElement passwordTextBox = driver.FindElement(By.Id("password"));

            wait.Until(ExpectedConditions.ElementToBeClickable(usernameTextBox));
            wait.Until(ExpectedConditions.ElementToBeClickable(passwordTextBox));

            passwordTextBox.Clear();
            passwordTextBox.SendKeys("SuperSecretPassword!");

            usernameTextBox.Clear();
            usernameTextBox.SendKeys("wrongUser");

            IWebElement form = driver.FindElement(By.Id("login"));

            form.Submit();

            IWebElement parentElement = driver.FindElement(By.Id("flash"));

            IWebElement boldElement = parentElement.FindElement(By.XPath(".//b"));

            string text = boldElement.Text;
            Assert.IsTrue(text == "Your username is invalid!");

            String currentURL = driver.Url;

            Assert.IsTrue("https://practice.expandtesting.com/login" == currentURL);
        }


        [Test]
        public void InvalidPassword()
        {
            driver.Navigate().GoToUrl("https://practice.expandtesting.com/login");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            IWebElement usernameTextBox = driver.FindElement(By.Id("username"));
            IWebElement passwordTextBox = driver.FindElement(By.Id("password"));

            wait.Until(ExpectedConditions.ElementToBeClickable(usernameTextBox));
            wait.Until(ExpectedConditions.ElementToBeClickable(passwordTextBox));

            passwordTextBox.Clear();
            passwordTextBox.SendKeys("WrongPassword");

            usernameTextBox.Clear();
            usernameTextBox.SendKeys("practice");

            IWebElement form = driver.FindElement(By.Id("login"));

            form.Submit();

            IWebElement parentElement = driver.FindElement(By.Id("flash"));

            IWebElement boldElement = parentElement.FindElement(By.XPath(".//b"));

            string text = boldElement.Text;
            Assert.IsTrue(text == "Your password is invalid!");

            String currentURL = driver.Url;
            Assert.IsTrue("https://practice.expandtesting.com/login" == currentURL);
        }




        [Test]
        public void SucsessfullShowInput()
        {
            driver.Navigate().GoToUrl("https://www.lambdatest.com/selenium-playground/simple-form-demo");

            Assert.IsTrue(driver.FindElement(By.Id("user-message")).Displayed);

            IWebElement userMessageTextBox = driver.FindElement(By.Id("user-message"));

            userMessageTextBox.Clear();
            userMessageTextBox.SendKeys("text");
            IWebElement button = driver.FindElement(By.Id("showInput"));
            button.Click();


            IWebElement text = driver.FindElement(By.Id("message"));
            Assert.IsTrue(text.Text == "text");

        }
        [Ignore("skip")]
        [Test]
        public void SuccessfulRegistration()
        {
            driver.Navigate().GoToUrl("https://practice.expandtesting.com/register");

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("username")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));



            IWebElement usernameTextBox = driver.FindElement(By.Id("username"));
            IWebElement passwordTextBox = driver.FindElement(By.Id("password"));
            IWebElement confirmPasswordTexBox = driver.FindElement(By.Id("confirmPassword"));

            passwordTextBox.Clear();
            passwordTextBox.SendKeys("99999");

            usernameTextBox.Clear();
            usernameTextBox.SendKeys("lalalalaa");

            confirmPasswordTexBox.Clear();
            confirmPasswordTexBox.SendKeys("99999");

            IWebElement form = driver.FindElement(By.Id("register"));

            form.Submit();

            IWebElement wellcomeMessage = driver.FindElement(By.Id("flash"));

            Assert.IsTrue(wellcomeMessage.Displayed);

            Assert.IsTrue("You're logged in. Please log out before registering a new account." == wellcomeMessage.Text);


        }

        [Test]
        public void MissingUsername()
        {
            driver.Navigate().GoToUrl("https://practice.expandtesting.com/register");

            IWebElement passwordTextBox = driver.FindElement(By.Id("password"));
            IWebElement confirmPasswordTexBox = driver.FindElement(By.Id("confirmPassword"));

            passwordTextBox.Clear();
            passwordTextBox.SendKeys("99999");

            confirmPasswordTexBox.Clear();
            confirmPasswordTexBox.SendKeys("99999");

            IWebElement form = driver.FindElement(By.Id("register"));
            form.Submit();


            IWebElement message = driver.FindElement(By.Id("flash"));
            string text = "All fields are required.";
            driver.FindElement(By.Id("flash"));

            Assert.IsTrue(message.Text == text);




        }

        [Test]
        public void MissingPassword()
        {
            driver.Navigate().GoToUrl("https://practice.expandtesting.com/register");

            IWebElement usernameTextBox = driver.FindElement(By.Id("username"));
            IWebElement confirmPasswordTextBox = driver.FindElement(By.Id("confirmPassword"));

            usernameTextBox.Clear();
            usernameTextBox.SendKeys("lalalalaa");

            confirmPasswordTextBox.Clear();
            confirmPasswordTextBox.SendKeys("99999");

            IWebElement form = driver.FindElement(By.Id("register"));
            form.Submit();

            IWebElement message = driver.FindElement(By.Id("flash"));

            string text = "All fields are required.";

            driver.FindElement(By.Id("flash"));

            Assert.IsTrue(message.Text == text);
        }

        [Test]
        public void NonMatchingPasswords()
        {
            driver.Navigate().GoToUrl("https://practice.expandtesting.com/register");

            IWebElement passwordTextBox = driver.FindElement(By.Id("password"));
            IWebElement usernameTextBox = driver.FindElement(By.Id("username"));
            IWebElement confirmPasswordTextBox = driver.FindElement(By.Id("confirmPassword"));

            usernameTextBox.Clear();
            usernameTextBox.SendKeys("Lalalalaa");

            passwordTextBox.Clear();
            passwordTextBox.SendKeys("99999");

            confirmPasswordTextBox.Clear();
            confirmPasswordTextBox.SendKeys("99999999");

            IWebElement form = driver.FindElement(By.Id("register"));
            form.Submit();
            IWebElement message = driver.FindElement(By.Id("flash"));

            string text = "Passwords do not match.";

            Assert.IsTrue(message.Text == text);












        }






        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}

   
