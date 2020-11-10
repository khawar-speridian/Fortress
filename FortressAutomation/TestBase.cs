using Authlete.Util;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Protractor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace FortressAutomation
{
	/// <summary>
	/// <Author>Khawar Iqbal</Author>
	/// <para>This class initializes the browser.</para>
	/// </summary>
	public class TestBase
	{

		public static NgWebDriver ngWebDriver;
		public static IWebDriver driver;
		public string browserName;
		public string url;

		public static Configuration Global_TestBase_Configuration = ConfigurationManager.OpenExeConfiguration(@"FortressAutomation.dll");

		public void initialization()
		{
			var _options = new ChromeOptions();
			_options.AddArguments("--headless");

			if ((Global_TestBase_Configuration.AppSettings.Settings["browser"].Value).Equals("chrome"))
            {
				driver = new ChromeDriver();
				ngWebDriver = new NgWebDriver(driver);
			}
			else if (browserName.Equals("FireFox"))
			{
				//driver = new FirefoxDriver();
			}
			driver.Manage().Cookies.DeleteAllCookies();
			driver.Manage().Window.Maximize();
			driver.Navigate().GoToUrl(Global_TestBase_Configuration.AppSettings.Settings["url"].Value);
		}
	}
}
