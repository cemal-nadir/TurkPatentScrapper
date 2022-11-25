using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TPHunter.WebServices.Shared.MainData.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttorneyCompanies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttorneyCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Designers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesignProductPriortyCountries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignProductPriortyCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesignProductPriortyTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignProductPriortyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesignStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesignTransactionDescriptionDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Detail = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignTransactionDescriptionDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesignTransactionTypeDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Detail = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignTransactionTypeDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HolderCode = table.Column<string>(type: "text", nullable: true),
                    HolderName = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InventorCode = table.Column<string>(type: "text", nullable: true),
                    InventorName = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocarnoClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocarnoClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatentApplicationTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentApplicationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatentClassTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentClassTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatentPriortyCountries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentPriortyCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatentProtectionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentProtectionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatentPublicationDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentPublicationDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatentTransactionNames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Transaction = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentTransactionNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkDecisionReasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkDecisionReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkPriortyCountries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkPriortyCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkTransactionDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkTransactionDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkTransactionNames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Transaction = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkTransactionNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkTransactionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkTransactionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attorneys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    AttorneyCompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attorneys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attorneys_AttorneyCompanies_AttorneyCompanyId",
                        column: x => x.AttorneyCompanyId,
                        principalTable: "AttorneyCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DesignTransactionDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DesignTransactionDescriptionDetailId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignTransactionDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignTransactionDescriptions_DesignTransactionDescriptionD~",
                        column: x => x.DesignTransactionDescriptionDetailId,
                        principalTable: "DesignTransactionDescriptionDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DesignTransactionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    DesignTransactionTypeDetailId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignTransactionTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignTransactionTypes_DesignTransactionTypeDetails_DesignT~",
                        column: x => x.DesignTransactionTypeDetailId,
                        principalTable: "DesignTransactionTypeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HolderRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HolderId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataType = table.Column<int>(type: "integer", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolderRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HolderRelations_Holders_HolderId",
                        column: x => x.HolderId,
                        principalTable: "Holders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentClassTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatentClasses_PatentClassTypes_PatentClassTypeId",
                        column: x => x.PatentClassTypeId,
                        principalTable: "PatentClassTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkDecisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Decision = table.Column<string>(type: "text", nullable: true),
                    TradeMarkDecisionReasonId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkDecisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeMarkDecisions_TradeMarkDecisionReasons_TradeMarkDecisi~",
                        column: x => x.TradeMarkDecisionReasonId,
                        principalTable: "TradeMarkDecisionReasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkPriorties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TradeMarkPriortyCountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    ApplicationNumber = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkPriorties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeMarkPriorties_TradeMarkPriortyCountries_TradeMarkPrior~",
                        column: x => x.TradeMarkPriortyCountryId,
                        principalTable: "TradeMarkPriortyCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Designs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationNumber = table.Column<string>(type: "text", nullable: true),
                    ApplicationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "text", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BulletinNumber = table.Column<string>(type: "text", nullable: true),
                    BulletinDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DesignStatusId = table.Column<Guid>(type: "uuid", nullable: true),
                    AttorneyId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Designs_Attorneys_AttorneyId",
                        column: x => x.AttorneyId,
                        principalTable: "Attorneys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Designs_DesignStatuses_DesignStatusId",
                        column: x => x.DesignStatusId,
                        principalTable: "DesignStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationNumber = table.Column<string>(type: "text", nullable: true),
                    ApplicationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PatentApplicationTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    DocumentNumber = table.Column<string>(type: "text", nullable: true),
                    DocumentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "text", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PatentProtectionTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    EpcPublishNumber = table.Column<string>(type: "text", nullable: true),
                    EpcApplicationNumber = table.Column<string>(type: "text", nullable: true),
                    PctPublishNumber = table.Column<string>(type: "text", nullable: true),
                    PctApplicationNumber = table.Column<string>(type: "text", nullable: true),
                    PctPublishDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    InventionTitle = table.Column<string>(type: "text", nullable: true),
                    InventionSummary = table.Column<string>(type: "text", nullable: true),
                    AttorneyId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patents_Attorneys_AttorneyId",
                        column: x => x.AttorneyId,
                        principalTable: "Attorneys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patents_PatentApplicationTypes_PatentApplicationTypeId",
                        column: x => x.PatentApplicationTypeId,
                        principalTable: "PatentApplicationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patents_PatentProtectionTypes_PatentProtectionTypeId",
                        column: x => x.PatentProtectionTypeId,
                        principalTable: "PatentProtectionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationNumber = table.Column<string>(type: "text", nullable: true),
                    ApplicationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "text", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    InternationalRegistrationNumber = table.Column<string>(type: "text", nullable: true),
                    DocumentNumber = table.Column<string>(type: "text", nullable: true),
                    DeclareBullettinDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RegistrationBullettinDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeclareBullettinNumber = table.Column<string>(type: "text", nullable: true),
                    RegistrationBullettinNumber = table.Column<string>(type: "text", nullable: true),
                    ProtectionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TradeMarkStatusId = table.Column<Guid>(type: "uuid", nullable: true),
                    Classes = table.Column<int[]>(type: "integer[]", nullable: true),
                    TradeMarkTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    AttorneyId = table.Column<Guid>(type: "uuid", nullable: true),
                    TradeMarkDecisionId = table.Column<Guid>(type: "uuid", nullable: true),
                    TradeMarkPriortyId = table.Column<Guid>(type: "uuid", nullable: true),
                    ImageId = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeMarks_Attorneys_AttorneyId",
                        column: x => x.AttorneyId,
                        principalTable: "Attorneys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeMarks_TradeMarkDecisions_TradeMarkDecisionId",
                        column: x => x.TradeMarkDecisionId,
                        principalTable: "TradeMarkDecisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeMarks_TradeMarkPriorties_TradeMarkPriortyId",
                        column: x => x.TradeMarkPriortyId,
                        principalTable: "TradeMarkPriorties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeMarks_TradeMarkStatuses_TradeMarkStatusId",
                        column: x => x.TradeMarkStatusId,
                        principalTable: "TradeMarkStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeMarks_TradeMarkTypes_TradeMarkTypeId",
                        column: x => x.TradeMarkTypeId,
                        principalTable: "TradeMarkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DesignerRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DesignId = table.Column<Guid>(type: "uuid", nullable: false),
                    DesignerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignerRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignerRelations_Designers_DesignerId",
                        column: x => x.DesignerId,
                        principalTable: "Designers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignerRelations_Designs_DesignId",
                        column: x => x.DesignId,
                        principalTable: "Designs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DesignId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PriortyApplicationNumber = table.Column<string>(type: "text", nullable: true),
                    DesignPriortyCountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExhibitionName = table.Column<string>(type: "text", nullable: true),
                    ExhibitionPlace = table.Column<string>(type: "text", nullable: true),
                    ExhibitionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FirstExhibitionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PriortyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DesignProductPriortyTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsProductApproved = table.Column<bool>(type: "boolean", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignProducts_DesignProductPriortyCountries_DesignPriortyC~",
                        column: x => x.DesignPriortyCountryId,
                        principalTable: "DesignProductPriortyCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DesignProducts_DesignProductPriortyTypes_DesignProductPrior~",
                        column: x => x.DesignProductPriortyTypeId,
                        principalTable: "DesignProductPriortyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DesignProducts_Designs_DesignId",
                        column: x => x.DesignId,
                        principalTable: "Designs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DesignId = table.Column<Guid>(type: "uuid", nullable: false),
                    DesignTransactionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DesignTransactionDescriptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignTransactions_Designs_DesignId",
                        column: x => x.DesignId,
                        principalTable: "Designs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignTransactions_DesignTransactionDescriptions_DesignTran~",
                        column: x => x.DesignTransactionDescriptionId,
                        principalTable: "DesignTransactionDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignTransactions_DesignTransactionTypes_DesignTransaction~",
                        column: x => x.DesignTransactionTypeId,
                        principalTable: "DesignTransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventorRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentId = table.Column<Guid>(type: "uuid", nullable: false),
                    InventorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventorRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventorRelations_Inventors_InventorId",
                        column: x => x.InventorId,
                        principalTable: "Inventors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventorRelations_Patents_PatentId",
                        column: x => x.PatentId,
                        principalTable: "Patents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentClassRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentClassRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatentClassRelations_PatentClasses_PatentClassId",
                        column: x => x.PatentClassId,
                        principalTable: "PatentClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatentClassRelations_Patents_PatentId",
                        column: x => x.PatentId,
                        principalTable: "Patents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Queue = table.Column<int>(type: "integer", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatentPayments_Patents_PatentId",
                        column: x => x.PatentId,
                        principalTable: "Patents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentPdFs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentId = table.Column<Guid>(type: "uuid", nullable: false),
                    PdfType = table.Column<int>(type: "integer", nullable: false),
                    FileId = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentPdFs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatentPdFs_Patents_PatentId",
                        column: x => x.PatentId,
                        principalTable: "Patents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentPriorties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentId = table.Column<Guid>(type: "uuid", nullable: false),
                    PriortyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PriortyNumber = table.Column<string>(type: "text", nullable: true),
                    PatentPriortyCountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentPriorties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatentPriorties_PatentPriortyCountries_PatentPriortyCountry~",
                        column: x => x.PatentPriortyCountryId,
                        principalTable: "PatentPriortyCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatentPriorties_Patents_PatentId",
                        column: x => x.PatentId,
                        principalTable: "Patents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentPublications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentId = table.Column<Guid>(type: "uuid", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PatentPublicationDescriptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentPublications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatentPublications_PatentPublicationDescriptions_PatentPubl~",
                        column: x => x.PatentPublicationDescriptionId,
                        principalTable: "PatentPublicationDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatentPublications_Patents_PatentId",
                        column: x => x.PatentId,
                        principalTable: "Patents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NotificationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PatentTransactionNameId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatentTransactions_Patents_PatentId",
                        column: x => x.PatentId,
                        principalTable: "Patents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatentTransactions_PatentTransactionNames_PatentTransaction~",
                        column: x => x.PatentTransactionNameId,
                        principalTable: "PatentTransactionNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Class = table.Column<int>(type: "integer", nullable: true),
                    Service = table.Column<string>(type: "text", nullable: true),
                    TradeMarkId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeMarkServices_TradeMarks_TradeMarkId",
                        column: x => x.TradeMarkId,
                        principalTable: "TradeMarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TradeMarkTransactionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NotificationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TradeMarkTransactionNameId = table.Column<Guid>(type: "uuid", nullable: true),
                    TradeMarkTransactionDescriptionId = table.Column<Guid>(type: "uuid", nullable: true),
                    TradeMarkId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeMarkTransactions_TradeMarks_TradeMarkId",
                        column: x => x.TradeMarkId,
                        principalTable: "TradeMarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TradeMarkTransactions_TradeMarkTransactionDescriptions_Trad~",
                        column: x => x.TradeMarkTransactionDescriptionId,
                        principalTable: "TradeMarkTransactionDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeMarkTransactions_TradeMarkTransactionNames_TradeMarkTr~",
                        column: x => x.TradeMarkTransactionNameId,
                        principalTable: "TradeMarkTransactionNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeMarkTransactions_TradeMarkTransactionTypes_TradeMarkTr~",
                        column: x => x.TradeMarkTransactionTypeId,
                        principalTable: "TradeMarkTransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignProductClassesRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LocarnoClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    DesignProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignProductClassesRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignProductClassesRelations_DesignProducts_DesignProductId",
                        column: x => x.DesignProductId,
                        principalTable: "DesignProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignProductClassesRelations_LocarnoClasses_LocarnoClassId",
                        column: x => x.LocarnoClassId,
                        principalTable: "LocarnoClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DesignProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageId = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignProductImages_DesignProducts_DesignProductId",
                        column: x => x.DesignProductId,
                        principalTable: "DesignProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkTransactionDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DecisionReason = table.Column<string>(type: "text", nullable: true),
                    AboutMark = table.Column<string>(type: "text", nullable: true),
                    TradeMarkTransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkTransactionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeMarkTransactionDetails_TradeMarkTransactions_TradeMark~",
                        column: x => x.TradeMarkTransactionId,
                        principalTable: "TradeMarkTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attorneys_AttorneyCompanyId",
                table: "Attorneys",
                column: "AttorneyCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignerRelations_DesignerId",
                table: "DesignerRelations",
                column: "DesignerId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignerRelations_DesignId",
                table: "DesignerRelations",
                column: "DesignId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignProductClassesRelations_DesignProductId",
                table: "DesignProductClassesRelations",
                column: "DesignProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignProductClassesRelations_LocarnoClassId",
                table: "DesignProductClassesRelations",
                column: "LocarnoClassId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignProductImages_DesignProductId",
                table: "DesignProductImages",
                column: "DesignProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignProducts_DesignId",
                table: "DesignProducts",
                column: "DesignId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignProducts_DesignPriortyCountryId",
                table: "DesignProducts",
                column: "DesignPriortyCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignProducts_DesignProductPriortyTypeId",
                table: "DesignProducts",
                column: "DesignProductPriortyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Designs_AttorneyId",
                table: "Designs",
                column: "AttorneyId");

            migrationBuilder.CreateIndex(
                name: "IX_Designs_DesignStatusId",
                table: "Designs",
                column: "DesignStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignTransactionDescriptions_DesignTransactionDescriptionD~",
                table: "DesignTransactionDescriptions",
                column: "DesignTransactionDescriptionDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignTransactions_DesignId",
                table: "DesignTransactions",
                column: "DesignId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignTransactions_DesignTransactionDescriptionId",
                table: "DesignTransactions",
                column: "DesignTransactionDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignTransactions_DesignTransactionTypeId",
                table: "DesignTransactions",
                column: "DesignTransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignTransactionTypes_DesignTransactionTypeDetailId",
                table: "DesignTransactionTypes",
                column: "DesignTransactionTypeDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_HolderRelations_HolderId",
                table: "HolderRelations",
                column: "HolderId");

            migrationBuilder.CreateIndex(
                name: "IX_InventorRelations_InventorId",
                table: "InventorRelations",
                column: "InventorId");

            migrationBuilder.CreateIndex(
                name: "IX_InventorRelations_PatentId",
                table: "InventorRelations",
                column: "PatentId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentClasses_PatentClassTypeId",
                table: "PatentClasses",
                column: "PatentClassTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentClassRelations_PatentClassId",
                table: "PatentClassRelations",
                column: "PatentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentClassRelations_PatentId",
                table: "PatentClassRelations",
                column: "PatentId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentPayments_PatentId",
                table: "PatentPayments",
                column: "PatentId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentPdFs_PatentId",
                table: "PatentPdFs",
                column: "PatentId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentPriorties_PatentId",
                table: "PatentPriorties",
                column: "PatentId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentPriorties_PatentPriortyCountryId",
                table: "PatentPriorties",
                column: "PatentPriortyCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentPublications_PatentId",
                table: "PatentPublications",
                column: "PatentId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentPublications_PatentPublicationDescriptionId",
                table: "PatentPublications",
                column: "PatentPublicationDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Patents_AttorneyId",
                table: "Patents",
                column: "AttorneyId");

            migrationBuilder.CreateIndex(
                name: "IX_Patents_PatentApplicationTypeId",
                table: "Patents",
                column: "PatentApplicationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Patents_PatentProtectionTypeId",
                table: "Patents",
                column: "PatentProtectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentTransactions_PatentId",
                table: "PatentTransactions",
                column: "PatentId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentTransactions_PatentTransactionNameId",
                table: "PatentTransactions",
                column: "PatentTransactionNameId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarkDecisions_TradeMarkDecisionReasonId",
                table: "TradeMarkDecisions",
                column: "TradeMarkDecisionReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarkPriorties_TradeMarkPriortyCountryId",
                table: "TradeMarkPriorties",
                column: "TradeMarkPriortyCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarks_AttorneyId",
                table: "TradeMarks",
                column: "AttorneyId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarks_TradeMarkDecisionId",
                table: "TradeMarks",
                column: "TradeMarkDecisionId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarks_TradeMarkPriortyId",
                table: "TradeMarks",
                column: "TradeMarkPriortyId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarks_TradeMarkStatusId",
                table: "TradeMarks",
                column: "TradeMarkStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarks_TradeMarkTypeId",
                table: "TradeMarks",
                column: "TradeMarkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarkServices_TradeMarkId",
                table: "TradeMarkServices",
                column: "TradeMarkId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarkTransactionDetails_TradeMarkTransactionId",
                table: "TradeMarkTransactionDetails",
                column: "TradeMarkTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarkTransactions_TradeMarkId",
                table: "TradeMarkTransactions",
                column: "TradeMarkId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarkTransactions_TradeMarkTransactionDescriptionId",
                table: "TradeMarkTransactions",
                column: "TradeMarkTransactionDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarkTransactions_TradeMarkTransactionNameId",
                table: "TradeMarkTransactions",
                column: "TradeMarkTransactionNameId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarkTransactions_TradeMarkTransactionTypeId",
                table: "TradeMarkTransactions",
                column: "TradeMarkTransactionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesignerRelations");

            migrationBuilder.DropTable(
                name: "DesignProductClassesRelations");

            migrationBuilder.DropTable(
                name: "DesignProductImages");

            migrationBuilder.DropTable(
                name: "DesignTransactions");

            migrationBuilder.DropTable(
                name: "HolderRelations");

            migrationBuilder.DropTable(
                name: "InventorRelations");

            migrationBuilder.DropTable(
                name: "PatentClassRelations");

            migrationBuilder.DropTable(
                name: "PatentPayments");

            migrationBuilder.DropTable(
                name: "PatentPdFs");

            migrationBuilder.DropTable(
                name: "PatentPriorties");

            migrationBuilder.DropTable(
                name: "PatentPublications");

            migrationBuilder.DropTable(
                name: "PatentTransactions");

            migrationBuilder.DropTable(
                name: "TradeMarkServices");

            migrationBuilder.DropTable(
                name: "TradeMarkTransactionDetails");

            migrationBuilder.DropTable(
                name: "Designers");

            migrationBuilder.DropTable(
                name: "LocarnoClasses");

            migrationBuilder.DropTable(
                name: "DesignProducts");

            migrationBuilder.DropTable(
                name: "DesignTransactionDescriptions");

            migrationBuilder.DropTable(
                name: "DesignTransactionTypes");

            migrationBuilder.DropTable(
                name: "Holders");

            migrationBuilder.DropTable(
                name: "Inventors");

            migrationBuilder.DropTable(
                name: "PatentClasses");

            migrationBuilder.DropTable(
                name: "PatentPriortyCountries");

            migrationBuilder.DropTable(
                name: "PatentPublicationDescriptions");

            migrationBuilder.DropTable(
                name: "Patents");

            migrationBuilder.DropTable(
                name: "PatentTransactionNames");

            migrationBuilder.DropTable(
                name: "TradeMarkTransactions");

            migrationBuilder.DropTable(
                name: "DesignProductPriortyCountries");

            migrationBuilder.DropTable(
                name: "DesignProductPriortyTypes");

            migrationBuilder.DropTable(
                name: "Designs");

            migrationBuilder.DropTable(
                name: "DesignTransactionDescriptionDetails");

            migrationBuilder.DropTable(
                name: "DesignTransactionTypeDetails");

            migrationBuilder.DropTable(
                name: "PatentClassTypes");

            migrationBuilder.DropTable(
                name: "PatentApplicationTypes");

            migrationBuilder.DropTable(
                name: "PatentProtectionTypes");

            migrationBuilder.DropTable(
                name: "TradeMarks");

            migrationBuilder.DropTable(
                name: "TradeMarkTransactionDescriptions");

            migrationBuilder.DropTable(
                name: "TradeMarkTransactionNames");

            migrationBuilder.DropTable(
                name: "TradeMarkTransactionTypes");

            migrationBuilder.DropTable(
                name: "DesignStatuses");

            migrationBuilder.DropTable(
                name: "Attorneys");

            migrationBuilder.DropTable(
                name: "TradeMarkDecisions");

            migrationBuilder.DropTable(
                name: "TradeMarkPriorties");

            migrationBuilder.DropTable(
                name: "TradeMarkStatuses");

            migrationBuilder.DropTable(
                name: "TradeMarkTypes");

            migrationBuilder.DropTable(
                name: "AttorneyCompanies");

            migrationBuilder.DropTable(
                name: "TradeMarkDecisionReasons");

            migrationBuilder.DropTable(
                name: "TradeMarkPriortyCountries");
        }
    }
}
