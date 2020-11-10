using FortressAutomation.pages;
using FortressAutomation.Pages;
using NUnit.Framework;
using System.Configuration;

namespace FortressAutomation.TestCases
{
    [TestFixture]
    class LoginPageTest:TestBase
    {
        LoginPage loginPage;
        TaskBoardPage taskBoardpg;
        
        [SetUp]
        public void SetUp()
        {
            initialization();
            loginPage = new LoginPage();
        }    
        [Test]
        public void LoginToApplicationTest()
        {
            taskBoardpg = loginPage.LoginToApplication(Global_TestBase_Configuration.AppSettings.Settings["username"].Value, Global_TestBase_Configuration.AppSettings.Settings["password"].Value);
            ngWebDriver.WaitForAngular();
        }
        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}
