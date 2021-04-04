using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace RerzukunftFrontend
{
    public class Login
    {
        [Fact]
        public void Test1()
        {
            var driver = new ChromeDriver("./");
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://TBBProject.de/");
            driver.FindElement(By.XPath("//a[contains(text(),'Anmeldung')]")).Click();
            driver.FindElement(By.XPath("//input[@id='Email']")).SendKeys("mertkahramanukten@gmail.com");
            driver.FindElement(By.XPath("//input[@id='Password']")).SendKeys("Kahraman34");
            driver.FindElement(By.XPath("//button[@class='btn btn-primary']")).Click();
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Baþarýlý Bir Þekilde Login Olundu.");
            driver.Quit();
        }
    }
}
