using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.WebServices.Scrap.API.ControllerServices.Abstract;
using TPHunter.WebServices.Shared.MainData.Core.Models;
using TPHunter.WebServices.Shared.MainData.Core.Services;
using TPHunter.WebServices.Shared.Utility.Core.Abstract.FileStorage;

namespace TPHunter.WebServices.Scrap.API.ControllerServices.Contracts
{
    public class DesignService : IControllerService<DesignModel>
    {
        private const string BucketName = "SistemPatent";
        private const string SubFileDirectoryName = "Design";

        private readonly IService<Attorney> _attorneyService;
        private readonly IService<AttorneyCompany> _attorneyCompanyService;
        private readonly IService<Holder> _holderService;
        private readonly IService<HolderRelation> _holderRelationService;
        private readonly IService<Design> _designService;
        private readonly IService<DesignStatus> _designStatusService;
        private readonly IService<Designer> _designerService;
        private readonly IService<DesignerRelation> _designerRelationService;
        private readonly IService<DesignTransaction> _designTransactionService;
        private readonly IService<DesignTransactionDescription> _designTransactionDescriptionService;
        private readonly IService<DesignTransactionDescriptionDetail> _designTransactionDescriptionDetailService;
        private readonly IService<DesignTransactionType> _designTransactionTypeService;
        private readonly IService<DesignTransactionTypeDetail> _designTransactionTypeDetailService;
        private readonly IService<DesignProduct> _designProductService;
        private readonly IService<DesignProductClassesRelation> _designProductClassesRelationService;
        private readonly IService<DesignProductImage> _designProductImageService;
        private readonly IService<DesignProductPriortyCountry> _designProductPriortyCountryService;
        private readonly IService<DesignProductPriortyType> _designProductPriortyTypeService;
        private readonly IService<LocarnoClass> _locarnoClassService;
        private readonly IFileTransferManager _fileTransferManager;

        public DesignService(
            IService<Attorney> attorneyService,
            IService<AttorneyCompany> attorneyCompanyService,
            IService<Holder> holderService,
            IService<HolderRelation> holderRelationService,
            IService<Design> designService,
            IService<DesignStatus> designStatusService,
            IService<Designer> designerService,
            IService<DesignerRelation> designerRelationService,
            IService<DesignTransaction> designTransactionService,
            IService<DesignTransactionDescription> designTransactionDescriptionService,
            IService<DesignTransactionDescriptionDetail> designTransactionDescriptionDetailService,
            IService<DesignTransactionType> designTransactionTypeService,
            IService<DesignTransactionTypeDetail> designTransactionTypeDetailService,
            IFileTransferManager fileTransferManager, IService<DesignProduct> designProductService,
            IService<DesignProductClassesRelation> designProductClassesRelationService,
            IService<DesignProductImage> designProductImageService,
            IService<DesignProductPriortyCountry> designProductPriortyCountryService,
            IService<DesignProductPriortyType> designProductPriortyTypeService,
            IService<LocarnoClass> locarnoClassService)
        {
            _attorneyService = attorneyService;
            _attorneyCompanyService = attorneyCompanyService;
            _holderService = holderService;
            _holderRelationService = holderRelationService;
            _designService = designService;
            _designStatusService = designStatusService;
            _fileTransferManager = fileTransferManager;
            _designProductService = designProductService;
            _designProductClassesRelationService = designProductClassesRelationService;
            _designProductImageService = designProductImageService;
            _designProductPriortyCountryService = designProductPriortyCountryService;
            _designProductPriortyTypeService = designProductPriortyTypeService;
            _locarnoClassService = locarnoClassService;
            _designerService = designerService;
            _designerRelationService = designerRelationService;
            _designTransactionService = designTransactionService;
            _designTransactionTypeService = designTransactionTypeService;
            _designTransactionTypeDetailService = designTransactionTypeDetailService;
            _designTransactionDescriptionService = designTransactionDescriptionService;
            _designTransactionDescriptionDetailService = designTransactionDescriptionDetailService;
        }

        public async Task<int> GetLastPulledCountAsync(ISearchParam searchParam)
        {
            return await _designService.GetCountAsync(x =>
                x.BulletinNumber == ((BulletinParam)searchParam).BulletinNumber.ToString());
        }

        public async Task<IEnumerable<string>> GetLastPulledApplicationNumbersAsync(ISearchParam searchParam)
        {
            return await _designService.AsNoTracking.Where(x =>
                    x.BulletinNumber == ((BulletinParam)searchParam).BulletinNumber.ToString())
                .Select(x => x.ApplicationNumber).ToListAsync();
        }

        public async Task InsertAsync(DesignModel model)
        {
            Design dbModel = new();

            #region Check Design Is Exists
            if ((await _designService.IsDataExists(x => x.ApplicationNumber == model.ApplicationNumber)))
            {
                await UpdateAsync(model);
                return;
            }
            #endregion

            #region Design
            dbModel.ApplicationNumber = model.ApplicationNumber;
            dbModel.BulletinNumber = model.BulletinNumber;
            dbModel.RegistrationNumber = model.RegistrationNumber;
            dbModel.ApplicationDate = model.ApplicationDate;
            dbModel.BulletinDate = model.BulletinDate;
            dbModel.RegistrationDate = model.RegistrationDate;
            #endregion

            #region Design Status
            if (!string.IsNullOrEmpty(model.Status))
            {
                var designStatus = await _designStatusService.SingleOrDefaultAsync(x => x.Status == model.Status) ??
                                   await _designStatusService.AddAsync(new DesignStatus()
                {
                    Status = model.Status
                });
                dbModel.DesignStatusId = designStatus.Id;
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

            #region Save Design
            dbModel = await _designService.AddAsync(dbModel);
            #endregion

            #region Designers
            foreach (var designer in model.Designers)
            {
                var designerModel = await _designerService.SingleOrDefaultAsync(x => x.Name == designer);
                designerModel ??= await _designerService.AddAsync(new Designer()
                {
                    Name = designer
                });
                await _designerRelationService.AddAsync(new DesignerRelation()
                {
                    DesignerId = designerModel.Id,
                    DesignId = dbModel.Id
                });
            }
            #endregion

            #region Design Transaction
            foreach (var transaction in model.DesignTransactions)
            {
                #region Design Transaction Type
                DesignTransactionTypeDetail designTransactionTypeDetail = default;
                if (!string.IsNullOrEmpty(transaction.TransactionDetail))
                {
                    designTransactionTypeDetail =
                        await _designTransactionTypeDetailService.SingleOrDefaultAsync(x =>
                            x.Detail == transaction.TransactionDetail);
                    designTransactionTypeDetail ??= await _designTransactionTypeDetailService.AddAsync(
                        new DesignTransactionTypeDetail()
                        {
                            Detail = transaction.TransactionDetail
                        });
                }
                var designTransactionType = await _designTransactionTypeService.SingleOrDefaultAsync(x =>
                    x.Type == transaction.TransactionType &&
                    x.DesignTransactionTypeDetailId ==
                    (designTransactionTypeDetail == default ? null : designTransactionTypeDetail.Id));
                designTransactionType ??= await _designTransactionTypeService.AddAsync(
                    new DesignTransactionType()
                    {
                        DesignTransactionTypeDetailId = designTransactionTypeDetail?.Id,
                        Type = transaction.TransactionType
                    });
                #endregion

                #region Design Transaction Description

                DesignTransactionDescriptionDetail designTransactionDescriptionDetail = new();
                if (!string.IsNullOrEmpty(transaction.DescriptionDetail))
                {
                    designTransactionDescriptionDetail =
                        await _designTransactionDescriptionDetailService.SingleOrDefaultAsync(x =>
                            x.Detail == transaction.DescriptionDetail);
                    designTransactionDescriptionDetail ??= await _designTransactionDescriptionDetailService.AddAsync(
                        new DesignTransactionDescriptionDetail()
                        {
                            Detail = transaction.DescriptionDetail
                        });
                }
                var designTransactionDescription = await _designTransactionDescriptionService.SingleOrDefaultAsync(x =>
                    x.Description == transaction.Description &&
                    x.DesignTransactionDescriptionDetailId ==
                    (designTransactionDescriptionDetail == default ? null : designTransactionDescriptionDetail.Id));
                designTransactionDescription ??= await _designTransactionDescriptionService.AddAsync(
                    new DesignTransactionDescription()
                    {
                        DesignTransactionDescriptionDetailId = designTransactionDescriptionDetail?.Id,
                        Description = transaction.Description
                    });
                #endregion


                await _designTransactionService.AddAsync(new DesignTransaction()
                {
                    Date = transaction.Date,
                    DesignId = dbModel.Id,
                    DesignTransactionDescriptionId = designTransactionDescription.Id,
                    DesignTransactionTypeId = designTransactionType.Id

                });
            }
            #endregion

            #region Design Product

            foreach (var product in model.Products)
            {
                DesignProductPriortyCountry designProductPriortyCountry = default;
                DesignProductPriortyType designProductPriortyType = default;

                #region Design Product Priorty Country
                if (!string.IsNullOrEmpty(product.PriortyCountry))
                {
                    designProductPriortyCountry =
                        await _designProductPriortyCountryService.SingleOrDefaultAsync(x =>
                            x.Country == product.PriortyCountry);
                    designProductPriortyCountry ??= await _designProductPriortyCountryService.AddAsync(
                        new DesignProductPriortyCountry()
                        {
                            Country = product.PriortyCountry
                        });
                }
                #endregion

                #region Design Product Priorty Type
                if (!string.IsNullOrEmpty(product.PriortyType))
                {
                    designProductPriortyType =
                        await _designProductPriortyTypeService.SingleOrDefaultAsync(x =>
                            x.Type == product.PriortyType);
                    designProductPriortyType ??= await _designProductPriortyTypeService.AddAsync(
                        new DesignProductPriortyType()
                        {
                            Type = product.PriortyType
                        });
                }
                #endregion

                #region Save Design Product
                var designProduct = await _designProductService.AddAsync(new DesignProduct()
                {
                    DesignProductPriortyTypeId = designProductPriortyType?.Id,
                    DesignId = dbModel.Id,
                    DesignPriortyCountryId = designProductPriortyCountry?.Id,
                    ExhibitionDate = product.ExhibitionDate,
                    ExhibitionName = product.ExhibitionName,
                    ExhibitionPlace = product.ExhibitionPlace,
                    FirstExhibitionDate = product.FirstExhibitionDate,
                    IsProductApproved = product.IsProductApproved,
                    Name = product.Name,
                    PriortyApplicationNumber = product.PriortyApplicationNumber,
                    PriortyDate = product.PriortyDate
                });
                #endregion

                #region Design Product Image

                foreach (var productImage in product.ProductImages)
                {
                    #region Save Design Product Image
                    await _designProductImageService.AddAsync(new DesignProductImage()
                    {
                        DesignProductId = designProduct.Id,
                        ImageId = await _fileTransferManager.Upload(productImage, ".jpg", BucketName,
                            SubFileDirectoryName)
                    });
                    #endregion
                }


                #endregion

                #region Design Product Classes

                foreach (var locarnoClass in product.LocarnoClass)
                {
                    var classModel = await _locarnoClassService.SingleOrDefaultAsync(x => x.Name == locarnoClass);
                    classModel ??= await _locarnoClassService.AddAsync(new LocarnoClass()
                    {
                        Name = locarnoClass
                    });
                    await _designProductClassesRelationService.AddAsync(new DesignProductClassesRelation()
                    {
                        LocarnoClassId = classModel.Id,
                        DesignProductId = designProduct.Id
                    });
                }

                #endregion

            }


            #endregion

            #region Design Holders
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
                    DataType = DataType.Design,
                    HolderId = holderModel.Id

                });
            }

            #endregion
        }

        public async Task RemoveAsync(string applicationNumber)
        {
            #region Remove Design
            var model = await _designService.SingleOrDefaultAsync(x => x.ApplicationNumber == applicationNumber);
            if (model is null)
                return;
            #region Remove Design Images On File Storage
            var designProducts = await _designProductService.AsNoTracking.Include(x => x.DesignProductImages).Where(x => x.DesignId == model.Id).ToListAsync();
            if (designProducts != null)
            {
                foreach (var designProductImages in designProducts.Select(x => x.DesignProductImages))
                {
                    foreach (var designProductImage in designProductImages)
                    {
                        await _fileTransferManager.DeleteFile(designProductImage.ImageId, ".jpg", BucketName,
                            SubFileDirectoryName);
                    }
                }

                _designProductService.RemoveRange(designProducts);
            }
            #endregion
            _designService.Remove(model);
            #endregion
            #region Remove Holder Relations
            var holderRelations = await _holderRelationService.Where(x => x.DataId == model.Id && x.DataType == DataType.Design, default);
            var enumerable = holderRelations.ToList();
            if (enumerable.Any())
                _holderRelationService.RemoveRange(enumerable);
            #endregion
        }

        public async Task RemoveAsync(Guid ıd)
        {
            #region Remove Design
            var model = await _designService.SingleOrDefaultAsync(x => x.Id == ıd);
            if (model is not null)
            {
                #region Remove Design Images On File Storage
                var designProducts = await _designProductService.AsNoTracking.Include(x => x.DesignProductImages).Where(x => x.DesignId == model.Id).ToListAsync();
                if (designProducts != null)
                {
                    foreach (var designProductImages in designProducts.Select(x => x.DesignProductImages))
                    {
                        foreach (var designProductImage in designProductImages)
                        {
                            await _fileTransferManager.DeleteFile(designProductImage.ImageId, ".jpg", BucketName,
                                SubFileDirectoryName);
                        }
                    }

                    _designProductService.RemoveRange(designProducts);
                }
                #endregion

                _designService.Remove(model);
            }
          
            #endregion
            #region Remove Holder Relations
            var holderRelations = await _holderRelationService.Where(x => x.DataId == ıd && x.DataType == DataType.Trademark, default);
            var enumerable = holderRelations.ToList();
            if (enumerable.Any())
                _holderRelationService.RemoveRange(enumerable);
            #endregion
        }

        public async Task UpdateAsync(DesignModel model)
        {
            var dbModel = await _designService.SingleOrDefaultAsync(x => x.ApplicationNumber == model.ApplicationNumber);

            #region Check Design Is Not Exists
            if (dbModel is null)
            {
                await InsertAsync(model);
                return;
            }
            #endregion

            #region Design
            dbModel.ApplicationNumber = model.ApplicationNumber;
            dbModel.BulletinNumber = model.BulletinNumber;
            dbModel.RegistrationNumber = model.RegistrationNumber;
            dbModel.ApplicationDate = model.ApplicationDate;
            dbModel.BulletinDate = model.BulletinDate;
            dbModel.RegistrationDate = model.RegistrationDate;
            #endregion

            #region Design Status
            if (!string.IsNullOrEmpty(model.Status))
            {
                var designStatus = await _designStatusService.SingleOrDefaultAsync(x => x.Status == model.Status) ??
                                   await _designStatusService.AddAsync(new DesignStatus()
                                   {
                                       Status = model.Status
                                   });
                dbModel.DesignStatusId = designStatus.Id;
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

            #region Save Design
            dbModel = _designService.Update(dbModel);
            #endregion

            #region Designers

            var removeDesignerRelations = await _designerRelationService.Where(x => x.DesignId == dbModel.Id,default);
            var designerRelations = removeDesignerRelations.ToList();
            if (designerRelations.Any())
                _designerRelationService.RemoveRange(designerRelations);
            foreach (var designer in model.Designers)
            {
                var designerModel = await _designerService.SingleOrDefaultAsync(x => x.Name == designer);
                designerModel ??= await _designerService.AddAsync(new Designer()
                {
                    Name = designer
                });
                await _designerRelationService.AddAsync(new DesignerRelation()
                {
                    DesignerId = designerModel.Id,
                    DesignId = dbModel.Id
                });
            }
            #endregion

            #region Design Transaction
            var removeDesignTransactions = await _designTransactionService.Where(x => x.DesignId == dbModel.Id, default);

            var designTransactions = removeDesignTransactions.ToList();
            if (designTransactions.Any())
                _designTransactionService.RemoveRange(designTransactions);
            foreach (var transaction in model.DesignTransactions)
            {
                #region Design Transaction Type
                DesignTransactionTypeDetail designTransactionTypeDetail = default;
                if (!string.IsNullOrEmpty(transaction.TransactionDetail))
                {
                    designTransactionTypeDetail =
                        await _designTransactionTypeDetailService.SingleOrDefaultAsync(x =>
                            x.Detail == transaction.TransactionDetail);
                    designTransactionTypeDetail ??= await _designTransactionTypeDetailService.AddAsync(
                        new DesignTransactionTypeDetail()
                        {
                            Detail = transaction.TransactionDetail
                        });
                }
                var designTransactionType = await _designTransactionTypeService.SingleOrDefaultAsync(x =>
                    x.Type == transaction.TransactionType &&
                    x.DesignTransactionTypeDetailId ==
                    (designTransactionTypeDetail == default ? null : designTransactionTypeDetail.Id));
                designTransactionType ??= await _designTransactionTypeService.AddAsync(
                    new DesignTransactionType()
                    {
                        DesignTransactionTypeDetailId = designTransactionTypeDetail?.Id,
                        Type = transaction.TransactionType
                    });
                #endregion

                #region Design Transaction Description

                DesignTransactionDescriptionDetail designTransactionDescriptionDetail = new();
                if (!string.IsNullOrEmpty(transaction.DescriptionDetail))
                {
                    designTransactionDescriptionDetail =
                        await _designTransactionDescriptionDetailService.SingleOrDefaultAsync(x =>
                            x.Detail == transaction.DescriptionDetail);
                    designTransactionDescriptionDetail ??= await _designTransactionDescriptionDetailService.AddAsync(
                        new DesignTransactionDescriptionDetail()
                        {
                            Detail = transaction.DescriptionDetail
                        });
                }
                var designTransactionDescription = await _designTransactionDescriptionService.SingleOrDefaultAsync(x =>
                    x.Description == transaction.Description &&
                    x.DesignTransactionDescriptionDetailId ==
                    (designTransactionDescriptionDetail == default ? null : designTransactionDescriptionDetail.Id));
                designTransactionDescription ??= await _designTransactionDescriptionService.AddAsync(
                    new DesignTransactionDescription()
                    {
                        DesignTransactionDescriptionDetailId = designTransactionDescriptionDetail?.Id,
                        Description = transaction.Description
                    });
                #endregion


                await _designTransactionService.AddAsync(new DesignTransaction()
                {
                    Date = transaction.Date,
                    DesignId = dbModel.Id,
                    DesignTransactionDescriptionId = designTransactionDescription.Id,
                    DesignTransactionTypeId = designTransactionType.Id

                });
            }
            #endregion

            #region Design Product

            #region Remove Design Images On File Storage
            var designProducts = await _designProductService.AsNoTracking.Include(x => x.DesignProductImages).Where(x => x.DesignId == dbModel.Id).ToListAsync();
            if (designProducts != null)
            {
                foreach (var designProductImages in designProducts.Select(x => x.DesignProductImages))
                {
                    foreach (var designProductImage in designProductImages)
                    {
                        await _fileTransferManager.DeleteFile(designProductImage.ImageId, ".jpg", BucketName,
                            SubFileDirectoryName);
                    }
                }

                _designProductService.RemoveRange(designProducts);
            }
            #endregion
            foreach (var product in model.Products)
            {
                DesignProductPriortyCountry designProductPriortyCountry = default;
                DesignProductPriortyType designProductPriortyType = default;

                #region Design Product Priorty Country
                if (!string.IsNullOrEmpty(product.PriortyCountry))
                {
                    designProductPriortyCountry =
                        await _designProductPriortyCountryService.SingleOrDefaultAsync(x =>
                            x.Country == product.PriortyCountry);
                    designProductPriortyCountry ??= await _designProductPriortyCountryService.AddAsync(
                        new DesignProductPriortyCountry()
                        {
                            Country = product.PriortyCountry
                        });
                }
                #endregion

                #region Design Product Priorty Type
                if (!string.IsNullOrEmpty(product.PriortyType))
                {
                    designProductPriortyType =
                        await _designProductPriortyTypeService.SingleOrDefaultAsync(x =>
                            x.Type == product.PriortyType);
                    designProductPriortyType ??= await _designProductPriortyTypeService.AddAsync(
                        new DesignProductPriortyType()
                        {
                            Type = product.PriortyType
                        });
                }
                #endregion

                #region Save Design Product
                var designProduct = await _designProductService.AddAsync(new DesignProduct()
                {
                    DesignProductPriortyTypeId = designProductPriortyType?.Id,
                    DesignId = dbModel.Id,
                    DesignPriortyCountryId = designProductPriortyCountry?.Id,
                    ExhibitionDate = product.ExhibitionDate,
                    ExhibitionName = product.ExhibitionName,
                    ExhibitionPlace = product.ExhibitionPlace,
                    FirstExhibitionDate = product.FirstExhibitionDate,
                    IsProductApproved = product.IsProductApproved,
                    Name = product.Name,
                    PriortyApplicationNumber = product.PriortyApplicationNumber,
                    PriortyDate = product.PriortyDate
                });
                #endregion

                #region Design Product Image

                foreach (var productImage in product.ProductImages)
                {
                    #region Save Design Product Image
                    await _designProductImageService.AddAsync(new DesignProductImage()
                    {
                        DesignProductId = designProduct.Id,
                        ImageId = await _fileTransferManager.Upload(productImage, ".jpg", BucketName,
                            SubFileDirectoryName)
                    });
                    #endregion
                }


                #endregion

                #region Design Product Classes

                foreach (var locarnoClass in product.LocarnoClass)
                {
                    var classModel = await _locarnoClassService.SingleOrDefaultAsync(x => x.Name == locarnoClass);
                    classModel ??= await _locarnoClassService.AddAsync(new LocarnoClass()
                    {
                        Name = locarnoClass
                    });
                    await _designProductClassesRelationService.AddAsync(new DesignProductClassesRelation()
                    {
                        LocarnoClassId = classModel.Id,
                        DesignProductId = designProduct.Id
                    });
                }

                #endregion

            }
            #endregion

            #region Design Holders
            var holderRelations = await _holderRelationService.Where(x => x.DataId == dbModel.Id && x.DataType == DataType.Design, default);
            var enumerable = holderRelations.ToList();
            if (enumerable.Any())
                _holderRelationService.RemoveRange(enumerable);
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
                    DataType = DataType.Design,
                    HolderId = holderModel.Id

                });
            }

            #endregion

        }
    }
}
