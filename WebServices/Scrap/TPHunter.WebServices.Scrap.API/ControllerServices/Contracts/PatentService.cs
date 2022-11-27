using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.WebServices.Scrap.API.ControllerServices.Abstract;
using TPHunter.WebServices.Scrap.PatentPdf.Abstract;
using TPHunter.WebServices.Shared.MainData.Core.Models;
using TPHunter.WebServices.Shared.MainData.Core.Services;
using TPHunter.WebServices.Shared.Utility.Core.Abstract.FileStorage;

namespace TPHunter.WebServices.Scrap.API.ControllerServices.Contracts
{
    public class PatentService : IControllerService<PatentModel>
    {
        private const string BucketName = "SistemPatent";
        private const string SubFileDirectoryName = "Patent";

        private readonly IService<Attorney> _attorneyService;
        private readonly IService<AttorneyCompany> _attorneyCompanyService;
        private readonly IService<Holder> _holderService;
        private readonly IService<HolderRelation> _holderRelationService;
        private readonly IService<Patent> _patentService;
        private readonly IService<Shared.MainData.Core.Models.PatentPdf> _patentPdfService;
        private readonly IService<PatentProtectionType> _patentProtectionTypeService;
        private readonly IService<PatentApplicationType> _patentApplicationTypeService;
        private readonly IService<Inventor> _inventorService;
        private readonly IService<InventorRelation> _inventorRelationService;
        private readonly IService<PatentPriorty> _patentPriortyService;
        private readonly IService<PatentPriortyCountry> _patentPriortyCountryService;
        private readonly IService<PatentClass> _patentClassService;
        private readonly IService<PatentClassType> _patentClassTypeService;
        private readonly IService<PatentClassRelation> _patentClassRelationService;
        private readonly IService<PatentTransaction> _patentTransactionService;
        private readonly IService<PatentTransactionName> _patentTransactionNameService;
        private readonly IService<PatentPublication> _patentPublicationService;
        private readonly IService<PatentPublicationDescription> _patentPublicationDescriptionService;
        private readonly IService<PatentPayment> _patentPaymentService;
        private readonly IFileTransferManager _fileTransferManager;
        private readonly IPdfDownloaderService _pdfDownloaderService;

        public PatentService(IService<Attorney> attorneyService, IService<AttorneyCompany> attorneyCompanyService,
            IService<Holder> holderService, IService<HolderRelation> holderRelationService,
            IFileTransferManager fileTransferManager, IService<Patent> patentService, IService<Shared.MainData.Core.Models.PatentPdf> patentPdfService,
            IService<PatentApplicationType> patentApplicationTypeService, IService<PatentProtectionType> patentProtectionTypeService, IService<Inventor> inventorService, IService<InventorRelation> inventorRelationService, IService<PatentPriorty> patentPriortyService, IService<PatentPriortyCountry> patentPriortyCountryService, IService<PatentClass> patentClassService, IService<PatentClassType> patentClassTypeService, IService<PatentClassRelation> patentClassRelationService, IService<PatentTransaction> patentTransactionService, IService<PatentTransactionName> patentTransactionNameService, IService<PatentPublication> patentPublicationService, IService<PatentPublicationDescription> patentPublicationDescriptionService, IService<PatentPayment> patentPaymentService, IPdfDownloaderService pdfDownloaderService)
        {
            _attorneyService = attorneyService;
            _attorneyCompanyService = attorneyCompanyService;
            _holderService = holderService;
            _holderRelationService = holderRelationService;
            _fileTransferManager = fileTransferManager;
            _patentService = patentService;
            _patentPdfService = patentPdfService;
            _patentApplicationTypeService = patentApplicationTypeService;
            _patentProtectionTypeService = patentProtectionTypeService;
            _inventorService = inventorService;
            _inventorRelationService = inventorRelationService;
            _patentPriortyService = patentPriortyService;
            _patentPriortyCountryService = patentPriortyCountryService;
            _patentClassService = patentClassService;
            _patentClassTypeService = patentClassTypeService;
            _patentClassRelationService = patentClassRelationService;
            _patentTransactionService = patentTransactionService;
            _patentTransactionNameService = patentTransactionNameService;
            _patentPublicationService = patentPublicationService;
            _patentPublicationDescriptionService = patentPublicationDescriptionService;
            _patentPaymentService = patentPaymentService;
            _pdfDownloaderService = pdfDownloaderService;
        }

        public async Task<IEnumerable<string>> GetLastPulledApplicationNumbersAsync(ISearchParam searchParam)
        {
            return await _patentService.AsNoTracking.Where(x =>
                x.ApplicationDate >= ((DateRangeParam)searchParam).StartDate && x.ApplicationDate <= ((DateRangeParam)searchParam).EndDate).Select(x=>x.ApplicationNumber).ToListAsync();
        }

        public async Task<int> GetLastPulledCountAsync(ISearchParam searchParam)
        {
            return await _patentService.GetCountAsync(x =>
                x.ApplicationDate>= ((DateRangeParam)searchParam).StartDate&&x.ApplicationDate <= ((DateRangeParam)searchParam).EndDate);
        }

        public async Task<IEnumerable<Guid>> GetLastPulledIdsAsync(ISearchParam searchParam)
        {
            return await _patentService.AsNoTracking.Where(x =>
                x.ApplicationDate >= ((DateRangeParam)searchParam).StartDate && x.ApplicationDate <= ((DateRangeParam)searchParam).EndDate).Select(x => x.Id).ToListAsync();
        }

        public async Task InsertAsync(PatentModel model)
        {
            Patent dbModel = new();

            #region Check Patent Is Exists
            if ((await _patentService.IsDataExists(x => x.ApplicationNumber == model.ApplicationNumber)))
            {
                await UpdateAsync(model);
                return;
            }
            #endregion

            #region Patent

            dbModel.ApplicationDate = model.ApplicationDate;
            dbModel.ApplicationNumber = model.ApplicationNumber;
            dbModel.DocumentDate = model.DocumentDate;
            dbModel.DocumentNumber = model.DocumentNumber;
            dbModel.EpcApplicationNumber = model.EpcApplicationNumber;
            dbModel.EpcPublishNumber = model.EpcPublishNumber;
            dbModel.InventionSummary = model.InventionSummary;
            dbModel.InventionTitle = model.InventionTitle;
            dbModel.PctApplicationNumber = model.PctApplicationNumber;
            dbModel.PctPublishDate = model.PctPublishDate;
            dbModel.PctPublishNumber = model.PctPublishNumber;
            dbModel.RegistrationDate = model.RegistrationDate;
            dbModel.RegistrationNumber = model.RegistrationNumber;

            #endregion

            #region Patent Application Type

            if (!string.IsNullOrEmpty(model.ApplicationType))
            {
                var applicationType = await _patentApplicationTypeService.SingleOrDefaultAsync(x => x.Type == model.ApplicationType);
                applicationType ??= await _patentApplicationTypeService.AddAsync(new PatentApplicationType()
                {
                    Type = model.ApplicationType
                });
                dbModel.PatentApplicationTypeId = applicationType.Id;
            }

            #endregion

            #region Patent Protection Type

            if (!string.IsNullOrEmpty(model.ProtectionType))
            {
                var protectionType = await _patentProtectionTypeService.SingleOrDefaultAsync(x => x.Type == model.ProtectionType);
                protectionType ??= await _patentProtectionTypeService.AddAsync(new PatentProtectionType()
                {
                    Type = model.ProtectionType
                });
                dbModel.PatentProtectionTypeId = protectionType.Id;
            }

            #endregion

            #region Attorney
            if (!string.IsNullOrEmpty(model.AttorneyName))
            {
                AttorneyCompany company = default;
                if (!string.IsNullOrEmpty(model.AttorneyCompanyName))
                {
                    company = await _attorneyCompanyService.SingleOrDefaultAsync(x => x.Name == model.AttorneyCompanyName) ??
                              await _attorneyCompanyService.AddAsync(new AttorneyCompany()
                              {
                                  Name = model.AttorneyCompanyName
                              });
                }
                var attorney = await _attorneyService.SingleOrDefaultAsync(x => x.Name == model.AttorneyName) ?? await _attorneyService.AddAsync(new Attorney()
                {
                    Name = model.AttorneyName,
                    AttorneyCompanyId = company?.Id
                });
                dbModel.AttorneyId = attorney.Id;
            }
            #endregion

            #region Save Patent

            dbModel = await _patentService.AddAsync(dbModel);

            #endregion

            #region Patent Inventors

            foreach (var inventor in model.Inventors)
            {
                var inventorModel = await _inventorService.SingleOrDefaultAsync(x => x.InventorCode == inventor.InventorCode);
                inventorModel ??= await _inventorService.AddAsync(new Inventor()
                {
                    Address = inventor.Address,
                    InventorCode = inventor.InventorCode,
                    InventorName = inventor.InventorName
                });
                await _inventorRelationService.AddAsync(new InventorRelation()
                {
                    InventorId = inventorModel.Id,
                    PatentId = dbModel.Id
                });
            }

            #endregion

            #region Patent Priorties

            foreach (var patentPriorty in model.PatentPriorties)
            {
                PatentPriortyCountry patentPriortyCountry = default;
                if (!string.IsNullOrEmpty(patentPriorty.PriortyCountry))
                    patentPriortyCountry =
                        await _patentPriortyCountryService.SingleOrDefaultAsync(x =>
                            x.Country == patentPriorty.PriortyCountry) ?? await _patentPriortyCountryService.AddAsync(
                            new PatentPriortyCountry()
                            {
                                Country = patentPriorty.PriortyCountry
                            });
                await _patentPriortyService.AddAsync(new PatentPriorty()
                {
                    PatentId = dbModel.Id,
                    PatentPriortyCountryId = patentPriortyCountry?.Id,
                    PriortyDate = patentPriorty.PriortyDate,
                    PriortyNumber = patentPriorty.PriortyNumber
                });
            }


            #endregion

            #region Patent Classes

            foreach (var patentClass in model.PatentClasses)
            {
                #region Patent Class Type

                PatentClassType patentClassType = default;
                if (!string.IsNullOrEmpty(patentClass.Type))
                    patentClassType =
                        await _patentClassTypeService.SingleOrDefaultAsync(x => x.Type == patentClass.Type) ??
                        await _patentClassTypeService.AddAsync(new PatentClassType()
                        {
                            Type = patentClass.Type
                        });


                #endregion

                #region Patent Class
                PatentClass patentClassModel = default;
                if (!string.IsNullOrEmpty(patentClass.Name))
                    patentClassModel =
                        await _patentClassService.SingleOrDefaultAsync(x => x.Name == patentClass.Name) ??
                        await _patentClassService.AddAsync(new PatentClass()
                        {
                            Name = patentClass.Name,
                            PatentClassTypeId = patentClassType.Id
                        });


                #endregion

                #region Patent Class Relation

                await _patentClassRelationService.AddAsync(new PatentClassRelation()
                {
                    PatentId = dbModel.Id,
                    PatentClassId = patentClassModel.Id
                });


                #endregion

            }


            #endregion

            #region Patent Transactions

            foreach (var patentTransaction in model.PatentTransactions)
            {
                var patentTransactionName =
                    await _patentTransactionNameService.SingleOrDefaultAsync(x =>
                        x.Transaction == patentTransaction.Transaction) ?? await _patentTransactionNameService.AddAsync(
                        new PatentTransactionName()
                        {
                            Transaction = patentTransaction.Transaction
                        });
                await _patentTransactionService.AddAsync(new PatentTransaction()
                {
                    Date = patentTransaction.Date,
                    NotificationDate = patentTransaction.NotificationDate,
                    PatentId = dbModel.Id,
                    PatentTransactionNameId = patentTransactionName.Id
                });
            }


            #endregion

            #region Patent Publications

            foreach (var patentPublication in model.PatentPublications)
            {
                var patentPublicationDescription =
                    await _patentPublicationDescriptionService.SingleOrDefaultAsync(x =>
                        x.Description == patentPublication.Description) ??
                    await _patentPublicationDescriptionService.AddAsync(new PatentPublicationDescription()
                    {
                        Description = patentPublication.Description
                    });

                await _patentPublicationService.AddAsync(new PatentPublication()
                {
                    PatentId = dbModel.Id,
                    PatentPublicationDescriptionId = patentPublicationDescription.Id,
                    PublishDate = patentPublication.PublishDate
                });
            }

            #endregion

            #region Patent Payments

            foreach (var patentPayment in model.PatentPayments)
            {
                await _patentPaymentService.AddAsync(new PatentPayment()
                {
                    PaidAmount = decimal.Parse(patentPayment.PaidAmount, CultureInfo.InvariantCulture),
                    PatentId = dbModel.Id,
                    PaymentDate = patentPayment.PaymentDate,
                    Queue = int.Parse(patentPayment.Queue),
                    Year = int.Parse(patentPayment.Year)
                });
            }

            #endregion

            #region Patent Holders

            foreach (var holder in model.Holders)
            {
                var holderModel = await _holderService.SingleOrDefaultAsync(x => x.HolderCode == holder.HolderCode) ??
                                  await _holderService.AddAsync(new Holder()
                                  {
                                      HolderCode = holder.HolderCode,
                                      HolderName = holder.HolderName,
                                      Address = holder.Address
                                  });

                await _holderRelationService.AddAsync(new HolderRelation()
                {
                    DataId = dbModel.Id,
                    DataType = DataType.Patent,
                    HolderId = holderModel.Id

                });
            }

            #endregion

            #region Patent Pdf's

            #region Analyses Report

            var analysisReport = await _pdfDownloaderService.DownloadPdf(model.AnalysisReportUrl);
            if (analysisReport != null)
            {
                await _patentPdfService.AddAsync(new Shared.MainData.Core.Models.PatentPdf()
                {
                    PatentId = dbModel.Id,
                    PdfType = PdfType.AnalysisReport,
                    FileId = await _fileTransferManager.Upload(analysisReport, ".pdf", BucketName,
                       SubFileDirectoryName)
                });
            }



            #endregion

            #region Document

            var document = await _pdfDownloaderService.DownloadPdf(model.DocumentsUrl);
            if (document != null)
            {
                await _patentPdfService.AddAsync(new Shared.MainData.Core.Models.PatentPdf()
                {
                    PatentId = dbModel.Id,
                    PdfType = PdfType.Documents,
                    FileId = await _fileTransferManager.Upload(document, ".pdf", BucketName,
                        SubFileDirectoryName)
                });
            }

            #endregion

            #region Research Report

            var researchReport = await _pdfDownloaderService.DownloadPdf(model.ResearchReportUrl);
            if (researchReport != null)
            {
                await _patentPdfService.AddAsync(new Shared.MainData.Core.Models.PatentPdf()
                {
                    PatentId = dbModel.Id,
                    PdfType = PdfType.ResearchReport,
                    FileId = await _fileTransferManager.Upload(researchReport, ".pdf", BucketName,
                        SubFileDirectoryName)
                });
            }

            #endregion

            #endregion
        }

        public async Task RemoveAsync(string applicationNumber)
        {
            #region Remove Patent
            var model = await _patentService.SingleOrDefaultAsync(x => x.ApplicationNumber == applicationNumber);
            if (model is null)
                return;
            #region Remove Patent Pdf's On File Storage
            var patentPdFs = await _patentPdfService.AsNoTracking.Where(x => x.PatentId == model.Id).ToListAsync();
            foreach (var patentPdf in patentPdFs)
            {
                await _fileTransferManager.DeleteFile(patentPdf.FileId, ".pdf", BucketName,
                    SubFileDirectoryName);
            }
            #endregion
            _patentService.Remove(model);
            #endregion
            #region Remove Holder Relations
            var holderRelations = await _holderRelationService.Where(x => x.DataId == model.Id && x.DataType == DataType.Patent, default);
            var enumerable = holderRelations.ToList();
            if (enumerable.Any())
                _holderRelationService.RemoveRange(enumerable);
            #endregion
        }

        public async Task RemoveAsync(Guid ıd)
        {
            #region Remove Patent
            var model = await _patentService.SingleOrDefaultAsync(x => x.Id == ıd);
            if (model is null)
                return;
            #region Remove Patent Pdf's On File Storage
            var patentPdFs = await _patentPdfService.AsNoTracking.Where(x => x.PatentId == model.Id).ToListAsync();
            foreach (var patentPdf in patentPdFs)
            {
                await _fileTransferManager.DeleteFile(patentPdf.FileId, ".pdf", BucketName,
                    SubFileDirectoryName);
            }
            #endregion
            _patentService.Remove(model);
            #endregion
            #region Remove Holder Relations
            var holderRelations = await _holderRelationService.Where(x => x.DataId == model.Id && x.DataType == DataType.Patent, default);
            var enumerable = holderRelations.ToList();
            if (enumerable.Any())
                _holderRelationService.RemoveRange(enumerable);
            #endregion
        }

        public async Task UpdateAsync(PatentModel model)
        {
            var dbModel = await _patentService.SingleOrDefaultAsync(x => x.ApplicationNumber == model.ApplicationNumber);

            #region Check Patent Is Not Exists
            if (dbModel is null)
            {
                await InsertAsync(model);
                return;
            }
            #endregion

            #region Patent

            dbModel.ApplicationDate = model.ApplicationDate;
            dbModel.ApplicationNumber = model.ApplicationNumber;
            dbModel.DocumentDate = model.DocumentDate;
            dbModel.DocumentNumber = model.DocumentNumber;
            dbModel.EpcApplicationNumber = model.EpcApplicationNumber;
            dbModel.EpcPublishNumber = model.EpcPublishNumber;
            dbModel.InventionSummary = model.InventionSummary;
            dbModel.InventionTitle = model.InventionTitle;
            dbModel.PctApplicationNumber = model.PctApplicationNumber;
            dbModel.PctPublishDate = model.PctPublishDate;
            dbModel.PctPublishNumber = model.PctPublishNumber;
            dbModel.RegistrationDate = model.RegistrationDate;
            dbModel.RegistrationNumber = model.RegistrationNumber;

            #endregion

            #region Patent Application Type

            if (!string.IsNullOrEmpty(model.ApplicationType))
            {
                var applicationType = await _patentApplicationTypeService.SingleOrDefaultAsync(x => x.Type == model.ApplicationType);
                applicationType ??= await _patentApplicationTypeService.AddAsync(new PatentApplicationType()
                {
                    Type = model.ApplicationType
                });
                dbModel.PatentApplicationTypeId = applicationType.Id;
            }

            #endregion

            #region Patent Protection Type

            if (!string.IsNullOrEmpty(model.ProtectionType))
            {
                var protectionType = await _patentProtectionTypeService.SingleOrDefaultAsync(x => x.Type == model.ProtectionType);
                protectionType ??= await _patentProtectionTypeService.AddAsync(new PatentProtectionType()
                {
                    Type = model.ProtectionType
                });
                dbModel.PatentProtectionTypeId = protectionType.Id;
            }

            #endregion

            #region Attorney
            if (!string.IsNullOrEmpty(model.AttorneyName))
            {
                AttorneyCompany company = default;
                if (!string.IsNullOrEmpty(model.AttorneyCompanyName))
                {
                    company = await _attorneyCompanyService.SingleOrDefaultAsync(x => x.Name == model.AttorneyCompanyName) ??
                              await _attorneyCompanyService.AddAsync(new AttorneyCompany()
                              {
                                  Name = model.AttorneyCompanyName
                              });
                }
                var attorney = await _attorneyService.SingleOrDefaultAsync(x => x.Name == model.AttorneyName) ?? await _attorneyService.AddAsync(new Attorney()
                {
                    Name = model.AttorneyName,
                    AttorneyCompanyId = company?.Id
                });
                dbModel.AttorneyId = attorney.Id;
            }
            #endregion

            #region Save Patent

            dbModel = _patentService.Update(dbModel);

            #endregion

            #region Patent Inventors

            var removeInventorRelations = await _inventorRelationService.Where(x => x.PatentId == dbModel.Id, default);
            var ınventorRelations = removeInventorRelations.ToList();
            if (ınventorRelations.Any())
                _inventorRelationService.RemoveRange(ınventorRelations);
            foreach (var inventor in model.Inventors)
            {
                var inventorModel = await _inventorService.SingleOrDefaultAsync(x => x.InventorCode == inventor.InventorCode);
                inventorModel ??= await _inventorService.AddAsync(new Inventor()
                {
                    Address = inventor.Address,
                    InventorCode = inventor.InventorCode,
                    InventorName = inventor.InventorName
                });
                await _inventorRelationService.AddAsync(new InventorRelation()
                {
                    InventorId = inventorModel.Id,
                    PatentId = dbModel.Id
                });
            }

            #endregion

            #region Patent Priorties

            var removePatentPriorties = await _patentPriortyService.Where(x => x.PatentId == dbModel.Id, default);
            var patentPriorties = removePatentPriorties.ToList();
            if (patentPriorties.Any())
                _patentPriortyService.RemoveRange(patentPriorties);
            foreach (var patentPriorty in model.PatentPriorties)
            {
                PatentPriortyCountry patentPriortyCountry = default;
                if (!string.IsNullOrEmpty(patentPriorty.PriortyCountry))
                    patentPriortyCountry =
                        await _patentPriortyCountryService.SingleOrDefaultAsync(x =>
                            x.Country == patentPriorty.PriortyCountry) ?? await _patentPriortyCountryService.AddAsync(
                            new PatentPriortyCountry()
                            {
                                Country = patentPriorty.PriortyCountry
                            });
                await _patentPriortyService.AddAsync(new PatentPriorty()
                {
                    PatentId = dbModel.Id,
                    PatentPriortyCountryId = patentPriortyCountry?.Id,
                    PriortyDate = patentPriorty.PriortyDate,
                    PriortyNumber = patentPriorty.PriortyNumber
                });
            }


            #endregion

            #region Patent Classes

            var removePatentClassRelations = await _patentClassRelationService.Where(x => x.PatentId == dbModel.Id, default);
            var patentClassRelations = removePatentClassRelations.ToList();
            if (patentClassRelations.Any())
                _patentClassRelationService.RemoveRange(patentClassRelations);
            foreach (var patentClass in model.PatentClasses)
            {
                #region Patent Class Type

                PatentClassType patentClassType = default;
                if (!string.IsNullOrEmpty(patentClass.Type))
                    patentClassType =
                        await _patentClassTypeService.SingleOrDefaultAsync(x => x.Type == patentClass.Type) ??
                        await _patentClassTypeService.AddAsync(new PatentClassType()
                        {
                            Type = patentClass.Type
                        });


                #endregion

                #region Patent Class
                PatentClass patentClassModel = default;
                if (!string.IsNullOrEmpty(patentClass.Name))
                    patentClassModel =
                        await _patentClassService.SingleOrDefaultAsync(x => x.Name == patentClass.Name) ??
                        await _patentClassService.AddAsync(new PatentClass()
                        {
                            Name = patentClass.Name,
                            PatentClassTypeId = patentClassType.Id
                        });


                #endregion

                #region Patent Class Relation

                await _patentClassRelationService.AddAsync(new PatentClassRelation()
                {
                    PatentId = dbModel.Id,
                    PatentClassId = patentClassModel.Id
                });


                #endregion

            }


            #endregion

            #region Patent Transactions

            var removePatentTransactions = await _patentTransactionService.Where(x => x.PatentId == dbModel.Id, default);
            var patentTransactions = removePatentTransactions.ToList();
            if (patentTransactions.Any())
                _patentTransactionService.RemoveRange(patentTransactions);
            foreach (var patentTransaction in model.PatentTransactions)
            {
                var patentTransactionName =
                    await _patentTransactionNameService.SingleOrDefaultAsync(x =>
                        x.Transaction == patentTransaction.Transaction) ?? await _patentTransactionNameService.AddAsync(
                        new PatentTransactionName()
                        {
                            Transaction = patentTransaction.Transaction
                        });
                await _patentTransactionService.AddAsync(new PatentTransaction()
                {
                    Date = patentTransaction.Date,
                    NotificationDate = patentTransaction.NotificationDate,
                    PatentId = dbModel.Id,
                    PatentTransactionNameId = patentTransactionName.Id
                });
            }


            #endregion

            #region Patent Publications

            var removePatentPublications =
                await _patentPublicationService.Where(x => x.PatentId == dbModel.Id, default);
            var patentPublications = removePatentPublications.ToList();
            if (patentPublications.Any())
                _patentPublicationService.RemoveRange(patentPublications);
            foreach (var patentPublication in model.PatentPublications)
            {
                var patentPublicationDescription =
                    await _patentPublicationDescriptionService.SingleOrDefaultAsync(x =>
                        x.Description == patentPublication.Description) ??
                    await _patentPublicationDescriptionService.AddAsync(new PatentPublicationDescription()
                    {
                        Description = patentPublication.Description
                    });

                await _patentPublicationService.AddAsync(new PatentPublication()
                {
                    PatentId = dbModel.Id,
                    PatentPublicationDescriptionId = patentPublicationDescription.Id,
                    PublishDate = patentPublication.PublishDate
                });
            }

            #endregion

            #region Patent Payments

            var removePatentPayments = await _patentPaymentService.Where(x => x.PatentId == dbModel.Id, default);
            var patentPayments = removePatentPayments.ToList();
            if (patentPayments.Any())
                _patentPaymentService.RemoveRange(patentPayments);
            foreach (var patentPayment in model.PatentPayments)
            {
                await _patentPaymentService.AddAsync(new PatentPayment()
                {
                    PaidAmount = decimal.Parse(patentPayment.PaidAmount, CultureInfo.InvariantCulture),
                    PatentId = dbModel.Id,
                    PaymentDate = patentPayment.PaymentDate,
                    Queue = int.Parse(patentPayment.Queue),
                    Year = int.Parse(patentPayment.Year)
                });
            }

            #endregion

            #region Patent Holders

            var removeHolderRelations =
                await _holderRelationService.Where(x => x.DataId == dbModel.Id && x.DataType == DataType.Patent,
                    default);
            var holderRelations = removeHolderRelations.ToList();
            if (holderRelations.Any())
                _holderRelationService.RemoveRange(holderRelations);
            foreach (var holder in model.Holders)
            {
                var holderModel = await _holderService.SingleOrDefaultAsync(x => x.HolderCode == holder.HolderCode) ??
                                  await _holderService.AddAsync(new Holder()
                                  {
                                      HolderCode = holder.HolderCode,
                                      HolderName = holder.HolderName,
                                      Address = holder.Address
                                  });

                await _holderRelationService.AddAsync(new HolderRelation()
                {
                    DataId = dbModel.Id,
                    DataType = DataType.Patent,
                    HolderId = holderModel.Id

                });
            }

            #endregion

            #region Patent Pdf's

            #region Analyses Report

            var removePdFs =await _patentPdfService.Where(x => x.PatentId == dbModel.Id,default);
            foreach (var removePdF in removePdFs)
            {
                await _fileTransferManager.DeleteFile(removePdF.FileId, ".pdf", BucketName, SubFileDirectoryName);
                _patentPdfService.Remove(removePdF);
            }
            var analysisReport = await _pdfDownloaderService.DownloadPdf(model.AnalysisReportUrl);
            if (analysisReport != null)
            {
                await _patentPdfService.AddAsync(new Shared.MainData.Core.Models.PatentPdf()
                {
                    PatentId = dbModel.Id,
                    PdfType = PdfType.AnalysisReport,
                    FileId = await _fileTransferManager.Upload(analysisReport, ".pdf", BucketName,
                       SubFileDirectoryName)
                });
            }



            #endregion

            #region Document

            var document = await _pdfDownloaderService.DownloadPdf(model.DocumentsUrl);
            if (document != null)
            {
                await _patentPdfService.AddAsync(new Shared.MainData.Core.Models.PatentPdf()
                {
                    PatentId = dbModel.Id,
                    PdfType = PdfType.Documents,
                    FileId = await _fileTransferManager.Upload(document, ".pdf", BucketName,
                        SubFileDirectoryName)
                });
            }

            #endregion

            #region Research Report

            var researchReport = await _pdfDownloaderService.DownloadPdf(model.ResearchReportUrl);
            if (researchReport != null)
            {
                await _patentPdfService.AddAsync(new Shared.MainData.Core.Models.PatentPdf()
                {
                    PatentId = dbModel.Id,
                    PdfType = PdfType.ResearchReport,
                    FileId = await _fileTransferManager.Upload(researchReport, ".pdf", BucketName,
                        SubFileDirectoryName)
                });
            }

            #endregion

            #endregion
        }
    }
}
