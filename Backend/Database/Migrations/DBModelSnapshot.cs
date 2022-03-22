﻿// <auto-generated />
using System;
using CodingDays.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CodingDays.Database.Migrations
{
    [DbContext(typeof(DB))]
    partial class DBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CodingDays.Database.Entities.Cypher", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.HasKey("Id");

                    b.ToTable("Cyphers");
                });

            modelBuilder.Entity("CodingDays.Database.Entities.CypherUsage", b =>
                {
                    b.Property<Guid>("TeamId")
                        .HasColumnType("char(36)");

                    b.Property<string>("CypherId")
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<Guid>("HintId")
                        .HasColumnType("char(36)");

                    b.HasKey("TeamId", "CypherId");

                    b.HasIndex("CypherId");

                    b.HasIndex("HintId");

                    b.ToTable("CypherUsages");
                });

            modelBuilder.Entity("CodingDays.Database.Entities.Hint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<byte>("Order")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("Step")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Hints");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Order = (byte)0,
                            Step = 0,
                            Text = "Další nápovědu dostanete osobně. Tento kód můžete zadat v dalším kroku znovu"
                        });
                });

            modelBuilder.Entity("CodingDays.Database.Entities.Registration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Birth")
                        .HasColumnType("datetime(6)");

                    b.Property<bool?>("Bonus")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Languages")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("NeedNtb")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Note")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<Guid?>("TeamId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("CodingDays.Database.Entities.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<int>("CurrentStep")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("CodingDays.Database.Entities.CypherUsage", b =>
                {
                    b.HasOne("CodingDays.Database.Entities.Cypher", "Cypher")
                        .WithMany("CypherUsages")
                        .HasForeignKey("CypherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CodingDays.Database.Entities.Hint", "Hint")
                        .WithMany("CypherUsages")
                        .HasForeignKey("HintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CodingDays.Database.Entities.Team", "Team")
                        .WithMany("CypherUsages")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cypher");

                    b.Navigation("Hint");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("CodingDays.Database.Entities.Registration", b =>
                {
                    b.HasOne("CodingDays.Database.Entities.Team", "Team")
                        .WithMany("Registrations")
                        .HasForeignKey("TeamId");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("CodingDays.Database.Entities.Cypher", b =>
                {
                    b.Navigation("CypherUsages");
                });

            modelBuilder.Entity("CodingDays.Database.Entities.Hint", b =>
                {
                    b.Navigation("CypherUsages");
                });

            modelBuilder.Entity("CodingDays.Database.Entities.Team", b =>
                {
                    b.Navigation("CypherUsages");

                    b.Navigation("Registrations");
                });
#pragma warning restore 612, 618
        }
    }
}
