using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace RerzukunftFrontend
{
    public class Initiativbewerbung
    {
        [Fact]
        public void Test5()
        {
            //browser ayarlarý yapýlýr.
            IWebDriver driver = new ChromeDriver("./");
            driver.Manage().Window.Maximize();
            //TBBProject test url ine baðlanýr.
            driver.Navigate().GoToUrl("https://TBBProject.de/");
            //Für Bewerber sekmesinin üstüne gelinir.
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(),'Für Bewerber')]")));
            var hoverAction = new Actions(driver);
            hoverAction.MoveToElement(element).Perform();
            var isEnabled = element.Enabled;
            //Initiativbewerbung sekmesine basýlýr.
            driver.FindElement(By.XPath("//a[contains(text(),'Initiativbewerbung')]")).Click();
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.XPath("//ul[@class='chosen-choices']")).Click();
            driver.FindElement(By.XPath("//li[contains(text(),'Bau')]")).Click();
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[@id='Name']")).Click();
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.XPath("//div[@id='Branch_chosen']//a[@class='chosen-single']")).Click();
            driver.FindElement(By.XPath("//li[contains(text(),'Neukoelln')]")).Click();
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[@id='Name']")).SendKeys("Mert Kahraman");
            driver.FindElement(By.XPath("//input[@id='Surname']")).SendKeys("Ukten");
            driver.FindElement(By.XPath("//input[@id='Phone']")).SendKeys("5325760721");
            driver.FindElement(By.XPath("//input[@id='Email']")).SendKeys("mertkahramanukten@gmail.com");
            driver.FindElement(By.XPath("//input[@placeholder='tag.monat.jahr (erforderlich)']")).SendKeys("01.01.1989");
            driver.FindElement(By.XPath("//div[@id='HasVermittlungsgutschein_chosen']//a[@class='chosen-single']")).Click();
            //driver.FindElement(By.XPath("//label[@class='upload-btn']")).Click();
            System.Threading.Thread.Sleep(1000);
            //SendKeys.SendWait(@"C:\Test.pdf");
            //SendKeys.SendWait(@"{Enter}");
            driver.FindElement(By.XPath("//button[@class='btn btn-primary']")).Click();
            System.Threading.Thread.Sleep(3000);
            var cwait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            cwait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'Allgemeine Bewerbung möglich.')]")));
            Console.WriteLine("Baþarýlý Bir Þekilde Baþvuru Yapýldý.");
            driver.Quit();
        }
    }
}
