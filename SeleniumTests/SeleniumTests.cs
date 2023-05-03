using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    public class SeleniumTests
    {
       
        private WebDriver driver;
        private const string baseUrl = "https://shorturl.nakov.repl.co/urls";

        [SetUp]
        public void OpenWebApp()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            //optional:
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Url = baseUrl;
        }

        [TearDown]
        public void CloseWebApp()
        {
            driver.Quit();
        }

        [Test]
        public void TestTableTopLeftCell()
        {
            var linkShortUrl = driver.FindElement(By.LinkText("Short URLs"));
            linkShortUrl.Click();

            var tableHeaderLeftCell = driver.FindElement(By.CssSelector("th:nth-child(1)"));
            Assert.That(tableHeaderLeftCell.Text, Is.EqualTo("Original URL"));
        }

        [Test]
        public void AddValidUrl()
        {
            var addUrlLink = driver.FindElement(By.LinkText("Add URL"));
            addUrlLink.Click();
            
            var addField = driver.FindElement(By.Id("url"));
            addField.SendKeys("https://wikipedia.org");

            var createButton = driver.FindElement(By.XPath("/html/body/main/form/table/tbody/tr[3]/td/button"));
            createButton.Click();

            Assert.That(driver.PageSource.Contains("https://wikipedia.org"));
        }

        [Test]
        public void AddInvalidUrl()
        {
            var addUrlLink = driver.FindElement(By.LinkText("Add URL"));
            addUrlLink.Click();

            var addField = driver.FindElement(By.Id("url"));
            addField.SendKeys("osas.org");

            var createButton = driver.FindElement(By.XPath("/html/body/main/form/table/tbody/tr[3]/td/button"));
            createButton.Click();

            var errMsg = driver.FindElement(By.XPath("/html/body/div[@class='err']"));

            Assert.That(errMsg.Text, Is.EqualTo("Invalid URL!"));

        }

        [Test]
        public void VisitNonExistingUrl()
        {
            //driver.Navigate().GoToUrl("https://shorturl.nakov.repl.co/go/invalid53652");

            driver.Url = "https://shorturl.nakov.repl.co/go/invalid53652";

            var errMsg = driver.FindElement(By.XPath("/html/body/div[@class='err']"));

            Assert.That(errMsg.Text, Is.EqualTo("Cannot navigate to given short URL"));

        }


        [Test]
        public void TestCounterIncrease()
        {
            var linkShortUrls = driver.FindElement(By.LinkText("Short URLs"));
            linkShortUrls.Click();

            var oldCounter = driver.FindElement(By.XPath("/html/body/main/table[@class='urls']/tbody/tr[1]/td[4]")).Text;
            var oldCounterNum = int.Parse(oldCounter);

            var linkShortUrlClick = driver.FindElement(By.XPath("/html/body/main/table[@class='urls']/tbody/tr[1]/td[2]/a[@class='shorturl']")).Click;

            driver.SwitchTo().Window(driver.WindowHandles[0]);
            //moje i s driver.Close();
            
            
            driver.Navigate().Refresh();
            var newCounter = driver.FindElement(By.XPath("/html/body/main/table[@class='urls']/tbody/tr[1]/td[4]")).Text;
            
            
           var newCounterNum = int.Parse(newCounter);  

           Assert.That(newCounterNum, Is.EqualTo(oldCounterNum));

        }
    }
}