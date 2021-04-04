using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace RerzukunftFrontend
{
    public class Login_Job_Ubersicht
    {
        [Fact]
        public void Test4()
        {
            //browser ayarlarý yapýlýr.
            var driver = new ChromeDriver("./");
            driver.Manage().Window.Maximize();
            //TBBProject test url ine baðlanýr.
            driver.Navigate().GoToUrl("https://TBBProject.de/");
            //anmeldung sekmesine basýlýr.
            driver.FindElement(By.XPath("//a[contains(text(),'Anmeldung')]")).Click();
            //mail adresi girilir.
            driver.FindElement(By.XPath("//input[@id='Email']")).SendKeys("mertkahramanukten@gmail.com");
            //þifre girilir.
            driver.FindElement(By.XPath("//input[@id='Password']")).SendKeys("Kahraman34");
            //anmeldung butonuna basýlýr.
            driver.FindElement(By.XPath("//button[@class='btn btn-primary']")).Click();
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("Baþarýlý Bir Þekilde Login Olundu.");
            //Für Bewerber sekmesinin üstüne gelinir.
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(),'Für Bewerber')]")));
            var hoverAction = new Actions(driver);
            hoverAction.MoveToElement(element).Perform();
            var isEnabled = element.Enabled;
            //Job Übersicht sekmesine basýlýr.
            driver.FindElement(By.XPath("//a[contains(text(),'Job Übersicht')]")).Click();
            System.Threading.Thread.Sleep(1000);
            // sayfadaki 2. ilana basýlýr.
            driver.FindElement(By.XPath("//body//a[2]")).Click();
            IWebElement userNameField = null;
            //ilana daha önce baþvuru yapýlýp yapýlmadýðýnýn kontrolü yapýlýr.
            try
            {
                //ilana daha önce baþvuru yapýlmadýysa "Für diesen Job bewerben" butonuna basýlýr.
                driver.FindElement(By.XPath("//a[@class='popup-with-zoom-anim button']")).Click();
                System.Threading.Thread.Sleep(3000);
                // Hochladen butonuna basýlýr.
                //driver.FindElement(By.XPath("//label[@class='upload-btn']")).Click();
                //System.Threading.Thread.Sleep(1000);
                //apload edilecek dosya seçilir.
                //SendKeys.SendWait(@"C:\Test.pdf");
                //SendKeys.SendWait(@"{Enter}");
                //Bewerbung senden butonuna basýlýr.
                driver.FindElement(By.XPath("//button[@class='send']")).Click();
                System.Threading.Thread.Sleep(5000);
                //baþvurunun baþarýlý bir þekilde yapýlýdýðý kontrol edilir.
                var cwait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
                cwait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'Sie haben sich auf diese Stelle beworben.')]")));
                Console.WriteLine("Baþarýlý Bir Þekilde Baþvuru Yapýldý.");
                driver.Quit();
            }
            catch
            {
                //daha önce baþarýlý bir þekilde baþvuru yapýldýðý ile ilgili mesaj kontrol edilir.
                System.Threading.Thread.Sleep(5000);
                var cwait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
                cwait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'Sie haben sich auf diese Stelle beworben.')]")));
                Console.WriteLine("Daha Önce Baþarýlý Bir Þekilde Baþvuru Yapýlmýþ.");
                driver.Quit();
            }
        }
    }
}
