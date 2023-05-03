using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace AppiumDesktopTests
{
    public class AppiumDesktopTests
    {
        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions options;
        private const string appLocation = @"C:\Users\krist\OneDrive\Desktop\C#\QA Auto Front\Podg\ExamPrepResources\ShortURL-DesktopClient-v1.0.net6\ShortURL-DesktopClient.exe";
        private const string appiumServer = "http://127.0.0.1:4723/wd/hub";
        private const string appServer = "https://shorturl.krismikov.repl.co/api";

        [SetUp]

        public void PrepareApp()
        {
            this.options = new AppiumOptions();
            options.AddAdditionalCapability("app", appLocation);
            driver = new WindowsDriver<WindowsElement>(new Uri(appiumServer), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]

        public void CloseApp()
        {
            driver.Quit();
        }

        [Test]

        public void TestAddNewUrl()
        {
            var inputAppUrl = driver.FindElementByAccessibilityId("textBoxApiUrl");
            inputAppUrl.Clear();
            inputAppUrl.SendKeys(appServer);

            var buttonConnect = driver.FindElementByAccessibilityId("buttonConnect");
            buttonConnect.Click();

            // Thread.Sleep(2000);

            var buttonAdd = driver.FindElementByAccessibilityId("buttonAdd");
            buttonAdd.Click();

            var inputUrl = driver.FindElementByAccessibilityId("textBoxURL");
            inputUrl.SendKeys("https://wikipedia.org");

            var buttonCreate = driver.FindElementByAccessibilityId("buttonCreate");
            buttonCreate.Click();

            var resultField = driver.FindElementByName("https://wikipedia.org");

            Assert.IsNotEmpty(resultField.Text);
            Assert.That(resultField.Text, Is.EqualTo("https://wikipedia.org"));

        }

    }

    
    
}