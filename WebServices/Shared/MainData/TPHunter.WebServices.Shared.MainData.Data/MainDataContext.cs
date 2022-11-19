using Microsoft.EntityFrameworkCore;
using TPHunter.WebServices.Shared.MainData.Core.Models;

namespace TPHunter.WebServices.Shared.MainData.Data
{
    public class MainDataContext : DbContext
    {
        public MainDataContext(DbContextOptions<MainDataContext> options) : base(options)
        {

        }

        #region Shared
        public DbSet<Attorney> Attorneys { get; set; }
        public DbSet<AttorneyCompany> AttorneyCompanies { get; set; }
        public DbSet<Holder> Holders { get; set; }
        public DbSet<HolderRelation> HolderRelations { get; set; }
        #endregion

        #region Designs
        public DbSet<Design> Designs { get; set; }
        public DbSet<Designer> Designers { get; set; }
        public DbSet<DesignerRelation> DesignerRelations { get; set; }
        public DbSet<DesignProduct> DesignProducts { get; set; }
        public DbSet<DesignProductClassesRelation> DesignProductClassesRelations { get; set; }
        public DbSet<DesignProductImage> DesignProductImages { get; set; }
        public DbSet<DesignProductPriortyCountry> DesignProductPriortyCountries { get; set; }
        public DbSet<DesignProductPriortyType> DesignProductPriortyTypes { get; set; }
        public DbSet<DesignStatus> DesignStatuses { get; set; }
        public DbSet<DesignTransaction> DesignTransactions { get; set; }
        public DbSet<DesignTransactionDescription> DesignTransactionDescriptions { get; set; }
        public DbSet<DesignTransactionDescriptionDetail> DesignTransactionDescriptionDetails { get; set; }
        public DbSet<DesignTransactionType> DesignTransactionTypes { get; set; }
        public DbSet<DesignTransactionTypeDetail> DesignTransactionTypeDetails { get; set; }
        public DbSet<LocarnoClass> LocarnoClasses { get; set; }
        #endregion

        #region Patents
        public DbSet<Inventor> Inventors { get; set; }
        public DbSet<InventorRelation> InventorRelations { get; set; }
        public DbSet<Patent> Patents { get; set; }
        public DbSet<PatentApplicationType> PatentApplicationTypes { get; set; }
        public DbSet<PatentClass> PatentClasses { get; set; }
        public DbSet<PatentClassRelation> PatentClassRelations { get; set; }
        public DbSet<PatentClassType> PatentClassTypes { get; set; }
        public DbSet<PatentPayment> PatentPayments { get; set; }
        public DbSet<PatentPdf> PatentPdFs { get; set; }
        public DbSet<PatentPriorty> PatentPriorties { get; set; }
        public DbSet<PatentPriortyCountry> PatentPriortyCountries { get; set; }
        public DbSet<PatentProtectionType> PatentProtectionTypes { get; set; }
        public DbSet<PatentPublication> PatentPublications { get; set; }
        public DbSet<PatentPublicationDescription> PatentPublicationDescriptions { get; set; }
        public DbSet<PatentTransaction> PatentTransactions { get; set; }
        public DbSet<PatentTransactionName> PatentTransactionNames { get; set; }
        #endregion

        #region Trademarks
        public DbSet<TradeMark> TradeMarks { get; set; }
        public DbSet<TradeMarkDecision> TradeMarkDecisions { get; set; }
        public DbSet<TradeMarkDecisionReason> TradeMarkDecisionReasons { get; set; }
        public DbSet<TradeMarkPriorty> TradeMarkPriorties { get; set; }
        public DbSet<TradeMarkPriortyCountry> TradeMarkPriortyCountries { get; set; }
        public DbSet<TradeMarkServices> TradeMarkServices { get; set; }
        public DbSet<TradeMarkStatus>TradeMarkStatuses { get; set; }
        public DbSet<TradeMarkTransaction>TradeMarkTransactions { get; set; }
        public DbSet<TradeMarkTransactionDescription>TradeMarkTransactionDescriptions { get; set; }
        public DbSet<TradeMarkTransactionDetail> TradeMarkTransactionDetails { get; set; }
        public DbSet<TradeMarkTransactionName> TradeMarkTransactionNames { get; set; }
        public DbSet<TradeMarkTransactionType>TradeMarkTransactionTypes { get; set; }
        public DbSet<TradeMarkType> TradeMarkTypes { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
