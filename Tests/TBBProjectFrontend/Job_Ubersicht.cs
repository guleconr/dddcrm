using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace RerzukunftFrontend
{
    public class Job_Ubersicht
    {
        [Fact]
        public void Test3()
        {
            IWebDriver driver = new ChromeDriver("./");
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://TBBProject.de/");
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(),'Für Bewerber')]")));
            var hoverAction = new Actions(driver);
            hoverAction.MoveToElement(element).Perform();
            var isEnabled = element.Enabled;
            driver.FindElement(By.XPath("//a[contains(text(),'Job Übersicht')]")).Click();
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.XPath("//body//a[2]")).Click();
            driver.FindElement(By.XPath("//a[@class='popup-with-zoom-anim button']")).Click();
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[@id='Name']")).SendKeys("Mert Kahraman");
            driver.FindElement(By.XPath("//input[@id='Surname']")).SendKeys("Ukten");
            driver.FindElement(By.XPath("//input[@id='Phone']")).SendKeys("5325760721");
            driver.FindElement(By.XPath("//input[@id='Email']")).SendKeys("mertkahramanukten@gmail.com");
            driver.FindElement(By.XPath("//textarea[@id='Message']")).SendKeys("Test");
            //driver.FindElement(By.XPath("//label[@class='upload-btn']")).Click();
            System.Threading.Thread.Sleep(5000);
            //SendKeys.SendWait(@"C:\Test.pdf");
            //SendKeys.SendWait(@"{Enter}");
            driver.FindElement(By.XPath("//button[@class='send']")).Click();
            System.Threading.Thread.Sleep(3000);
            var cwait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            cwait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'Sie haben sich auf diese Stelle beworben.')]")));
            Console.WriteLine("Baþarýlý Bir Þekilde Baþvuru Yapýldý.");
            driver.Quit();
        }
    }
}
