using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.WebServices.Shared.MainData.Data
{
    public partial class MainDataContext : DbContext
    {
        public MainDataContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //dbsetler burada yer alıcak
        public DbSet<UserVekil> UserVekil { get; set; }
        public DbSet<DefaultUserVekil> DefaultUserVekil { get; set; }

        //dbsetler burada yer alıcak

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserVekil>(entity => {
                entity.HasKey(x => x.ID);
                entity.Property(x => x.UserID).IsRequired();
                entity.Property(x => x.VekilID).IsRequired();
            });
            modelBuilder.Entity<DefaultUserVekil>(entity =>
            {
                entity.HasKey(x => x.VekilID);
                entity.Property(x => x.VekilID).ValueGeneratedNever();
            });
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
