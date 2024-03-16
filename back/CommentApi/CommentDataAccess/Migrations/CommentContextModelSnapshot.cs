﻿// <auto-generated />
using System;
using CommentDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CommentDataAccess.Migrations
{
    [DbContext(typeof(CommentContext))]
    partial class CommentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CommentDataAccess.Entities.Commentaire", b =>
                {
                    b.Property<Guid>("IdComment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Datepublication")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdAI")
                        .HasColumnType("int");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NombreEtoile")
                        .HasColumnType("int");

                    b.HasKey("IdComment");

                    b.HasIndex("IdComment");

                    b.ToTable("Commentaires");
                });
#pragma warning restore 612, 618
        }
    }
}
