using FortressAutomation.pages;
using FortressAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace FortressAutomation.TestCases
{
    [TestFixture]
    class TaskBoardPageTest:TestBase
    {
        #pragma warning disable CS0219 // Variable is assigned but its value is never used
        LoginPage loginPage;
        TaskBoardPage taskBoardkpg;
        int totalRecords = 0;

        [OneTimeSetUp]
        public void SetUp()
        {
            initialization();
            loginPage = new LoginPage();
            taskBoardkpg = loginPage.LoginToApplication(Global_TestBase_Configuration.AppSettings.Settings["username"].Value, Global_TestBase_Configuration.AppSettings.Settings["password"].Value);
            ngWebDriver.WaitForAngular();
        }
        [Test, Order(0)]
        public void Get_TaskBoardPage_Title_Toverify_Default_Loading_Test()
        {
            //arrange
            string expectedTtile = "TaskBoard - Fortress 2.0";
            //act
            string actualTitle = taskBoardkpg.Get_TaskBoardPage_Title_Toverify_Default_Loading();
            //assert
            Assert.AreEqual(expectedTtile, actualTitle, "Taskboard page title is not matching with actual title.");
        }
        [Test, Order(1)]
        public void Apply_AllCounties_Filter_Test()
        {
            int count = taskBoardkpg.Apply_AllCounties_Filter();
            Console.WriteLine("Total Counties " + count);
        }
        [Test, Order(2)]
        public void Apply_FCL_SaleType_Filter_Test()
        {
            taskBoardkpg.Apply_FCL_SaleType_Filter();
            driver.Navigate().Refresh();
        }
        [Test, Order(3)]
        public void Apply_Sheriff_SaleType_Filter_Test() 
        {
            taskBoardkpg.Apply_Sheriff_SaleType_Filter();
            driver.Navigate().Refresh();
        }
        [Test, Order(4)]
        public void Tabs_Verification_AfterApplying_FCLSaleType_Filter_Test()
        {
            //arrange
            int tabs_should_be_shown = 7;
            //act
            int actual_tabs_showing = taskBoardkpg.Tabs_Verification_AfterApplying_FCLSaleType_Filter();
            //assert
            Assert.AreEqual(tabs_should_be_shown, actual_tabs_showing, "On FCL selection required tabs not are shown");
            driver.Navigate().Refresh();
        }
        [Test, Order(5)]
        public void NonForeclosureSaleTypeFilterTest()
        {
            int count = taskBoardkpg.NonForeclosureSaleTypeFilter();
            Console.WriteLine("Total Non-FCL Records: " + count);
            driver.Navigate().Refresh();
        }
        [Test, Order(6)]
        public void REOSaleTypeFilterTest()
        {
            int count = taskBoardkpg.REOSaleTypeFilter();
            Console.WriteLine("Total REO Filter Records: " + count);
            driver.Navigate().Refresh();
        }
        [Test, Order(7)]
        public void CivicSaleTypeFilterTest()
        {
            int count = taskBoardkpg.CivicSaleTypeFilter();
            Console.WriteLine("Total Civic Filter Records: " + count);
            driver.Navigate().Refresh();
        }
        [Test, Order(8)]
        public void TabsForNonFCLSaleTypeTest()
        {
            //arrange
            int tabs_should_be_shown = 8;
            //act
            int actual_tabs_showing = taskBoardkpg.TabsForNonFCLSaleType();
            //assert
            Assert.AreEqual(tabs_should_be_shown, actual_tabs_showing, "On Non-FCL selection, required tabs not are shown");
            driver.Navigate().Refresh();
        }
        [Test, Order(9)]
        public void OpenMarketSaleTypeFilterSaleTypeFilterTest()
        {
            int count = taskBoardkpg.OpenMarketSaleTypeFilter();
            Console.WriteLine("Total Open Markete Filter Records: " + count);
            driver.Navigate().Refresh();
        }
        [Test, Order(10)]
        public void Apply_MLS_SaleType_Filter_Test()
        {
            int records_shown_in_saleType_column = taskBoardkpg.Apply_MLS_SaleType_Filter();
            Console.WriteLine("Total MLS Filter Records: " + records_shown_in_saleType_column);
            driver.Navigate().Refresh();
        }
        [Test, Order(11)]
        public void TabsForOpenMarketSaleTypeTest()
        {
            // arrange
            int tabs_should_be_shown = 6;
            //act
            int actual_tabs_showing = taskBoardkpg.TabsForOpenMarketSaleType();
            //assert
            Assert.AreEqual(tabs_should_be_shown, actual_tabs_showing, "On Open Market selection, required tabs not are shown");
            driver.Navigate().Refresh();
        }
        [Test, Order(12)]
        public void Apply_AllChannels_Filter_Test()
        {
            int count = taskBoardkpg.Apply_AllChannels_Filter();
            Console.WriteLine("Total Channels: " + count);
        }
        
        [Test, Order(13)]
        public void AllTime_FilterSum_ShouldBe_EqualTo_SumOf_OtherTime_Filters_Test()
        {
            //act
            List<int> list = taskBoardkpg.AllTime_FilterSum_ShouldBe_EqualTo_SumOf_OtherTime_Filters();
            int allTimeCounter = list.ElementAt(0);
            int otherTimeCounterSum = list.ElementAt(1);
            //assert
            Assert.AreNotEqual(allTimeCounter, otherTimeCounterSum, "Time filters not are working fine ");
        }
        [Test, Order(14)]
        public void Change_Property_Status_To_Bid_Test()
        {
            //arrange
            string expectedMeesage = "×\r\nSelected trustee sale records have been updated successfully";
            //act
            string successMessage = taskBoardkpg.Change_Property_Status_To_Bid();
            //assert
            Assert.AreEqual(expectedMeesage,successMessage, "Status is not changed to bid successfully");
        }
        [Test, Order(15)]
        public void MoveRecord_FromqcTab_ToBidTab_Test()
        {
            //act
            List<int> counter_of_records_in_bid_tab = taskBoardkpg.MoveRecord_FromqcTab_ToBidTab();
            int counter_of_records_in_bid_tab_before_element_is_moved = counter_of_records_in_bid_tab.ElementAt(0);
            int counter_of_records_in_bid_tab_after_element_is_moved = counter_of_records_in_bid_tab.ElementAt(1);
            //assert
            Assert.AreNotEqual(counter_of_records_in_bid_tab_before_element_is_moved, counter_of_records_in_bid_tab_after_element_is_moved, "Record is not moved to bid tab");
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            //driver.Close();
        }
    }
}
