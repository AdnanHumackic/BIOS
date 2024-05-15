using Microsoft.EntityFrameworkCore;
using PCShop_api.Data.Models;

namespace PCShop_api.Data
{
    public class ApplicationDbContext: DbContext
    {

        public DbSet<Artikal> Artikal { get; set; }
        public DbSet<TipArtikla> TipArtikla { get; set; }
        public DbSet<Kompatibilnost> Kompatibilnost { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<Korpa> Korpa { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Radnik> Radnik { get; set; }
        public DbSet<Kupac> Kupac { get; set; }
        public DbSet<KorisnickiNalog> KorisnickiNalog { get; set; }
        public DbSet<Drzava> Drzava { get; set; }
        public DbSet<AutentifikacijaToken> AutentifikacijaToken { get; set; }
        public DbSet<Narudzba> Narudzba { get; set; }
        public DbSet<ArtikalSlika> ArtikalSlika { get; set; }

        public DbSet<Zadaci> Zadatak { get; set; }
        public virtual DbSet<Recenzija> Recenzije { get; set; }
        public virtual DbSet<Dokumenti> Dokumenti { get; set; }
        public virtual DbSet<Dostavljaci> Dostavljaci { get; set; }

        public ApplicationDbContext(
          DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<AutentifikacijaToken>()
               .HasOne(a => a.korisnickiNalog)
               .WithMany()
               .HasForeignKey(a => a.KorisnickiNalogID)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

