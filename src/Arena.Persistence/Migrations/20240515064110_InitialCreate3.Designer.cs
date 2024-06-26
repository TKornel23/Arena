﻿// <auto-generated />
using System;
using Arena.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Arena.Persistence.Migrations
{
    [DbContext(typeof(ArenaContext))]
    [Migration("20240515064110_InitialCreate3")]
    partial class InitialCreate3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("Arena.Domain.Arena", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(36)")
                        .HasColumnName("ID");

                    b.Property<int>("RoundCount")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ROUND_COUNT");

                    b.HasKey("Guid");

                    b.ToTable("Arena", "ar");
                });

            modelBuilder.Entity("Arena.Domain.Round", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(36)")
                        .HasColumnName("ID");

                    b.Property<Guid>("ArenaGuid")
                        .HasColumnType("nvarchar(36)")
                        .HasColumnName("ARENA_GUID");

                    b.Property<int>("Id")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ROUND");

                    b.HasKey("Guid");

                    b.HasIndex("ArenaGuid");

                    b.ToTable("ROUNDS", "ar");
                });

            modelBuilder.Entity("Arena.Domain.Round", b =>
                {
                    b.HasOne("Arena.Domain.Arena", "Arena")
                        .WithMany("Rounds")
                        .HasForeignKey("ArenaGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Arena.Domain.Entity", "Attacker", b1 =>
                        {
                            b1.Property<Guid>("RoundGuid")
                                .HasColumnType("nvarchar(36)");

                            b1.Property<float>("Change")
                                .HasColumnType("REAL")
                                .HasColumnName("ATTACKER_HEALTH_CHANGE");

                            b1.Property<Guid>("Guid")
                                .HasColumnType("TEXT")
                                .HasColumnName("ATTACKER_GUID");

                            b1.Property<float>("Health")
                                .HasColumnType("REAL")
                                .HasColumnName("ATTACKER_HEALTH");

                            b1.Property<float>("MaxHealth")
                                .HasColumnType("REAL")
                                .HasColumnName("ATTACKER_MAX_HEALTH");

                            b1.Property<string>("Role")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("ATTACKER_ROLE");

                            b1.HasKey("RoundGuid");

                            b1.ToTable("ROUNDS", "ar");

                            b1.WithOwner()
                                .HasForeignKey("RoundGuid");
                        });

                    b.OwnsOne("Arena.Domain.Entity", "Defender", b1 =>
                        {
                            b1.Property<Guid>("RoundGuid")
                                .HasColumnType("nvarchar(36)");

                            b1.Property<float>("Change")
                                .HasColumnType("REAL")
                                .HasColumnName("DEFENDER_HEALTH_CHANGE");

                            b1.Property<Guid>("Guid")
                                .HasColumnType("TEXT")
                                .HasColumnName("DEFENDER_GUID");

                            b1.Property<float>("Health")
                                .HasColumnType("REAL")
                                .HasColumnName("DEFENDER_HEALTH");

                            b1.Property<float>("MaxHealth")
                                .HasColumnType("REAL")
                                .HasColumnName("DEFENDER_MAX_HEALTH");

                            b1.Property<string>("Role")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("DEFENDER_ROLE");

                            b1.HasKey("RoundGuid");

                            b1.ToTable("ROUNDS", "ar");

                            b1.WithOwner()
                                .HasForeignKey("RoundGuid");
                        });

                    b.Navigation("Arena");

                    b.Navigation("Attacker")
                        .IsRequired();

                    b.Navigation("Defender")
                        .IsRequired();
                });

            modelBuilder.Entity("Arena.Domain.Arena", b =>
                {
                    b.Navigation("Rounds");
                });
#pragma warning restore 612, 618
        }
    }
}
