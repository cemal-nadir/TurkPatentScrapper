using Microsoft.AspNetCore.Mvc;
using TPHunter.Shared.ApiUtility.ControllerBases.Dtos;

namespace TPHunter.Shared.ApiUtility.ControllerBases
{
    /// <summary>
    /// Api'larda statik hale getirdiğimiz response tipini dönebilmemiz için gerekli controller base
    /// </summary>
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
