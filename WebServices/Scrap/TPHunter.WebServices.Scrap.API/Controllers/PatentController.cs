using TPHunter.Shared.Scrapper.Models;
using TPHunter.WebServices.Scrap.API.ControllerServices.Abstract;
using TPHunter.WebServices.Scrap.API.ControllerServices.Contracts;
using TPHunter.WebServices.Shared.MainData.Core.Models;
using TPHunter.WebServices.Shared.MainData.Core.Services;

namespace TPHunter.WebServices.Scrap.API.Controllers
{
    public class PatentController :  ControllerBases<PatentModel,Patent>
    {
        public PatentController(IControllerService<PatentModel> controllerService, IService<Patent> service, IService<HolderRelation> holderRelationService) : base(controllerService, service, holderRelationService)
        {
        }
    }
}
