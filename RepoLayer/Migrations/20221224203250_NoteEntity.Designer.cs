﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepoLayer.Context;

#nullable disable

namespace RepoLayer.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20221224203250_NoteEntity")]
    partial class NoteEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RepoLayer.Entity.CollabEntity", b =>
                {
                    b.Property<long>("CollabID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CollabID"), 1L, 1);

                    b.Property<string>("CollaEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Note_ID")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Updatedata")
                        .HasColumnType("datetime2");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("CollabID");

                    b.HasIndex("Note_ID");

                    b.HasIndex("UserID");

                    b.ToTable("CollabTable");
                });

            modelBuilder.Entity("RepoLayer.Entity.LabelEntity", b =>
                {
                    b.Property<long>("LableID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LableID"), 1L, 1);

                    b.Property<string>("LableName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Note_ID")
                        .HasColumnType("bigint");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("LableID");

                    b.HasIndex("Note_ID");

                    b.HasIndex("UserID");

                    b.ToTable("LabelTable");
                });

            modelBuilder.Entity("RepoLayer.Entity.NoteEntity", b =>
                {
                    b.Property<long>("Note_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Note_ID"), 1L, 1);

                    b.Property<bool>("Archive")
                        .HasColumnType("bit");

                    b.Property<string>("Background_Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note_Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Pin")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Reminder")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Trash")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("Note_ID");

                    b.HasIndex("UserID");

                    b.ToTable("noteTable");
                });

            modelBuilder.Entity("RepoLayer.Entity.UserEntity", b =>
                {
                    b.Property<long>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserID"), 1L, 1);

                    b.Property<string>("EmailID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("userTable");
                });

            modelBuilder.Entity("RepoLayer.Entity.CollabEntity", b =>
                {
                    b.HasOne("RepoLayer.Entity.NoteEntity", "Note")
                        .WithMany()
                        .HasForeignKey("Note_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepoLayer.Entity.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Note");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RepoLayer.Entity.LabelEntity", b =>
                {
                    b.HasOne("RepoLayer.Entity.NoteEntity", "Note")
                        .WithMany()
                        .HasForeignKey("Note_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepoLayer.Entity.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Note");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RepoLayer.Entity.NoteEntity", b =>
                {
                    b.HasOne("RepoLayer.Entity.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
