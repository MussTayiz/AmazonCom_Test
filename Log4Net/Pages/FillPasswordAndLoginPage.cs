using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace Log4Net.Pages
{
    class FillPasswordAndLoginPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public FillPasswordAndLoginPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }



        [FindsBy(How = How.XPath, Using = "/html//input[@id='ap_password']")]
        private IWebElement passwordTextBox;

        [FindsBy(How = How.XPath, Using = "/html//input[@id='signInSubmit']")]
        private IWebElement loginButton;

        public CreateListPage FillPasswordAndLogin(string passwordText)
        {

            loginButton = wait.Until<IWebElement>((d) => {
                try
                {
                    IWebElement element = d.FindElement(By.XPath("/html//input[@id='signInSubmit']"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });

            passwordTextBox.SendKeys(passwordText);
            loginButton.Click();
            return new CreateListPage(driver);
        }

    }
}
