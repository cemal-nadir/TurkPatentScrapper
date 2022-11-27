using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.WebServices.Scrap.API.ControllerServices.Abstract;
using TPHunter.WebServices.Scrap.API.ControllerServices.Contracts;
using TPHunter.WebServices.Shared.MainData.Core.Models;
using TPHunter.WebServices.Shared.Utility.ApiUtility.ControllerBases.Dtos;
using TPHunter.WebServices.Shared.MainData.Core.Services;

namespace TPHunter.WebServices.Scrap.API.Controllers
{
    
    public class DesignController : ControllerBase<PatentModel>
    {
        private readonly IControllerService<PatentModel> _service;
        private readonly IService<Design> _designService;
        private readonly IService<HolderRelation> _holderRelationService;
        public DesignController(IControllerService<PatentModel> service, IService<HolderRelation> holderRelationService, IService<Design> designService) : base(service)
        {
            _service = service;
            _holderRelationService = holderRelationService;
            _designService = designService;
        }
        [HttpPost("GetLastPulledCount")]
        public async Task<IActionResult> GetLastPulledCount(BulletinParam param)
        {
            return CreateActionResultInstance(
                Response<int>.Success(200,
                    await _service.GetLastPulledCountAsync(param)));
        }
        [HttpPost("GetLastPulledApplicationNumbers")]
        public async Task<IActionResult> GetLastPulledApplicationNumbers(BulletinParam param)
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
                    x.DataType == DataType.Design && holderCodes.Contains(x.Holder.HolderCode)).Select(x => x.DataId).ToListAsync();



            var response = await _designService.AsNoTracking.Where(dataIds.Count > 0 && attorneyIds.Any() ?
                x => x.AttorneyId != null && (attorneyIds.Contains(x.AttorneyId.Value) || dataIds.Contains(x.Id)) :
                dataIds.Count > 0 ?
                    x => dataIds.Contains(x.Id) :
                    x => attorneyIds.Contains(x.AttorneyId.Value)).Select(x => x.ApplicationNumber).ToListAsync();

            return CreateActionResultInstance(Response<IEnumerable<string>>.Success(200, response));
        }
    }
}
