﻿// <auto-generated />
using MessaingData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MessagingData.Migrations
{
    [DbContext(typeof(MessagingContext))]
    [Migration("20250311143131_updatedPasswordTable")]
    partial class updatedPasswordTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MessagingDomain.Password", b =>
                {
                    b.Property<int>("PasswordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PasswordId"));

                    b.Property<string>("HashedValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PasswordId");

                    b.ToTable("Passwords");
                });

            modelBuilder.Entity("MessagingDomain.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PasswordId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("PasswordId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MessagingDomain.User", b =>
                {
                    b.HasOne("MessagingDomain.Password", "Password")
                        .WithMany()
                        .HasForeignKey("PasswordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Password");
                });
#pragma warning restore 612, 618
        }
    }
}
