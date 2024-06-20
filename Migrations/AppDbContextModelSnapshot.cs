﻿// <auto-generated />
using System;
using LearnHubBackOffice.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LearnHubBO.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LearnHubBackOffice.Models.Cours", b =>
                {
                    b.Property<int>("IdCours")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCours"));

                    b.Property<DateTime>("DateCreationCours")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModificationCours")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCoursCategorie")
                        .HasColumnType("int");

                    b.Property<int>("IdFormateur")
                        .HasColumnType("int");

                    b.Property<string>("TitreCours")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCours");

                    b.HasIndex("IdCoursCategorie");

                    b.HasIndex("IdFormateur");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("LearnHubBackOffice.Models.CoursCategorie", b =>
                {
                    b.Property<int>("IdCoursCategorie")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCoursCategorie"));

                    b.Property<string>("NomCoursCategorie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCoursCategorie");

                    b.ToTable("CoursCategories");
                });

            modelBuilder.Entity("LearnHubBackOffice.Models.Formateur", b =>
                {
                    b.Property<int>("IdFormateur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdFormateur"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotDePasseHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomFormateur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdFormateur");

                    b.ToTable("Formateurs");
                });

            modelBuilder.Entity("LearnHubBackOffice.Models.Cours", b =>
                {
                    b.HasOne("LearnHubBackOffice.Models.CoursCategorie", "CoursCategorie")
                        .WithMany()
                        .HasForeignKey("IdCoursCategorie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LearnHubBackOffice.Models.Formateur", "Formateur")
                        .WithMany()
                        .HasForeignKey("IdFormateur")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CoursCategorie");

                    b.Navigation("Formateur");
                });
#pragma warning restore 612, 618
        }
    }
}
