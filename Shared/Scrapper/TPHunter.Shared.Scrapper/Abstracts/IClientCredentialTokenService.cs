using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Shared.Scrapper.Abstracts
{
    public interface IClientCredentialTokenService
    {
        Task<string> GetToken();
    }
}
