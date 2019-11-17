using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace Log4Net.Pages
{
    class CreateListPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public CreateListPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//a[@id='nav-link-accountList']/span[@class='nav-line-1']")]
        private IWebElement helloNameText;

        public string IsLoginSuccess()
        {
            helloNameText = wait.Until<IWebElement>((d) => {
                try
                {
                    IWebElement element = d.FindElement(By.XPath("//a[@id='nav-link-accountList']/span[@class='nav-line-1']"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });
            string temp = helloNameText.GetAttribute("innerHTML");
            return temp;
        }

        //
        [FindsBy(How = How.XPath, Using = "//span[@id='WLNEW_create']//input[@class='a-button-input a-declarative']")]
        private IWebElement createListButton;

        [FindsBy(How = How.XPath, Using = "//input[@id='WLNEW_list_name']")]
        private IWebElement listNameTextBox;

        [FindsBy(How = How.XPath, Using = "//span[@id='WLNEW_privacy_public']//input[@class='a-button-input a-declarative']")]
        private IWebElement publicButton;

        [FindsBy(How = How.XPath, Using = "//a[@id='nav-hamburger-menu']/i[@class='hm-icon nav-sprite']")]
        private IWebElement menuButton;

        [FindsBy(How = How.XPath, Using = "/html//div[@id='hmenu-customer-name']")]
        private IWebElement yourAccountButton;

        [FindsBy(How = How.CssSelector, Using = @"[href='https\:\/\/www\.amazon\.com\/wishlist\?ref_\=ya_d_l_lists']")]
        private IWebElement listsLink;

        [FindsBy(How = How.XPath, Using = "//div[@id='wishlist-page']/ul//span[@class='a-declarative']")]
        private IWebElement createListLink;

        [FindsBy(How = How.XPath, Using = "/html//div[@id='your-lists-nav']")]
        private IList<IWebElement> shoppingListUL;

        public YourListPage GoToCreateListPageAndCreateList()
        {
            menuButton = wait.Until<IWebElement>((d) => {

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

            menuButton.Click();

            //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            //js.ExecuteScript("document.querySelector('div#hmenu-content > .hmenu.hmenu-visible').scrollTop=1300");

            yourAccountButton = wait.Until<IWebElement>((d) => {

                try
                {
                    IWebElement element = d.FindElement(By.XPath("/html//div[@id='hmenu-customer-name']"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });

            yourAccountButton.Click();

            listsLink = wait.Until<IWebElement>((d) => {

                try
                {
                    IWebElement element = d.FindElement(By.CssSelector(@"[href='https\:\/\/www\.amazon\.com\/wishlist\?ref_\=ya_d_l_lists']"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });

            listsLink.Click();

            //Kac tane liste var al tek eleman olarak aldıgı icin split yaptım

            ArrayList firstListCount = shoppingListCount();
            string[] divide1 = firstListCount[0].ToString().Split();

            createListLink = wait.Until<IWebElement>((d => {
                try
                {
                    IWebElement element1 = d.FindElement(By.XPath("//div[@id='wishlist-page']/ul//span[@class='a-declarative']"));
                    if (element1.Displayed)
                        return element1;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            }));

            createListLink.Click();

            createListButton = wait.Until<IWebElement>((d => {
                try
                {
                    IWebElement element1 = d.FindElement(By.XPath("//span[@id='WLNEW_create']//input[@class='a-button-input a-declarative']"));
                    if (element1.Displayed)
                        return element1;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            }));

            listNameTextBox.Clear();
            listNameTextBox.SendKeys("Test Shoping List");
            publicButton.Click();
            createListButton.Click();

            Thread.Sleep(1000);
            ArrayList secondListCount = shoppingListCount();
            string[] divide2 = secondListCount[0].ToString().Split();

            //NUnit.Framework.Assert.Pass(divide1.Length + " : <-> :" + divide2.Length);
            //Bide burda kaç tane liste var al int olarak yourlistpage'e at

            return new YourListPage(driver, divide1.Length, divide2.Length);
        }

        public ArrayList shoppingListCount()
        {
            shoppingListUL = wait.Until<IList<IWebElement>>((d) => {
                try
                {
                    IList<IWebElement> list = d.FindElements(By.XPath("/html//div[@id='your-lists-nav']"));
                    if (list.IsReadOnly)
                        return list;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;

            });

            ArrayList items = new ArrayList();
            foreach (IWebElement item in shoppingListUL)
            {
                items.Add(item.Text);
                Console.WriteLine(item.Text);
            }
            return items;
        }


    }
}
