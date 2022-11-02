using Browser.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.Source.Core.Configs;
using TPHunter.Source.Core.Helpers;
using TPHunter.Source.Core.Models.Scrapper;
using TPHunter.Source.ImageProcess;
using TPHunter.Source.Scrapper.Models;

namespace TPHunter.Source.Scrapper.Functions
{
    public static class MainHelper
    {
        public static void GoTPPage(this IWebDriver driver)
        {
            driver.Navigate().GoToUrl(RuntimeConfigs.GeneralConfig.TPConfig.TPSearchPage);
        }
        public static void ClickSearchType(this IWebDriver driver, SearchType searchType)
        {
            var searchDiv = driver.FindElement(By.CssSelector("#__next > div > div.jss22.jss25 > main > div.jss1 > div > div.jss3 > div.MuiTabs-root.jss13.MuiTabs-vertical > div.MuiTabs-scroller.MuiTabs-scrollable > div"), 20);
            var buttons = searchDiv.FindElements(By.TagName("button"));
            switch (searchType)
            {
                case SearchType.Trademark:
                    driver.ClickWithJs(buttons.FirstOrDefault(x => x.GetAttribute("title") == "Marka Araştırma"));
                    break;
                case SearchType.Patent:
                    driver.ClickWithJs(buttons.FirstOrDefault(x => x.GetAttribute("title") == "Patent Araştırma"));
                    break;
                case SearchType.Design:
                    driver.ClickWithJs(buttons.FirstOrDefault(x => x.GetAttribute("title") == "Tasarım Araştırma"));
                    break;
            }
        }
        public static void CloseDataPopUp(this IWebDriver driver)
        {
            var divs = driver.FindElements(By.ClassName("react-draggable"), 20);
            var button = (from div in divs
                          let b = div.FindElements(By.TagName("button")).FirstOrDefault(x => x.GetAttribute("title") == "Kapat")
                          where b != null
                          select b).FirstOrDefault();
            driver.ClickWithJs(button);
        }
        public static IEnumerable<IWebElement> GetResponseListTableRows(this IWebDriver driver)
        {
            return driver.FindElement(By.CssSelector("#results > tbody"), 20).FindElements(By.TagName("tr"));
        }
        public static IEnumerable<ResponseListTableModel> GetResponseListButtons(this IEnumerable<IWebElement> webElements)
        {
            List<ResponseListTableModel> responseListTableModels = new();
            foreach (var rows in webElements)
            {
                var cols = rows.FindElements(By.TagName("td"));
                var button = cols.LastOrDefault().FindElement(By.TagName("button"));
                var applicationNumber = cols.FirstOrDefault(x => x.GetAttribute("role") == "applicationNo");
                responseListTableModels.Add(new()
                {
                    ApplicationNumber = applicationNumber.Text,
                    DetailButton = button
                });
            }
            return responseListTableModels;
        }
        public static MarkModel GetMarkData(this IWebDriver driver, ScrapType scrapType)
        {
            MarkModel markModel = new();
            List<IWebElement> cacheRows = new();
            var sections = (scrapType is ScrapType.Download) ? driver.FindElement(By.ClassName("MuiCardContent-root"), 20).FindElements(By.TagName("fieldset"))
                : driver.FindElement(By.Id("search-results"), 20).FindElements(By.TagName("fieldset"));

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
                List<MarkServicesModel> cacheMarkServices = new();
                cacheRows = sections[1].FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                foreach (var cacheRow in cacheRows)
                {
                    cacheMarkServices.Add(new MarkServicesModel()
                    {
                        Class = cacheRow.FindElements(By.TagName("td"))[0].Text.ParseClass(),
                        Service = cacheRow.FindElements(By.TagName("td"))[1].Text
                    });
                }
                markModel.Services = cacheMarkServices;
            }
            #endregion
            #region Section Başvuru İşlem Bilgileri
            if (sections.Count > 1)
            {
                List<MarkTransactionsModel> cacheMarkTransactions = new();
                string transactionType = default(string);
                var markTransactionSection = sections.Last();
                cacheRows = markTransactionSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();

                foreach (var cacheRow in cacheRows)
                {
                    var cols = cacheRow.FindElements(By.TagName("td"));
                    if (cols.Count == 1)
                    {

                        transactionType = cols[0].FindElement(By.TagName("strong")).Text;
                    }
                    else
                    {
                        MarkTransactionsModel cacheModel = new();
                        cacheModel.TransactionType = transactionType;
                        cacheModel.NotificationDate = cols[0].Text.CustomConvertToDatetime();
                        cacheModel.TransactionDate = cols[1].Text.CustomConvertToDatetime();
                        cacheModel.Name = cols[2].Text;
                        //Açıklama kısmında tablo var ise o ekleniyor
                        if (cols[3].FindElements(By.TagName("tbody")).Any())
                        {
                            List<MarkTransactionDetail> cacheMarkTransactionDetails = new();
                            var detailRows = cols[3].FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
                            foreach (var detailRow in detailRows)
                            {
                                cacheMarkTransactionDetails.Add(new MarkTransactionDetail()
                                {
                                    DecisionReason = detailRow.FindElements(By.TagName("td"))[0].Text,
                                    AboutMark = detailRow.FindElements(By.TagName("td"))[1].Text
                                });
                            }
                            cacheModel.MarkTransactionDetails = cacheMarkTransactionDetails;
                        }
                        else
                        {
                            cacheModel.Description = cols[3].Text;
                        }
                        cacheMarkTransactions.Add(cacheModel);
                    }
                }
                markModel.Transactions = cacheMarkTransactions;
            }

            #endregion

            return markModel;
        }
        public static DesignModel GetDesignData(this IWebDriver driver, ScrapType scrapType)
        {
            DesignModel designModel = new();
            List<IWebElement> cacheRows = new();
            var sections = (scrapType is ScrapType.Download) ? driver.FindElement(By.ClassName("MuiCardContent-root"), 20).FindElements(By.TagName("fieldset"))
                : driver.FindElement(By.Id("search-results"), 20).FindElements(By.TagName("fieldset"));
            IWebElement cacheSection;

            #region Section Dosya Bilgileri
            cacheRows = sections[0].FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
            designModel.ApplicationNumber = cacheRows[0].FindElements(By.TagName("td"))[1].Text;
            designModel.ApplicationDate = cacheRows[1].FindElements(By.TagName("td"))[1].Text.CustomConvertToDatetime();
            designModel.RegistrationNumber = cacheRows[2].FindElements(By.TagName("td"))[1].Text;
            designModel.RegistrationDate = cacheRows[3].FindElements(By.TagName("td"))[1].Text.CustomConvertToDatetime();
            designModel.BulletinNumber = cacheRows[4].FindElements(By.TagName("td"))[1].Text;
            designModel.BulletinDate = cacheRows[5].FindElements(By.TagName("td"))[1].Text.CustomConvertToDatetime();
            designModel.Status = cacheRows[6].FindElements(By.TagName("td"))[1].Text;
            #endregion
            #region Section Başvuru Sahipleri

            cacheSection = sections.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Başvuru Sahipleri");
            if (cacheSection != null)
            {
                List<HolderModel> cacheHolders = new();
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                foreach (var cacheRow in cacheRows)
                {
                    var cacheHolderCols = cacheRow.FindElements(By.TagName("td"));
                    cacheHolders.Add(new HolderModel()
                    {
                        HolderCode = cacheHolderCols[0].Text,
                        HolderName = cacheHolderCols[1].FindElements(By.TagName("h6")).FirstOrDefault().Text,
                        Address = cacheHolderCols[1].FindElements(By.TagName("h6")).LastOrDefault().Text
                    });
                }
                designModel.Holders = cacheHolders;
            }
            #endregion
            #region Section Tasarımcılar

            cacheSection = sections.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Tasarımcılar");
            if (cacheSection != null)
            {
                List<string> cacheDesigners = new();
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                foreach (var cacheRow in cacheRows)
                {
                    cacheDesigners.Add(cacheRow.FindElement(By.TagName("td")).FindElements(By.TagName("h6")).FirstOrDefault().Text);
                }
                designModel.Designers = cacheDesigners;
            }
            #endregion
            #region Section Vekil Bilgileri
            cacheSection = sections.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Vekil Bilgileri");
            if (cacheSection != null)
            {
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("h6")).ToList();
                var attorneyCompanyIndex = cacheRows[0].Text.LastIndexOf('(');
                designModel.AttorneyCompanyName = (attorneyCompanyIndex != -1) ? cacheRows[0].Text.Substring(attorneyCompanyIndex + 1, cacheRows[0].Text.Length - 1) : null;
                designModel.AttorneyName = (attorneyCompanyIndex != -1) ? cacheRows[0].Text.Substring(0, attorneyCompanyIndex - 1) : cacheRows[0].Text;
                designModel.AttorneyAddress = cacheRows[1].Text;
            }
            #endregion
            #region Section Tasarımlar

            cacheSection = sections.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Section Tasarımlar");
            if (cacheSection != null)
            {
                List<ProductModel> cacheProductModels = new();
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                foreach (var cacheRow in cacheRows)
                {
                    var cacheCols = cacheRow.FindElements(By.TagName("td"));
                    ProductModel cacheProductModel = new();
                    cacheProductModel.Name = cacheCols[1].Text;
                    cacheProductModel.LocarnoClass = cacheCols[2].Text.Contains(",") ? cacheCols[2].Text.Split(',') : new string[] { cacheCols[2].Text };
                    cacheProductModel.ProductImages = cacheCols[3].FindElements(By.TagName("img")).Select(x => x.GetAttribute("src")).ToArray();
                    Ioc.ProcessorFactory();
                    cacheProductModel.IsProductApproved = Ioc.Resolve<IProcessor>().IsProductImageApproved(cacheProductModel.ProductImages.FirstOrDefault()).Result;
                    if (cacheCols[4].FindElements(By.TagName("tbody")).Any())
                    {
                        var cachePriortyRows = cacheCols[4].FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
                        cacheProductModel.PriortyApplicationNumber = cachePriortyRows[0].FindElements(By.TagName("td")).Last().Text.Split('/')[0].Replace(" ", "");
                        cacheProductModel.PriortyCountry = cachePriortyRows[0].FindElements(By.TagName("td")).Last().Text.Split('/')[1].Replace(" ", "");
                        cacheProductModel.ExhibitionName = cachePriortyRows[1].FindElements(By.TagName("td")).Last().Text.Split('/')[0].Replace(" ", "");
                        cacheProductModel.ExhibitionPlace = cachePriortyRows[1].FindElements(By.TagName("td")).Last().Text.Split('/')[1].Replace(" ", "");
                        cacheProductModel.ExhibitionDate = cachePriortyRows[2].FindElements(By.TagName("td")).Last().Text.CustomConvertToDatetime();
                        cacheProductModel.FirstExhibitionDate = cachePriortyRows[3].FindElements(By.TagName("td")).Last().Text.CustomConvertToDatetime();
                        cacheProductModel.PriortyDate = cachePriortyRows[4].FindElements(By.TagName("td")).Last().Text.Split('/')[0].Replace(" ", "").CustomConvertToDatetime();
                        cacheProductModel.PriortyType = cachePriortyRows[4].FindElements(By.TagName("td")).Last().Text.Split('/')[1].Replace(" ", "");
                    }
                    cacheProductModels.Add(cacheProductModel);
                }
                designModel.Products = cacheProductModels;
            }
            #endregion
            #region Section Başvuru İşlem Bilgileri

            cacheSection = sections.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Başvuru İşlem Bilgileri");
            if (cacheSection != null)
            {
                List<DesignTransactionModel> cacheDesignTransactionModel = new();
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                string transactionType = default(string);
                string transactionDetail = default(string);
                foreach (var cacheRow in cacheRows)
                {
                    var cols = cacheRow.FindElements(By.TagName("td"));
                    if (cols.Count == 1)
                    {
                        var cacheTransactionTypeText = cols[0].FindElement(By.TagName("strong")).Text;
                        transactionType = cacheTransactionTypeText.Contains("(") ? cacheTransactionTypeText.Substring(0, cacheTransactionTypeText.IndexOf('(')) : cacheTransactionTypeText;
                        transactionDetail = cacheTransactionTypeText.Contains("(") ? cacheTransactionTypeText.Substring(cacheTransactionTypeText.IndexOf('(') + 1, cacheTransactionTypeText.Length - 1) : default(string);
                    }
                    else
                    {
                        DesignTransactionModel cacheModel = new();
                        cacheModel.TransactionType = transactionType;
                        cacheModel.TransactionDetail = transactionDetail;
                        cacheModel.Date = cols[0].Text.CustomConvertToDatetime();
                        cacheModel.Description = cols[1].Text.Contains("(") ? cols[1].Text.Substring(0, cols[1].Text.IndexOf('(')) : cols[1].Text;
                        cacheModel.DescriptionDetail = cols[1].Text.Contains("(") ? cols[1].Text.Substring(cols[1].Text.IndexOf('(') + 1, cols[1].Text.Length - 1) : default(string);
                        cacheDesignTransactionModel.Add(cacheModel);
                    }
                }
                designModel.DesignTransactions = cacheDesignTransactionModel;
            }
            #endregion

            return designModel;
        }
        public static void CheckAndSolveCaptcha(this IWebDriver driver)
        {
            if (driver.FindElements(By.ClassName("swal2-container")).Any())
            {
                driver.ClickWithJs(driver.FindElement(By.ClassName("swal2-confirm")));
            }
        }
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            driver.CheckAndSolveCaptcha();
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
        public static IEnumerable<IWebElement> FindElements(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            driver.CheckAndSolveCaptcha();
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElements(by));
            }
            return driver.FindElements(by);
        }
        public static bool CheckPageIsLastAndClick(this IWebDriver driver)
        {
            var nextPageButton = driver.FindElement(By.ClassName("MuiTablePagination-root"), 20).FindElements(By.TagName("button")).FirstOrDefault(x => x.GetAttribute("title") == "Next page");
            if (nextPageButton.GetAttribute("class").Contains("Mui-disabled"))
                return true;

            driver.ClickWithJs(nextPageButton);
            driver.WaitAjaxLoad();
            return false;
        }
        public static void WaitAjaxLoad(this IWebDriver driver)
        {
            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("return jQuery.active == 0 && document.readyState === 'complete'");
            }
            catch (JavaScriptException ex)
            {
                if (ex.Message.Contains("javascript error: jQuery is not defined"))
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState === 'complete'");
                }
                else
                    throw new Exception("Javascript çalıştırılırken bir hata meydana geldi " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Sebebi Bilinmeyen Bir Hata Oluştu " + ex.Message);
            }
        }
        public static string FixMarkImageBase64(this string base64Text)
        {
            return base64Text.Replace('-', '+').Replace("data:image/jpeg;base64,", "").Replace('_', '/');
        }



        public enum SearchType
        {
            Trademark,
            Patent,
            Design
        }
        public enum ScrapType
        {
            Download,
            Upload
        }
    }
}
