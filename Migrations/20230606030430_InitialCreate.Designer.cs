﻿// <auto-generated />
using System;
using MegaDesk_Razor_ACZ.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MegaDesk_Razor_ACZ.Migrations
{
    [DbContext(typeof(MegaDesk_Razor_ACZContext))]
    [Migration("20230606030430_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("MegaDesk_Razor_ACZ.Models.Desk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Depth")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DrawerCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaterialId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Width")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.ToTable("Desk");
                });

            modelBuilder.Entity("MegaDesk_Razor_ACZ.Models.DeskQuote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomerName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeskId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int?>("ProductionSpeedCostId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DeskId");

                    b.HasIndex("ProductionSpeedCostId");

                    b.ToTable("DeskQuote");
                });

            modelBuilder.Entity("MegaDesk_Razor_ACZ.Models.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BasePrice")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Material");
                });

            modelBuilder.Entity("MegaDesk_Razor_ACZ.Models.ProductionSpeedCost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double?>("TierAPrice")
                        .HasColumnType("REAL");

                    b.Property<double?>("TierBPrice")
                        .HasColumnType("REAL");

                    b.Property<double?>("TierCPrice")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("ProductionSpeedCost");
                });

            modelBuilder.Entity("MegaDesk_Razor_ACZ.Models.Desk", b =>
                {
                    b.HasOne("MegaDesk_Razor_ACZ.Models.Material", "material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("material");
                });

            modelBuilder.Entity("MegaDesk_Razor_ACZ.Models.DeskQuote", b =>
                {
                    b.HasOne("MegaDesk_Razor_ACZ.Models.Desk", "Desk")
                        .WithMany()
                        .HasForeignKey("DeskId");

                    b.HasOne("MegaDesk_Razor_ACZ.Models.ProductionSpeedCost", "ProductionSpeedCost")
                        .WithMany()
                        .HasForeignKey("ProductionSpeedCostId");

                    b.Navigation("Desk");

                    b.Navigation("ProductionSpeedCost");
                });
#pragma warning restore 612, 618
        }
    }
}
