using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace Log4Net.Pages
{
    class RegisterPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public RegisterPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='ap_customer_name']")]
        private IWebElement CustomerNameTextBox;

        [FindsBy(How = How.XPath, Using = "//input[@id='ap_email']")]
        private IWebElement EmailTextBox;

        [FindsBy(How = How.XPath, Using = "//input[@id='ap_password']")]
        private IWebElement PasswordTextBox;

        [FindsBy(How = How.XPath, Using = "//input[@id='ap_password_check']")]
        private IWebElement PasswordConfirmTextBox;

        [FindsBy(How = How.XPath, Using = "//input[@id='continue']")]
        private IWebElement CreateAccountButton;

        [FindsBy(How = How.XPath, Using = "//a[@id='createAccountSubmit']")]
        private IWebElement CreateAccountButton2;

        public void CreateAnAccount(string[] DataOfTheForm)
        {
            CreateAccountButton = wait.Until<IWebElement>((d) => {
                try
                {
                    IWebElement element = d.FindElement(By.XPath("//input[@id='continue']"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });

            if (CreateAccountButton.Displayed)
            {
                CustomerNameTextBox.SendKeys(DataOfTheForm[0]);
                EmailTextBox.SendKeys(DataOfTheForm[1]);
                PasswordTextBox.SendKeys(DataOfTheForm[2]);
                PasswordConfirmTextBox.SendKeys(DataOfTheForm[2]);
                //CreateAccountButton.Click();
            }
            else if (CreateAccountButton2.Displayed)
            {
                CustomerNameTextBox.SendKeys(DataOfTheForm[0]);
                EmailTextBox.SendKeys(DataOfTheForm[1]);
                PasswordTextBox.SendKeys(DataOfTheForm[2]);
                PasswordConfirmTextBox.SendKeys(DataOfTheForm[2]);
                //CreateAccountButton2.Click();
            }


        }
    }
}
