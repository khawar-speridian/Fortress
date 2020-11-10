using FortressAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FortressAutomation.pages
{
    class LoginPage:TestBase
    {
        //OR - Object Repository
        [FindsBy(How = How.Id, Using = "userNameInput")]
        private IWebElement userName;

        [FindsBy(How = How.Id, Using = "passwordInput")]
        private IWebElement password;

        [FindsBy(How = How.Id, Using = "submissionArea")]
        private IWebElement signinBtn;

        public LoginPage()
        {
            PageFactory.InitElements(driver, this);
        }
        public TaskBoardPage LoginToApplication(string usrname, string paswd) {
            
            userName.SendKeys(usrname);
            password.SendKeys(paswd);
            signinBtn.Submit();
            
            string expectedTtile = "Sign In";
            string actualTitle = driver.Title;
            Assert.AreNotEqual(expectedTtile, actualTitle, "Application login is not working");
            return new TaskBoardPage();
        }
    }
}
