using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TPHunter.Shared.ApiUtility.ControllerBases;
using TPHunter.Shared.ApiUtility.ControllerBases.Dtos;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.WebServices.Scrap.API.ControllerServices.Abstract;
using TPHunter.WebServices.Shared.MainData.Core.Services;
using TPHunter.WebServices.Shared.MainData.Core.Models;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Scrap.API.ControllerServices.Contracts
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerBases<TCModel,TEModel>: CustomBaseController where TCModel:IModel where TEModel:EntityBase,IItemBase
    {
        private readonly IControllerService<TCModel> _controllerService;
        private readonly IService<TEModel> _service;
        private readonly IService<HolderRelation> _holderRelationService;
        public ControllerBases(IControllerService<TCModel> controllerService,IService<TEModel> service,IService<HolderRelation>holderRelationService)
        {
            _controllerService = controllerService;
            _service = service;
            _holderRelationService = holderRelationService;
        }



        [HttpPost]
        public async Task<IActionResult> Insert(TCModel model)
        {
            await _controllerService.InsertAsync(model);
            return CreateActionResultInstance(Response<NoContent>.Success(201));
        }
        [HttpDelete("{ıd:guid}")]
        public async Task<IActionResult> Remove(Guid ıd)
        {
            await _controllerService.RemoveAsync(ıd);
            return CreateActionResultInstance(Response<NoContent>.Success(201));
        }
        [HttpPut]
        public async Task<IActionResult> Update(TCModel model)
        {
            await _controllerService.UpdateAsync(model);
            return CreateActionResultInstance(Response<NoContent>.Success(201));
        }


        [HttpPost("GetLastPulledCount")]
        public async Task<IActionResult> GetLastPulledCount(SearchParam param)
        {
            return CreateActionResultInstance(
                Response<int>.Success(200,
                    await _controllerService.GetLastPulledCountAsync(param)));
        }
        [HttpPost("GetLastPulledApplicationNumbers")]
        public async Task<IActionResult> GetLastPulledApplicationNumbers(SearchParam param)
        {
            return CreateActionResultInstance(
                Response<IEnumerable<string>>.Success(200,
                    await _controllerService.GetLastPulledApplicationNumbersAsync(param)));
        }
        [HttpPost("GetLastPulledIds")]
        public async Task<IActionResult> GetLastPulledIds(SearchParam param)
        {
            return CreateActionResultInstance(
                Response<IEnumerable<Guid>>.Success(200,
                    await _controllerService.GetLastPulledIdsAsync(param)));
        }
        [HttpGet("GetFilteredApplicationNumbers")]
        public async Task<IActionResult> GetFilteredApplicationNumbers([FromQuery] Guid[] attorneyIds,
            [FromQuery] string[] holderCodes)
        {
            if (attorneyIds is { Length: 0 } && holderCodes is { Length: 0 })
            {
                return CreateActionResultInstance(Response<NoContent>.Success(200));
            }



            var dataIds = new List<Guid>();
            if (holderCodes.Length > 0)
                dataIds = await _holderRelationService.AsNoTracking.Include(x => x.Holder).Where(x =>
                    x.DataType == DataType.Trademark && holderCodes.Contains(x.Holder.HolderCode)).Select(x => x.DataId).ToListAsync();



            var response = await _service.AsNoTracking.Where(dataIds.Count > 0 && attorneyIds.Any() ?
                x => x.AttorneyId != null && (attorneyIds.Contains(x.AttorneyId.Value) || dataIds.Contains(x.Id)) :
                dataIds.Count > 0 ?
                    x => dataIds.Contains(x.Id) :
                    x => attorneyIds.Contains(x.AttorneyId.Value)).Select(x => x.ApplicationNumber).ToListAsync();

            return CreateActionResultInstance(Response<IEnumerable<string>>.Success(200, response));
        }
    }
}