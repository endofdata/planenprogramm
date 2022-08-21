﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Planenprogramm;

#nullable disable

namespace Planenprogramm.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20220821143753_Annotation_Null")]
    partial class Annotation_Null
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("Planenprogramm.Entities.Tarp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Annotation")
                        .HasColumnType("TEXT");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Tarps");
                });

            modelBuilder.Entity("Planenprogramm.Entities.TarpCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Additional")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Length")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TarpTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Width")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TarpTypeId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Planenprogramm.Entities.TarpType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TarpTypes");
                });

            modelBuilder.Entity("Planenprogramm.Entities.Tarp", b =>
                {
                    b.HasOne("Planenprogramm.Entities.TarpCategory", "Category")
                        .WithMany("Tarps")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Planenprogramm.Entities.TarpCategory", b =>
                {
                    b.HasOne("Planenprogramm.Entities.TarpType", "TarpType")
                        .WithMany("Categories")
                        .HasForeignKey("TarpTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TarpType");
                });

            modelBuilder.Entity("Planenprogramm.Entities.TarpCategory", b =>
                {
                    b.Navigation("Tarps");
                });

            modelBuilder.Entity("Planenprogramm.Entities.TarpType", b =>
                {
                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}