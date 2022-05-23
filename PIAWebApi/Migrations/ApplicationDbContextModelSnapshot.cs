﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PIAWebApi;

#nullable disable

namespace PIAWebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PIAWebApi.Entidades.Ganadores", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ParticipanteId")
                        .HasColumnType("int");

                    b.Property<string>("Premios")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParticipanteId");

                    b.ToTable("Ganadores");
                });

            modelBuilder.Entity("PIAWebApi.Entidades.Participante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaGanadores")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Ganador")
                        .HasColumnType("bit");

                    b.Property<int>("LoteriaId")
                        .HasColumnType("int");

                    b.Property<string>("Telefono")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("Participantes");
                });

            modelBuilder.Entity("PIAWebApi.Entidades.Rifa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("NameRifa")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("Rifas");
                });

            modelBuilder.Entity("PIAWebApi.Entidades.RifasParticipantes", b =>
                {
                    b.Property<int>("RifaId")
                        .HasColumnType("int");

                    b.Property<int>("ParticipanteId")
                        .HasColumnType("int");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.HasKey("RifaId", "ParticipanteId");

                    b.HasIndex("ParticipanteId");

                    b.ToTable("RifasParticipantes");
                });

            modelBuilder.Entity("PIAWebApi.Entidades.Ganadores", b =>
                {
                    b.HasOne("PIAWebApi.Entidades.Participante", "participante")
                        .WithMany("Ganadores")
                        .HasForeignKey("ParticipanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("participante");
                });

            modelBuilder.Entity("PIAWebApi.Entidades.RifasParticipantes", b =>
                {
                    b.HasOne("PIAWebApi.Entidades.Participante", "Participante")
                        .WithMany("RifasParticipantes")
                        .HasForeignKey("ParticipanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PIAWebApi.Entidades.Rifa", "Rifa")
                        .WithMany("RifasParticipantes")
                        .HasForeignKey("RifaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Participante");

                    b.Navigation("Rifa");
                });

            modelBuilder.Entity("PIAWebApi.Entidades.Participante", b =>
                {
                    b.Navigation("Ganadores");

                    b.Navigation("RifasParticipantes");
                });

            modelBuilder.Entity("PIAWebApi.Entidades.Rifa", b =>
                {
                    b.Navigation("RifasParticipantes");
                });
#pragma warning restore 612, 618
        }
    }
}