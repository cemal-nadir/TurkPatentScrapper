using System.Collections.Generic;

namespace TPHunter.WebServices.Shared.Utility.ApiUtility.ControllerBases.Dtos
{
    /// <summary>
    /// Hata dönüleceği durumlarda bu DTO tercih edilmelidir
    /// </summary>
    public class Error
    {
        public List<string> Errors { get; set; }
    }
}
