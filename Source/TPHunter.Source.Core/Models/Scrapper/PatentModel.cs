using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Source.Core.Models.Scrapper
{
    public class PatentModel
    {
        public string ApplicationNumber { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string ApplicationType { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string ProtectionType { get; set; }
        public string EPCPublishNumber { get; set; }
        public string EPCApplicationNumber { get; set; }
        public string PCTPublishNumber { get; set; }
        public string PCTApplicationNumber { get; set; }
        public DateTime? PCTPublishDate { get; set; }
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
        public byte[] Documents { get; set; }
        public byte[] AnalysisReport { get; set; }
        public byte[] ResearchReport { get; set; }

    }
}
