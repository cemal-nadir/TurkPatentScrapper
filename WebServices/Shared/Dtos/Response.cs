using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TPHunter.WebServices.Shared.ApiResponse.Dtos
{
    /// <summary>
    /// Response oluşturmak için kullanılan static factor metodlarının yer aldıkğı class
    /// </summary>
    /// <typeparam name="T">Generic Tip</typeparam>
    public class Response<T>
    {
        public T Data { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        [JsonIgnore]
        public bool IsSuccesful { get; set; }
        public List<string> Errors { get; set; }
        public long? TotalRecord { get; set; }
        public long? FilteredRecord { get; set; }
       
        /// <summary>
        /// İstek başarılıysa kullanılacak dönüş tipi
        /// </summary>
        /// <param name="data">Veri</param>
        /// <param name="statusCode">Http Status Kodu</param>
        /// <returns></returns>
        public static Response<T> Success(int statusCode,T data)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccesful = true };
        }
        /// <summary>
        ///  İstek başarılıysa kullanılacak dönüş tipi. Datanın pagination ile getirildiği durumlarda kullanılır
        /// </summary>
        /// <param name="data">Veri</param>
        /// <param name="statusCode">Http Status Kodu</param>
        /// <param name="totalRecord">Toplam Kayıt Sayısı</param>
        /// <param name="filteredRecord">Filtrelenmiş Kayıt Sayısı</param>
        /// <returns></returns>
        public static Response<T> Success(int statusCode,T data, long totalRecord, long filteredRecord)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccesful = true, TotalRecord = totalRecord, FilteredRecord = filteredRecord };
        }
        /// <summary>
        /// İstek başarılıysa ve geriye değer dönülmeyecekse kullanılacak dönüş tipi
        /// </summary>
        /// <param name="statusCode">Htto Status Kodu</param>
        /// <returns></returns>
        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default(T), StatusCode = statusCode, IsSuccesful = true };
        }
        /// <summary>
        /// İstek hatalıysa ve birden fazla hataya sahipse kullanılacak dönüş tipi
        /// </summary>
        /// <param name="errors">Hata Listesi</param>
        /// <param name="statusCode">Http Status Kodu</param>
        /// <returns></returns>
        public static Response<T> Fail(int statusCode,List<string> errors)
        {
            return new Response<T>
            {
                Errors = errors,
                IsSuccesful = false,
                StatusCode = statusCode
            };
        }
        /// <summary>
        /// İstek hatalıysa ve tek hataya sahipse kullanılacak dönüş tipi
        /// </summary>
        /// <param name="error">Hata</param>
        /// <param name="statusCode">Http Status Kodu</param>
        /// <returns></returns>
        public static Response<T> Fail(int statusCode,string error)
        {
            return new Response<T> { Errors = new List<string>() { error }, IsSuccesful = false, StatusCode = statusCode };
        }
    }
}
