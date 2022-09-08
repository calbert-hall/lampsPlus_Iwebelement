using Applitools.Appium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ApplitoolsTutorial
{
    [TestFixture]
    public class BasicDemo
    {
        //private RemoteWebDriver driver;
        private AppiumDriver<AppiumWebElement> driver;
        private Eyes eyes;

        [Test]
        public void AndroidTest()
        {
            // Initialize the eyes SDK (IMPORTANT: make sure your API key is set in the APPLITOOLS_API_KEY env variable).
            eyes = new Eyes();
            //eyes.ApiKey = Environment.GetEnvironmentVariable("APPLITOOLS_API_KEY");//"VJMt4z4djBoqW40fclJgEpLGuwGppgZ98m5wtUuWhru0110";
            // Set the desired capabilities.
            AppiumOptions options = new AppiumOptions();
            /*options.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Samsung Galaxy S10");
            options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "9.0");
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");*/

            //options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "9.0");
            options.AddAdditionalCapability("device", "iPhone 12 Pro");
            options.AddAdditionalCapability("os_version", "14");
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");

            options.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Chrome");
            options.AddAdditionalCapability("deviceOrientation", "portrait");

            // Initialize BrowserStack credentials. (IMPORTANT: make sure you have the below environment variables set).
            Dictionary<string, object> browserstackOptions = new Dictionary<string, object>();
            browserstackOptions.Add("userName",  "applitools");
            browserstackOptions.Add("accessKey", "zBo67o7BsoKhdkf8Va4u");

            options.AddAdditionalCapability("bstack:options", browserstackOptions);

            driver = new IOSDriver<AppiumWebElement>(new Uri("http://hub-cloud.browserstack.com/wd/hub/"), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);

            driver.Url = $"https://www.lampsplus.com/products/737T0";

            // Start visual UI testing.
            eyes.Open(driver, "Lamps Plus", "Appium C#");

            IWebElement shipsElement = driver.FindElementByCssSelector(".shipsInMessage");
                
            //eyes.Check(Target.Window().Fully().Layout(By.CssSelector(".shipsInMessage"))); //Works      
            eyes.Check(Target.Window().Fully().Layout(shipsElement)); //Fails

            // ** Lines of code in stacktrace that cause failure! **
            //Type type = shipsElement.GetType();
            //FieldInfo field = type.GetField("elementId");
            //Object instObject = field.GetValue(shipsElement);

            // End the test.
            eyes.Close(false);
        }

        [TearDown]
        public void AfterEach()
        {
            // Close the browser if driver isn't null.
            driver?.Quit();

            // If the test was aborted before eyes.close was called, ends the test as aborted.
            eyes.AbortIfNotClosed();
        }

    }
}
