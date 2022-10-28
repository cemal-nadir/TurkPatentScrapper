using Browser.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.Source.Core.Helpers;
using TPHunter.Source.Scrapper.Models;

namespace TPHunter.Source.Scrapper.Functions
{
    public static class DownloadHelper
    {
        public static void SearchMarks(this IWebDriver driver, string bulletinNumber)
        {
            driver.FindElement(By.CssSelector("input[placeholder='Marka İlan Bülten No']"), 20).Click();
            driver.FindElement(By.CssSelector("input[placeholder='Marka İlan Bülten No']"), 20).SendKeys(bulletinNumber);
            driver.ClickWithJs(driver.FindElement(By.CssSelector("#__next > div > div.jss22.jss25 > main > div.jss1 > div > div.jss3 > div.jss9 > div.jss15 > div > button.MuiButtonBase-root.MuiButton-root.MuiButton-contained.MuiButton-containedSecondary"), 20));
        }
        public static MarkModel GetMarkData(this IWebDriver driver)
        {
            MarkModel markModel = new();
            List<IWebElement> cacheRows = new();
            var sections = driver.FindElement(By.ClassName("MuiCardContent-root"), 20).FindElements(By.TagName("fieldset"));
            #region Section Marka Bilgileri
            List<HolderModel> cacheHolders = new();
            markModel.ImageText = sections[0].FindElement(By.TagName("img")).GetAttribute("src");
            cacheRows = sections[0].FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
            markModel.ApplicationNumber = cacheRows[0].FindElements(By.TagName("td"))[1].Text;
            markModel.ApplicationDate = cacheRows[0].FindElements(By.TagName("td"))[3].Text.CustomConvertToDatetime();
            markModel.RegistrationNumber = cacheRows[1].FindElements(By.TagName("td"))[1].Text;
            markModel.RegistrationDate = cacheRows[1].FindElements(By.TagName("td"))[3].Text.CustomConvertToDatetime();
            markModel.InternationalRegistrationNumber = cacheRows[2].FindElements(By.TagName("td"))[1].Text;
            markModel.DocumentNumber = cacheRows[2].FindElements(By.TagName("td"))[3].Text;
            markModel.DeclareBullettinDate = cacheRows[3].FindElements(By.TagName("td"))[1].Text.CustomConvertToDatetime();
            markModel.RegistrationBullettinDate = cacheRows[3].FindElements(By.TagName("td"))[3].Text.CustomConvertToDatetime();
            markModel.DeclareBullettinNumber = cacheRows[4].FindElements(By.TagName("td"))[1].Text;
            markModel.RegistrationBullettinNumber = cacheRows[4].FindElements(By.TagName("td"))[3].Text;
            markModel.ProtectionDate = cacheRows[5].FindElements(By.TagName("td"))[1].Text.CustomConvertToDatetime();
            markModel.Status = cacheRows[5].FindElements(By.TagName("td"))[3].Text;
            markModel.PriortyInformation = cacheRows[6].FindElements(By.TagName("td"))[1].Text;
            markModel.Classes = cacheRows[7].FindElements(By.TagName("td"))[1].Text.ParseClasses();
            markModel.Type = cacheRows[7].FindElements(By.TagName("td"))[3].Text;
            markModel.Name = cacheRows[8].FindElements(By.TagName("td"))[1].Text;
            markModel.AttorneyName = cacheRows[9].FindElements(By.TagName("td"))[1].FindElements(By.TagName("p"))[0].Text;
            markModel.AttorneyCompanyName = cacheRows[9].FindElements(By.TagName("td"))[1].FindElements(By.TagName("p"))[1].Text;
            foreach (var holderData in cacheRows[10].FindElements(By.TagName("td"))[1].FindElements(By.TagName("div")))
            {
                cacheHolders.Add(new HolderModel()
                {
                    HolderCode = holderData.FindElements(By.TagName("p"))[0].Text,
                    HolderName = holderData.FindElements(By.TagName("p"))[1].Text,
                    Address = holderData.FindElements(By.TagName("p"))[2].Text
                });
            }
            markModel.Holders = cacheHolders;
            markModel.Decision = cacheRows[11].FindElements(By.TagName("td"))[1].Text;
            markModel.DecisionReason = cacheRows[11].FindElements(By.TagName("td"))[3].Text;
            #endregion


            #region Section Mal ve Hizmet Bilgileri
            if (sections.Count > 2)
            {
                //sections[1] mal ve hizmet bilgileri
            }
            #endregion
            #region Section Başvuru İşlem Bilgileri
            if (sections.Count > 1)
            {
                var markTransactionSection = sections.Last();

                //markTransactionSection başvuru işlem bilgileri

            }

            #endregion

            return markModel;
        }
    }
}
