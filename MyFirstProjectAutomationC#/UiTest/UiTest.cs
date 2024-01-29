using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.AccessControl;
using OpenQA.Selenium.Support.UI;
using static System.Runtime.InteropServices.JavaScript.JSType;
using String = System.String;
using Boolean = System.Boolean;
using System.Reflection.Metadata;
namespace MyFirstProjectAutomationC_.UiTest
{
    internal class UiTest
    {
        private const string baseUrl = "https://nationalize.io/";
        private IWebDriver driver;
        [SetUp]
        public void SetUp()
        {
            // Set up the WebDriver
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }
        //עובד צריכה לוודא שנכנסתי
        [Test]
        public async Task ValidateLogin()
        {
            //Navigate to the login page
            driver.Navigate().GoToUrl(baseUrl);
            IWebElement loginButton = driver.FindElement(By.LinkText("Log in"));
            //loginButton
            loginButton.Click();
            //email
            IWebElement EmailButton = driver.FindElement(By.Id("user_email"));
            EmailButton.SendKeys("Admin@gmail.com");
            //password
            IWebElement user_password = driver.FindElement(By.Id("user_password"));
             user_password.SendKeys("Asdasd1!");
            //Signing in
            IWebElement SigningIn = driver.FindElement(By.XPath("//*[@id=\"login_form\"]/div/div[2]/button"));
            SigningIn.Click();
           //לוודא שנכנסתי 
            
        }
       //לא עובד...
        [Test]
        public async Task LoginWithoutPassword()
        {
            // Navigate to the login page
            driver.Navigate().GoToUrl(baseUrl);
            //loginButton
            IWebElement loginButton = driver.FindElement(By.LinkText("Log in"));
            loginButton.Click();
            //email
            IWebElement EmailButton = driver.FindElement(By.Id("user_email"));
            EmailButton.SendKeys("Admin@gmail.com");
            //Signing in
            IWebElement SigningIn = driver.FindElement(By.XPath("//*[@id=\"login_form\"]/div/div[2]/button"));
            SigningIn.Click();
            //verify 
            IWebElement errorMessage = driver.FindElement(By.XPath("//*[@id=\"login_form\"]/div/div[1]"));
            bool isErrorMessageDisplayed = errorMessage.Displayed;
            if (isErrorMessageDisplayed == false)
                return;
        }
       //עובד, אני צריכה לוודא שהתחברתי,
        [Test]
        public async Task SignUp()
        {
            // Navigate to the login page
            driver.Navigate().GoToUrl(baseUrl);
            //loginButton
            IWebElement loginButton = driver.FindElement(By.LinkText("Log in"));
            loginButton.Click();
            //Sign Up button
            IWebElement signUpButton = driver.FindElement(By.LinkText("Sign up"));
            signUpButton.Click();
            //email
            IWebElement EmailButton = driver.FindElement(By.Id("user_email"));
            EmailButton.SendKeys("Admin@gmail.com");
            //password
            IWebElement password = driver.FindElement(By.Id("user_password"));
            password.SendKeys("Asdasd1!");
            //password
            IWebElement passwordConfirmation = driver.FindElement(By.Id("user_password_confirmation"));
            passwordConfirmation.SendKeys("Asdasd1!");
            //Signing in
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));
            IWebElement SigningIn = driver.FindElement(By.XPath("//*[@id=\"registration_form\"]/div/button"));
            SigningIn.Click();
            //לוודא שנכנסתי
        }
        //עובד
        [Test]
        public void ChangeColor()
        {
            // Navigate to the login page
            driver.Navigate().GoToUrl(baseUrl);
            //loginButton
            IWebElement ChangeButton = driver.FindElement(By.LinkText("nationalize.io"));
            ChangeButton.Click();
            //OrengeColor
            IWebElement ChangeOrengeColor = driver.FindElement(By.LinkText("genderize.io"));
            ChangeOrengeColor.Click();
            //check if the Button - genderize.io is orenge 
            String headerColor = driver.FindElement(By.XPath("//*[@id=\"genderize\"]/header/nav/div[1]/a")).GetCssValue("background-color");
            String color = "rgba(250, 130, 49, 1)";
            Assert.That(headerColor, Is.EqualTo(color), "the color isnt Orenge");
            //check if the Button - Docs is orenge 
            String headerColorDocs = driver.FindElement(By.XPath("//*[@id=\"genderize\"]/header/nav/div[2]/a[1]")).GetCssValue("background-color");
            String color2 = "rgba(250, 130, 49, 0.063)";
            Assert.That(headerColorDocs, Is.EqualTo(color2), "the color isnt Orenge");
        }  
       //לא מעלה את הקובץ
        [Test]
        public async Task UploadFile()
        {
            //Tool לא הצלחתי לזהות את 
            driver.Navigate().GoToUrl("https://nationalize.io/tools/csv");
                IWebElement CSVToolButton = driver.FindElement(By.XPath("//*[@id=\"upload-form\"]/div/div/div[1]/label"));
            CSVToolButton.Click();
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "annual-enterprise-survey-2021-financial-year-provisional-csv");

            if (File.Exists(filePath))
            {
                IWebElement fileInput = driver.FindElement(By.Id("input-file"));
                fileInput.SendKeys(filePath);
            }
            else
            {
                Console.WriteLine($"File not found: {filePath}");
            }

            //Assert.IsTrue(driver.Url.Contains("dashboard"), "Login failed");
        }
    }
}