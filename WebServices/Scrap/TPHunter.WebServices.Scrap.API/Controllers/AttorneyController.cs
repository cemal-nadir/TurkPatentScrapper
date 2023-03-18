using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TPHunter.Shared.ApiUtility.ControllerBases;
using TPHunter.Shared.ApiUtility.ControllerBases.Dtos;
using TPHunter.WebServices.Shared.MainData.Core.Models;
using TPHunter.WebServices.Shared.MainData.Core.Services;

namespace TPHunter.WebServices.Scrap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttorneyController : CustomBaseController
    {
        private readonly IService<Attorney> _attorneyService;

        public AttorneyController(IService<Attorney> attorneyService)
        {
            _attorneyService = attorneyService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAttorneyIdsByNames([FromQuery] string[] attorneyName)
        {

            return CreateActionResultInstance(Response<IEnumerable<Guid>>.Success(200,(await _attorneyService.Where(x => attorneyName.Contains(x.Name))).Select(x => x.Id))) ;
        }
    }
}
