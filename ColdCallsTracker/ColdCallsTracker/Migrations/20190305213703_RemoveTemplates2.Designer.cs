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
    [Migration("20190305213703_RemoveTemplates2")]
    partial class RemoveTemplates2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ColdCallsTracker.Code.Data.Models.CallRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("DateCreate");

                    b.Property<DateTime>("DateModify");

                    b.Property<int>("PhoneId");

                    b.HasKey("Id");

                    b.HasIndex("PhoneId");

                    b.ToTable("CallRecords");
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActivityType");

                    b.Property<DateTime>("DateCreate");

                    b.Property<DateTime>("DateModify");

                    b.Property<string>("Name");

                    b.Property<string>("Remarks");

                    b.Property<int?>("StateId");

                    b.Property<string>("WebSites");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.Models.Costing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<double>("Cost");

                    b.Property<DateTime>("DateCreate");

                    b.Property<DateTime>("DateModify");

                    b.Property<string>("Name");

                    b.Property<double>("Qty");

                    b.Property<int>("QuoteId");

                    b.Property<double>("Total");

                    b.Property<int>("Unit");

                    b.HasKey("Id");

                    b.HasIndex("QuoteId");

                    b.ToTable("Costings");
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.Models.CostingTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<double?>("Cost");

                    b.Property<DateTime>("DateCreate");

                    b.Property<DateTime>("DateModify");

                    b.Property<string>("Name");

                    b.Property<double>("Qty");

                    b.Property<double?>("Total");

                    b.Property<int>("Unit");

                    b.HasKey("Id");

                    b.ToTable("CostingTemplates");
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.Models.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId");

                    b.Property<DateTime>("DateCreate");

                    b.Property<DateTime>("DateModify");

                    b.Property<string>("Number");

                    b.Property<string>("Remarks");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.Models.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyId");

                    b.Property<DateTime>("DateCreate");

                    b.Property<DateTime>("DateModify");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.Models.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("States");
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.Models.SystemSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<DateTime>("DateCreate");

                    b.Property<DateTime>("DateModify");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("SystemSettings");
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.Models.CallRecord", b =>
                {
                    b.HasOne("ColdCallsTracker.Code.Data.Models.Phone", "Phone")
                        .WithMany("CallRecords")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.Models.Company", b =>
                {
                    b.HasOne("ColdCallsTracker.Code.Data.Models.State", "State")
                        .WithMany("Companies")
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.Models.Costing", b =>
                {
                    b.HasOne("ColdCallsTracker.Code.Data.Models.Quote", "Quote")
                        .WithMany("Costings")
                        .HasForeignKey("QuoteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.Models.Phone", b =>
                {
                    b.HasOne("ColdCallsTracker.Code.Data.Models.Company", "Company")
                        .WithMany("Phones")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ColdCallsTracker.Code.Data.Models.Quote", b =>
                {
                    b.HasOne("ColdCallsTracker.Code.Data.Models.Company", "Company")
                        .WithMany("Quotes")
                        .HasForeignKey("CompanyId");
                });
#pragma warning restore 612, 618
        }
    }
}
