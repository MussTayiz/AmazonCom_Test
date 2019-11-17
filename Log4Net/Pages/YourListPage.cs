using System;
using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Log4Net.Pages
{
    class YourListPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        private int shoppingListCount1;
        private int shoppingListCount2;

        public YourListPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        public YourListPage(IWebDriver driver, int shoppingListCount1, int shoppingListCount2)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);

            this.shoppingListCount1 = shoppingListCount1;
            this.shoppingListCount2 = shoppingListCount2;

        }

        public int[] CreateShoppingListControl()
        {
            int[] temp = new int[2];
            temp[0] = shoppingListCount1;
            temp[1] = shoppingListCount2;
            return temp;
        }

        [FindsBy(How = How.XPath, Using = "//a[@id='nav-hamburger-menu']/i[@class='hm-icon nav-sprite']")]
        private IWebElement menuButton;

        //Menu Altında
        [FindsBy(How = How.XPath, Using = "/html//div[@id='hmenu-customer-name']")]
        private IWebElement yourAccountButton;

        [FindsBy(How = How.CssSelector, Using = @"[href='https\:\/\/www\.amazon\.com\/wishlist\?ref_\=ya_d_l_lists']")]
        private IWebElement listsLink;

        [FindsBy(How = How.CssSelector, Using = "//*[contains(text(),'Test Shoping List')]")]
        private IWebElement testShopingList;

        public void GoToMyList()
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
                catch (ElementNotVisibleException) { }
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

            testShopingList = wait.Until<IWebElement>((d) => {

                try
                {
                    IWebElement element = d.FindElement(By.XPath("//*[contains(text(),'Test Shoping List')]"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });

            testShopingList.Click();


        }

        [FindsBy(How = How.XPath, Using = "//div[@id='wfaLink']//a[@href='#']")]
        private IWebElement addIdeaLink;

        [FindsBy(How = How.XPath, Using = "/html//span[@id='wfa-note-add-button-wrapper-announce']")]
        private IWebElement addToListButton;

        [FindsBy(How = How.XPath, Using = "/html//input[@id='wfaTextInput']")]
        private IWebElement ideaTextBox;


        [FindsBy(How = How.XPath, Using = "//div[@id='item-page-wrapper']//li//div[@class='a-row a-size-small']/h3")]
        private IList<IWebElement> ideaListCountElement;

        // Shopimg list için fikir ekle, istek listesine taşı, ve sil
        public int[] ShopingListAddIdeaList(string ideaNameText)
        {

            // sayıyı al
            int firstIdeaListCount = ideaListCount().Count;

            addIdeaLink = wait.Until<IWebElement>((d) => {

                try
                {
                    IWebElement element = d.FindElement(By.XPath("//div[@id='wfaLink']//a[@href='#']"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });

            addIdeaLink.Click();

            addToListButton = wait.Until<IWebElement>((d) => {

                try
                {
                    IWebElement element = d.FindElement(By.XPath("/html//span[@id='wfa-note-add-button-wrapper-announce']"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });

            ideaTextBox.SendKeys("Amazon Kindle");
            ideaTextBox.SendKeys(Keys.Enter);
            ideaTextBox.Clear();

            ideaTextBox.SendKeys(ideaNameText);
            ideaTextBox.SendKeys(Keys.Enter);

            // Eklemeden Sonra İdeaList Sayısı
            Thread.Sleep(1000);
            int secondIdeaListCount = ideaListCount().Count;

            int[] temp = new int[2];
            temp[0] = firstIdeaListCount;
            temp[1] = secondIdeaListCount;

            return temp;
        }

        public ArrayList ideaListCount()
        {
            ideaListCountElement = wait.Until<IList<IWebElement>>((d) => {
                try
                {
                    IList<IWebElement> list = d.FindElements(By.XPath("/html//div[@id='item-page-wrapper']//li//div[@class='a-row a-size-small']/h3"));
                    if (list.IsReadOnly)
                        return list;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;

            });

            ArrayList items = new ArrayList();
            foreach (IWebElement item in ideaListCountElement)
            {
                items.Add(item.Text);
                Console.WriteLine(item.Text);
            }

            return items;
        }



        [FindsBy(How = How.XPath, Using = "//div[@class='a-column a-span6']/span[@class='a-dropdown-container']/span[@class='a-button a-button-dropdown a-button-small g-move-button']//span[@role='button']")]
        private IWebElement moveSelectBox;

        [FindsBy(How = How.XPath, Using = "//div[@id='a-popover-3']//ul/li[3]/a[@href='javascript:void(0)']/span[@class='a-size-base a-text-ellipsis']")]
        private IWebElement wishListItem;

        [FindsBy(How = How.XPath, Using = "/html//ul[@id='g-items']/li[1]//div[@class='a-section']//span")]
        private IWebElement movedToWishListMessage;


        public string AddToWishList()
        {
            moveSelectBox = wait.Until<IWebElement>((d) => {

                try
                {
                    IWebElement element = d.FindElement(By.XPath("//div[@class='a-column a-span6']/span[@class='a-dropdown-container']/span[@class='a-button a-button-dropdown a-button-small g-move-button']//span[@role='button']"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });

            moveSelectBox.Click();

            wishListItem = wait.Until<IWebElement>((d) => {

                try
                {
                    IWebElement element = d.FindElement(By.XPath("//div[@id='a-popover-3']//ul/li[3]/a[@href='javascript:void(0)']/span[@class='a-size-base a-text-ellipsis']"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });
            wishListItem.Click();

            movedToWishListMessage = wait.Until<IWebElement>((d) => {

                try
                {
                    IWebElement element = d.FindElement(By.XPath("/html//ul[@id='g-items']/li[1]//div[@class='a-section']//span"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });
            // 
            return movedToWishListMessage.GetAttribute("innerHTML").Substring(0, 8);
        }

        [FindsBy(How = How.XPath, Using = "//div[@id='your-lists-nav']/nav/ul//a[@href='/hz/wishlist/ls/2JY7AOXNI81O']//span[@class='a-color-base']")]
        private IWebElement wishListButton;

        [FindsBy(How = How.CssSelector, Using = "[data-action='reg-item-delete'] .a-button-small")]
        private IWebElement deleteButton;

        [FindsBy(How = How.XPath, Using = "//ul[@id='g-items']/li[@class='a-spacing-none g-item-sortable']//div[@class='a-section']/div[2]/div/div/div")]
        private IWebElement deletedItemFromWishListMessage;


        public string DeleteToWishList()
        {

            wishListButton = wait.Until<IWebElement>((d) => {

                try
                {
                    IWebElement element = d.FindElement(By.XPath("//div[@id='your-lists-nav']/nav/ul//a[@href='/hz/wishlist/ls/2JY7AOXNI81O']//span[@class='a-color-base']"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });

            wishListButton.Click();
            deleteButton.Click();

            deletedItemFromWishListMessage = wait.Until<IWebElement>((d) => {

                try
                {
                    IWebElement element = d.FindElement(By.XPath("//ul[@id='g-items']/li[@class='a-spacing-none g-item-sortable']//div[@class='a-section']/div[2]/div/div/div"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });
            //Assert.Pass(deletedItemFromWishListMessage.GetAttribute("innerHTML"));
            return deletedItemFromWishListMessage.GetAttribute("innerHTML");

        }

        [FindsBy(How = How.CssSelector, Using = "#g-items .g-item-sortable:nth-of-type(1) [data-] [role]")]
        private IWebElement topSearchButton;

        [FindsBy(How = How.CssSelector, Using = ".a-pagination > li:nth-of-type(4) > a")]
        private IWebElement goPage3Button;

        [FindsBy(How = How.XPath, Using = "[data-component-id='23'] .s-image-fixed-height")]
        private IWebElement seventhItemInThirdPage;

        [FindsBy(How = How.XPath, Using = "//*[contains(text(),'ASIN')]/../td")]
        private IWebElement seventhItemASINValue;


        public string ClickTopSearchButtonAndAssert7Result()
        {
            topSearchButton = wait.Until<IWebElement>((d) => {

                try
                {
                    IWebElement element = d.FindElement(By.CssSelector("#g-items .g-item-sortable:nth-of-type(1) [data-] [role]"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });

            topSearchButton.Click();

            // 3.Sayfa butonu için scrollHeight
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            goPage3Button = wait.Until<IWebElement>((d) => {

                try
                {
                    IWebElement element = d.FindElement(By.CssSelector(".a-pagination > li:nth-of-type(4) > a"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });
            goPage3Button.Click();

            seventhItemInThirdPage = wait.Until<IWebElement>((d) => {
                try
                {
                    IWebElement element = d.FindElement(By.CssSelector("[data-component-id='23'] .s-image-fixed-height"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });

            seventhItemInThirdPage.Click();

            //js.ExecuteScript("window.scrollTo(0, document.body.scrollTop = 3500)");

            seventhItemASINValue = wait.Until<IWebElement>((d) => {
                try
                {
                    IWebElement element = d.FindElement(By.XPath("//*[contains(text(),'ASIN')]/../td"));
                    if (element.Displayed)
                        return element;
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });

            //Assert.Pass(seventhItemASINValue.GetAttribute("innerHTML"));

            return seventhItemASINValue.GetAttribute("innerHTML");

        }

    }
}
