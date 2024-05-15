﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PCShop_api.Data;

#nullable disable

namespace PCShop_api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231230182650_zadaci_v3")]
    partial class zadaci_v3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PCShop_api.Data.Models.Artikal", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("Cijena")
                        .HasColumnType("int");

                    b.Property<string>("ImeArtikla")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Proizvodjac")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slika")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipID")
                        .HasColumnType("int");

                    b.Property<bool>("isObrisan")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("TipID");

                    b.ToTable("Artikal");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.AutentifikacijaToken", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<bool>("Is2FOtkljucano")
                        .HasColumnType("bit");

                    b.Property<int>("KorisnickiNalogID")
                        .HasColumnType("int");

                    b.Property<string>("TwoFKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vrijednost")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ipAdresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("vrijemeEvidentiranja")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("KorisnickiNalogID");

                    b.ToTable("AutentifikacijaToken");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Drzava", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skracenica")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Drzava");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Kompatibilnost", b =>
                {
                    b.Property<int>("ArtikalKompatibilnostID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArtikalKompatibilnostID"), 1L, 1);

                    b.Property<int>("Artikal1ID")
                        .HasColumnType("int");

                    b.Property<int>("Artikal2ID")
                        .HasColumnType("int");

                    b.HasKey("ArtikalKompatibilnostID");

                    b.HasIndex("Artikal1ID");

                    b.HasIndex("Artikal2ID");

                    b.ToTable("Kompatibilnost");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.KorisnickiNalog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<bool>("Is2FActive")
                        .HasColumnType("bit");

                    b.Property<string>("KorisnickoIme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lozinka")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SlikaKorisnika")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("KorisnickiNalog");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Korpa", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("ArtikalID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatumDodavanja")
                        .HasColumnType("datetime2");

                    b.Property<int>("EvidentiraoKorisnikId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ArtikalID");

                    b.HasIndex("EvidentiraoKorisnikId");

                    b.ToTable("Korpa");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Narudzba", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTelefona")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dostavljac")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EvidentiraoKorisnikId")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("UkupnaCijena")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.HasIndex("EvidentiraoKorisnikId");

                    b.ToTable("Narudzba");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.TipArtikla", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Tip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("TipArtikla");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Wishlist", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("ArtikalID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatumDodavanja")
                        .HasColumnType("datetime2");

                    b.Property<int>("EvidentiraoKorisnikId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ArtikalID");

                    b.HasIndex("EvidentiraoKorisnikId");

                    b.ToTable("Wishlist");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Zadaci", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DatumDodavanja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumZavrsetka")
                        .HasColumnType("datetime2");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RadnikID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RadnikID");

                    b.ToTable("Zadatak");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Admin", b =>
                {
                    b.HasBaseType("PCShop_api.Data.Models.KorisnickiNalog");

                    b.Property<DateTime>("DatumRodjenja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumZaposlenja")
                        .HasColumnType("datetime2");

                    b.Property<int>("DrzavaID")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("DrzavaID");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Kupac", b =>
                {
                    b.HasBaseType("PCShop_api.Data.Models.KorisnickiNalog");

                    b.Property<DateTime>("DatumRodjenja")
                        .HasColumnType("datetime2");

                    b.Property<int>("DrzavaID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("DrzavaID");

                    b.ToTable("Kupac");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Radnik", b =>
                {
                    b.HasBaseType("PCShop_api.Data.Models.KorisnickiNalog");

                    b.Property<DateTime>("DatumRodjenja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumZaposlenja")
                        .HasColumnType("datetime2");

                    b.Property<int>("DrzavaID")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("DrzavaID");

                    b.ToTable("Radnik");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Artikal", b =>
                {
                    b.HasOne("PCShop_api.Data.Models.TipArtikla", "TipArtikla")
                        .WithMany()
                        .HasForeignKey("TipID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TipArtikla");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.AutentifikacijaToken", b =>
                {
                    b.HasOne("PCShop_api.Data.Models.KorisnickiNalog", "korisnickiNalog")
                        .WithMany()
                        .HasForeignKey("KorisnickiNalogID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("korisnickiNalog");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Kompatibilnost", b =>
                {
                    b.HasOne("PCShop_api.Data.Models.Artikal", "Artikal1")
                        .WithMany()
                        .HasForeignKey("Artikal1ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PCShop_api.Data.Models.Artikal", "Artikal2")
                        .WithMany()
                        .HasForeignKey("Artikal2ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Artikal1");

                    b.Navigation("Artikal2");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Korpa", b =>
                {
                    b.HasOne("PCShop_api.Data.Models.Artikal", "Artikal")
                        .WithMany("Korpe")
                        .HasForeignKey("ArtikalID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PCShop_api.Data.Models.KorisnickiNalog", "EvidentiraoKorisnik")
                        .WithMany()
                        .HasForeignKey("EvidentiraoKorisnikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Artikal");

                    b.Navigation("EvidentiraoKorisnik");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Narudzba", b =>
                {
                    b.HasOne("PCShop_api.Data.Models.KorisnickiNalog", "EvidentiraoKorisnik")
                        .WithMany()
                        .HasForeignKey("EvidentiraoKorisnikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EvidentiraoKorisnik");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Wishlist", b =>
                {
                    b.HasOne("PCShop_api.Data.Models.Artikal", "Artikal")
                        .WithMany("Wishlists")
                        .HasForeignKey("ArtikalID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PCShop_api.Data.Models.KorisnickiNalog", "EvidentiraoKorisnik")
                        .WithMany()
                        .HasForeignKey("EvidentiraoKorisnikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Artikal");

                    b.Navigation("EvidentiraoKorisnik");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Zadaci", b =>
                {
                    b.HasOne("PCShop_api.Data.Models.Radnik", "Radnik")
                        .WithMany()
                        .HasForeignKey("RadnikID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Radnik");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Admin", b =>
                {
                    b.HasOne("PCShop_api.Data.Models.Drzava", "DrzavaPorijekla")
                        .WithMany()
                        .HasForeignKey("DrzavaID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PCShop_api.Data.Models.KorisnickiNalog", null)
                        .WithOne()
                        .HasForeignKey("PCShop_api.Data.Models.Admin", "ID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("DrzavaPorijekla");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Kupac", b =>
                {
                    b.HasOne("PCShop_api.Data.Models.Drzava", "DrzavaPorijekla")
                        .WithMany()
                        .HasForeignKey("DrzavaID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PCShop_api.Data.Models.KorisnickiNalog", null)
                        .WithOne()
                        .HasForeignKey("PCShop_api.Data.Models.Kupac", "ID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("DrzavaPorijekla");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Radnik", b =>
                {
                    b.HasOne("PCShop_api.Data.Models.Drzava", "DrzavaPorijekla")
                        .WithMany()
                        .HasForeignKey("DrzavaID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PCShop_api.Data.Models.KorisnickiNalog", null)
                        .WithOne()
                        .HasForeignKey("PCShop_api.Data.Models.Radnik", "ID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("DrzavaPorijekla");
                });

            modelBuilder.Entity("PCShop_api.Data.Models.Artikal", b =>
                {
                    b.Navigation("Korpe");

                    b.Navigation("Wishlists");
                });
#pragma warning restore 612, 618
        }
    }
}