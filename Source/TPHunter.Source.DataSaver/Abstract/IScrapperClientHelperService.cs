using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TPHunter.Source.DataSaver.Abstract
{
    public interface IScrapperClientHelperService
    {
        public Task<IEnumerable<Guid>> GetAttorneyIdsByNames(string[] attorneyNames);

        public Task<IEnumerable<string>> GetFilteredTrademarkApplicationNumbers(Guid[] attorneyIds = null,
            string[] holderCodes = null);
        public Task<IEnumerable<string>> GetFilteredPatentApplicationNumbers(Guid[] attorneyIds = null,
            string[] holderCodes = null);
        public Task<IEnumerable<string>> GetFilteredDesignApplicationNumbers(Guid[] attorneyIds = null,
            string[] holderCodes = null);
    }
}
