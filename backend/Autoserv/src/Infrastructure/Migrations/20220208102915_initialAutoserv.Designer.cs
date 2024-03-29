﻿// <auto-generated />
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AutoservContext))]
    [Migration("20220208102915_initialAutoserv")]
    partial class initialAutoserv
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ApplicationCore.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_catalog_brands");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_catalog_brands_name");

                    b.ToTable("catalog_brands", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Bosh"
                        },
                        new
                        {
                            Id = 2,
                            Name = "HEPU"
                        },
                        new
                        {
                            Id = 3,
                            Name = "MANN"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
