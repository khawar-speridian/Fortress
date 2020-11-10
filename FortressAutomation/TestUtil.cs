using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ExpectedConditions = OpenQA.Selenium.Support.UI.ExpectedConditions;

namespace FortressAutomation
{
    class TestUtil:TestBase
    {
        public static int PAGE_LOAD_TIME = 0;
        public static int IMPLICIT_WAIT = 20;

        public static void HoverOverElementAndClick(IWebElement element)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
            Thread.Sleep(2000);
            element.Click();
        }
        public static IWebElement WaitUntilElementClickable(IWebElement element, int timeout = 30)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementToBeClickable(element));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + element + "' was not found in current context page.");
                throw;
            }
        }
        public static int HighlightColumn(IList<IWebElement> elementsList, int counter)
        {
            int totalRecordsFound = 0;
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            if (counter != 0)
            {
                foreach (IWebElement getOneCell in elementsList)
                {
                        //Console.WriteLine(getOneCell.Text);
                        totalRecordsFound = totalRecordsFound + 1;
                        js.ExecuteScript("arguments[0].setAttribute('style', 'border: 3px dotted red;'); ", getOneCell);
                }
            }
            return totalRecordsFound;

        }
        public static int GetTextOfSideBarTabs(string tabName)
        {
            string readLine = null;
            int count = 0;
            using (StringReader reader = new StringReader(tabName))
            {
                for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    readLine = line;
                }
                readLine = readLine.Replace("(", "");
                readLine = readLine.Replace(")", "");
                readLine = readLine.Trim();
                count = Convert.ToInt32(readLine);
            }
            return count;
        }
    }
}
 
