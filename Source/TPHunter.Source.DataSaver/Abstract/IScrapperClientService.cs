using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Source.DataSaver.Abstract
{
    public interface IScrapperClientService<in TRequest> where TRequest : class
    {
        public Task InsertAsync(TRequest model);
        public Task UpdateAsync(TRequest model);
        public Task RemoveAsync(Guid ıd);
        public Task<int> GetLastPulledCountAsync(ISearchParam searchParam);
        public Task<IEnumerable<string>> GetLastPulledApplicationNumbersAsync(ISearchParam searchParam);
    }
}
