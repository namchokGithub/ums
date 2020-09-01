﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UMS.Models;

namespace UMS.Migrations.Models
{
    [DbContext(typeof(AccountContext))]
    partial class AccountContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UMS.Models.Account", b =>
                {
                    b.Property<string>("acc_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("acc_ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("acc_Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("acc_Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("acc_IsActive")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("acc_Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("acc_NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("acc_NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("acc_PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("acc_Rolename")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("acc_SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("acc_TypeAccoutname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("acc_User")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("acc_Id");

                    b.ToTable("Account");
                });
#pragma warning restore 612, 618
        }
    }
}