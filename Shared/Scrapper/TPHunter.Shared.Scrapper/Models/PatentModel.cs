using System;
using System.Collections.Generic;
using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Shared.Scrapper.Models
{
    public class PatentModel : IModel
    {
        public string ApplicationNumber { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string ApplicationType { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string ProtectionType { get; set; }
        public string EpcPublishNumber { get; set; }
        public string EpcApplicationNumber { get; set; }
        public string PctPublishNumber { get; set; }
        public string PctApplicationNumber { get; set; }
        public DateTime? PctPublishDate { get; set; }
        public IEnumerable<HolderModel> Holders { get; set; }
        public IEnumerable<InventorModel> Inventors { get; set; }
        public string InventionTitle { get; set; }
        public string InventionSummary { get; set; }
        public string AttorneyName { get; set; }
        public string AttorneyCompanyName { get; set; }
        public string AttorneyCompanyAddress { get; set; }
        public IEnumerable<PatentPriortyModel> PatentPriorties { get; set; }
        public IEnumerable<PatentClassesModel> PatentClasses { get; set; }
        public IEnumerable<PatentTransactionModel> PatentTransactions { get; set; }
        public IEnumerable<PatentPublicationModel> PatentPublications { get; set; }
        public IEnumerable<PatentPaymentModel> PatentPayments { get; set; }
        public string DocumentsUrl { get; set; }
        public string AnalysisReportUrl { get; set; }
        public string ResearchReportUrl { get; set; }

    }
}
