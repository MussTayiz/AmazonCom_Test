using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace Log4Net.Pages
{
    class FillEmailPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public FillEmailPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='ap_email']")]
        private IWebElement emailTextBox;

        [FindsBy(How = How.XPath, Using = "//span[@id='continue']//input[@id='continue']")]
        private IWebElement continueButton;

        public FillPasswordAndLoginPage FillEmailAndContinue(string emailText)
        {
            /*
            continueButton = wait.Until<IWebElement>((d) => {
                try
                {
                    IWebElement element = d.FindElement(By.XPath("//span[@id='continue']//input[@id='continue']"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });
            */
            emailTextBox.SendKeys(emailText);
            continueButton.Click();
            return new FillPasswordAndLoginPage(driver);
        }


    }
}
