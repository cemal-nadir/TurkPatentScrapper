using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.WebServices.Scrap.API.ControllerServices.Abstract;
using TPHunter.WebServices.Shared.Utility.ApiUtility.ControllerBases;
using TPHunter.WebServices.Shared.Utility.ApiUtility.ControllerBases.Dtos;

namespace TPHunter.WebServices.Scrap.API.ControllerServices.Contracts
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerBase<TModel> : CustomBaseController where TModel:IModel
    {
        private readonly IControllerService<TModel> _service;
        public ControllerBase(IControllerService<TModel> service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(IEnumerable<TModel> models)
        {
            foreach (var model in models)
            {
                await _service.InsertAsync(model);
            }

            return CreateActionResultInstance(Response<NoContent>.Success(201));
        }
        [HttpDelete("{ıd:guid}")]
        public async Task<IActionResult> Remove(Guid ıd)
        {
            await _service.RemoveAsync(ıd);
            return CreateActionResultInstance(Response<NoContent>.Success(201));
        }
        [HttpPut]
        public async Task<IActionResult> Update(TModel model)
        {
            await _service.UpdateAsync(model);
            return CreateActionResultInstance(Response<NoContent>.Success(201));
        }
    }
}
