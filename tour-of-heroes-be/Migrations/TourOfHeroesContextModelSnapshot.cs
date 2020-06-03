﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tour_of_heroes_be;

namespace tour_of_heroes_be.Migrations
{
    [DbContext(typeof(TourOfHeroesContext))]
    partial class TourOfHeroesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("tour_of_heroes_be.Models.Hero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("Removed")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Name", "Removed")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL AND [Removed] IS NOT NULL");

                    b.ToTable("Heroes");
                });
#pragma warning restore 612, 618
        }
    }
}