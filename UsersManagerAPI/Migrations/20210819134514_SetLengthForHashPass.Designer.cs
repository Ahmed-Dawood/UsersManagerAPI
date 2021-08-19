﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UsersManagerAPI.DataAccess;

namespace UsersManagerAPI.Migrations
{
    [DbContext(typeof(UsersBDContext))]
    [Migration("20210819134514_SetLengthForHashPass")]
    partial class SetLengthForHashPass
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UsersManagerAPI.DomainClasses.Models.CompanyInfo", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyPricingPlan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("UsersManagerAPI.DomainClasses.Models.UserInfo", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountPricingPlan")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<int?>("CompanyInfoCompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Role")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("SaltKey")
                        .IsRequired()
                        .HasColumnType("varchar(48)");

                    b.Property<DateTime?>("UpdatedDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("UserId");

                    b.HasIndex("CompanyInfoCompanyId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UsersManagerAPI.DomainClasses.Models.CompanyInfo", b =>
                {
                    b.HasOne("UsersManagerAPI.DomainClasses.Models.UserInfo", "AdminUser")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("AdminUser");
                });

            modelBuilder.Entity("UsersManagerAPI.DomainClasses.Models.UserInfo", b =>
                {
                    b.HasOne("UsersManagerAPI.DomainClasses.Models.CompanyInfo", null)
                        .WithMany("CompanyEmployees")
                        .HasForeignKey("CompanyInfoCompanyId");
                });

            modelBuilder.Entity("UsersManagerAPI.DomainClasses.Models.CompanyInfo", b =>
                {
                    b.Navigation("CompanyEmployees");
                });
#pragma warning restore 612, 618
        }
    }
}
