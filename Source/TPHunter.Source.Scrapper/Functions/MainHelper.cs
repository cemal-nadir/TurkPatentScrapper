﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TPHunter.Source.Core.Configs;
using TPHunter.Source.Core.Helpers;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.Source.Browser.Helpers;
using TPHunter.Source.ImageProcess;
using TPHunter.Source.Scrapper.Models;
using System.Threading;
using SeleniumExtras.WaitHelpers;

namespace TPHunter.Source.Scrapper.Functions
{
    public static class MainHelper
    {
        public static void GoTpPage(this IWebDriver driver)
        {
            driver.Navigate().GoToUrl(RuntimeConfigs.GeneralConfig.TpConfig.TpSearchPage);
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(searchType), searchType, null);
            }
            Thread.Sleep(2000);

        }
        public static void CloseDataPopUp(this IWebDriver driver)
        {
            var divs = driver.FindElements(By.ClassName("react-draggable"), 20);
            var button = (from div in divs
                          let b = div.FindElements(By.TagName("button")).FirstOrDefault(x => x.GetAttribute("title") == "Kapat")
                          where b != null
                          select b).FirstOrDefault();
            try
            {
                driver.ClickWithJs(button);
            }
            catch
            {
                // ignored
            }
        }
        public static IEnumerable<IWebElement> GetResponseListTableRows(this IWebDriver driver)
        {
            Thread.Sleep(2000);
            return driver.FindElement(By.CssSelector("#results > tbody"), 20).FindElements(By.TagName("tr"));
        }
        public static IEnumerable<ResponseListTableModel> GetResponseListButtons(this IEnumerable<IWebElement> webElements)
        {
            return (from rows in webElements
                    select rows.FindElements(By.TagName("td"))
                into cols
                    let button = cols.LastOrDefault()?.FindElement(By.TagName("button"))
                    let applicationNumber = cols.FirstOrDefault(x => x.GetAttribute("role") == "applicationNo")
                    select new ResponseListTableModel()
                    { ApplicationNumber = applicationNumber?.Text, DetailButton = button }).ToList();
        }
        public static MarkModel GetMarkData(this IWebDriver driver)
        {
            driver.WaitAjaxLoad();
            if (!driver.IsDataAccessible()) return null;

            MarkModel markModel = new();
            var sections = driver.FindElements(By.TagName("fieldset"), 20);

            #region Section Marka Bilgileri

            var webElements = sections.ToList();
            markModel.ImageText = webElements.FirstOrDefault()?.FindElement(By.TagName("img")).GetAttribute("src").FixImageBase64();
            var cacheRows = webElements.FirstOrDefault()?.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
            markModel.ApplicationNumber = cacheRows?[0].FindElements(By.TagName("td"))[1].Text.NormalizeText();
            markModel.ApplicationDate = cacheRows?[0].FindElements(By.TagName("td"))[3].Text.CustomConvertToDatetime();
            markModel.RegistrationNumber = cacheRows?[1].FindElements(By.TagName("td"))[1].Text.NormalizeText();
            markModel.RegistrationDate = cacheRows?[1].FindElements(By.TagName("td"))[3].Text.CustomConvertToDatetime();
            markModel.InternationalRegistrationNumber = cacheRows?[2].FindElements(By.TagName("td"))[1].Text.NormalizeText();
            markModel.DocumentNumber = cacheRows?[2].FindElements(By.TagName("td"))[3].Text.NormalizeText();
            markModel.DeclareBullettinDate = cacheRows?[3].FindElements(By.TagName("td"))[1].Text.CustomConvertToDatetime();
            markModel.RegistrationBullettinDate = cacheRows?[3].FindElements(By.TagName("td"))[3].Text.CustomConvertToDatetime();
            markModel.RegistrationBullettinNumber = cacheRows?[4].FindElements(By.TagName("td"))[3].Text.NormalizeText();
            markModel.ProtectionDate = cacheRows?[5].FindElements(By.TagName("td"))[1].Text.CustomConvertToDatetime();
            markModel.Status = cacheRows?[5].FindElements(By.TagName("td"))[3].Text.NormalizeText();
            markModel.PriortyInformation = cacheRows?[6].FindElements(By.TagName("td"))[1].Text.NormalizeText();
            markModel.Classes = cacheRows?[7].FindElements(By.TagName("td"))[1].Text.NormalizeText().ParseClasses();
            markModel.Type = cacheRows?[7].FindElements(By.TagName("td"))[3].Text.NormalizeText();
            markModel.Name = cacheRows?[8].FindElements(By.TagName("td"))[1].Text.NormalizeText();
            markModel.AttorneyName = cacheRows?[9].FindElements(By.TagName("td"))[1].FindElements(By.TagName("p"))[0].Text.NormalizeText();
            markModel.AttorneyCompanyName = cacheRows?[9].FindElements(By.TagName("td"))[1].FindElements(By.TagName("p"))[1].Text.NormalizeText();
            var cacheHolders = cacheRows?[10].FindElements(By.TagName("td"))[1]
                .FindElements(By.TagName("div")).Select(holderData => new HolderModel()
                {
                    HolderCode = holderData.FindElements(By.TagName("p"))[0].Text.NormalizeText(),
                    HolderName = holderData.FindElements(By.TagName("p"))[1].Text.NormalizeText(),
                    Address = holderData.FindElements(By.TagName("p"))[2].Text.NormalizeText()
                }).ToList();
            markModel.Holders = cacheHolders;
            markModel.Decision = cacheRows?[11].FindElements(By.TagName("td"))[1].Text.NormalizeText();
            markModel.DecisionReason = cacheRows?[11].FindElements(By.TagName("td"))[3].Text.NormalizeText();
            #endregion
            #region Section Mal ve Hizmet Bilgileri

            if (webElements.Count > 2)
            {
                cacheRows = webElements[1].FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                var cacheMarkServices = cacheRows.Select(cacheRow => new MarkServicesModel()
                {
                    Class = cacheRow.FindElements(By.TagName("td"))[0].Text.NormalizeText().ParseClass(),
                    Service = cacheRow.FindElements(By.TagName("td"))[1].Text.NormalizeText()
                }).ToList();
                markModel.Services = cacheMarkServices;
            }
            #endregion
            #region Section Başvuru İşlem Bilgileri

            if (webElements.Count <= 1) return markModel;
            {
                List<MarkTransactionsModel> cacheMarkTransactions = new();
                var transactionType = default(string);
                var markTransactionSection = webElements.Last();
                cacheRows = markTransactionSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();

                foreach (var cols in cacheRows.Select(cacheRow => cacheRow.FindElements(By.TagName("td"))))
                {
                    var enumerable = cols.ToList();
                    if (enumerable.Count() == 1)
                    {

                        transactionType = enumerable[0].FindElement(By.TagName("strong")).Text.NormalizeText();
                    }
                    else
                    {
                        MarkTransactionsModel cacheModel = new()
                        {
                            TransactionType = transactionType,
                            NotificationDate = enumerable[0].Text.CustomConvertToDatetime(),
                            TransactionDate = enumerable[1].Text.CustomConvertToDatetime(),
                            Name = enumerable[2].Text.NormalizeText()
                        };
                        //Açıklama kısmında tablo var ise o ekleniyor
                        if (enumerable[3].FindElements(By.TagName("tbody")).Any())
                        {
                            var detailRows = enumerable[3].FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
                            var cacheMarkTransactionDetails = detailRows.Select(detailRow => new MarkTransactionDetail()
                            {
                                DecisionReason = detailRow.FindElements(By.TagName("td"))[0].Text.NormalizeText(),
                                AboutMark = detailRow.FindElements(By.TagName("td"))[1].Text.NormalizeText()
                            }).ToList();
                            cacheModel.MarkTransactionDetails = cacheMarkTransactionDetails;
                        }
                        else
                        {
                            cacheModel.Description = enumerable[3].Text.NormalizeText();
                        }
                        cacheMarkTransactions.Add(cacheModel);
                    }
                }
                markModel.Transactions = cacheMarkTransactions;
            }

            #endregion

            return markModel;
        }
        public static DesignModel GetDesignData(this IWebDriver driver)
        {
            driver.WaitAjaxLoad();
            if (!driver.IsDataAccessible()) return null;

            DesignModel designModel = new();
            var sections = driver.FindElements(By.TagName("fieldset"), 20);
            var webElements = sections.ToList();
            #region Section Dosya Bilgileri


            var cacheRows = webElements.FirstOrDefault()?.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
            designModel.ApplicationNumber = cacheRows?[0].FindElements(By.TagName("td"))[1].Text.NormalizeText();
            designModel.ApplicationDate = cacheRows?[1].FindElements(By.TagName("td"))[1].Text.CustomConvertToDatetime();
            designModel.RegistrationNumber = cacheRows?[2].FindElements(By.TagName("td"))[1].Text.NormalizeText();
            designModel.RegistrationDate = cacheRows?[3].FindElements(By.TagName("td"))[1].Text.CustomConvertToDatetime();
            designModel.BulletinDate = cacheRows?[5].FindElements(By.TagName("td"))[1].Text.CustomConvertToDatetime();
            designModel.Status = cacheRows?[6].FindElements(By.TagName("td"))[1].Text.NormalizeText();
            #endregion
            #region Section Başvuru Sahipleri

            var cacheSection = webElements.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Başvuru Sahipleri");
            if (cacheSection != null)
            {
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                var cacheHolders = cacheRows.Select(cacheRow => cacheRow.FindElements(By.TagName("td")))
                    .Select(cacheHolderCols => new HolderModel()
                    {
                        HolderCode = cacheHolderCols[0].Text.NormalizeText(),
                        HolderName = cacheHolderCols[1].FindElements(By.TagName("h6")).FirstOrDefault()?.Text.NormalizeText(),
                        Address = cacheHolderCols[1].FindElements(By.TagName("h6")).LastOrDefault()?.Text.NormalizeText()
                    }).ToList();
                designModel.Holders = cacheHolders;
            }
            #endregion
            #region Section Tasarımcılar

            cacheSection = webElements.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Tasarımcılar");
            if (cacheSection != null)
            {
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                var cacheDesigners = cacheRows.Select(cacheRow =>
                    cacheRow.FindElement(By.TagName("td")).FindElements(By.TagName("h6")).FirstOrDefault()?.Text
                        .NormalizeText()).ToList();
                designModel.Designers = cacheDesigners;
            }
            #endregion
            #region Section Vekil Bilgileri
            cacheSection = webElements.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Vekil Bilgileri");
            if (cacheSection != null)
            {
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("h6")).ToList();
                var attorneyCompanyIndex = cacheRows[0].Text.LastIndexOf('(');
                designModel.AttorneyCompanyName = (attorneyCompanyIndex != -1)
                    ? cacheRows[0].Text.Substring(attorneyCompanyIndex + 1, cacheRows[0].Text.Length - 1)
                    : null;
                designModel.AttorneyName = (attorneyCompanyIndex != -1)
                    ? cacheRows[0].Text.Substring(0, attorneyCompanyIndex - 1)
                    : cacheRows[0].Text;
                designModel.AttorneyAddress = cacheRows[1].Text.NormalizeText();
            }
            #endregion
            #region Section Tasarımlar

            cacheSection = webElements.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Section Tasarımlar");
            if (cacheSection != null)
            {
                List<ProductModel> cacheProductModels = new();
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                foreach (var cacheRow in cacheRows)
                {
                    var cacheCols = cacheRow.FindElements(By.TagName("td"));
                    ProductModel cacheProductModel = new()
                    {
                        Name = cacheCols[1].Text.NormalizeText(),
                        LocarnoClass = cacheCols[2].Text.Contains(",") ? cacheCols[2].Text.Split(',') : new[] { cacheCols[2].Text.NormalizeText() },
                        ProductImages = cacheCols[3].FindElements(By.TagName("img")).Select(x => x.GetAttribute("src")).Select(x => x.FixImageBase64()).ToArray()
                    };

                    cacheProductModel.IsProductApproved = Ioc.Resolve<IProcessor>(nameof(Processor)).IsProductImageApproved(cacheProductModel.ProductImages.FirstOrDefault()).Result;
                    if (cacheCols[4].FindElements(By.TagName("tbody")).Any())
                    {
                        var cachePriortyRows = cacheCols[4].FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
                        cacheProductModel.PriortyApplicationNumber = cachePriortyRows[0].FindElements(By.TagName("td")).Last().Text.Split('/')[0].Replace(" ", "").NormalizeText();
                        cacheProductModel.PriortyCountry = cachePriortyRows[0].FindElements(By.TagName("td")).Last().Text.Split('/')[1].Replace(" ", "").NormalizeText();
                        cacheProductModel.ExhibitionName = cachePriortyRows[1].FindElements(By.TagName("td")).Last().Text.Split('/')[0].Replace(" ", "").NormalizeText();
                        cacheProductModel.ExhibitionPlace = cachePriortyRows[1].FindElements(By.TagName("td")).Last().Text.Split('/')[1].Replace(" ", "").NormalizeText();
                        cacheProductModel.ExhibitionDate = cachePriortyRows[2].FindElements(By.TagName("td")).Last().Text.CustomConvertToDatetime();
                        cacheProductModel.FirstExhibitionDate = cachePriortyRows[3].FindElements(By.TagName("td")).Last().Text.CustomConvertToDatetime();
                        cacheProductModel.PriortyDate = cachePriortyRows[4].FindElements(By.TagName("td")).Last().Text.Split('/')[0].Replace(" ", "").CustomConvertToDatetime();
                        cacheProductModel.PriortyType = cachePriortyRows[4].FindElements(By.TagName("td")).Last().Text.Split('/')[1].Replace(" ", "").NormalizeText();
                    }
                    cacheProductModels.Add(cacheProductModel);
                }
                designModel.Products = cacheProductModels;
            }
            #endregion
            #region Section Başvuru İşlem Bilgileri

            cacheSection = webElements.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Başvuru İşlem Bilgileri");
            if (cacheSection == null) return designModel;
            {
                List<DesignTransactionModel> cacheDesignTransactionModel = new();
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                var transactionType = default(string);
                var transactionDetail = default(string);
                foreach (var cols in cacheRows.Select(cacheRow => cacheRow.FindElements(By.TagName("td"))))
                {
                    if (cols.Count == 1)
                    {
                        var cacheTransactionTypeText = cols[0].FindElement(By.TagName("strong")).Text.NormalizeText();
                        transactionType = cacheTransactionTypeText.Contains("(") ? cacheTransactionTypeText.Substring(0, cacheTransactionTypeText.IndexOf('(')) : cacheTransactionTypeText.NormalizeText();
                        transactionDetail = cacheTransactionTypeText.Contains("(") ? cacheTransactionTypeText.Substring(cacheTransactionTypeText.IndexOf('(') + 1, cacheTransactionTypeText.Length - 1) : null;
                    }
                    else
                    {
                        DesignTransactionModel cacheModel = new()
                        {
                            TransactionType = transactionType,
                            TransactionDetail = transactionDetail,
                            Date = cols[0].Text.CustomConvertToDatetime(),
                            Description = cols[1].Text.Contains("(") ? cols[1].Text.Substring(0, cols[1].Text.IndexOf('(')) : cols[1].Text.NormalizeText(),
                            DescriptionDetail = cols[1].Text.Contains("(") ? cols[1].Text.Substring(cols[1].Text.IndexOf('(') + 1, cols[1].Text.Length - 1) : null
                        };
                        cacheDesignTransactionModel.Add(cacheModel);
                    }
                }
                designModel.DesignTransactions = cacheDesignTransactionModel;
            }
            #endregion

            return designModel;
        }
        public static PatentModel GetPatentData(this IWebDriver driver)
        {
            driver.WaitAjaxLoad();
            if (!driver.IsDataAccessible()) return null;

            PatentModel patentModel = new();

            var sections = driver.FindElements(By.TagName("fieldset"), 20);
            var webElements = sections.ToList();
            #region Section Başvuru Bilgileri


            var cacheRows = webElements.FirstOrDefault()?.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
            patentModel.ApplicationNumber = cacheRows?[0].FindElements(By.TagName("td"))[1].Text.NormalizeText();
            patentModel.ApplicationDate = cacheRows?[0].FindElements(By.TagName("td"))[3].Text.CustomConvertToDatetime();
            patentModel.ApplicationType = cacheRows?[1].FindElements(By.TagName("td"))[1].Text.NormalizeText();
            patentModel.DocumentNumber = cacheRows?[1].FindElements(By.TagName("td"))[3].Text.NormalizeText();
            patentModel.DocumentDate = cacheRows?[2].FindElements(By.TagName("td"))[1].Text.CustomConvertToDatetime();
            patentModel.RegistrationNumber = cacheRows?[2].FindElements(By.TagName("td"))[3].Text.NormalizeText();
            patentModel.RegistrationDate = cacheRows?[3].FindElements(By.TagName("td"))[1].Text.CustomConvertToDatetime();
            patentModel.ProtectionType = cacheRows?[3].FindElements(By.TagName("td"))[3].Text.NormalizeText();
            patentModel.EpcPublishNumber = cacheRows?[4].FindElements(By.TagName("td"))[1].Text.NormalizeText();
            patentModel.EpcApplicationNumber = cacheRows?[4].FindElements(By.TagName("td"))[3].Text.NormalizeText();
            patentModel.PctPublishNumber = cacheRows?[5].FindElements(By.TagName("td"))[1].Text.NormalizeText();
            patentModel.PctApplicationNumber = cacheRows?[5].FindElements(By.TagName("td"))[3].Text.NormalizeText();
            patentModel.PctPublishDate = cacheRows?[6].FindElements(By.TagName("td"))[1].Text.CustomConvertToDatetime();
            #endregion
            #region Section Başvuru Sahipleri

            var cacheSection = webElements.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Başvuru Sahipleri");
            if (cacheSection != null)
            {
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                var cacheHolders = cacheRows.Select(cacheRow => new HolderModel()
                {
                    HolderCode = cacheRow.FindElements(By.TagName("td"))[0].Text.NormalizeText(),
                    HolderName = cacheRow.FindElements(By.TagName("td"))[1].Text.NormalizeText(),
                    Address = cacheRow.FindElements(By.TagName("td"))[2].Text.NormalizeText()
                }).ToList();
                patentModel.Holders = cacheHolders;
            }
            #endregion
            #region Section Buluş Sahipleri

            cacheSection = webElements.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Buluş Sahipleri");
            if (cacheSection != null)
            {
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                var cacheInventors = cacheRows.Select(cacheRow => new InventorModel()
                {
                    InventorCode = cacheRow.FindElements(By.TagName("td"))[0].Text.NormalizeText(),
                    InventorName = cacheRow.FindElements(By.TagName("td"))[1].Text.NormalizeText(),
                    Address = cacheRow.FindElements(By.TagName("td"))[2].Text.NormalizeText()
                }).ToList();
                patentModel.Inventors = cacheInventors;
            }
            #endregion
            #region Section Buluş Bilgileri
            cacheSection = webElements.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Buluş Bilgileri");
            if (cacheSection != null)
            {
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                patentModel.InventionTitle = cacheRows[0].FindElements(By.TagName("td"))[1].Text.NormalizeText();
                patentModel.InventionSummary = cacheRows[1].FindElements(By.TagName("td"))[1].Text.NormalizeText();
            }
            #endregion
            #region Section Vekil Bilgileri
            cacheSection = webElements.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Vekil Bilgileri");
            if (cacheSection != null)
            {
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                patentModel.AttorneyName = cacheRows[0].FindElements(By.TagName("td"))[1].FindElements(By.TagName("h6"))[0].Text.NormalizeText();
                patentModel.AttorneyCompanyName = cacheRows[0].FindElements(By.TagName("td"))[1].FindElements(By.TagName("h6"))[1].Text.NormalizeText();
                patentModel.AttorneyCompanyAddress = cacheRows[1].FindElements(By.TagName("td"))[1].Text.NormalizeText();
            }
            #endregion
            #region Section Buluşun Tasnif Sınıfları
            cacheSection = webElements.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Buluşun Tasnif Sınıfları");
            if (cacheSection != null)
            {
                List<PatentClassesModel> cachePatentClasses = new();
                var cacheTables = cacheSection.FindElements(By.TagName("table")).ToList();
                foreach (var cacheTable in cacheTables)
                {
                    var cacheClassTitle = cacheTable.FindElement(By.TagName("th")).Text.NormalizeText();
                    cacheRows = cacheTable.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                    cachePatentClasses.AddRange(cacheRows.Select(cacheRow => new PatentClassesModel()
                    {
                        Name = cacheRow.FindElement(By.TagName("td")).Text.NormalizeText(),
                        Type = cacheClassTitle
                    }));
                }
                patentModel.PatentClasses = cachePatentClasses;
            }
            #endregion
            #region Section Başvuruya İlişkin Bilgiler
            cacheSection = webElements.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Başvuruya İlişkin Bilgiler");
            if (cacheSection != null)
            {
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                var cachePatentTransactions = cacheRows.Select(cacheRow => new PatentTransactionModel()
                {
                    Date = cacheRow.FindElements(By.TagName("td"))[0].Text.CustomConvertToDatetime(),
                    NotificationDate = cacheRow.FindElements(By.TagName("td"))[1].Text.CustomConvertToDatetime(),
                    Transaction = cacheRow.FindElements(By.TagName("td"))[2].Text.NormalizeText()
                }).ToList();
                patentModel.PatentTransactions = cachePatentTransactions;
            }
            #endregion
            #region Section Yayınlar
            cacheSection = webElements.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Yayınlar");
            if (cacheSection != null)
            {
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                var cachePatentPublication = cacheRows.Select(cacheRow => new PatentPublicationModel()
                {
                    PublishDate = cacheRow.FindElements(By.TagName("td"))[0].Text.CustomConvertToDatetime(),
                    Description = cacheRow.FindElements(By.TagName("td"))[1].Text.NormalizeText()
                }).ToList();
                patentModel.PatentPublications = cachePatentPublication;
            }
            #endregion
            #region Section Ödeme Tarihleri
            cacheSection = webElements.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Ödeme Tarihleri");
            if (cacheSection != null)
            {
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                var cachePatentPayments = cacheRows.Select(cacheRow => new PatentPaymentModel()
                {
                    Queue = cacheRow.FindElements(By.TagName("td"))[0].Text.NormalizeText(),
                    Year = cacheRow.FindElements(By.TagName("td"))[1].Text.NormalizeText(),
                    PaymentDate = cacheRow.FindElements(By.TagName("td"))[2].Text.CustomConvertToDatetime(),
                    PaidAmount = cacheRow.FindElements(By.TagName("td"))[3].Text.NormalizeText()
                }).ToList();
                patentModel.PatentPayments = cachePatentPayments;
            }
            #endregion
            #region Section Rüçhan Bilgileri
            cacheSection = webElements.FirstOrDefault(x => x.FindElement(By.TagName("legend")).Text == "Rüçhan Bilgileri");
            if (cacheSection != null)
            {
                cacheRows = cacheSection.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                var cachePatentpriorties = cacheRows.Select(cacheRow => new PatentPriortyModel()
                {
                    PriortyDate = cacheRow.FindElements(By.TagName("td"))[0].Text.CustomConvertToDatetime(),
                    PriortyNumber = cacheRow.FindElements(By.TagName("td"))[1].Text.NormalizeText(),
                    PriortyCountry = cacheRow.FindElements(By.TagName("td"))[2].Text.NormalizeText()
                }).ToList();
                patentModel.PatentPriorties = cachePatentpriorties;
            }
            #endregion
            #region Patent'le İlişkili Pdf'ler
            patentModel.DocumentsUrl = $"{RuntimeConfigs.GeneralConfig.TpConfig.TpPatentPdfPage}?patentAppNo={patentModel.ApplicationNumber}&documentsTpye=all";
            #region İnceleme Raporu Pdf'i
            patentModel.AnalysisReportUrl = $"{RuntimeConfigs.GeneralConfig.TpConfig.TpPatentPdfPage}?patentAppNo={patentModel.ApplicationNumber}&documentsTpye=inceleme";
            #endregion
            #region Araştırma Raporu Pdf'i
            patentModel.ResearchReportUrl = $"{RuntimeConfigs.GeneralConfig.TpConfig.TpPatentPdfPage}?patentAppNo={patentModel.ApplicationNumber}&documentsTpye=arastirma";
            #endregion

            #endregion

            return patentModel;
        }

        private static void CheckAndSolveCaptcha(this IWebDriver driver)
        {
            try
            {
                driver.ClickWithJs(driver.FindElement(By.ClassName("swal2-confirm")));
            }
            catch
            {
                // ignored
            }
        }
        private static void CheckAndCloseAlert(this IWebDriver driver)
        {
            if (driver.FindElements(By.XPath("//*[@id=\"__next\"]/div/section")).Any())
            {
                driver.ClickWithJs(driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/section"), 20).FindElement(By.TagName("span")));
            }
        }
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {

            if (timeoutInSeconds <= 0) return driver.FindElement(by);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            try
            {
                return wait.Until(ExpectedConditions.ElementToBeClickable(by));
            }
            catch
            {
                driver.CheckAndCloseAlert();
                driver.CheckAndSolveCaptcha();
                return wait.Until(ExpectedConditions.ElementToBeClickable(by));
            }
        }
        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, By by, int timeoutInSeconds)
        {

            if (timeoutInSeconds <= 0) return driver.FindElements(by);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            try
            {
                return wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
            }
            catch
            {
                driver.CheckAndCloseAlert();
                driver.CheckAndSolveCaptcha();
                return wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
            }

        }
        //private static Func<IWebDriver, ReadOnlyCollection<IWebElement>> PresenceOfAllElementsLocatedBy(
        //    By locator,IWebElement element)
        //{
        //    return driver =>
        //    {
        //        try
        //        {
        //            var elements =
        //                driver.FindElement(By.XPath(driver.GetXPath(element))).FindElements(locator);
        //            return elements.Any() ? elements : null;
        //        }
        //        catch (StaleElementReferenceException)
        //        {
        //            return null;
        //        }
        //    };
        //}
        //private static Func<IWebDriver, IWebElement> ElementExists(By locator,IWebElement element) => driver => driver.FindElement(By.XPath(driver.GetXPath(element))).FindElement(locator);
        //private static IWebElement ElementIfVisible(IWebElement element) => !element.Displayed ? null : element;
        public static bool CheckPageIsLastAndClick(this IWebDriver driver)
        {
            var nextPageButton = driver.FindElement(By.ClassName("MuiTablePagination-root"), 20).FindElements(By.TagName("button")).FirstOrDefault(x => x.GetAttribute("title") == "Next page");
            if (nextPageButton != null && nextPageButton.GetAttribute("class").Contains("Mui-disabled"))
                return true;

            driver.ClickWithJs(nextPageButton);
            driver.WaitAjaxLoad();
            return false;
        }

        public static int GetDataCount(this IWebDriver driver)
        {
            var time = 20;
            while (!driver.FindElement(By.XPath("//*[@id=\"search-results\"]"), 20).FindElements(By.TagName("div")).Any())
            {
                Thread.Sleep(500);
                time--;
                if (time == 0)
                {
                    throw new Exception("Sorgulama sonucu yüklenmiyor.");
                }
            }

            if (driver.FindElement(By.XPath("//*[@id=\"search-results\"]"), 20).Text == "Sonuç bulunamadı")
                return 0;
            var value = driver.FindElement(By.XPath("//*[@id=\"search-results\"]/div[2]/p"), 20).Text.Substring(0,
                driver.FindElement(By.XPath("//*[@id=\"search-results\"]/div[2]/p"), 20).Text.IndexOf("kayıt bulundu", StringComparison.Ordinal)).Replace(" ", "");

            return int.Parse(value);
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




        public enum SearchType
        {
            Trademark,
            Patent,
            Design
        }
    }
}
