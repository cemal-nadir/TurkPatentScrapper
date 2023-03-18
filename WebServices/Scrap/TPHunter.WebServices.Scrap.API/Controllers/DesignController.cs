using TPHunter.Shared.Scrapper.Models;
using TPHunter.WebServices.Scrap.API.ControllerServices.Abstract;
using TPHunter.WebServices.Scrap.API.ControllerServices.Contracts;
using TPHunter.WebServices.Shared.MainData.Core.Models;
using TPHunter.WebServices.Shared.MainData.Core.Services;

namespace TPHunter.WebServices.Scrap.API.Controllers
{
    
    public class DesignController : ControllerBases<DesignModel,Design>
    {
        public DesignController(IControllerService<DesignModel> controllerService, IService<Design> service, IService<HolderRelation> holderRelationService) : base(controllerService, service, holderRelationService)
        {
        }
    }
}
