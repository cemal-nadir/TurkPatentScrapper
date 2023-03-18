using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.WebServices.Scrap.API.ControllerServices.Abstract;
using TPHunter.WebServices.Shared.MainData.Core.Models;
using TPHunter.WebServices.Shared.MainData.Core.Services;
using TPHunter.WebServices.Shared.Utility.Core.Abstract.FileStorage;

namespace TPHunter.WebServices.Scrap.API.ControllerServices.Contracts
{
    public class TradeMarkService : IControllerService<MarkModel>
    {

        private const string BucketName = "SistemPatent";
        private const string SubFileDirectoryName = "TradeMark";

        private readonly IService<TradeMark> _trademarkService;
        private readonly IService<TradeMarkStatus> _trademarkStatusService;
        private readonly IService<TradeMarkType> _trademarkTypeService;
        private readonly IService<Attorney> _attorneyService;
        private readonly IService<AttorneyCompany> _attorneyCompanyService;
        private readonly IService<TradeMarkDecision> _trademarkDecisionService;
        private readonly IService<TradeMarkDecisionReason> _trademarkDecisionReasonService;
        private readonly IService<TradeMarkPriorty> _trademarkPriortyService;
        private readonly IService<TradeMarkPriortyCountry> _trademarkPriortyCountryService;
        private readonly IService<TradeMarkServices> _trademarkServicesService;
        private readonly IService<TradeMarkTransaction> _trademarkTransactionService;
        private readonly IService<TradeMarkTransactionDescription> _trademarkTransactionDescriptionService;
        private readonly IService<TradeMarkTransactionDetail> _trademarkTransactionDetailService;
        private readonly IService<TradeMarkTransactionName> _trademarkTransactionNameService;
        private readonly IService<TradeMarkTransactionType> _trademarkTransactionTypeService;
        private readonly IService<Holder> _holderService;
        private readonly IService<HolderRelation> _holderRelationService;
        private readonly IFileTransferManager _fileTransferManager;
        public TradeMarkService(IService<TradeMark> trademarkService,
            IService<TradeMarkStatus> trademarkStatusService,
            IService<TradeMarkType> trademarkTypeService,
            IService<Attorney> attorneyService,
            IService<AttorneyCompany> attorneyCompanyService,
            IService<TradeMarkDecision> trademarkDecisionService,
            IService<TradeMarkDecisionReason> trademarkDecisionReasonService,
            IService<TradeMarkPriorty> trademarkPriortyService,
            IService<TradeMarkPriortyCountry> trademarkPriortyCountryService,
            IService<TradeMarkServices> trademarkServiceServices,
            IService<TradeMarkTransaction> trademarkTransactionService,
            IService<TradeMarkTransactionDescription> trademarkTransactionDescriptionService,
            IService<TradeMarkTransactionDetail> trademarkTransactionDetailService,
            IService<TradeMarkTransactionName> trademarkTransactionNameService,
            IService<TradeMarkTransactionType> trademarkTransactionTypeService,
            IService<Holder> holderService,
            IService<HolderRelation> holderRelationService,
            IFileTransferManager fileTransferManager
            )
        {
            _trademarkService = trademarkService;
            _trademarkStatusService = trademarkStatusService;
            _trademarkTypeService = trademarkTypeService;
            _attorneyService = attorneyService;
            _attorneyCompanyService = attorneyCompanyService;
            _trademarkDecisionService = trademarkDecisionService;
            _trademarkDecisionReasonService = trademarkDecisionReasonService;
            _trademarkPriortyService = trademarkPriortyService;
            _trademarkPriortyCountryService = trademarkPriortyCountryService;
            _trademarkServicesService = trademarkServiceServices;
            _fileTransferManager = fileTransferManager;
            _trademarkTransactionService = trademarkTransactionService;
            _trademarkTransactionDescriptionService = trademarkTransactionDescriptionService;
            _trademarkTransactionDetailService = trademarkTransactionDetailService;
            _trademarkTransactionNameService = trademarkTransactionNameService;
            _trademarkTransactionTypeService = trademarkTransactionTypeService;
            _holderService = holderService;
            _holderRelationService = holderRelationService;
        }

        public async Task<int> GetLastPulledCountAsync(ISearchParam searchParam)
        {
            return await _trademarkService.GetCountAsync(x =>
                x.DeclareBullettinNumber == searchParam.BulletinNumber.ToString());
        }

        public async Task<IEnumerable<string>> GetLastPulledApplicationNumbersAsync(ISearchParam searchParam)
        {
            return await _trademarkService.AsNoTracking.Where(x =>
                    x.DeclareBullettinNumber == searchParam.BulletinNumber.ToString())
                .Select(x => x.ApplicationNumber).ToListAsync();
        }

        public async Task<IEnumerable<Guid>> GetLastPulledIdsAsync(ISearchParam searchParam)
        {
            return await _trademarkService.AsNoTracking.Where(x =>
                    x.DeclareBullettinNumber == searchParam.BulletinNumber.ToString())
                .Select(x => x.Id).ToListAsync();
        }

        public async Task InsertAsync(MarkModel model)
        {
            TradeMark dbModel = new();

            #region Check TradeMark Is Exists
            if ((await _trademarkService.IsDataExists(x => x.ApplicationNumber == model.ApplicationNumber)))
            {
                await UpdateAsync(model);
                return;
            }
            #endregion

            #region TradeMark
            dbModel.ApplicationNumber = model.ApplicationNumber;
            dbModel.Name = model.Name;
            dbModel.ApplicationDate = model.ApplicationDate;
            dbModel.Classes = model.Classes;
            dbModel.DeclareBullettinDate = model.DeclareBullettinDate;
            dbModel.DocumentNumber = model.DocumentNumber;
            #region Save TradeMark Image
            dbModel.ImageId = await _fileTransferManager.Upload(model.ImageText, ".jpg", BucketName, SubFileDirectoryName);
            #endregion
            dbModel.InternationalRegistrationNumber = model.InternationalRegistrationNumber;
            dbModel.Name = model.Name;
            dbModel.ProtectionDate = model.ProtectionDate;
            dbModel.RegistrationBullettinDate = model.RegistrationBullettinDate;
            dbModel.RegistrationBullettinNumber = model.RegistrationBullettinNumber;
            dbModel.RegistrationDate = model.RegistrationDate;
            dbModel.RegistrationNumber = model.RegistrationNumber;
            dbModel.DeclareBullettinNumber = model.Bulletin;
            #endregion

            #region TradeMark Status
            if (!string.IsNullOrEmpty(model.Status))
            {
                var status = await _trademarkStatusService.SingleOrDefaultAsync(x => x.Status == model.Status) ?? await _trademarkStatusService.AddAsync(new TradeMarkStatus()
                {
                    Status = model.Status
                });
                dbModel.TradeMarkStatusId = status.Id;

            }
            #endregion

            #region TradeMark Type
            if (!string.IsNullOrEmpty(model.Type))
            {
                var tradeMarkType = await _trademarkTypeService.SingleOrDefaultAsync(x => x.Type == model.Type) ?? await _trademarkTypeService.AddAsync(new TradeMarkType()
                {
                    Type = model.Type
                });
                dbModel.TradeMarkTypeId = tradeMarkType.Id;
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

            #region TradeMark Decision
            if (!string.IsNullOrEmpty(model.Decision))
            {
                TradeMarkDecisionReason decisionReason = default;
                if (!string.IsNullOrEmpty(model.DecisionReason))
                {
                    decisionReason = await _trademarkDecisionReasonService.SingleOrDefaultAsync(x => x.Reason == model.DecisionReason) ??
                                     await _trademarkDecisionReasonService.AddAsync(new TradeMarkDecisionReason()
                    {
                        Reason = model.DecisionReason
                    });
                }
                var decision = await _trademarkDecisionService.SingleOrDefaultAsync(x => x.Decision == model.Decision) ??
                               await _trademarkDecisionService.AddAsync(new TradeMarkDecision()
                {
                    Decision = model.Decision,
                    TradeMarkDecisionReasonId = decisionReason?.Id
                });
                dbModel.TradeMarkDecisionId = decision.Id;

            }
            #endregion

            #region TradeMark Priorty
            if (!string.IsNullOrEmpty(model.PriortyInformation))
            {
                var priortyInformations = model.PriortyInformation.Split(' ').Select(x => x.Replace(" ", "")).ToArray();
                TradeMarkPriortyCountry tradeMarkPriortyCountry = default;
                if (priortyInformations.Length == 3)
                {
                    tradeMarkPriortyCountry = await _trademarkPriortyCountryService.SingleOrDefaultAsync(x => x.Country == priortyInformations[1]) ??
                                              await _trademarkPriortyCountryService.AddAsync(new TradeMarkPriortyCountry()
                    {
                        Country = priortyInformations[1]
                    });
                }
                var tradeMarkPriorty = await _trademarkPriortyService.AddAsync(new TradeMarkPriorty()
                {
                    TradeMarkPriortyCountryId = tradeMarkPriortyCountry?.Id,
                    Date = Convert.ToDateTime(priortyInformations[0]),
                    ApplicationNumber = priortyInformations[2]
                });
                dbModel.TradeMarkPriortyId = tradeMarkPriorty.Id;

            }
            #endregion

            #region Save TradeMark
            dbModel = await _trademarkService.AddAsync(dbModel);
            #endregion

            #region TradeMark Services

            if (model.Services != null)
                foreach (var service in model.Services)
                {
                    await _trademarkServicesService.AddAsync(new TradeMarkServices()
                    {
                        Class = service.Class,
                        Service = service.Service,
                        TradeMarkId = dbModel.Id
                    });
                }

            #endregion

            #region TradeMark Transactions

            if (model.Transactions != null)
                foreach (var transaction in model.Transactions)
                {
                    var transactionType =
                        await _trademarkTransactionTypeService.SingleOrDefaultAsync(x =>
                            x.Type == transaction.TransactionType) ??
                        await _trademarkTransactionTypeService.AddAsync(new TradeMarkTransactionType()
                        {
                            Type = transaction.TransactionType
                        });

                    TradeMarkTransactionName tradeMarkTransactionName = default;
                    if (!string.IsNullOrEmpty(transaction.Name))
                    {
                        tradeMarkTransactionName =
                            await _trademarkTransactionNameService.SingleOrDefaultAsync(x =>
                                x.Transaction == transaction.Name) ??
                            await _trademarkTransactionNameService.AddAsync(new TradeMarkTransactionName()
                            {
                                Transaction = transaction.Name
                            });
                    }

                    TradeMarkTransactionDescription tradeMarkTransactionDescription = default;
                    if (!string.IsNullOrEmpty(transaction.Description))
                    {
                        tradeMarkTransactionDescription =
                            await _trademarkTransactionDescriptionService.SingleOrDefaultAsync(x =>
                                x.Description == transaction.Description) ??
                            await _trademarkTransactionDescriptionService.AddAsync(new TradeMarkTransactionDescription()
                            {
                                Description = transaction.Description
                            });
                    }

                    var transactionModel = await _trademarkTransactionService.AddAsync(new TradeMarkTransaction()
                    {
                        NotificationDate = transaction.NotificationDate,
                        TradeMarkId = dbModel.Id,
                        TradeMarkTransactionDescriptionId = tradeMarkTransactionDescription?.Id,
                        TradeMarkTransactionNameId = tradeMarkTransactionName?.Id,
                        TradeMarkTransactionTypeId = transactionType.Id,
                        TransactionDate = transaction.TransactionDate
                    });


                    if (transaction.MarkTransactionDetails == null ||
                        !transaction.MarkTransactionDetails.Any()) continue;
                    foreach (var transactionDetail in transaction.MarkTransactionDetails)
                    {
                        await _trademarkTransactionDetailService.AddAsync(new TradeMarkTransactionDetail()
                        {
                            AboutMark = transactionDetail.AboutMark,
                            DecisionReason = transactionDetail.DecisionReason,
                            TradeMarkTransactionId = transactionModel.Id
                        });
                    }
                }

            #endregion

            #region TradeMark Holders

            if (model.Holders != null)
                foreach (var holder in model.Holders)
                {
                    var holderModel =
                        await _holderService.SingleOrDefaultAsync(x => x.HolderCode == holder.HolderCode) ??
                        await _holderService.AddAsync(new Holder()
                        {
                            HolderCode = holder.HolderCode,
                            HolderName = holder.HolderName,
                            Address = holder.Address
                        });

                    await _holderRelationService.AddAsync(new HolderRelation()
                    {
                        DataId = dbModel.Id,
                        DataType = DataType.Trademark,
                        HolderId = holderModel.Id
                    });
                }

            #endregion

        }

        public async Task RemoveAsync(string applicationNumber)
        {
            #region Remove TradeMark
            var model = await _trademarkService.SingleOrDefaultAsync(x => x.ApplicationNumber == applicationNumber);
            if (model is null)
                return;
            _trademarkService.Remove(model);
            #endregion
            #region Remove Holder Relations
            var holderRelations = await _holderRelationService.Where(x => x.DataId == model.Id && x.DataType == DataType.Trademark);
            var enumerable = holderRelations.ToList();
            if (enumerable.Any())
                _holderRelationService.RemoveRange(enumerable);
            #endregion
        }

        public async Task RemoveAsync(Guid ıd)
        {
            #region Remove TradeMark
            var model = await _trademarkService.SingleOrDefaultAsync(x => x.Id == ıd);
            if (model is not null)
                _trademarkService.Remove(model);
            #endregion
            #region Remove Holder Relations
            var holderRelations = await _holderRelationService.Where(x => x.DataId == ıd && x.DataType == DataType.Trademark);
            var enumerable = holderRelations.ToList();
            if (enumerable.Any())
                _holderRelationService.RemoveRange(enumerable);
            #endregion
        }

        public async Task UpdateAsync(MarkModel model)
        {
            var dbModel = await _trademarkService.SingleOrDefaultAsync(x => x.ApplicationNumber == model.ApplicationNumber);

            #region Check TradeMark Is Not Exists
            if (dbModel is null)
            {
                await InsertAsync(model);
                return;
            }
            #endregion

            #region TradeMark
            dbModel.ApplicationNumber = model.ApplicationNumber;
            dbModel.Name = model.Name;
            dbModel.ApplicationDate = model.ApplicationDate;
            dbModel.Classes = model.Classes;
            dbModel.DeclareBullettinDate = model.DeclareBullettinDate;
            dbModel.DocumentNumber = model.DocumentNumber;
            #region Save TradeMark Image
            await _fileTransferManager.DeleteFile(dbModel.ImageId, ".jpg", BucketName, SubFileDirectoryName);
            dbModel.ImageId = await _fileTransferManager.Upload(model.ImageText, ".jpg", BucketName, SubFileDirectoryName);
            #endregion
            dbModel.InternationalRegistrationNumber = model.InternationalRegistrationNumber;
            dbModel.Name = model.Name;
            dbModel.ProtectionDate = model.ProtectionDate;
            dbModel.RegistrationBullettinDate = model.RegistrationBullettinDate;
            dbModel.RegistrationBullettinNumber = model.RegistrationBullettinNumber;
            dbModel.RegistrationDate = model.RegistrationDate;
            dbModel.RegistrationNumber = model.RegistrationNumber;
            #endregion

            #region TradeMark Status
            if (!string.IsNullOrEmpty(model.Status))
            {
                var status = await _trademarkStatusService.SingleOrDefaultAsync(x => x.Status == model.Status) ?? await _trademarkStatusService.AddAsync(new TradeMarkStatus()
                {
                    Status = model.Status
                });
                dbModel.TradeMarkStatusId = status.Id;

            }
            #endregion

            #region TradeMark Type
            if (!string.IsNullOrEmpty(model.Type))
            {
                var tradeMarkType = await _trademarkTypeService.SingleOrDefaultAsync(x => x.Type == model.Type) ?? await _trademarkTypeService.AddAsync(new TradeMarkType()
                {
                    Type = model.Type
                });
                dbModel.TradeMarkTypeId = tradeMarkType.Id;
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

            #region TradeMark Decision
            if (!string.IsNullOrEmpty(model.Decision))
            {
                TradeMarkDecisionReason decisionReason = default;
                if (!string.IsNullOrEmpty(model.DecisionReason))
                {
                    decisionReason = await _trademarkDecisionReasonService.SingleOrDefaultAsync(x => x.Reason == model.DecisionReason) ??
                                     await _trademarkDecisionReasonService.AddAsync(new TradeMarkDecisionReason()
                    {
                        Reason = model.DecisionReason
                    });
                }
                var decision = await _trademarkDecisionService.SingleOrDefaultAsync(x => x.Decision == model.Decision) ??
                               await _trademarkDecisionService.AddAsync(new TradeMarkDecision()
                {
                    Decision = model.Decision,
                    TradeMarkDecisionReasonId = decisionReason?.Id
                });
                dbModel.TradeMarkDecisionId = decision.Id;

            }
            #endregion

            #region TradeMark Priorty
            if (!string.IsNullOrEmpty(model.PriortyInformation))
            {
                var priortyInformations = model.PriortyInformation.Split(' ').Select(x => x.Replace(" ", "")).ToArray();
                TradeMarkPriortyCountry tradeMarkPriortyCountry = default;
                if (priortyInformations.Length == 3)
                {
                    tradeMarkPriortyCountry = await _trademarkPriortyCountryService.SingleOrDefaultAsync(x => x.Country == priortyInformations[1]) ??
                                              await _trademarkPriortyCountryService.AddAsync(new TradeMarkPriortyCountry()
                    {
                        Country = priortyInformations[1]
                    });
                }
                var tradeMarkPriorty = await _trademarkPriortyService.AddAsync(new TradeMarkPriorty()
                {
                    TradeMarkPriortyCountryId = tradeMarkPriortyCountry?.Id,
                    Date = Convert.ToDateTime(priortyInformations[0]),
                    ApplicationNumber = priortyInformations[2]
                });
                dbModel.TradeMarkPriortyId = tradeMarkPriorty.Id;

            }
            #endregion

            #region Save TradeMark
            dbModel = _trademarkService.Update(dbModel);
            #endregion

            #region TradeMark Services
            var removeServices = await _trademarkServicesService.Where(x => x.TradeMarkId == dbModel.Id);
            var tradeMarkServicesEnumerable = removeServices.ToList();
            if (tradeMarkServicesEnumerable.Any())
                _trademarkServicesService.RemoveRange(tradeMarkServicesEnumerable);

            if (model.Services != null)
                foreach (var service in model.Services)
                {
                    await _trademarkServicesService.AddAsync(new TradeMarkServices()
                    {
                        Class = service.Class,
                        Service = service.Service,
                        TradeMarkId = dbModel.Id
                    });
                }

            #endregion

            #region TradeMark Transactions
            var removeTransactions = await _trademarkTransactionService.Where(x => x.TradeMarkId == dbModel.Id);
            var tradeMarkTransactions = removeTransactions.ToList();
            if (tradeMarkTransactions.Any())
                _trademarkTransactionService.RemoveRange(tradeMarkTransactions);
            if (model.Transactions != null)
                foreach (var transaction in model.Transactions)
                {
                    var transactionType =
                        await _trademarkTransactionTypeService.SingleOrDefaultAsync(x =>
                            x.Type == transaction.TransactionType) ??
                        await _trademarkTransactionTypeService.AddAsync(new TradeMarkTransactionType()
                        {
                            Type = transaction.TransactionType
                        });

                    TradeMarkTransactionName tradeMarkTransactionName = default;
                    if (!string.IsNullOrEmpty(transaction.Name))
                    {
                        tradeMarkTransactionName =
                            await _trademarkTransactionNameService.SingleOrDefaultAsync(x =>
                                x.Transaction == transaction.Name) ??
                            await _trademarkTransactionNameService.AddAsync(new TradeMarkTransactionName()
                            {
                                Transaction = transaction.Name
                            });
                    }

                    TradeMarkTransactionDescription tradeMarkTransactionDescription = default;
                    if (!string.IsNullOrEmpty(transaction.Description))
                    {
                        tradeMarkTransactionDescription =
                            await _trademarkTransactionDescriptionService.SingleOrDefaultAsync(x =>
                                x.Description == transaction.Description) ??
                            await _trademarkTransactionDescriptionService.AddAsync(new TradeMarkTransactionDescription()
                            {
                                Description = transaction.Description
                            });
                    }

                    var transactionModel = await _trademarkTransactionService.AddAsync(new TradeMarkTransaction()
                    {
                        NotificationDate = transaction.NotificationDate,
                        TradeMarkId = dbModel.Id,
                        TradeMarkTransactionDescriptionId = tradeMarkTransactionDescription?.Id,
                        TradeMarkTransactionNameId = tradeMarkTransactionName?.Id,
                        TradeMarkTransactionTypeId = transactionType.Id,
                        TransactionDate = transaction.TransactionDate
                    });


                    if (transaction.MarkTransactionDetails == null ||
                        !transaction.MarkTransactionDetails.Any()) continue;
                    foreach (var transactionDetail in transaction.MarkTransactionDetails)
                    {
                        await _trademarkTransactionDetailService.AddAsync(new TradeMarkTransactionDetail()
                        {
                            AboutMark = transactionDetail.AboutMark,
                            DecisionReason = transactionDetail.DecisionReason,
                            TradeMarkTransactionId = transactionModel.Id
                        });
                    }
                }

            #endregion

            #region TradeMark Holders
            var holderRelations = await _holderRelationService.Where(x => x.DataId == dbModel.Id && x.DataType == DataType.Trademark);
            var enumerable = holderRelations.ToList();
            if (enumerable.Any())
                _holderRelationService.RemoveRange(enumerable);

            if (model.Holders != null)
                foreach (var holder in model.Holders)
                {
                    var holderModel =
                        await _holderService.SingleOrDefaultAsync(x => x.HolderCode == holder.HolderCode) ??
                        await _holderService.AddAsync(new Holder()
                        {
                            HolderCode = holder.HolderCode,
                            HolderName = holder.HolderName,
                            Address = holder.Address
                        });

                    await _holderRelationService.AddAsync(new HolderRelation()
                    {
                        DataId = dbModel.Id,
                        DataType = DataType.Trademark,
                        HolderId = holderModel.Id
                    });
                }

            #endregion

        }
    }
}
