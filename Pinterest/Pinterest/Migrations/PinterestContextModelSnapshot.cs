﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Pinterest.Helpers;

namespace Pinterest.Migrations
{
    [DbContext(typeof(PinterestContext))]
    partial class PinterestContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Pinterest.Models.Board", b =>
                {
                    b.Property<int>("BoardId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatorId");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("BoardId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("Pinterest.Models.Following", b =>
                {
                    b.Property<int>("FolloweeId");

                    b.Property<int>("FollowerId");

                    b.HasKey("FolloweeId", "FollowerId");

                    b.HasIndex("FollowerId");

                    b.ToTable("Followings");
                });

            modelBuilder.Entity("Pinterest.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Height")
                        .IsRequired();

                    b.Property<string>("URL")
                        .IsRequired();

                    b.Property<string>("Width")
                        .IsRequired();

                    b.HasKey("ImageId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Pinterest.Models.Pin", b =>
                {
                    b.Property<int>("PinId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BoardId");

                    b.Property<int>("ImageId");

                    b.Property<string>("Link");

                    b.Property<string>("Note")
                        .IsRequired();

                    b.HasKey("PinId");

                    b.HasIndex("BoardId");

                    b.HasIndex("ImageId");

                    b.ToTable("Pins");
                });

            modelBuilder.Entity("Pinterest.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Pinterest.Models.Board", b =>
                {
                    b.HasOne("Pinterest.Models.User", "Creator")
                        .WithMany("Boards")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Pinterest.Models.Following", b =>
                {
                    b.HasOne("Pinterest.Models.Board", "Followee")
                        .WithMany("Followings")
                        .HasForeignKey("FolloweeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Pinterest.Models.User", "Follower")
                        .WithMany("Followings")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Pinterest.Models.Pin", b =>
                {
                    b.HasOne("Pinterest.Models.Board", "Board")
                        .WithMany("Pins")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Pinterest.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
