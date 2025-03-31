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
using System.Reflection.Emit;
using OpenQA.Selenium.DevTools.V131.Autofill;
using System.Security.Cryptography.X509Certificates;
using static OpenQA.Selenium.BiDi.Modules.Input.Pointer;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using OpenQA.Selenium.DevTools.V131.Page;
using static OpenQA.Selenium.BiDi.Modules.Script.RealmInfo;
using System.Net.Mail;

namespace SeleniumCsharp

{

    public class Tests
    {
        private IWebDriver driver;

        private IJavaScriptExecutor js;

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

            js = (IJavaScriptExecutor)driver;

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
        [Ignore("skip")]
        [Test]
        public void formValidation()
        {
            driver.Navigate().GoToUrl("https://practice.expandtesting.com/form-validation");

            IWebElement contactNameTexBox = driver.FindElement(By.Id("validationCustom01"));
            IWebElement picUpDateTextBov = driver.FindElement(By.Name("pickupdate"));
            IWebElement contactNumberTextBox = driver.FindElement(By.Name("contactnumber"));

            SelectElement dropDown = new SelectElement(driver.FindElement(By.Id("validationCustom04")));
            dropDown.SelectByValue("card");

            contactNameTexBox.Clear();
            contactNameTexBox.SendKeys("LALAL");

            picUpDateTextBov.Clear();
            picUpDateTextBov.SendKeys("12.07.2024");

            contactNumberTextBox.Clear();
            contactNumberTextBox.SendKeys("013-9999954");


            WebElement button = (WebElement)driver.FindElement(By.LinkText("Register"));
            button.Click();


            IWebElement message = driver.FindElement(By.ClassName("alert"));
            string text = "Thank you for validating your ticket";

            Assert.IsTrue(message.Text == text);

        }
        [Ignore("skip")]
        [Test]
        public void AddRemoveElements()
        {
            driver.Navigate().GoToUrl("https://practice.expandtesting.com/add-remove-elements");
            WebElement button = (WebElement)driver.FindElement(By.ClassName("btn"));

            wait.Until(ExpectedConditions.ElementToBeClickable(button));
            button.Click();

            button.Click();

            button.Click();

            button.Click();

            WebElement parentElement = (WebElement)driver.FindElement(By.Id("elements"));
            List<WebElement> allChildElements = (List<WebElement>)parentElement.FindElement(By.XPath("*"));




            Assert.IsTrue(allChildElements.Count == 4);





        }
        [Test]
        public void registerUser()
        {
            //2. Navigate to url 'http://automationexercise.com'
            driver.Navigate().GoToUrl("https://automationexercise.com/");

            String currentURL = driver.Url;
            //3. Verify that home page is visible successfully
            //Assert.IsTrue("https://automationexercise.com/" == currentURL);

            //IWebElement button = (WebElement)driver.FindElement(By.ClassName("fa"));  

            IWebElement loginButton = driver.FindElement(By.XPath("//a[@href='/login']"));

            IWebElement popUpButton = driver.FindElement(By.XPath("//button[@aria-label='Consent']"));
            //wait.Until(ExpectedConditions.ElementToBeClickable(button));
            popUpButton.Click();
            loginButton.Click();

            //4. Click on 'Signup / Login' button

            IWebElement message = (WebElement)driver.FindElement(By.ClassName("signup-form"));
            //5. Verify 'New User Signup!' is visible
            IWebElement emailTextBox = driver.FindElement(By.CssSelector("input[data-qa='signup-email']"));
            IWebElement nameTextBox = driver.FindElement(By.Name("name"));

            emailTextBox.Clear();
            emailTextBox.SendKeys("biba.nikolova123333@abv.bg");
            nameTextBox.Clear();
            nameTextBox.SendKeys("Bilyana");
            
            IWebElement signupButton = driver.FindElement(By.CssSelector("button[data-qa='signup-button']")); 
            signupButton.Click();                                                             
            IWebElement messageTitle = driver.FindElement(By.ClassName("title"));

            Assert.IsTrue(messageTitle.Displayed);

             string text = "Enter Account Information";                

            IWebElement bElement = driver.FindElement(By.XPath("//b[text()='" + text + "']"));
            Assert.IsTrue(bElement.Displayed);


            IWebElement genderButton = driver.FindElement(By.Id("id_gender2"));
            genderButton.Click();
            IWebElement nameTexBox = driver.FindElement(By.Id("name"));
            IWebElement emailTextBov = driver.FindElement(By.Id("email"));
            IWebElement passwordTextBox = driver.FindElement(By.Id("password"));
            passwordTextBox.Clear();
            passwordTextBox.SendKeys("999");

            SelectElement dropDays = new SelectElement(driver.FindElement(By.Id("days")));
            dropDays.SelectByValue("12");
            SelectElement dropMonth = new SelectElement(driver.FindElement(By.Id("months")));
            dropMonth.SelectByValue("7");
            SelectElement dropYear = new SelectElement(driver.FindElement(By.Id("years")));
            dropYear.SelectByValue("1996");                              


            IWebElement newsletterCheckbox = driver.FindElement(By.Id("newsletter")); // 10: Select checkbox 'Sign up for our newsletter!'

            wait.Until(ExpectedConditions.ElementToBeClickable(newsletterCheckbox));
            newsletterCheckbox.Click();

            IWebElement optinCheckbox = driver.FindElement(By.Id("optin"));
            optinCheckbox.Click();                                                          //11. Select checkbox 'Receive special offers from our partners!'



            IWebElement firstNameTextBox = driver.FindElement(By.Id("first_name"));
            firstNameTextBox.Clear();
            firstNameTextBox.SendKeys("Bilyana");                                                  // populni vsiickii poleta SendKeys(username);
            IWebElement lastNameTextBox = driver.FindElement(By.Id("last_name"));
            lastNameTextBox.Clear();
            lastNameTextBox.SendKeys("Nikolova");
            IWebElement companyTextBox = driver.FindElement(By.Id("company"));           //12. Fill details: First name, Last name, Company, Address, Address2, Cou
            companyTextBox.Clear();
            companyTextBox.SendKeys("Vivacom");
            IWebElement addressNameTexBox = driver.FindElement(By.Id("address1"));
            addressNameTexBox.Clear();
            addressNameTexBox.SendKeys("Bdin14 Vidin");
            IWebElement address2NameTextBox = driver.FindElement(By.Id("address2")); // proveri si ID-to
            address2NameTextBox.Clear();
            address2NameTextBox.SendKeys("-");

            SelectElement dropContry = new SelectElement(driver.FindElement(By.Id("country")));
            dropContry.SelectByValue("Australia");
            IWebElement stateTextBox = driver.FindElement(By.Id("state"));
            stateTextBox.Clear();
            stateTextBox.SendKeys("Vidin");                                                                                        // lipsva edno ot poletata state
            IWebElement cityTextBox = driver.FindElement(By.Id("city"));
            cityTextBox.Clear();
            cityTextBox.SendKeys("Vidin");
            IWebElement zipCodeTextBox = driver.FindElement(By.Id("zipcode"));
            zipCodeTextBox.Clear();
            zipCodeTextBox.SendKeys("1700");
            IWebElement mobileNumberTextBox = driver.FindElement(By.Id("mobile_number"));
            mobileNumberTextBox.Clear();
            mobileNumberTextBox.SendKeys("0888034797");


            IWebElement createAccountButton = driver.FindElement(By.CssSelector("button[data-qa='create-account']"));
            createAccountButton.Click();                                                                                                           // 13. Click 'Create Account button'
                                                                                                                                                   
            IWebElement acountCreaqted = driver.FindElement(By.XPath("//*[@data-qa='account-created']"));
            Assert.IsTrue(acountCreaqted.Displayed);                                                                  //14.Verify that 'ACCOUNT CREATED!' is visible
                                                                                                                      // napravi 14-ta kato nemarish elementa s data-qa atributa i sravnish teksta mu s textAc  data-qa="account-created"

            //15. Click 'Continue' button
            IWebElement buttonContinue = (WebElement)driver.FindElement(By.CssSelector("a[data-qa='continue-button']"));   
            wait.Until(ExpectedConditions.ElementToBeClickable(buttonContinue));
            buttonContinue.Click();



            IWebElement loggedInLink = driver.FindElement(By.XPath("//a[contains(text(),'Logged in as')]")); 


            IWebElement buttonDel = (WebElement)driver.FindElement(By.LinkText("Delete Account"));                                
            wait.Until(ExpectedConditions.ElementToBeClickable(buttonDel));                                              //17. Click 'Delete Account' button
            buttonDel.Click();

          
            IWebElement messageACOUNTDELITED = driver.FindElement(By.CssSelector("[data-qa='account-deleted']"));
            Assert.IsTrue(messageACOUNTDELITED.Displayed);

        
            IWebElement continueButtonl = (WebElement)driver.FindElement(By.CssSelector("[data-qa='continue-button']"));                                
            wait.Until(ExpectedConditions.ElementToBeClickable(continueButtonl));                                              //17. Click 'Delete Account' button
            continueButtonl.Click();                                                                     //18. Verify that 'ACCOUNT DELETED!' is visible and click 'Continue' button






        }
        [Test]
        public void loginUserWithCorrectEmailAndPassword()
        {
            driver.Navigate().GoToUrl("https://automationexercise.com");
            IWebElement popUpButton = driver.FindElement(By.XPath("//button[@aria-label='Consent']"));
            popUpButton.Click();

            IWebElement loginSingInButton = driver.FindElement(By.XPath("//a[@href='/login']"));
            loginSingInButton.Click();

            IWebElement loginToYourAccount = driver.FindElement(By.XPath("//*[text()='Login to your account']"));
            Assert.IsTrue(loginToYourAccount.Displayed);

            IWebElement emailAddress = driver.FindElement(By.CssSelector("[data-qa='login-email']"));
            emailAddress.Clear();
            emailAddress.SendKeys("biba.nikolova12333@abv.bg");

            IWebElement passwoard = driver.FindElement(By.CssSelector("[data-qa='login-password']"));
            passwoard.Clear();
            passwoard.SendKeys("999");

            IWebElement loginButton = driver.FindElement(By.CssSelector("[data-qa='login-button']"));
            loginButton.Click();

            IWebElement loginAsButton = driver.FindElement(By.XPath("//*[contains(text(), 'bilyana')]"));
            Assert.IsTrue(loginAsButton.Displayed);

            IWebElement delButton = driver.FindElement(By.XPath("//*[contains(text(), 'Delete Account')]"));
            delButton.Click();

            IWebElement accountDeleted = driver.FindElement(By.CssSelector("[data-qa='account-deleted']"));
            Assert.IsTrue(accountDeleted.Displayed);


        }
        [Test]
        public void loginUserWithIncorrectEmailAndPassword()
        {
            driver.Navigate().GoToUrl("http://automationexercise.com");

            IWebElement popUpButton = driver.FindElement(By.XPath("//button[@aria-label='Consent']"));
            popUpButton.Click();

            IWebElement loginSingInButton = driver.FindElement(By.XPath("//a[@href='/login']"));
            loginSingInButton.Click();

            IWebElement emailAddress = driver.FindElement(By.CssSelector("[data-qa='login-email']"));
            emailAddress.Clear();
            emailAddress.SendKeys("biba.nikolova1233@abv.bg");

            IWebElement passwoard = driver.FindElement(By.CssSelector("[data-qa='login-password']"));
            passwoard.Clear();
            passwoard.SendKeys("99");

            IWebElement loginButton = driver.FindElement(By.CssSelector("[data-qa='login-button']"));
            loginButton.Click();

            IWebElement incorrect = driver.FindElement(By.XPath("//*[contains(text(), 'Your email or password is incorrect!')]"));


        }
        [Test]
        public void logoutUser()
        {
            driver.Navigate().GoToUrl("https://automationexercise.com/");

            IWebElement popUpButton = driver.FindElement(By.XPath("//button[@aria-label='Consent']"));
            popUpButton.Click();

            IWebElement singUpLoginButton = driver.FindElement(By.XPath("//a[@href='/login']"));
            singUpLoginButton.Click();

            IWebElement loginToYourAccountMessage = driver.FindElement(By.XPath("//*[text()='Login to your account']"));
            Assert.IsTrue(loginToYourAccountMessage.Displayed);

            IWebElement emailTextBox = driver.FindElement(By.CssSelector("[data-qa='login-email']"));
            emailTextBox.Clear();
            emailTextBox.SendKeys("biba.nikolova12311@abv.bg");

            IWebElement passwordTextBox = driver.FindElement(By.CssSelector("[data-qa='login-password']"));
            passwordTextBox.Clear();
            passwordTextBox.SendKeys("999");

            IWebElement loginButton = driver.FindElement(By.CssSelector("[data-qa='login-button']"));
            loginButton.Click();
            IWebElement loginAsButton = driver.FindElement(By.XPath("//*[contains(text(), 'bilyana')]"));
            Assert.IsTrue(loginAsButton.Displayed);

            IWebElement logOutButton = driver.FindElement(By.XPath("//*[text()=' Logout']"));
            logOutButton.Click();

            driver.Navigate().GoToUrl("https://automationexercise.com/login");



        }
        [Test]
        public void registerUserWitHExistingEmail()
        {
            driver.Navigate().GoToUrl("https://automationexercise.com/");

            IWebElement popUpButton = driver.FindElement(By.XPath("//button[@aria-label='Consent']"));
            popUpButton.Click();

            IWebElement singUpLoginButton = driver.FindElement(By.XPath("//a[@href='/login']"));
            singUpLoginButton.Click();

            IWebElement newUserSignupMessage = driver.FindElement(By.XPath("//*[text()='New User Signup!']"));

            IWebElement nameTextBox = driver.FindElement(By.CssSelector("[data-qa='signup-name']"));
            nameTextBox.Clear();
            nameTextBox.SendKeys("Bilyana");

            IWebElement sinUpEmailTextBox = driver.FindElement(By.CssSelector("[data-qa='signup-email']"));
            sinUpEmailTextBox.Clear();
            sinUpEmailTextBox.SendKeys("biba.nikolova12333@abv.bg");

            IWebElement singupButton = driver.FindElement(By.CssSelector("[data-qa='signup-button']"));
            singupButton.Click();

            IWebElement emailAddressAlreadyExist = driver.FindElement(By.XPath("//*[text()='Email Address already exist!']"));
            Assert.IsTrue(emailAddressAlreadyExist.Displayed);




        }
        [Test]
        public void contactUsForm()
        {

            driver.Navigate().GoToUrl("https://automationexercise.com/");
            IWebElement popUpButton = driver.FindElement(By.XPath("//*[text()='Consent']"));
            popUpButton.Click();

            IWebElement contactUsButton = driver.FindElement(By.XPath("//*[text()=' Contact us']"));
            contactUsButton.Click();
            IWebElement getInTouchMessage = driver.FindElement(By.XPath("//*[text()='Get In Touch']"));
            Assert.IsTrue(getInTouchMessage.Displayed);
            IWebElement emailTextBox = driver.FindElement(By.CssSelector("[data-qa='email']"));
            emailTextBox.Clear();
            emailTextBox.SendKeys("biba.nikolova12311@abv.bg");
            IWebElement nameTextBox = driver.FindElement(By.CssSelector("[data-qa='name']"));
            nameTextBox.Clear();
            nameTextBox.SendKeys("Bilyana");
            IWebElement subjectTextBox = driver.FindElement(By.CssSelector("[data-qa='subject']"));
            subjectTextBox.Clear();
            subjectTextBox.SendKeys("Ban");
            IWebElement messageTextBox = driver.FindElement(By.CssSelector("[data-qa='message']"));
            messageTextBox.Clear();
            messageTextBox.SendKeys("Lalalalalallala");
            IWebElement uploadFileTextBox = driver.FindElement(By.Name("upload_file"));
            uploadFileTextBox.Click();
            uploadFileTextBox.GetAttribute("Screenshot 2025-01-22 122527");


            IWebElement submitButton = driver.FindElement(By.CssSelector("['submit-button']"));
            submitButton.Click();
            IWebElement scrolUpButton = driver.FindElement(By.Id("scrollUp"));
            scrolUpButton.Click();

            IWebElement succsessMessage = driver.FindElement(By.Id("form-section"));
            Assert.IsTrue(succsessMessage.Displayed);

            IWebElement homeButton = driver.FindElement(By.ClassName("btn btn-success"));
            homeButton.Click();

        }
        [Test]
        public void verifyTestCasesPage()
        {
            driver.Navigate().GoToUrl("https://automationexercise.com/");
            IWebElement popUpButton = driver.FindElement(By.XPath("//*[text()='Consent']"));
            popUpButton.Click();

            IWebElement testCasesButton = driver.FindElement(By.XPath("//*[text()=' Test Cases']"));
            testCasesButton.Click();
            IWebElement testCasesMessage = driver.FindElement(By.XPath("//*[text()=' Test Cases']"));
            Assert.IsTrue(testCasesMessage.Displayed);

        }
        [Test]
        public void verifyAllProductsAndProductDetailPage()
        {
            driver.Navigate().GoToUrl("https://automationexercise.com/");

            IWebElement popUpButton = driver.FindElement(By.XPath("//button[@aria-label='Consent']"));
            popUpButton.Click();

            IWebElement productButton = driver.FindElement(By.XPath("//*[text()=' Products']"));
            productButton.Click();
            IWebElement allProduct = driver.FindElement(By.XPath("//*[text()='All Products']"));
            Assert.IsTrue(allProduct.Displayed);

            IWebElement vielProduct = driver.FindElement(By.XPath("//*[text()='View Product']"));
            vielProduct.Click();

            IWebElement productName = driver.FindElement(By.XPath("//*[text()='Blue Top']"));
            Assert.IsTrue(productName.Displayed);
            IWebElement productCategory = driver.FindElement(By.XPath("//*[text()='Category: Women > Tops']"));
            Assert.IsTrue(productCategory.Displayed);
            IWebElement productPrice = driver.FindElement(By.XPath("//*[text()='Rs. 500']"));
            Assert.IsTrue(productPrice.Displayed);
            IWebElement productQuantity = driver.FindElement(By.XPath("//*[text()='Quantity']"));
            Assert.IsTrue(productQuantity.Displayed);
            IWebElement productAvalability = driver.FindElement(By.XPath("//*[text()=' In Stock']"));
            Assert.IsTrue(productAvalability.Displayed);
            IWebElement productCondition = driver.FindElement(By.XPath("//*[text()='New']"));
            Assert.IsTrue(productCondition.Displayed);
            IWebElement productBrand = driver.FindElement(By.XPath("//*[text()=' Polo']"));
            Assert.IsTrue(productBrand.Displayed);
        }
        [Test]
        public void searchProduct()
        {
            driver.Navigate().GoToUrl("https://automationexercise.com/");

            IWebElement popUpButton = driver.FindElement(By.XPath("//button[@aria-label='Consent']"));
            popUpButton.Click();

            IWebElement productButton = driver.FindElement(By.XPath("//*[text()=' Products']"));
            productButton.Click();

            IWebElement searhProductButton = driver.FindElement(By.Id("search_product"));
            searhProductButton.Clear();
            searhProductButton.SendKeys("Blue Top");
            IWebElement submitSearch = driver.FindElement(By.Id("submit_search"));
            submitSearch.Click();
            IWebElement productIcon = driver.FindElement(By.ClassName("overlay-content"));
            Assert.IsTrue(productIcon.Displayed);

        }
        [Test]
        public void verifySubscriptionInHomePage()
        {
            driver.Navigate().GoToUrl("https://automationexercise.com/");

            IWebElement popUpButton = driver.FindElement(By.XPath("//button[@aria-label='Consent']"));
            popUpButton.Click();

            //IWebElement subscriptionel = driver.FindElement(By.XPath("//h2[@aria-label='Subscription']"));
            //Assert.IsTrue(subscriptionel.Displayed);
            IWebElement subscriptionEl = driver.FindElement(By.XPath("//*[text()='Subscription']"));
            Assert.IsTrue(subscriptionEl.Displayed);

            js.ExecuteScript("arguments[0].scrollIntoView(true);", subscriptionEl);

            IWebElement enterEmail = driver.FindElement(By.Id("susbscribe_email"));
            enterEmail.Clear();
            enterEmail.SendKeys("biba.nikolova12311@abv.bg");

            IWebElement subscribeButton = driver.FindElement(By.Id("subscribe"));
            subscribeButton.Click();



        }
        [Test]
        public void verifySubscriptioninCartPage()
        {
            driver.Navigate().GoToUrl("https://automationexercise.com/");

            IWebElement popUpButton = driver.FindElement(By.XPath("//button[@aria-label='Consent']"));
            popUpButton.Click();

            IWebElement cartButton = driver.FindElement(By.XPath("//*[text()=' Cart']"));
            cartButton.Click();

            IWebElement subscribeButton = driver.FindElement(By.Id("subscribe"));
            Assert.IsTrue(subscribeButton.Displayed);

            IWebElement enterEmail = driver.FindElement(By.Id("susbscribe_email"));
            enterEmail.Clear();
            enterEmail.SendKeys("biba.nikolova12311@abv.bg");

        }
        [Test]
        public void addProductsinCart()
        {

            driver.Navigate().GoToUrl("https://automationexercise.com/");

            IWebElement popUpButton = driver.FindElement(By.XPath("//button[@aria-label='Consent']"));
            popUpButton.Click();

            IWebElement productButton = driver.FindElement(By.XPath("//*[text()=' Products']"));
            productButton.Click();

            IWebElement addToCarButton = driver.FindElement(By.XPath("//a[@data-product-id='1']"));
            addToCarButton.Click();

            //IWebElement continionShippingButton = driver.FindElement(By.XPath("//*[text()=' Continue Shopping']"));
            IWebElement continionShippingButton = driver.FindElement(By.XPath("//*[text()='Continue Shopping']"));
            continionShippingButton.Click();

            IWebElement addToCarButton2 = driver.FindElement(By.XPath("//a[@data-product-id='2']"));
            addToCarButton2.Click();

            //IWebElement viewCartButton = driver.FindElement(By.XPath("//*[text()=' View Cart']"));
            //viewCartButton.Click();

            IWebElement viewCartButton = driver.FindElement(By.XPath("//*[contains(text(),'View Cart')]"));
            viewCartButton.Click();

          
        }

        [Test]
        public void verifyProductQuantityinCart()
        {
            driver.Navigate().GoToUrl("https://automationexercise.com/");

            IWebElement popUpButton = driver.FindElement(By.XPath("//button[@aria-label='Consent']")); 
            popUpButton.Click();

            IWebElement viewProductButton = driver.FindElement(By.XPath("//*[text()='View Product']"));
            viewProductButton.Click(); 
            driver.Navigate().GoToUrl("https://automationexercise.com/product_details/1");

            string currentUrl = driver.Url;
            Assert.AreEqual("https://automationexercise.com/product_details/1", currentUrl);
            IWebElement productQuantity1 = driver.FindElement(By.Id("quantity"));
            //IWebElement productQuantity1 = driver.FindElement(By.Id("Quantity"));
            productQuantity1.Click();
            productQuantity1.Click();
            productQuantity1.Click();
            productQuantity1.Click();
            IWebElement addToCartButton = driver.FindElement(By.XPath("//button[contains(normalize-space(text()), ' Add to cart')]"));
            //IWebElement addToCartButton = driver.FindElement(By.XPath("//button[text()='Add to cart']"));
            //IWebElement addToCartButton = driver.FindElement(By.XPath("//button[@class='btn btn-default cart' and text()='Add to cart']"));
            //IWebElement addToCartButton = driver.FindElement(By.XPath("//button[contains(text(), 'Add to cart')]"));
            //IWebElement addToCarButton = driver.FindElement(By.XPath("//button[contains(text(),' Add to cart ')]"));
            addToCartButton.Click();                                    

            IWebElement viewCartButton = driver.FindElement(By.XPath("//*[text()=' View Cart']"));
            viewCartButton.Click();

        }
        [Test]
        public void placeOrderRegisterBeforeCheckout()
        {
            driver.Navigate().GoToUrl("https://automationexercise.com/");
            IWebElement popUpButton = driver.FindElement(By.XPath("//button[@aria-label='Consent']"));     //Navigate to url 'http://automationexercise.com'
            popUpButton.Click();           
            //string currentUrl = driver.Url;
            //Assert.AreEqual("https://automationexercise.com/product_details/1", currentUrl);
            //IWebElement shoppingCart = driver.FindElement(By.XPath("//*[text()='Shopping Cart']"));
            //Assert.IsTrue(shoppingCart.Displayed);
            //IWebElement proceedToChecoutButton = driver.FindElement(By.XPath("//*[text()='Proceed To Checkout']"));
            //proceedToChecoutButton.Click();
            IWebElement registerLoginButton = driver.FindElement(By.XPath("//*[text()=' Signup / Login']"));  //4. Click 'Signup / Login' button
            registerLoginButton.Click();
            IWebElement nameTextBox = driver.FindElement(By.CssSelector("[data-qa='signup-name']"));
            nameTextBox.Clear();                                                                             //5.Fill all details in Signup and create account
            nameTextBox.SendKeys("Bilyanaa");
            IWebElement emailTextBox = driver.FindElement(By.CssSelector("[data-qa='signup-email']")); 
            emailTextBox.Clear();
            emailTextBox.SendKeys("biba.nikolova123388@abv.bg");
            IWebElement signupButton = driver.FindElement(By.CssSelector("[data-qa='signup-button']"));
            signupButton.Click();
            IWebElement genderButton = driver.FindElement(By.Id("id_gender2"));
            genderButton.Click();
            IWebElement nameTexBox = driver.FindElement(By.Id("name"));
            nameTexBox.Clear();
            nameTexBox.SendKeys("Bilyanaa");
            //IWebElement emailTextBov = driver.FindElement(By.Id("email"));
            //emailTextBov.Clear();
            //emailTextBov.SendKeys("biba.nikolova1233333@abv.bg");
            IWebElement passwordTextBox = driver.FindElement(By.Id("password"));
            passwordTextBox.Clear();
            passwordTextBox.SendKeys("999");

            SelectElement dropDays = new SelectElement(driver.FindElement(By.Id("days")));
            dropDays.SelectByValue("12");
            SelectElement dropMonth = new SelectElement(driver.FindElement(By.Id("months")));
            dropMonth.SelectByValue("7");
            SelectElement dropYear = new SelectElement(driver.FindElement(By.Id("years")));
            dropYear.SelectByValue("1996");

            IWebElement newsletterCheckbox = driver.FindElement(By.Id("newsletter"));

            wait.Until(ExpectedConditions.ElementToBeClickable(newsletterCheckbox));
            newsletterCheckbox.Click();

            IWebElement optinCheckbox = driver.FindElement(By.Id("optin"));
            optinCheckbox.Click();

            //IWebElement sinUpEmailTextBox = driver.FindElement(By.CssSelector("[data-qa='signup-email']"));
            //sinUpEmailTextBox.Clear();
            //sinUpEmailTextBox.SendKeys("biba.nikolova1233333@abv.bg");
            IWebElement firstNameTextBox = driver.FindElement(By.Id("first_name"));
            firstNameTextBox.Clear();
            firstNameTextBox.SendKeys("Bilyanaa");
            IWebElement lastNameTextBox = driver.FindElement(By.Id("last_name"));
            lastNameTextBox.Clear();
            lastNameTextBox.SendKeys("Nikolova");
            IWebElement companyTextBox = driver.FindElement(By.Id("company"));
            companyTextBox.Clear();
            companyTextBox.SendKeys("Vivacom");
            IWebElement addressNameTexBox = driver.FindElement(By.Id("address1"));
            addressNameTexBox.Clear();
            addressNameTexBox.SendKeys("Bdin14 Vidin");
            IWebElement address2NameTextBox = driver.FindElement(By.Id("address2"));
            address2NameTextBox.Clear();
            address2NameTextBox.SendKeys("-");

            SelectElement dropContry = new SelectElement(driver.FindElement(By.Id("country")));
            dropContry.SelectByValue("Australia");
            IWebElement stateTextBox = driver.FindElement(By.Id("state"));
            stateTextBox.Clear();
            stateTextBox.SendKeys("Vidin");
            IWebElement cityTextBox = driver.FindElement(By.Id("city"));
            cityTextBox.Clear();
            cityTextBox.SendKeys("Vidin");
            IWebElement zipCodeTextBox = driver.FindElement(By.Id("zipcode"));
            zipCodeTextBox.Clear();
            zipCodeTextBox.SendKeys("1700");
            IWebElement mobileNumberTextBox = driver.FindElement(By.Id("mobile_number"));
            mobileNumberTextBox.Clear();
            mobileNumberTextBox.SendKeys("0888034797");

            IWebElement createAccountButton = driver.FindElement(By.CssSelector("button[data-qa='create-account']"));
            createAccountButton.Click();                                                                                // Verify 'ACCOUNT CREATED!' and click 'Continue' button
            IWebElement acountCreaqted = driver.FindElement(By.XPath("//*[@data-qa='account-created']"));
            Assert.IsTrue(acountCreaqted.Displayed);
            IWebElement continueButton = driver.FindElement(By.XPath("//*[@data-qa='continue-button']"));
            continueButton.Click();
            IWebElement loggedAs = driver.FindElement(By.XPath("//a[contains(text(),'Logged in as')]"));  // Verify ' Logged in as username' at top
            Assert.IsTrue(loggedAs.Displayed);

            IWebElement producstButton = driver.FindElement(By.XPath("//*[text()=' Products']"));
            producstButton.Click();
            driver.Navigate().GoToUrl("https://automationexercise.com/product_details/1");
            //IWebElement addToCart = driver.FindElement(By.XPath("//button[contains(text(),' Add to cart')]"));
            //8. Add products to cart  
            //addToCart.Click();
            IWebElement addToCartButton = driver.FindElement(By.XPath("//button[@class='btn btn-default cart']"));
            addToCartButton.Click();
            IWebElement continueShoppingButton = driver.FindElement(By.XPath("//button[@class='btn btn-success close-modal btn-block' and text()='Continue Shopping']"));
        
            //IWebElement continueShoppingButton = driver.FindElement(By.XPath("//*[text()='Continue Shopping']"));
            //continueShoppingButton.Click();
            IWebElement cartButton = driver.FindElement(By.XPath("//*[text()=' Cart']"));
            cartButton.Click();                                                                                              //Click 'Cart' button


            //ontinueShopping.Click(); 
            IWebElement proceedToChecoutButton = driver.FindElement(By.XPath("//a[contains(text(),'Proceed To Checkout')]")); 
            proceedToChecoutButton.Click();                                                                           // Click Proceed To Checkout


            //IWebElement reviewYourOrder = driver.FindElement(By.XPath("//a[contains(text(),'Review Your Order')]"));
            IWebElement reviewYourOrder = driver.FindElement(By.XPath("//h2[contains(text(), 'Review Your Order')]"));
            Assert.IsTrue(reviewYourOrder.Displayed);                                                                         //12. Verify Address Details and Review Your Order
            IWebElement commentTextArea = driver.FindElement(By.ClassName("form-control"));             //13. Enter description in comment text area and click 'Place Order'
            commentTextArea.SendKeys("This is a test order");
            IWebElement placeOrder = driver.FindElement(By.XPath("//a[contains(text(),'Place Order')]"));
            placeOrder.Click();
            IWebElement nameOnKard = driver.FindElement(By.CssSelector("input[data-qa='name-on-card']")); //14. Enter payment details: Name on Card, Card Number, CVC, Expiration date
            nameOnKard.SendKeys("Bilyana Nikolova");
            IWebElement numOnKard = driver.FindElement(By.CssSelector("input[data-qa='card-number']")); //14. Enter payment details: Name on Card, Card Number, CVC, Expiration date
            numOnKard.SendKeys("0000 0000 00");
            IWebElement cvcNumber = driver.FindElement(By.CssSelector("input[data-qa='cvc']"));
            cvcNumber.SendKeys("166");
            IWebElement expiryMonth = driver.FindElement(By.CssSelector("input[data-qa='expiry-month']"));
            expiryMonth.SendKeys("0308");
            IWebElement expiryYear = driver.FindElement(By.CssSelector("input[data-qa='expiry-year']"));
            expiryYear.SendKeys("2027");
            IWebElement payButton = driver.FindElement(By.CssSelector("button[data-qa='pay-button']"));    //15. Click 'Pay and Confirm Order' button
            payButton.Click();
            IWebElement yourOrderHasBeenplacedSuccessfully = driver.FindElement(By.CssSelector("div.alert-success.alert"));
            IWebElement delAccount = driver.FindElement(By.XPath("//a[contains(text(),' Delete Account')]"));   //17. Click 'Delete Account' button
            delAccount.Click();
            IWebElement delMessage = driver.FindElement(By.XPath("//*[contains(text(),'Account Deleted!')]"));  //Verify 'ACCOUNT DELETED!' and click 'Continue' button
            Assert.IsTrue(delMessage.Displayed);

        }
        [Test]
        public void placeOrderRegisterWhileCheckout()
        {

            driver.Navigate().GoToUrl("https://automationexercise.com/");
            IWebElement popUpButton = driver.FindElement(By.XPath("//button[@aria-label='Consent']"));    
            popUpButton.Click();

            IWebElement producstButton = driver.FindElement(By.XPath("//*[text()=' Products']"));
            producstButton.Click();
            driver.Navigate().GoToUrl("https://automationexercise.com/product_details/1");
            IWebElement addToCartButton = driver.FindElement(By.XPath("//button[@class='btn btn-default cart']"));
            addToCartButton.Click();
            IWebElement continueShoppingButton = driver.FindElement(By.XPath("//button[@class='btn btn-success close-modal btn-block' and text()='Continue Shopping']"));

            IWebElement proceedToChecoutButton = driver.FindElement(By.XPath("//a[contains(text(),'Proceed To Checkout')]"));
            proceedToChecoutButton.Click();
            IWebElement registerLoginButton = driver.FindElement(By.XPath("//*[text()=' Signup / Login']"));
            registerLoginButton.Click();
            IWebElement nameTextBox = driver.FindElement(By.CssSelector("[data-qa='signup-name']"));
            nameTextBox.Clear();
            nameTextBox.SendKeys("Bilyanaa");
            IWebElement emailTextBox = driver.FindElement(By.CssSelector("[data-qa='signup-email']"));
            emailTextBox.Clear();
            emailTextBox.SendKeys("biba.nikolova123388@abv.bg");
            IWebElement signupButton = driver.FindElement(By.CssSelector("[data-qa='signup-button']"));
            signupButton.Click();
            IWebElement genderButton = driver.FindElement(By.Id("id_gender2"));
            genderButton.Click();
            IWebElement nameTexBox = driver.FindElement(By.Id("name"));
            nameTexBox.Clear();
            nameTexBox.SendKeys("Bilyanaa");
            //IWebElement emailTextBov = driver.FindElement(By.Id("email"));
            //emailTextBov.Clear();
            //emailTextBov.SendKeys("biba.nikolova1233333@abv.bg");
            IWebElement passwordTextBox = driver.FindElement(By.Id("password"));
            passwordTextBox.Clear();
            passwordTextBox.SendKeys("999");

            SelectElement dropDays = new SelectElement(driver.FindElement(By.Id("days")));
            dropDays.SelectByValue("12");
            SelectElement dropMonth = new SelectElement(driver.FindElement(By.Id("months")));
            dropMonth.SelectByValue("7");
            SelectElement dropYear = new SelectElement(driver.FindElement(By.Id("years")));
            dropYear.SelectByValue("1996");

            IWebElement newsletterCheckbox = driver.FindElement(By.Id("newsletter"));

            wait.Until(ExpectedConditions.ElementToBeClickable(newsletterCheckbox));
            newsletterCheckbox.Click();

            IWebElement optinCheckbox = driver.FindElement(By.Id("optin"));
            optinCheckbox.Click();
            //IWebElement sinUpEmailTextBox = driver.FindElement(By.CssSelector("[data-qa='signup-email']"));
            //sinUpEmailTextBox.Clear();
            //sinUpEmailTextBox.SendKeys("biba.nikolova1233333@abv.bg");
            IWebElement firstNameTextBox = driver.FindElement(By.Id("first_name"));
            firstNameTextBox.Clear();
            firstNameTextBox.SendKeys("Bilyanaa");
            IWebElement lastNameTextBox = driver.FindElement(By.Id("last_name"));
            lastNameTextBox.Clear();
            lastNameTextBox.SendKeys("Nikolova");
            IWebElement companyTextBox = driver.FindElement(By.Id("company"));
            companyTextBox.Clear();
            companyTextBox.SendKeys("Vivacom");
            IWebElement addressNameTexBox = driver.FindElement(By.Id("address1"));
            addressNameTexBox.Clear();
            addressNameTexBox.SendKeys("Bdin14 Vidin");
            IWebElement address2NameTextBox = driver.FindElement(By.Id("address2"));
            address2NameTextBox.Clear();
            address2NameTextBox.SendKeys("-");

            SelectElement dropContry = new SelectElement(driver.FindElement(By.Id("country")));
            dropContry.SelectByValue("Australia");
            IWebElement stateTextBox = driver.FindElement(By.Id("state"));
            stateTextBox.Clear();
            stateTextBox.Clear();
            stateTextBox.SendKeys("Vidin");
            IWebElement cityTextBox = driver.FindElement(By.Id("city"));
            cityTextBox.Clear();
            cityTextBox.SendKeys("Vidin");
            IWebElement zipCodeTextBox = driver.FindElement(By.Id("zipcode"));
            zipCodeTextBox.Clear();
            zipCodeTextBox.SendKeys("1700");
            IWebElement mobileNumberTextBox = driver.FindElement(By.Id("mobile_number"));
            mobileNumberTextBox.Clear();
            mobileNumberTextBox.SendKeys("0888034797");

            IWebElement createAccountButton = driver.FindElement(By.CssSelector("button[data-qa='create-account']"));
            createAccountButton.Click();
            IWebElement acountCreaqted = driver.FindElement(By.XPath("//*[@data-qa='account-created']"));
            Assert.IsTrue(acountCreaqted.Displayed);
            IWebElement continueButton = driver.FindElement(By.XPath("//*[@data-qa='continue-button']"));
            continueButton.Click();
            IWebElement loggedAs = driver.FindElement(By.XPath("//a[contains(text(),'Logged in as')]"));
            Assert.IsTrue(loggedAs.Displayed);
            IWebElement cartButton = driver.FindElement(By.XPath("//*[text()=' Cart']"));
            cartButton.Click();
            IWebElement proceedToChecoutButton2 = driver.FindElement(By.XPath("//a[contains(text(),'Proceed To Checkout')]"));
            proceedToChecoutButton2.Click();
            IWebElement commentTextArea = driver.FindElement(By.ClassName("form-control"));             
            commentTextArea.SendKeys("This is a test order");
            IWebElement placeOrder = driver.FindElement(By.XPath("//a[contains(text(),'Place Order')]"));
            placeOrder.Click();
            IWebElement nameOnKard = driver.FindElement(By.CssSelector("input[data-qa='name-on-card']")); 
            IWebElement numOnKard = driver.FindElement(By.CssSelector("input[data-qa='card-number']")); 
            numOnKard.SendKeys("0000 0000 00");
            IWebElement cvcNumber = driver.FindElement(By.CssSelector("input[data-qa='cvc']"));
            cvcNumber.SendKeys("166");
            IWebElement expiryMonth = driver.FindElement(By.CssSelector("input[data-qa='expiry-month']"));
            expiryMonth.SendKeys("0308");
            IWebElement expiryYear = driver.FindElement(By.CssSelector("input[data-qa='expiry-year']"));
            expiryYear.SendKeys("2027");
            IWebElement payButton = driver.FindElement(By.CssSelector("button[data-qa='pay-button']"));    
            payButton.Click();
            IWebElement yourOrderHasBeenplacedSuccessfully = driver.FindElement(By.CssSelector("div.alert-success.alert"));
            IWebElement delAccount = driver.FindElement(By.XPath("//a[contains(text(),' Delete Account')]"));  
            delAccount.Click();
            IWebElement delMessage = driver.FindElement(By.XPath("//*[contains(text(),'Account Deleted!')]")); 
            Assert.IsTrue(delMessage.Displayed);

        }
        [Test]
        public void removeProductsFromCart()
        {
            driver.Navigate().GoToUrl("https://automationexercise.com/");
            IWebElement popUpButton = driver.FindElement(By.XPath("//button[@aria-label='Consent']"));
            popUpButton.Click();
            IWebElement addToCartButton = driver.FindElement(By.XPath("//a[@data-product-id='1' and contains(@class, 'add-to-cart')]"));
            addToCartButton.Click();
            IWebElement viewCartButton = driver.FindElement(By.XPath("//*[text()='View Cart']"));  
            viewCartButton.Click();
            IWebElement xElement = driver.FindElement(By.XPath("//a[@class='cart_quantity_delete' and @data-product-id='1']"));
            xElement.Click();

        }
        [Test]
        public void viewCategoryProducts()
        {
            driver.Navigate().GoToUrl("https://automationexercise.com/");
            IWebElement popUpButton = driver.FindElement(By.XPath("//button[@aria-label='Consent']"));
            popUpButton.Click();

            IWebElement womenCategoruButton = driver.FindElement(By.XPath("//i[@class='fa fa-plus']"));
            womenCategoruButton.Click();
            IWebElement dressCategory = driver.FindElement(By.XPath("//a[contains(text(), 'Dress')]"));
            //IWebElement dressCategory = driver.FindElement(By.CssSelector("a[href='/category_products/1']:contains('Dress')"));
            dressCategory.Click();
            IWebElement text = driver.FindElement(By.XPath("//*[text()='Women - Dress Products']"));
            Assert.IsTrue(text.Displayed);
            IWebElement mensCategoruButton = driver.FindElement(By.XPath("//a[@data-toggle='collapse' and @data-parent='#accordian' and @href='#Men']"));
            //IWebElement mensCategoruButton = driver.FindElement(By.XPath("//i[@class='fa fa-plus']"));
            mensCategoruButton.Click();
            IWebElement tshirts = driver.FindElement(By.XPath("//a[contains(text(), 'Tshirts')]"));
            tshirts.Click();
            //IWebElement tshirts = driver.FindElement(By.XPath("//a[@href='/category_products/3' and text()='Tshirts']"));
            Assert.IsTrue(tshirts.Displayed);

        }












    }










}











