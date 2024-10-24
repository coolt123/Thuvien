﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThuvienMvc.Models;

#nullable disable

namespace ThuvienMvc.Migrations
{
    [DbContext(typeof(Data))]
    [Migration("20240920201304_Db2")]
    partial class Db2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ThuvienMvc.Models.Admin", b =>
                {
                    b.Property<int>("IdAmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAmin"));

                    b.Property<string>("AdminName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Createdat")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameAdmin")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("Updatedat")
                        .HasColumnType("datetime2");

                    b.Property<bool>("deleteflag")
                        .HasColumnType("bit");

                    b.HasKey("IdAmin");

                    b.ToTable("admins", (string)null);
                });

            modelBuilder.Entity("ThuvienMvc.Models.Author", b =>
                {
                    b.Property<int>("IdAuthor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAuthor"));

                    b.Property<DateTime>("Createdat")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameAuthor")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Updatedat")
                        .HasColumnType("datetime2");

                    b.Property<bool>("deleteflag")
                        .HasColumnType("bit");

                    b.HasKey("IdAuthor");

                    b.ToTable("authors", (string)null);
                });

            modelBuilder.Entity("ThuvienMvc.Models.Book", b =>
                {
                    b.Property<int>("IdBook")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdBook"));

                    b.Property<DateTime>("Createdat")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("IdAuthor")
                        .HasColumnType("int");

                    b.Property<int?>("IdGenres")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("PublishingYear")
                        .HasColumnType("int");

                    b.Property<int?>("QuantityInStock")
                        .HasColumnType("int");

                    b.Property<string>("SubTitle")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Updatedat")
                        .HasColumnType("datetime2");

                    b.Property<bool>("deleteflag")
                        .HasColumnType("bit");

                    b.HasKey("IdBook");

                    b.HasIndex("IdAuthor");

                    b.HasIndex("IdGenres");

                    b.ToTable("books", (string)null);
                });

            modelBuilder.Entity("ThuvienMvc.Models.Borrowing", b =>
                {
                    b.Property<int>("IdBor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdBor"));

                    b.Property<DateTime?>("ActualEndAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Createdat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Endat")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<DateTime>("Startat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Updatedat")
                        .HasColumnType("datetime2");

                    b.Property<bool>("deleteflag")
                        .HasColumnType("bit");

                    b.HasKey("IdBor");

                    b.HasIndex("IdUser");

                    b.ToTable("borrowings", (string)null);
                });

            modelBuilder.Entity("ThuvienMvc.Models.BorrowingItems", b =>
                {
                    b.Property<int>("IDitem")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDitem"));

                    b.Property<DateTime>("Createdat")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdBook")
                        .HasColumnType("int");

                    b.Property<int>("IdBor")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updatedat")
                        .HasColumnType("datetime2");

                    b.Property<bool>("deleteflag")
                        .HasColumnType("bit");

                    b.HasKey("IDitem");

                    b.HasIndex("IdBook");

                    b.HasIndex("IdBor");

                    b.ToTable("borrowing_items", (string)null);
                });

            modelBuilder.Entity("ThuvienMvc.Models.Genre", b =>
                {
                    b.Property<int>("IdGenres")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdGenres"));

                    b.Property<DateTime>("Createdat")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameGenres")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Updatedat")
                        .HasColumnType("datetime2");

                    b.Property<bool>("deleteflag")
                        .HasColumnType("bit");

                    b.HasKey("IdGenres");

                    b.ToTable("geres", (string)null);
                });

            modelBuilder.Entity("ThuvienMvc.Models.Rating", b =>
                {
                    b.Property<int>("IdRate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRate"));

                    b.Property<DateTime>("Createdat")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdBook")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<int>("Star")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updatedat")
                        .HasColumnType("datetime2");

                    b.Property<bool>("deleteflag")
                        .HasColumnType("bit");

                    b.HasKey("IdRate");

                    b.HasIndex("IdBook");

                    b.HasIndex("IdUser");

                    b.ToTable("ratings", (string)null);
                });

            modelBuilder.Entity("ThuvienMvc.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUser"));

                    b.Property<DateTime>("Createdat")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameUser")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("Updatedat")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("deleteflag")
                        .HasColumnType("bit");

                    b.HasKey("IdUser");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("ThuvienMvc.Models.Book", b =>
                {
                    b.HasOne("ThuvienMvc.Models.Author", "Author")
                        .WithMany("Book")
                        .HasForeignKey("IdAuthor")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ThuvienMvc.Models.Genre", "Genre")
                        .WithMany("Book")
                        .HasForeignKey("IdGenres")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Author");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("ThuvienMvc.Models.Borrowing", b =>
                {
                    b.HasOne("ThuvienMvc.Models.User", "User")
                        .WithMany("Borrowing")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ThuvienMvc.Models.BorrowingItems", b =>
                {
                    b.HasOne("ThuvienMvc.Models.Book", "Book")
                        .WithMany("BorrowingItems")
                        .HasForeignKey("IdBook")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ThuvienMvc.Models.Borrowing", "Borrowing")
                        .WithMany("BorrowingItems")
                        .HasForeignKey("IdBor")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Borrowing");
                });

            modelBuilder.Entity("ThuvienMvc.Models.Rating", b =>
                {
                    b.HasOne("ThuvienMvc.Models.Book", "Book")
                        .WithMany("Rating")
                        .HasForeignKey("IdBook")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ThuvienMvc.Models.User", "User")
                        .WithMany("Rating")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ThuvienMvc.Models.Author", b =>
                {
                    b.Navigation("Book");
                });

            modelBuilder.Entity("ThuvienMvc.Models.Book", b =>
                {
                    b.Navigation("BorrowingItems");

                    b.Navigation("Rating");
                });

            modelBuilder.Entity("ThuvienMvc.Models.Borrowing", b =>
                {
                    b.Navigation("BorrowingItems");
                });

            modelBuilder.Entity("ThuvienMvc.Models.Genre", b =>
                {
                    b.Navigation("Book");
                });

            modelBuilder.Entity("ThuvienMvc.Models.User", b =>
                {
                    b.Navigation("Borrowing");

                    b.Navigation("Rating");
                });
#pragma warning restore 612, 618
        }
    }
}
