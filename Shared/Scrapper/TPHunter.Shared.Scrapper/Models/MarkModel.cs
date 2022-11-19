using System;
using System.Collections.Generic;
using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Shared.Scrapper.Models
{
   public class MarkModel : IModel
    {
        public string ApplicationNumber { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string InternationalRegistrationNumber { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? DeclareBullettinDate { get; set; }
        public DateTime? RegistrationBullettinDate { get; set; }
        public string DeclareBullettinNumber { get; set; }
        public string RegistrationBullettinNumber { get; set; }
        public DateTime? ProtectionDate { get; set; }
        public string Status { get; set; }
        public string PriortyInformation { get; set; }
        public int[] Classes { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string AttorneyName { get; set; }
        public string AttorneyCompanyName { get; set; }
        public string Decision { get; set; }
        public string DecisionReason { get; set; }
        public IEnumerable<HolderModel> Holders { get; set; }
        public IEnumerable<MarkServicesModel> Services { get; set; }
        public IEnumerable<MarkTransactionsModel> Transactions { get; set; }
        public string ImageText { get; set; }


    }
}
