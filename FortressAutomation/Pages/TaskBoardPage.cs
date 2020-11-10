using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Protractor;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace FortressAutomation.Pages
{
    class TaskBoardPage:TestBase
    {
        int totalRecords = 0;

        //OR - Object Repository
        [FindsBy(How = How.XPath, Using = "//ul[@id='status-panel']//li[@class='ng-scope active']")]
        private IWebElement inPlayTab;

        [FindsBy(How = How.XPath, Using = "//*[@id='status-panel']/li[2]")]
        private IWebElement qcTab;

        [FindsBy(How = How.XPath, Using = "//*[@id='status-panel']/li[3]")]
        private IWebElement reviewTab;

        [FindsBy(How = How.XPath, Using = "//*[@id='status-panel']/li[4]")]
        private IWebElement bidTab;

        [FindsBy(How = How.XPath, Using = "//*[@id='status-panel']/li[5]")]
        private IWebElement biddingTab;

        [FindsBy(How = How.XPath, Using = "//*[@id='status-panel']/li[6]")]
        private IWebElement pendingTab;

        [FindsBy(How = How.XPath, Using = "//*[@id='status-panel']/li[7]")]
        private IWebElement escrowTab;

        [FindsBy(How = How.XPath, Using = "//*[@id='status-panel']/li[8]")]
        private IWebElement passTab;

        [FindsBy(How = How.XPath, Using = "//*[@id='status-panel']/li[9]")]
        private IWebElement otherTab;

        [FindsBy(How = How.XPath, Using = "//*[@id='status-panel']/li[10]")]
        private IWebElement resultTab;

        [FindsBy(How = How.XPath, Using = "//span[text()='All Counties']")]
        private IWebElement countiesFilter;

        [FindsBy(How = How.XPath, Using = "//table[@class='k-selectable']//tbody//tr[@class='ng-scope' or @class='k-alt ng-scope']//td//span[@ng-bind='dataItem.PrimaryPropertySummaryData.CountyName']//parent::td")]
        private IList<IWebElement> getCellsOfCountyColumn;

        [FindsBy(How = How.XPath, Using = "//*[@id='grid-filter-top']/div[1]/div[1]/ft-channel-dropdown/div")]
        private IWebElement channelFiltersDropDown;

        [FindsBy(How = How.XPath, Using = "//*[@id='grid-filter-top']/div[1]/div[1]/ft-channel-dropdown/div/div/ul/li[1]/p")]
        private IWebElement allChannelFilter;

        [FindsBy(How = How.XPath, Using = "//table[@class='k-selectable']//tbody//tr[@class='ng-scope' or @class='k-alt ng-scope']//td//span[@ng-bind='dataItem.TrusteeSaleData.ChannelName']//parent::td")]
        private IList<IWebElement> getCellsOfChannelColumn;

        [FindsBy(How = How.XPath, Using = "//*[@id='grid-filter-top']/div[1]/div[3]/sale-type-picker/div")]
        private IWebElement saleTypeFiltersDropDown;

        [FindsBy(How = How.XPath, Using = "//table[@class='k-selectable']//tbody//tr[@class='ng-scope' or @class='k-alt ng-scope' ]//td//p[@class='text-black' or @class='text-purple' or @class='text-green' or @class='text-blue']//parent::td")]
        private IList<IWebElement> getCellsOfSaleTypeColumn;
        
        [FindsBy(How = How.XPath, Using = "//*[@id='grid-filter-top']/div[1]/div[3]/sale-type-picker/div/div/ul/li[1]/p")]
        private IWebElement foreClosure;

        [FindsBy(How = How.XPath, Using = "//*[@id='grid-filter-top']/div[1]/div[3]/sale-type-picker/div/div/ul/li[1]/ul[2]/li/p")]
        private IWebElement sheriffFilter;

        [FindsBy(How = How.XPath, Using = "//*[@id='grid-filter-top']/div[1]/div[3]/sale-type-picker/div/div/ul/li[2]/p")]
        private IWebElement nonForeClosure;

        [FindsBy(How = How.XPath, Using = "//*[@id='grid-filter-top']/div[1]/div[3]/sale-type-picker/div/div/ul/li[2]/ul[1]/li/p")]
        private IWebElement reoFilter;

        [FindsBy(How = How.XPath, Using = "//*[@id='grid-filter-top']/div[1]/div[3]/sale-type-picker/div/div/ul/li[2]/ul[4]/li/p")]
        private IWebElement civicFilter;

        [FindsBy(How = How.XPath, Using = "//*[@id='grid-filter-top']/div[1]/div[3]/sale-type-picker/div/div/ul/li[3]/p")]
        private IWebElement openMarket;

        [FindsBy(How = How.XPath, Using = "//*[@id='grid-filter-top']/div[1]/div[3]/sale-type-picker/div/div/ul/li[3]/ul[1]/li/p")]
        private IWebElement mlsFilter;

        [FindsBy(How = How.XPath, Using = "//span[text()='Assignments']")]
        private IWebElement assignmentsdDropdown;

        [FindsBy(How = How.XPath, Using = "//table[@class='k-selectable']//tbody//tr[@class='ng-scope' or @class='k-alt ng-scope']//td//span[@ng-bind='dataItem.PrimaryPropertySummaryData.AssignedToName']//parent::td")]
        private IList<IWebElement> getCellsOfAssignmentColumn;

        [FindsBy(How = How.XPath, Using = "//*[@id='sale-time-panel']/a[1]")]
        private IWebElement allTimeFilter;

        [FindsBy(How = How.XPath, Using = "//div[@id='sale-time-panel']//a")]
        private IList<IWebElement> otherTimeFilters;

        [FindsBy(How = How.XPath, Using = "//table[@class='k-selectable']//tbody//tr[@class='ng-scope' or @class='k-alt ng-scope']//td[text()='QC']//preceding-sibling::td//input")]
        private IList<IWebElement> checkBoxClicking;

        [FindsBy(How = How.XPath, Using = "//table[@class='k-selectable']//tbody//tr[@class='ng-scope' or @class='k-alt ng-scope']//td[text()='QC']")]
        private IList<IWebElement> changeStatusToBid;

        [FindsBy(How = How.XPath, Using = "//*[@id='taskBoardCtrl']/notifications-center")]
        private IWebElement successMessage;

        [FindsBy(How = How.XPath, Using = "//div[@class='side-bar padding-right--none col-sm-1']//a[text()='Bid']")]
        private IWebElement bidButton;

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[1]/div/table/thead/tr/th[3]//span")]
        private IWebElement statusColumn;

        [FindsBy(How = How.XPath, Using = "//div[@class='k-column-menu k-popup k-group k-reset k-state-border-up']//ul[@class='k-widget k-reset k-header k-menu k-menu-vertical']")]
        private IWebElement hoverOnColumnsLink;

        [FindsBy(How = How.XPath, Using = "//ul[@class='k-group k-menu-group k-popup k-reset k-state-border-left']")]
        private IWebElement hoverOnColumnsList;

        [FindsBy(How = How.XPath, Using = "//div[@class='k-column-menu k-popup k-group k-reset k-state-border-up']//ul[@class='k-group k-menu-group k-popup k-reset k-state-border-left']//li[@class='k-item k-state-default k-first']//span//input")]
        private IWebElement addCheckBoxColumnToGrid;

        [FindsBy(How = How.XPath, Using = "//table[@class='k-selectable']//tbody//tr[@data-uid='73a1772e-86a1-4779-a58e-2600aa82be32']//td")]
        private IList<IWebElement> getCountOfFirstRowCellsInGrid;


        




        public TaskBoardPage()
        {
            PageFactory.InitElements(driver, this);
        }
        public string Get_TaskBoardPage_Title_Toverify_Default_Loading()
        {
            string title = driver.Title;
            return title;
        }
        public int Apply_AllCounties_Filter()
        {
            if (countiesFilter.Text == "All Counties")
            {
                totalRecords = TestUtil.HighlightColumn(getCellsOfCountyColumn, getCellsOfCountyColumn.Count);
            }
            return totalRecords;
        }
        public int Apply_AllChannels_Filter()
        {
            ngWebDriver.WaitForAngular();
            string filterName = channelFiltersDropDown.Text;
            if (filterName == "All Channels")
            {
                totalRecords = TestUtil.HighlightColumn(getCellsOfChannelColumn, getCellsOfChannelColumn.Count);
            }
            else
            {
                channelFiltersDropDown.Click();
                //Click on All Channel filter in list
                allChannelFilter.Click();
                //Click on footer to close the list
                driver.FindElement(By.XPath("//div[@id='grid']/div[@class='k-pager-wrap k-grid-pager k-widget']//span[text()=' © 2020  Wedgewood. All rights reserved.']")).Click();
                ngWebDriver.WaitForAngular();
                filterName = channelFiltersDropDown.Text;
                if (filterName == "All Channels")
                {
                    totalRecords = TestUtil.HighlightColumn(getCellsOfChannelColumn, getCellsOfChannelColumn.Count);
                }
            }
            return totalRecords;
        }
        public void Apply_FCL_SaleType_Filter()
        {
            saleTypeFiltersDropDown.Click();
            nonForeClosure.Click();
            openMarket.Click();
            //Click on footer to close the list
            driver.FindElement(By.XPath("//div[@id='grid']/div[@class='k-pager-wrap k-grid-pager k-widget']//span[text()=' © 2020  Wedgewood. All rights reserved.']")).Click();
            ngWebDriver.WaitForAngular();
            int totalRecords = TestUtil.HighlightColumn(getCellsOfSaleTypeColumn, getCellsOfSaleTypeColumn.Count);
        }
        public void Apply_Sheriff_SaleType_Filter()
        {
            ngWebDriver.WaitForAngular();
            saleTypeFiltersDropDown.Click();
            nonForeClosure.Click();
            openMarket.Click();
            foreClosure.Click();
            ngWebDriver.WaitForAngular();
            sheriffFilter.Click();
            ngWebDriver.WaitForAngular();
            //Click on footer to close the list
            driver.FindElement(By.XPath("//div[@id='grid']/div[@class='k-pager-wrap k-grid-pager k-widget']//span[text()=' © 2020  Wedgewood. All rights reserved.']")).Click();
            ngWebDriver.WaitForAngular();
            totalRecords = TestUtil.HighlightColumn(getCellsOfSaleTypeColumn, getCellsOfSaleTypeColumn.Count);
        }
        public int Tabs_Verification_AfterApplying_FCLSaleType_Filter()
        {
            ngWebDriver.WaitForAngular();
            saleTypeFiltersDropDown.Click();
            Thread.Sleep(1000);
            nonForeClosure.Click();
            Thread.Sleep(1000);
            openMarket.Click();
            //Click anywhere to close the list
            driver.FindElement(By.XPath("//div[@id='grid']/div[@class='k-pager-wrap k-grid-pager k-widget']//span[text()=' © 2020  Wedgewood. All rights reserved.']")).Click();
            ngWebDriver.WaitForAngular();
            int count = SideMenuTabsAfterClickingOnSaleTypeFilter();
            return count;
        }
        public int NonForeclosureSaleTypeFilter()
        {
            ngWebDriver.WaitForAngular();
            saleTypeFiltersDropDown.Click();
            foreClosure.Click();
            openMarket.Click();
            driver.FindElement(By.XPath("//div[@id='grid']/div[@class='k-pager-wrap k-grid-pager k-widget']//span[text()=' © 2020  Wedgewood. All rights reserved.']")).Click();
            ngWebDriver.WaitForAngular();
            totalRecords = TestUtil.HighlightColumn(getCellsOfSaleTypeColumn, getCellsOfSaleTypeColumn.Count);
            ngWebDriver.WaitForAngular();
            return totalRecords;
        }
        public int REOSaleTypeFilter()
        {
            ngWebDriver.WaitForAngular();
            saleTypeFiltersDropDown.Click();
            foreClosure.Click();
            nonForeClosure.Click();
            openMarket.Click();
            ngWebDriver.WaitForAngular();
            reoFilter.Click();
            driver.FindElement(By.XPath("//div[@id='grid']/div[@class='k-pager-wrap k-grid-pager k-widget']//span[text()=' © 2020  Wedgewood. All rights reserved.']")).Click();
            ngWebDriver.WaitForAngular();
            totalRecords = TestUtil.HighlightColumn(getCellsOfSaleTypeColumn, getCellsOfSaleTypeColumn.Count);
            ngWebDriver.WaitForAngular();
            return totalRecords;
        }
        public int CivicSaleTypeFilter()
        {
            ngWebDriver.WaitForAngular();
            saleTypeFiltersDropDown.Click();
            foreClosure.Click();
            nonForeClosure.Click();
            openMarket.Click();
            ngWebDriver.WaitForAngular();
            civicFilter.Click();
            driver.FindElement(By.XPath("//div[@id='grid']/div[@class='k-pager-wrap k-grid-pager k-widget']//span[text()=' © 2020  Wedgewood. All rights reserved.']")).Click();
            ngWebDriver.WaitForAngular();
            totalRecords = TestUtil.HighlightColumn(getCellsOfSaleTypeColumn, getCellsOfSaleTypeColumn.Count);
            ngWebDriver.WaitForAngular();
            return totalRecords;
        }
        public int TabsForNonFCLSaleType()
        {
            ngWebDriver.WaitForAngular();
            saleTypeFiltersDropDown.Click();
            Thread.Sleep(1000);
            foreClosure.Click();
            Thread.Sleep(1000);
            openMarket.Click();
            //Click anywhere to close the list
            driver.FindElement(By.XPath("//div[@id='grid']/div[@class='k-pager-wrap k-grid-pager k-widget']//span[text()=' © 2020  Wedgewood. All rights reserved.']")).Click();
            ngWebDriver.WaitForAngular();
            int count = SideMenuTabsAfterClickingOnSaleTypeFilter();
            return count;
        }
        public int OpenMarketSaleTypeFilter()
        {
            ngWebDriver.WaitForAngular();
            saleTypeFiltersDropDown.Click();
            foreClosure.Click();
            ngWebDriver.WaitForAngular();
            nonForeClosure.Click();
            openMarket.Click();
            ngWebDriver.WaitForAngular();
            openMarket.Click();
            ngWebDriver.WaitForAngular();
            //Click anywhere to close the list
            driver.FindElement(By.XPath("//div[@id='grid']/div[@class='k-pager-wrap k-grid-pager k-widget']//span[text()=' © 2020  Wedgewood. All rights reserved.']")).Click();
            ngWebDriver.WaitForAngular();
            int totalRecords = TestUtil.HighlightColumn(getCellsOfSaleTypeColumn, getCellsOfSaleTypeColumn.Count);
            return totalRecords;
        }
        public int Apply_MLS_SaleType_Filter()
        {
            ngWebDriver.WaitForAngular();
            saleTypeFiltersDropDown.Click();
            foreClosure.Click();
            ngWebDriver.WaitForAngular();
            nonForeClosure.Click();
            ngWebDriver.WaitForAngular();
            openMarket.Click();
            ngWebDriver.WaitForAngular();
            mlsFilter.Click();
            ngWebDriver.WaitForAngular();
            //Click anywhere to close the list
            driver.FindElement(By.XPath("//div[@id='grid']/div[@class='k-pager-wrap k-grid-pager k-widget']//span[text()=' © 2020  Wedgewood. All rights reserved.']")).Click();
            ngWebDriver.WaitForAngular();
            int totalRecords = TestUtil.HighlightColumn(getCellsOfSaleTypeColumn, getCellsOfSaleTypeColumn.Count);
            return totalRecords;
        }
        public int TabsForOpenMarketSaleType()
        {
            ngWebDriver.WaitForAngular();
            saleTypeFiltersDropDown.Click();
            foreClosure.Click();
            nonForeClosure.Click();
            //Click anywhere to close the list
            driver.FindElement(By.XPath("//div[@id='grid']/div[@class='k-pager-wrap k-grid-pager k-widget']//span[text()=' © 2020  Wedgewood. All rights reserved.']")).Click();
            ngWebDriver.WaitForAngular();
            int count = SideMenuTabsAfterClickingOnSaleTypeFilter();
            return count;
        }
        public int SideMenuTabsAfterClickingOnSaleTypeFilter()
        {
            string readLine = null;
            string tabName = null;
            List<string> list = new List<string>();
            IList<IWebElement> sideMenuTabs = driver.FindElements(By.XPath("//div[@id='tabNormal']//ul//li//a"));
            for (int i = 0; i < sideMenuTabs.Count; i++)
            {
                //Console.WriteLine(sideMenuTabs.Count);
                if (sideMenuTabs.ElementAt(i).Displayed && sideMenuTabs.ElementAt(i).Enabled)
                {
                    tabName = sideMenuTabs.ElementAt(i).Text;
                    using (StringReader reader = new StringReader(tabName))
                    {
                        readLine = reader.ReadLine();
                        list.Add(readLine);
                    }
                }
            }
            return list.Count;
        }
        public List<int> AllTime_FilterSum_ShouldBe_EqualTo_SumOf_OtherTime_Filters()
        {
            ngWebDriver.WaitForAngular();
            string allTimeFilterText = allTimeFilter.Text;
            allTimeFilterText = allTimeFilterText.Substring(allTimeFilterText.IndexOf("("));
            allTimeFilterText = allTimeFilterText.Replace("(", "");
            allTimeFilterText = allTimeFilterText.Replace(")", "");
            int allTimeCount = Convert.ToInt32(allTimeFilterText);
            List<int> list = new List<int>();
            list.Add(allTimeCount);
            int otherTimeCount = 0;
            int totalCount = 0;
            for (int i = 1; i < otherTimeFilters.Count; i++)
            {
                IWebElement timeFilterEelement = otherTimeFilters.ElementAt(i);
                string timeFilterText = timeFilterEelement.Text;
                timeFilterText = timeFilterText.Substring(timeFilterText.IndexOf("(")+1);
                timeFilterText = timeFilterText.Replace("(", "");
                timeFilterText = timeFilterText.Replace(")", "");
                timeFilterText = timeFilterText.Trim();
                bool success = Int32.TryParse(timeFilterText, out otherTimeCount);
                if (success)
                {
                    totalCount = totalCount + otherTimeCount;
                }
            }
            list.Add(totalCount);
            return list;
        }
        public List<int> MoveRecord_FromqcTab_ToBidTab()
        {
            ngWebDriver.WaitForAngular();
            List<int> list = new List<int>();
            saleTypeFiltersDropDown.Click();
            nonForeClosure.Click();
            openMarket.Click();
            ngWebDriver.WaitForAngular();
            //Click anywhere to close the list
            driver.FindElement(By.XPath("//div[@id='grid']/div[@class='k-pager-wrap k-grid-pager k-widget']//span[text()=' © 2020  Wedgewood. All rights reserved.']")).Click();
            ngWebDriver.WaitForAngular();
            qcTab.Click();
            ngWebDriver.WaitForAngular();
            string bidTabText = bidTab.Text;
            int count = TestUtil.GetTextOfSideBarTabs(bidTabText);
            list.Add(count);
            IWebElement checkBox = checkBoxClicking.ElementAt(0);
            checkBox.Click();
            bidButton.Click();
            ngWebDriver.WaitForAngular();
            bidTabText = bidTab.Text;
            int newCount = TestUtil.GetTextOfSideBarTabs(bidTabText);
            list.Add(newCount);
            return list;
        }
        public string Change_Property_Status_To_Bid()
        {
            //ngWebDriver.WaitForAngular();
            saleTypeFiltersDropDown.Click();
            nonForeClosure.Click();
            openMarket.Click();
            //Click footer to close the list
            driver.FindElement(By.XPath("//div[@id='grid']/div[@class='k-pager-wrap k-grid-pager k-widget']//span[text()=' © 2020  Wedgewood. All rights reserved.']")).Click();
            //ngWebDriver.WaitForAngular();
            Thread.Sleep(2000);
            IWebElement checkBox = checkBoxClicking.ElementAt(0);
            checkBox.Click();
            bidButton.Click();
            Thread.Sleep(2000);
            string message = successMessage.Text;
            return message;
        }
    }
}
