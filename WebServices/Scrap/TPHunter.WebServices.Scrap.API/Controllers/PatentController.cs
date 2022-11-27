using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.WebServices.Scrap.API.ControllerServices.Abstract;
using TPHunter.WebServices.Scrap.API.ControllerServices.Contracts;
using TPHunter.WebServices.Shared.MainData.Core.Models;
using TPHunter.WebServices.Shared.MainData.Core.Services;
using TPHunter.WebServices.Shared.Utility.ApiUtility.ControllerBases.Dtos;

namespace TPHunter.WebServices.Scrap.API.Controllers
{
    public class PatentController :  ControllerBase<PatentModel>
    {
        private readonly IControllerService<PatentModel> _service;
        private readonly IService<Patent> _patentService;
        private readonly IService<HolderRelation> _holderRelationService;
        public PatentController(IControllerService<PatentModel> service, IService<Patent> patentService, IService<HolderRelation> holderRelationService) : base(service)
        {
            _service = service;
            _patentService = patentService;
            _holderRelationService = holderRelationService;
        }
        [HttpPost("GetLastPulledCount")]
        public async Task<IActionResult> GetLastPulledCount(DateRangeParam param)
        {
            return CreateActionResultInstance(
                Response<int>.Success(200,
                    await _service.GetLastPulledCountAsync(param)));
        }
        [HttpPost("GetLastPulledApplicationNumbers")]
        public async Task<IActionResult> GetLastPulledApplicationNumbers(DateRangeParam param)
        {
            return CreateActionResultInstance(
                Response<IEnumerable<string>>.Success(200,
                    await _service.GetLastPulledApplicationNumbersAsync(param)));
        }
        [HttpPost("GetLastPulledIds")]
        public async Task<IActionResult> GetLastPulledIds(BulletinParam param)
        {
            return CreateActionResultInstance(
                Response<IEnumerable<Guid>>.Success(200,
                    await _service.GetLastPulledIdsAsync(param)));
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
                    x.DataType == DataType.Patent && holderCodes.Contains(x.Holder.HolderCode)).Select(x => x.DataId).ToListAsync();



            var response = await _patentService.AsNoTracking.Where(dataIds.Count > 0 && attorneyIds.Any() ?
                x => x.AttorneyId != null && (attorneyIds.Contains(x.AttorneyId.Value) || dataIds.Contains(x.Id)) :
                dataIds.Count > 0 ?
                    x => dataIds.Contains(x.Id) :
                    x => attorneyIds.Contains(x.AttorneyId.Value)).Select(x => x.ApplicationNumber).ToListAsync();

            return CreateActionResultInstance(Response<IEnumerable<string>>.Success(200, response));
        }
    }
}
