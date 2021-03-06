﻿// <auto-generated />
using System;
using ColdCallsTracker.Code.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ColdCallsTracker.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190210205936_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ColdCallsTracker.Code.Data.CallRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("Date");

                    b.Property<int>("PhoneId");

                    b.HasKey("Id");

                    b.HasIndex("PhoneId");

                    b.ToTable("CallRecords");
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActivityType");

                    b.Property<string>("Name");

                    b.Property<string>("Remarks");

                    b.Property<int?>("StateId");

                    b.Property<string>("WebSites");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId");

                    b.Property<string>("Number");

                    b.Property<string>("Remarks");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("States");
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.CallRecord", b =>
                {
                    b.HasOne("ColdCallsTracker.Code.Data.Phone", "Phone")
                        .WithMany("CallRecords")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.Company", b =>
                {
                    b.HasOne("ColdCallsTracker.Code.Data.State", "State")
                        .WithMany("Companies")
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.Phone", b =>
                {
                    b.HasOne("ColdCallsTracker.Code.Data.Company", "Company")
                        .WithMany("Phones")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
