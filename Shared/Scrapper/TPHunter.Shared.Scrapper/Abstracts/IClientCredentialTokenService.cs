using System.Threading.Tasks;

namespace TPHunter.Shared.Scrapper.Abstracts
{
    public interface IClientCredentialTokenService
    {
        Task<string> GetToken();
    }
}
