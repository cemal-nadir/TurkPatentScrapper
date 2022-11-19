using System;
using System.Collections.Generic;
using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Shared.Scrapper.Models
{
   public class DesignModel:IModel
    {
        public string ApplicationNumber { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string BulletinNumber { get; set; }
        public DateTime? BulletinDate { get; set; }
        public string Status { get; set; }
        public IEnumerable<HolderModel> Holders { get; set; }
        public IEnumerable<string> Designers { get; set; }
        public string AttorneyName { get; set; }
        public string AttorneyCompanyName { get; set; }
        public string AttorneyAddress { get; set; }
        public IEnumerable<ProductModel> Products { get; set; }
        public IEnumerable<DesignTransactionModel> DesignTransactions { get; set; }

    }
}
