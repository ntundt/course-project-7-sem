﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using diploma.Data;

#nullable disable

namespace diploma.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("ClaimUserRole", b =>
                {
                    b.Property<int>("ClaimsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserRolesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ClaimsId", "UserRolesId");

                    b.HasIndex("UserRolesId");

                    b.ToTable("ClaimUserRole");

                    b.HasData(
                        new
                        {
                            ClaimsId = 1,
                            UserRolesId = 1
                        },
                        new
                        {
                            ClaimsId = 2,
                            UserRolesId = 1
                        },
                        new
                        {
                            ClaimsId = 3,
                            UserRolesId = 1
                        },
                        new
                        {
                            ClaimsId = 4,
                            UserRolesId = 1
                        },
                        new
                        {
                            ClaimsId = 5,
                            UserRolesId = 1
                        });
                });

            modelBuilder.Entity("ContestUser", b =>
                {
                    b.Property<Guid>("ContestsUserParticipatesInId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ParticipantsId")
                        .HasColumnType("TEXT");

                    b.HasKey("ContestsUserParticipatesInId", "ParticipantsId");

                    b.HasIndex("ParticipantsId");

                    b.ToTable("ContestUser");
                });

            modelBuilder.Entity("ContestUser1", b =>
                {
                    b.Property<Guid>("CommissionMembersId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ContestId")
                        .HasColumnType("TEXT");

                    b.HasKey("CommissionMembersId", "ContestId");

                    b.HasIndex("ContestId");

                    b.ToTable("ContestUser1");
                });

            modelBuilder.Entity("GradeAdjustment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AttemptId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("Grade")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AttemptId");

                    b.HasIndex("UserId");

                    b.ToTable("GradeAdjustments");
                });

            modelBuilder.Entity("diploma.Features.Attempts.Attempt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Dbms")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ErrorMessage")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("OriginalAttemptId")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Originality")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ProblemId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SolutionPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("OriginalAttemptId");

                    b.HasIndex("ProblemId");

                    b.ToTable("Attempts");
                });

            modelBuilder.Entity("diploma.Features.Authentication.Claim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Claims");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ManageContests"
                        },
                        new
                        {
                            Id = 2,
                            Name = "ManageProblems"
                        },
                        new
                        {
                            Id = 3,
                            Name = "ManageAttempts"
                        },
                        new
                        {
                            Id = 4,
                            Name = "ManageContestParticipants"
                        },
                        new
                        {
                            Id = 5,
                            Name = "ManageSchemaDescriptions"
                        });
                });

            modelBuilder.Entity("diploma.Features.Authentication.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("diploma.Features.Contests.Contest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("DescriptionPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FinishDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Contests");
                });

            modelBuilder.Entity("diploma.Features.Problems.Problem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("CaseSensitive")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ContestId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("FloatMaxDelta")
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxGrade")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("OrderMatters")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Ordinal")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("SchemaDescriptionId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SolutionDbms")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SolutionPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StatementPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("TimeLimit")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ContestId");

                    b.HasIndex("SchemaDescriptionId");

                    b.ToTable("Problems");
                });

            modelBuilder.Entity("diploma.Features.SchemaDescriptions.SchemaDescription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ContestId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ContestId");

                    b.ToTable("SchemaDescriptions");
                });

            modelBuilder.Entity("diploma.Features.SchemaDescriptions.SchemaDescriptionFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Dbms")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(260)
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasProblems")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Problems")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SchemaDescriptionId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SchemaDescriptionId");

                    b.ToTable("SchemaDescriptionFiles");
                });

            modelBuilder.Entity("diploma.Features.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AdditionalInfo")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EmailConfirmationToken")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EmailConfirmationTokenExpiresAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PasswordRecoveryToken")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PasswordRecoveryTokenExpiresAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserRoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ClaimUserRole", b =>
                {
                    b.HasOne("diploma.Features.Authentication.Claim", null)
                        .WithMany()
                        .HasForeignKey("ClaimsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("diploma.Features.Authentication.UserRole", null)
                        .WithMany()
                        .HasForeignKey("UserRolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContestUser", b =>
                {
                    b.HasOne("diploma.Features.Contests.Contest", null)
                        .WithMany()
                        .HasForeignKey("ContestsUserParticipatesInId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("diploma.Features.Users.User", null)
                        .WithMany()
                        .HasForeignKey("ParticipantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContestUser1", b =>
                {
                    b.HasOne("diploma.Features.Users.User", null)
                        .WithMany()
                        .HasForeignKey("CommissionMembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("diploma.Features.Contests.Contest", null)
                        .WithMany()
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GradeAdjustment", b =>
                {
                    b.HasOne("diploma.Features.Attempts.Attempt", "Attempt")
                        .WithMany()
                        .HasForeignKey("AttemptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("diploma.Features.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attempt");

                    b.Navigation("User");
                });

            modelBuilder.Entity("diploma.Features.Attempts.Attempt", b =>
                {
                    b.HasOne("diploma.Features.Users.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("diploma.Features.Attempts.Attempt", "OriginalAttempt")
                        .WithMany()
                        .HasForeignKey("OriginalAttemptId");

                    b.HasOne("diploma.Features.Problems.Problem", "Problem")
                        .WithMany()
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("OriginalAttempt");

                    b.Navigation("Problem");
                });

            modelBuilder.Entity("diploma.Features.Contests.Contest", b =>
                {
                    b.HasOne("diploma.Features.Users.User", "Author")
                        .WithMany("AuthoredContests")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("diploma.Features.Problems.Problem", b =>
                {
                    b.HasOne("diploma.Features.Contests.Contest", "Contest")
                        .WithMany("Problems")
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("diploma.Features.SchemaDescriptions.SchemaDescription", "SchemaDescription")
                        .WithMany()
                        .HasForeignKey("SchemaDescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contest");

                    b.Navigation("SchemaDescription");
                });

            modelBuilder.Entity("diploma.Features.SchemaDescriptions.SchemaDescription", b =>
                {
                    b.HasOne("diploma.Features.Contests.Contest", "Contest")
                        .WithMany()
                        .HasForeignKey("ContestId");

                    b.Navigation("Contest");
                });

            modelBuilder.Entity("diploma.Features.SchemaDescriptions.SchemaDescriptionFile", b =>
                {
                    b.HasOne("diploma.Features.SchemaDescriptions.SchemaDescription", "SchemaDescription")
                        .WithMany("Files")
                        .HasForeignKey("SchemaDescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SchemaDescription");
                });

            modelBuilder.Entity("diploma.Features.Users.User", b =>
                {
                    b.HasOne("diploma.Features.Authentication.UserRole", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("diploma.Features.Contests.Contest", b =>
                {
                    b.Navigation("Problems");
                });

            modelBuilder.Entity("diploma.Features.SchemaDescriptions.SchemaDescription", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("diploma.Features.Users.User", b =>
                {
                    b.Navigation("AuthoredContests");
                });
#pragma warning restore 612, 618
        }
    }
}
