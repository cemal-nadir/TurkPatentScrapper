using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.WebServices.Shared.ApiResponse.Dtos
{
    /// <summary>
    /// Hata dönüleceği durumlarda bu DTO tercih edilmelidir
    /// </summary>
    public class Error
    {
        public List<string> Errors { get; set; }
    }
}
