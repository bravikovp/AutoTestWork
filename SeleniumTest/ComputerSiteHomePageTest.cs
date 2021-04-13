using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTest
{
    public class ComputerSiteHomePageTest
    {
        public ChromeDriver driver;
        
        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver();
        }
        
        private By emailInputLocator = By.Name("email");
        private By sendButtonLocator = By.Id("sendMe");
        private By formErrorLocator = By.ClassName("form-error");                                                     
        private By girlRadioLocator = By.Id("girl");                                                        
        private By boyRadioLocator = By.Id("boy");                                                        
        private By resultTextLocator = By.ClassName("result-text");

        private string correctEmail = "pavelb@mail.ru";
        private string incorrectEmail = "pavelb";
        private string url = "https://qa-course.kontur.host/selenium-practice/";

        [Test]
        public void ComputerSiteHomePage_ClickSendButtonWithEmptyEmailInput_ShowFormError()          
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(sendButtonLocator).Click();

            Assert.AreEqual("Введите email", driver.FindElement(formErrorLocator).Text);       
        }

        [Test]
        public void ComputerSiteHomePage_ClickSendButtonWithIncorrectEmailInput_ShowFormError()                
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(emailInputLocator).SendKeys(incorrectEmail);
            driver.FindElement(sendButtonLocator).Click();

            Assert.AreEqual("Некорректный email", driver.FindElement(formErrorLocator).Text);            
        }

        [Test]
        public void ComputerSiteHomePage_SendCorrectEmailAndEnableBoyRadio_ShowResultTextForBoy()               
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(boyRadioLocator).Click();
            driver.FindElement(emailInputLocator).SendKeys(correctEmail);
            driver.FindElement(sendButtonLocator).Click();

            Assert.AreEqual("Хорошо, мы пришлём имя для вашего мальчика на e-mail:", driver.FindElement(resultTextLocator).Text);       
        }

        [Test]
        public void ComputerSiteHomePage_InputCorrectEmailAndEnableGirlRadio_EmailInputNotChange()                 
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(emailInputLocator).SendKeys(correctEmail);
            driver.FindElement(girlRadioLocator).Click();                                  

            Assert.AreEqual(correctEmail, driver.FindElement(emailInputLocator).GetAttribute("value"));       
        }

        [Test]
        public void ComputerSiteHomePage_SendCorrectEmailAndEnableGirlRadio_ShowResultTextForGirl()              
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(girlRadioLocator).Click();
            driver.FindElement(emailInputLocator).SendKeys(correctEmail);
            driver.FindElement(sendButtonLocator).Click();

            Assert.AreEqual("Хорошо, мы пришлём имя для вашей девочки на e-mail:", driver.FindElement(resultTextLocator).Text);          
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
