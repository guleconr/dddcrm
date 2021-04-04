using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace RerzukunftFrontend
{
    public class KullaniciGuncelleme
    {
        [Fact]
        public void Test2()
        {
            IWebDriver driver = new ChromeDriver("./");
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://TBBProject.de/");
            driver.FindElement(By.XPath("//a[contains(text(),'Anmeldung')]")).Click();
            driver.FindElement(By.XPath("//input[@id='Email']")).SendKeys("mertkahramanukten@gmail.com");
            driver.FindElement(By.XPath("//input[@id='Password']")).SendKeys("Kahraman34");
            driver.FindElement(By.XPath("//button[@class='btn btn-primary']")).Click();
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Baþarýlý Bir Þekilde Login Olundu.");
            driver.FindElement(By.XPath("//a[@class='nav-link userMenu']")).Click();
            driver.FindElement(By.XPath("//a[contains(text(),'Bearbeiten')]")).Click();
            driver.FindElement(By.XPath("//input[@id='Phone']")).SendKeys(Keys.Control + "a");
            var r = new Random();
            var phoneNumber = r.Next(10000000).ToString().PadRight(10, '0');
            driver.FindElement(By.XPath("//input[@id='Phone']")).SendKeys(phoneNumber);
            driver.FindElement(By.XPath("//button[@class='btn btn-primary']")).Click();
            System.Threading.Thread.Sleep(8000);
            Console.WriteLine("Telefon numarasý baþarýlý bir þekilde deðiþtirildi..");
            driver.Quit();
        }
    }
}
