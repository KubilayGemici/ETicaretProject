﻿// <auto-generated />
using System;
using BilgeAdamBitirmeProjesi.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BilgeAdamBitirmeProjesi.Model.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("BilgeAdamBitirmeProjesi.Model.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("CreatedComputerName")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedIP")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.Property<Guid?>("CreatedUserID")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("ModifiedComputerName")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedIP")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.Property<Guid?>("ModifiedUserID")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreatedUserID");

                    b.HasIndex("ModifiedUserID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BilgeAdamBitirmeProjesi.Model.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CreatedComputerName")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedIP")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.Property<Guid?>("CreatedUserID")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifiedComputerName")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedIP")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.Property<Guid?>("ModifiedUserID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CreatedUserID");

                    b.HasIndex("ModifiedUserID");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BilgeAdamBitirmeProjesi.Model.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedComputerName")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedIP")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.Property<Guid?>("CreatedUserID")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifiedComputerName")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedIP")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.Property<Guid?>("ModifiedUserID")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PaymentType")
                        .HasColumnType("integer");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasMaxLength(15);

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("integer")
                        .HasMaxLength(20);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CreatedUserID");

                    b.HasIndex("ModifiedUserID");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BilgeAdamBitirmeProjesi.Model.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedComputerName")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedIP")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.Property<Guid?>("CreatedUserID")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ModifiedComputerName")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedIP")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.Property<Guid?>("ModifiedUserID")
                        .HasColumnType("uuid");

                    b.Property<string>("Order")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasMaxLength(10);

                    b.Property<string>("ProductDetail")
                        .HasColumnType("text");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("QuantityPerUnit")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<short>("UnitsInStock")
                        .HasColumnType("smallint");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatedUserID");

                    b.HasIndex("ModifiedUserID");

                    b.HasIndex("UserId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("BilgeAdamBitirmeProjesi.Model.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedComputerName")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedIP")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.Property<Guid?>("CreatedUserID")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ImageUrl")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<string>("LastIPAdress")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp without time zone")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ModifiedComputerName")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedIP")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.Property<Guid?>("ModifiedUserID")
                        .HasColumnType("uuid");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("character varying(12)")
                        .HasMaxLength(12);

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CreatedUserID");

                    b.HasIndex("ModifiedUserID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fd6ddaa7-3b29-46b1-9326-599c8c71558a"),
                            Email = "admin@admin.com",
                            FirstName = "Admin",
                            ImageUrl = "/",
                            LastIPAdress = "94.54.234.138",
                            LastLogin = new DateTime(2020, 7, 9, 12, 46, 12, 163, DateTimeKind.Local).AddTicks(8337),
                            LastName = "Admin",
                            Password = "123",
                            Status = 1,
                            Title = "Admin"
                        });
                });

            modelBuilder.Entity("BilgeAdamBitirmeProjesi.Model.Entities.Category", b =>
                {
                    b.HasOne("BilgeAdamBitirmeProjesi.Model.Entities.User", "CreatedUserCategory")
                        .WithMany("CreatedUserCategories")
                        .HasForeignKey("CreatedUserID");

                    b.HasOne("BilgeAdamBitirmeProjesi.Model.Entities.User", "ModifiedUserCategory")
                        .WithMany("ModifiedUserCategories")
                        .HasForeignKey("ModifiedUserID");
                });

            modelBuilder.Entity("BilgeAdamBitirmeProjesi.Model.Entities.Comment", b =>
                {
                    b.HasOne("BilgeAdamBitirmeProjesi.Model.Entities.User", "CreatedUserComment")
                        .WithMany("CreatedUserComments")
                        .HasForeignKey("CreatedUserID");

                    b.HasOne("BilgeAdamBitirmeProjesi.Model.Entities.User", "ModifiedUserComment")
                        .WithMany("ModifiedUserComments")
                        .HasForeignKey("ModifiedUserID");

                    b.HasOne("BilgeAdamBitirmeProjesi.Model.Entities.Product", "Product")
                        .WithMany("Comments")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BilgeAdamBitirmeProjesi.Model.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BilgeAdamBitirmeProjesi.Model.Entities.Order", b =>
                {
                    b.HasOne("BilgeAdamBitirmeProjesi.Model.Entities.User", "CreatedUserOrder")
                        .WithMany("CreatedUserOrders")
                        .HasForeignKey("CreatedUserID");

                    b.HasOne("BilgeAdamBitirmeProjesi.Model.Entities.User", "ModifiedUserOrder")
                        .WithMany("ModifiedUserOrders")
                        .HasForeignKey("ModifiedUserID");

                    b.HasOne("BilgeAdamBitirmeProjesi.Model.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BilgeAdamBitirmeProjesi.Model.Entities.User", null)
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BilgeAdamBitirmeProjesi.Model.Entities.Product", b =>
                {
                    b.HasOne("BilgeAdamBitirmeProjesi.Model.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BilgeAdamBitirmeProjesi.Model.Entities.User", "CreatedUserProduct")
                        .WithMany("CreatedUserProducts")
                        .HasForeignKey("CreatedUserID");

                    b.HasOne("BilgeAdamBitirmeProjesi.Model.Entities.User", "ModifiedUserProduct")
                        .WithMany("ModifiedUserProducts")
                        .HasForeignKey("ModifiedUserID");

                    b.HasOne("BilgeAdamBitirmeProjesi.Model.Entities.User", "User")
                        .WithMany("Products")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BilgeAdamBitirmeProjesi.Model.Entities.User", b =>
                {
                    b.HasOne("BilgeAdamBitirmeProjesi.Model.Entities.User", "CreatedUser")
                        .WithMany("CreatedUsers")
                        .HasForeignKey("CreatedUserID");

                    b.HasOne("BilgeAdamBitirmeProjesi.Model.Entities.User", "ModifiedUser")
                        .WithMany("ModifiedUsers")
                        .HasForeignKey("ModifiedUserID");
                });
#pragma warning restore 612, 618
        }
    }
}
