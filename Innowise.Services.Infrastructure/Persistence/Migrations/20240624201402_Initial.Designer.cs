﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Innowise.Services.Infrastructure.Persistence;

#nullable disable

namespace Innowise.Services.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ServicesDbContext))]
    [Migration("20240624201402_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Innowise.Services.Domain.Domain.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("ServiceCategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SpecializationId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ServiceCategoryId");

                    b.HasIndex("SpecializationId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Innowise.Services.Domain.Domain.ServiceCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TimeSlotSize")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ServiceCategories");
                });

            modelBuilder.Entity("Innowise.Services.Domain.Domain.Specialization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("SpecializationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("Innowise.Services.Domain.Domain.Service", b =>
                {
                    b.HasOne("Innowise.Services.Domain.Domain.ServiceCategory", "ServiceCategory")
                        .WithMany()
                        .HasForeignKey("ServiceCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Innowise.Services.Domain.Domain.Specialization", "Specialization")
                        .WithMany()
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceCategory");

                    b.Navigation("Specialization");
                });
#pragma warning restore 612, 618
        }
    }
}
