﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Snipper.Data;

namespace Snipper.Web.Data.Migrations
{
    [DbContext(typeof(SnipperDbContext))]
    partial class SnipperDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Snipper.Web.Entities.Category", b =>
                {
                    b.Property<string>("Slug")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Slug");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Snipper.Web.Entities.Snippet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategorySlug")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CategorySlug");

                    b.ToTable("Snippets");
                });

            modelBuilder.Entity("Snipper.Web.Entities.SnippetFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid>("SnippetId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SnippetId");

                    b.ToTable("SnippetFiles");
                });

            modelBuilder.Entity("Snipper.Web.Entities.Snippet", b =>
                {
                    b.HasOne("Snipper.Web.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategorySlug");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Snipper.Web.Entities.SnippetFile", b =>
                {
                    b.HasOne("Snipper.Web.Entities.Snippet", "Snippet")
                        .WithMany("Files")
                        .HasForeignKey("SnippetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Snippet");
                });

            modelBuilder.Entity("Snipper.Web.Entities.Snippet", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
