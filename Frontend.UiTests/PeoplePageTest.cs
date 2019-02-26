using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using Xunit;

namespace Frontend.UiTests
{
    public class PeoplePageTest
    {
        private RemoteWebDriver _driver;

        public PeoplePageTest()
        {
            var capabilities = new ChromeOptions().ToCapabilities();
            var uri = new Uri("http://selenium-chrome:4444/wd/hub/");
            _driver = new RemoteWebDriver(uri, capabilities);
        }

        [Fact]
        public void CanListPeople()
        {
            _driver.Navigate().GoToUrl("http://frontend/people");

            // marco
            var element = _driver.FindElement(By.XPath("/html/body/div/table/tbody/tr[1]/td[1]"));
            Assert.Equal("Marco", element.Text.Trim());
            // marco@des.com
            element = _driver.FindElement(By.XPath("/html/body/div/table/tbody/tr[1]/td[2]"));
            Assert.Equal("marco@des.com", element.Text.Trim());
        }

        [Fact]
        public void CanCreatePerson()
        {
            _driver.Navigate().GoToUrl("http://frontend/people/create");

            // create person
            _driver.FindElementById("Name").SendKeys("Jack");
            _driver.FindElementById("Email").SendKeys("jack@test.com");
            _driver.FindElementByXPath("//input[@type='submit' and @value='Create']").Click();

            // go to list, check it's listed
            _driver.Navigate().GoToUrl("http://frontend/people");

            var element = _driver.FindElementByXPath("//td[contains(., 'jack@test.com')]");

            Assert.NotNull(element);
        }
    }
}
