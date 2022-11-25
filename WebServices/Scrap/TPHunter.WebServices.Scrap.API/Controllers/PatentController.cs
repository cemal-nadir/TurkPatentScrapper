using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.WebServices.Scrap.API.ControllerServices.Abstract;
using TPHunter.WebServices.Scrap.API.ControllerServices.Contracts;

namespace TPHunter.WebServices.Scrap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatentController :  ControllerBase<PatentModel>
    {
        private readonly IControllerService<PatentModel> _service;
        public PatentController(IControllerService<PatentModel> service) : base(service)
        {
            _service = service;
        }
        [HttpPost("GetLastPulledCount")]
        public async Task<IActionResult> GetLastPulledCount(DateRangeParam param)
        {
            return CreateActionResultInstance(
                Shared.Utility.ApiUtility.ControllerBases.Dtos.Response<int>.Success(200,
                    await _service.GetLastPulledCountAsync(param)));
        }
        [HttpPost("GetLastPulledApplicationNumbers")]
        public async Task<IActionResult> GetLastPulledApplicationNumbers(DateRangeParam param)
        {
            return CreateActionResultInstance(
                Shared.Utility.ApiUtility.ControllerBases.Dtos.Response<IEnumerable<string>>.Success(200,
                    await _service.GetLastPulledApplicationNumbersAsync(param)));
        }
    }
}
