using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace Log4Net.Pages
{
    class HomePage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        public void GoToMainPage()
        {
            driver.Navigate().GoToUrl("https://www.amazon.com/");
        }

        [FindsBy(How = How.XPath, Using = "//a[@id='nav-hamburger-menu']/i[@class='hm-icon nav-sprite']")]
        private IWebElement menuButtonTest;

        [FindsBy(How = How.CssSelector, Using = "[data-menu-id] li:nth-of-type(39) div")]
        private IWebElement scrollTest;
        // silinecek
        public void GoToMyAccount_Test()
        {
            driver.Navigate().GoToUrl("https://www.amazon.com/");

            menuButtonTest = wait.Until<IWebElement>((d) => {

                try
                {
                    IWebElement element = d.FindElement(By.XPath("//a[@id='nav-hamburger-menu']/i[@class='hm-icon nav-sprite']"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });

            menuButtonTest.Click();

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.querySelector('div#hmenu-content > .hmenu.hmenu-visible').scrollTop=1500");

            scrollTest = wait.Until<IWebElement>((d) => {

                try
                {
                    IWebElement element = d.FindElement(By.CssSelector("[data-menu-id] li:nth-of-type(39) div"));
                    if (element.Enabled)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });
            scrollTest.Click();




        }

        [FindsBy(How = How.XPath, Using = "//a[@id='nav-link-accountList']/span[2]/span")]
        private IWebElement openLoginMenu;

        [FindsBy(How = How.XPath, Using = "//*[contains(text(), 'New customer? ')]/a")]
        private IWebElement startHereLink;

        public RegisterPage GoToRegisterPage()
        {
            Actions builder = new Actions(driver);
            builder.MoveToElement(openLoginMenu).Click().Build().Perform();

            startHereLink = wait.Until<IWebElement>((d) => {
                try
                {
                    IWebElement element = d.FindElement(By.XPath("//*[contains(text(), 'New customer? ')]/a"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });

            startHereLink.Click();
            return new RegisterPage(driver);
        }


        [FindsBy(How = How.XPath, Using = "//a[@id='nav-hamburger-menu']/i[@class='hm-icon nav-sprite']")]
        private IWebElement menuButton;

        [FindsBy(How = How.XPath, Using = "/html//div[@id='hmenu-customer-name']")]
        private IWebElement signInButton;

        public FillEmailPage GoToFillEmailPage()
        {
            menuButton.Click();

            signInButton = wait.Until<IWebElement>((d) => {
                try
                {
                    IWebElement element = d.FindElement(By.XPath("/html//div[@id='hmenu-customer-name']"));
                    if (element.Displayed)
                        return element;
                }
                catch (ElementNotVisibleException) { }
                catch (StaleElementReferenceException) { }
                return null;
            });

            signInButton.Click();
            return new FillEmailPage(driver);
        }



    }


}
