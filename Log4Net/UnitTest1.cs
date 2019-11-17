using NUnit.Framework;
using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Log4Net.Pages;

using log4net;
using log4net.Config;
using System.Threading;

namespace Log4Net
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver chromeDriver;
        private HomePage homePage;
        private RegisterPage registerPage;
        private FillEmailPage fillEmailPage;
        private FillPasswordAndLoginPage fillPasswordAndLogin;
        private CreateListPage createListPage;
        private YourListPage yourListPage;

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        [SetUp]
        public void SetupForChrome()
        {
            chromeDriver = new ChromeDriver();
            chromeDriver.Manage().Window.Maximize();
        }

        [Test, Order(1)]
        public void GoToMainPage()
        {
            homePage = new HomePage(chromeDriver);
            homePage.GoToMainPage();
            log.Info("Success");
        }

        [Test]
        [Ignore("For Register")]
        public void GoToRegisterPage()
        {
            homePage = new HomePage(chromeDriver);
            homePage.GoToMainPage();
            registerPage = homePage.GoToRegisterPage();
        }

        //Register 
        string[] DataOfTheForm = { "TestName", "abc@gmail.com", "test123456#" };


        [Test]
        [Ignore("DataForm dizisini güncellemek gerekli")]
        public void CreateAnAccount()
        {
            homePage = new HomePage(chromeDriver);
            homePage.GoToMainPage();

            registerPage = homePage.GoToRegisterPage();
            registerPage.CreateAnAccount(DataOfTheForm);
        }

        string[] LoginData = { "abc@gmail.com", "test123456#" };

        [Test, Order(3)]
        public void Login()
        {
            homePage = new HomePage(chromeDriver);
            homePage.GoToMainPage();

            fillEmailPage = homePage.GoToFillEmailPage();
            fillPasswordAndLogin = fillEmailPage.FillEmailAndContinue(LoginData[0]);
            createListPage = fillPasswordAndLogin.FillPasswordAndLogin(LoginData[1]);

            // Login Control
            string helloAndName = createListPage.IsLoginSuccess();
            log.Info("Login is Success");
            Assert.AreEqual(helloAndName, "Hello, Mustafa");
            Assert.Pass(helloAndName);

        }

        [Test, Order(4)]
        public void ClickCreateAList()
        {
            homePage = new HomePage(chromeDriver);
            homePage.GoToMainPage();

            fillEmailPage = homePage.GoToFillEmailPage();
            fillPasswordAndLogin = fillEmailPage.FillEmailAndContinue(LoginData[0]);
            createListPage = fillPasswordAndLogin.FillPasswordAndLogin(LoginData[1]);
            yourListPage = createListPage.GoToCreateListPageAndCreateList();

            int[] counts = yourListPage.CreateShoppingListControl();

            // İlk alınan liste sayısı ve yeni shopping list eklendikten sonraki sayı
            log.Info("New shopping list created.");
            Assert.Greater(counts[1], counts[0]);

        }

        [Test, Order(5)]
        public void GoToAccountPageAndMyLists()
        {
            homePage = new HomePage(chromeDriver);
            homePage.GoToMainPage();

            fillEmailPage = homePage.GoToFillEmailPage();
            fillPasswordAndLogin = fillEmailPage.FillEmailAndContinue(LoginData[0]);
            fillPasswordAndLogin.FillPasswordAndLogin(LoginData[1]);


            yourListPage = new YourListPage(chromeDriver);
            yourListPage.GoToMyList();
        }

        [Test, Order(6)]
        public void ShopingListAddIdeaList()
        {
            homePage = new HomePage(chromeDriver);
            homePage.GoToMainPage();

            fillEmailPage = homePage.GoToFillEmailPage();
            fillPasswordAndLogin = fillEmailPage.FillEmailAndContinue(LoginData[0]);
            fillPasswordAndLogin.FillPasswordAndLogin(LoginData[1]);

            yourListPage = new YourListPage(chromeDriver);
            yourListPage.GoToMyList();

            int[] ideaListCounts = yourListPage.ShopingListAddIdeaList("iPhone 8");

            Assert.Less(ideaListCounts[0], ideaListCounts[1]);
            log.Info("New Idea list added.");
        }

        [Test, Order(7)]
        public void MoveToWishList()
        {
            homePage = new HomePage(chromeDriver);
            homePage.GoToMainPage();

            fillEmailPage = homePage.GoToFillEmailPage();
            fillPasswordAndLogin = fillEmailPage.FillEmailAndContinue(LoginData[0]);
            fillPasswordAndLogin.FillPasswordAndLogin(LoginData[1]);


            yourListPage = new YourListPage(chromeDriver);
            yourListPage.GoToMyList();
            string successMessage = yourListPage.AddToWishList();

            Assert.AreEqual(successMessage, "Moved to");
            log.Info("Idea list has been moved to wish list.");

        }

        [Test, Order(8)]
        public void DeleteItemFromWishList()
        {
            homePage = new HomePage(chromeDriver);
            homePage.GoToMainPage();

            fillEmailPage = homePage.GoToFillEmailPage();
            fillPasswordAndLogin = fillEmailPage.FillEmailAndContinue(LoginData[0]);
            fillPasswordAndLogin.FillPasswordAndLogin(LoginData[1]);


            yourListPage = new YourListPage(chromeDriver);
            yourListPage.GoToMyList();
            string deletedMessage = yourListPage.DeleteToWishList();

            Assert.AreEqual(deletedMessage, "Deleted");
            log.Info("Idea list deleted from wish list.");

        }

        private string ASINValue;
        [Test, Order(9)]
        public void ReturnShopingListAndClickTopSearchResult()
        {
            homePage = new HomePage(chromeDriver);
            homePage.GoToMainPage();

            fillEmailPage = homePage.GoToFillEmailPage();
            fillPasswordAndLogin = fillEmailPage.FillEmailAndContinue(LoginData[0]);
            fillPasswordAndLogin.FillPasswordAndLogin(LoginData[1]);


            yourListPage = new YourListPage(chromeDriver);
            yourListPage.GoToMyList();
            ASINValue = yourListPage.ClickTopSearchButtonAndAssert7Result();

            log.Info("ASIN value was obtained to find the product directly in the search.");
            Assert.Pass("Arama için ürün id : " + ASINValue.Trim());
            
        }

        [TearDown]
        public void FinishTests()
        {
            Thread.Sleep(100);
            chromeDriver.Close();
        }

    }
}