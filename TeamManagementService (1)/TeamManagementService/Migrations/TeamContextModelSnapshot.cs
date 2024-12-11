﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeamManagementService.Data;

#nullable disable

namespace TeamManagementService.Migrations
{
    [DbContext(typeof(TeamContext))]
    partial class TeamContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TeamManagementService.Model.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeamId"));

                    b.Property<int>("NumberOfTeam")
                        .HasColumnType("int");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeamId");

                    b.ToTable("Teams", (string)null);

                    b.HasData(
                        new
                        {
                            TeamId = 1,
                            NumberOfTeam = 5,
                            TeamName = "Optic"
                        },
                        new
                        {
                            TeamId = 2,
                            NumberOfTeam = 8,
                            TeamName = "Cloud9"
                        },
                        new
                        {
                            TeamId = 3,
                            NumberOfTeam = 3,
                            TeamName = "Faze"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
