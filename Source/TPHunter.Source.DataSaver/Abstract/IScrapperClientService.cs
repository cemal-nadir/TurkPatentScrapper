using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPHunter.Shared.ApiUtility.ControllerBases.Dtos;
using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Source.DataSaver.Abstract
{
    public interface IScrapperClientService<in TModel> where TModel:IModel
    {
        public Task InsertAsync(TModel model);
        public Task UpdateAsync(TModel model);
        public Task RemoveAsync(Guid ıd);
        public Task<Response<int>> GetLastPulledCountAsync(ISearchParam searchParam);
        public Task<Response<IEnumerable<string>>> GetLastPulledApplicationNumbersAsync(ISearchParam searchParam);
        public Task<Response<IEnumerable<Guid>>> GetLastPulledIdsAsync(ISearchParam searchParam);
    }
}
