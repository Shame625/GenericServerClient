﻿// <auto-generated />
using System;
using DatabaseCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DatabaseCore.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20191212102112_PlayerDboNameLength")]
    partial class PlayerDboNameLength
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GenericEntity.Dbo.Character", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Class");

                    b.Property<DateTime>("InsertDate")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<long>("Level");

                    b.Property<string>("Name")
                        .HasMaxLength(12);

                    b.Property<float>("Pos_X");

                    b.Property<float>("Pos_Y");

                    b.Property<float>("Pos_Z");

                    b.Property<long?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("GenericEntity.Dbo.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InsertDate")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Text")
                        .HasMaxLength(256);

                    b.Property<long?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("GenericEntity.Dbo.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InsertDate")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("UserName")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GenericEntity.Dbo.Character", b =>
                {
                    b.HasOne("GenericEntity.Dbo.User", "User")
                        .WithMany("Characters")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GenericEntity.Dbo.Message", b =>
                {
                    b.HasOne("GenericEntity.Dbo.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
