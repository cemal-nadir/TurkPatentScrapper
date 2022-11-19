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
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttorneyCompanies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Designers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DesignProductPriortyCountries",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignProductPriortyCountries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DesignProductPriortyTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignProductPriortyTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DesignStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DesignTransactionDescriptionDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Detail = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignTransactionDescriptionDetails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DesignTransactionTypeDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Detail = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignTransactionTypeDetails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Holders",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    HolderCode = table.Column<string>(type: "text", nullable: true),
                    HolderName = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Inventors",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    InventorCode = table.Column<string>(type: "text", nullable: true),
                    InventorName = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LocarnoClasses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocarnoClasses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PatentApplicationTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentApplicationTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PatentClassTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentClassTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PatentPriortyCountries",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentPriortyCountries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PatentProtectionTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentProtectionTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PatentPublicationDescriptions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentPublicationDescriptions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PatentTransactionNames",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Transaction = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentTransactionNames", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkDecisionReasons",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkDecisionReasons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkPriortyCountries",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkPriortyCountries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkTransactionDescriptions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkTransactionDescriptions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkTransactionNames",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Transaction = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkTransactionNames", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkTransactionTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkTransactionTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Attorneys",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    AttorneyCompanyID = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attorneys", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Attorneys_AttorneyCompanies_AttorneyCompanyID",
                        column: x => x.AttorneyCompanyID,
                        principalTable: "AttorneyCompanies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DesignTransactionDescriptions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DesignTransactionDescriptionDetailID = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignTransactionDescriptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DesignTransactionDescriptions_DesignTransactionDescriptionD~",
                        column: x => x.DesignTransactionDescriptionDetailID,
                        principalTable: "DesignTransactionDescriptionDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DesignTransactionTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    DesignTransactionTypeDetailID = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignTransactionTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DesignTransactionTypes_DesignTransactionTypeDetails_DesignT~",
                        column: x => x.DesignTransactionTypeDetailID,
                        principalTable: "DesignTransactionTypeDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HolderRelations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    HolderID = table.Column<Guid>(type: "uuid", nullable: false),
                    DataID = table.Column<Guid>(type: "uuid", nullable: false),
                    DataType = table.Column<int>(type: "integer", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolderRelations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HolderRelations_Holders_HolderID",
                        column: x => x.HolderID,
                        principalTable: "Holders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentClasses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentClassTypeID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentClasses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PatentClasses_PatentClassTypes_PatentClassTypeID",
                        column: x => x.PatentClassTypeID,
                        principalTable: "PatentClassTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkDecisions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Decision = table.Column<string>(type: "text", nullable: true),
                    TradeMarkDecisionReasonID = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkDecisions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TradeMarkDecisions_TradeMarkDecisionReasons_TradeMarkDecisi~",
                        column: x => x.TradeMarkDecisionReasonID,
                        principalTable: "TradeMarkDecisionReasons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkPriorties",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TradeMarkPriortyCountryID = table.Column<Guid>(type: "uuid", nullable: true),
                    ApplicationNumber = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkPriorties", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TradeMarkPriorties_TradeMarkPriortyCountries_TradeMarkPrior~",
                        column: x => x.TradeMarkPriortyCountryID,
                        principalTable: "TradeMarkPriortyCountries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Designs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationNumber = table.Column<string>(type: "text", nullable: true),
                    ApplicationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "text", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BulletinNumber = table.Column<string>(type: "text", nullable: true),
                    BulletinDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DesignStatusID = table.Column<Guid>(type: "uuid", nullable: true),
                    AttorneyID = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Designs_Attorneys_AttorneyID",
                        column: x => x.AttorneyID,
                        principalTable: "Attorneys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Designs_DesignStatuses_DesignStatusID",
                        column: x => x.DesignStatusID,
                        principalTable: "DesignStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patents",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationNumber = table.Column<string>(type: "text", nullable: true),
                    ApplicationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PatentApplicationTypeID = table.Column<Guid>(type: "uuid", nullable: true),
                    DocumentNumber = table.Column<string>(type: "text", nullable: true),
                    DocumentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "text", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PatentProtectionTypeID = table.Column<Guid>(type: "uuid", nullable: true),
                    EPCPublishNumber = table.Column<string>(type: "text", nullable: true),
                    EPCApplicationNumber = table.Column<string>(type: "text", nullable: true),
                    PCTPublishNumber = table.Column<string>(type: "text", nullable: true),
                    PCTApplicationNumber = table.Column<string>(type: "text", nullable: true),
                    PCTPublishDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    InventionTitle = table.Column<string>(type: "text", nullable: true),
                    InventionSummary = table.Column<string>(type: "text", nullable: true),
                    AttorneyID = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patents", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Patents_Attorneys_AttorneyID",
                        column: x => x.AttorneyID,
                        principalTable: "Attorneys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patents_PatentApplicationTypes_PatentApplicationTypeID",
                        column: x => x.PatentApplicationTypeID,
                        principalTable: "PatentApplicationTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patents_PatentProtectionTypes_PatentProtectionTypeID",
                        column: x => x.PatentProtectionTypeID,
                        principalTable: "PatentProtectionTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarks",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
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
                    TradeMarkStatusID = table.Column<Guid>(type: "uuid", nullable: true),
                    Classes = table.Column<int[]>(type: "integer[]", nullable: true),
                    TradeMarkTypeID = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    AttorneyID = table.Column<Guid>(type: "uuid", nullable: true),
                    TradeMarkDecisionID = table.Column<Guid>(type: "uuid", nullable: true),
                    TradeMarkPriortyID = table.Column<Guid>(type: "uuid", nullable: true),
                    ImageID = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TradeMarks_Attorneys_AttorneyID",
                        column: x => x.AttorneyID,
                        principalTable: "Attorneys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeMarks_TradeMarkDecisions_TradeMarkDecisionID",
                        column: x => x.TradeMarkDecisionID,
                        principalTable: "TradeMarkDecisions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeMarks_TradeMarkPriorties_TradeMarkPriortyID",
                        column: x => x.TradeMarkPriortyID,
                        principalTable: "TradeMarkPriorties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeMarks_TradeMarkStatuses_TradeMarkStatusID",
                        column: x => x.TradeMarkStatusID,
                        principalTable: "TradeMarkStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeMarks_TradeMarkTypes_TradeMarkTypeID",
                        column: x => x.TradeMarkTypeID,
                        principalTable: "TradeMarkTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DesignerRelations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    DesignID = table.Column<Guid>(type: "uuid", nullable: false),
                    DesignerID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignerRelations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DesignerRelations_Designers_DesignerID",
                        column: x => x.DesignerID,
                        principalTable: "Designers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignerRelations_Designs_DesignID",
                        column: x => x.DesignID,
                        principalTable: "Designs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignProducts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    DesignID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PriortyApplicationNumber = table.Column<string>(type: "text", nullable: true),
                    DesignPriortyCountryID = table.Column<Guid>(type: "uuid", nullable: true),
                    ExhibitionName = table.Column<string>(type: "text", nullable: true),
                    ExhibitionPlace = table.Column<string>(type: "text", nullable: true),
                    ExhibitionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FirstExhibitionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PriortyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DesignProductPriortyTypeID = table.Column<Guid>(type: "uuid", nullable: true),
                    IsProductApproved = table.Column<bool>(type: "boolean", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DesignProducts_DesignProductPriortyCountries_DesignPriortyC~",
                        column: x => x.DesignPriortyCountryID,
                        principalTable: "DesignProductPriortyCountries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DesignProducts_DesignProductPriortyTypes_DesignProductPrior~",
                        column: x => x.DesignProductPriortyTypeID,
                        principalTable: "DesignProductPriortyTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DesignProducts_Designs_DesignID",
                        column: x => x.DesignID,
                        principalTable: "Designs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignTransactions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    DesignID = table.Column<Guid>(type: "uuid", nullable: false),
                    DesignTransactionTypeID = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DesignTransactionDescriptionID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DesignTransactions_Designs_DesignID",
                        column: x => x.DesignID,
                        principalTable: "Designs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignTransactions_DesignTransactionDescriptions_DesignTran~",
                        column: x => x.DesignTransactionDescriptionID,
                        principalTable: "DesignTransactionDescriptions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignTransactions_DesignTransactionTypes_DesignTransaction~",
                        column: x => x.DesignTransactionTypeID,
                        principalTable: "DesignTransactionTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventorRelations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentID = table.Column<Guid>(type: "uuid", nullable: false),
                    InventorID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventorRelations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventorRelations_Inventors_InventorID",
                        column: x => x.InventorID,
                        principalTable: "Inventors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventorRelations_Patents_PatentID",
                        column: x => x.PatentID,
                        principalTable: "Patents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentClassRelations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentID = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentClassID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentClassRelations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PatentClassRelations_PatentClasses_PatentClassID",
                        column: x => x.PatentClassID,
                        principalTable: "PatentClasses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatentClassRelations_Patents_PatentID",
                        column: x => x.PatentID,
                        principalTable: "Patents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentPayments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentID = table.Column<Guid>(type: "uuid", nullable: false),
                    Queue = table.Column<int>(type: "integer", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaidAmount = table.Column<double>(type: "double precision", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentPayments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PatentPayments_Patents_PatentID",
                        column: x => x.PatentID,
                        principalTable: "Patents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentPDFs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentID = table.Column<Guid>(type: "uuid", nullable: false),
                    PDFType = table.Column<int>(type: "integer", nullable: false),
                    FileID = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentPDFs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PatentPDFs_Patents_PatentID",
                        column: x => x.PatentID,
                        principalTable: "Patents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentPriorties",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentID = table.Column<Guid>(type: "uuid", nullable: false),
                    PriortyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PriortyNumber = table.Column<string>(type: "text", nullable: true),
                    PatentPriortyCountryID = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentPriorties", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PatentPriorties_PatentPriortyCountries_PatentPriortyCountry~",
                        column: x => x.PatentPriortyCountryID,
                        principalTable: "PatentPriortyCountries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatentPriorties_Patents_PatentID",
                        column: x => x.PatentID,
                        principalTable: "Patents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentPublications",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentID = table.Column<Guid>(type: "uuid", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PatentPublicationDescriptionID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentPublications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PatentPublications_PatentPublicationDescriptions_PatentPubl~",
                        column: x => x.PatentPublicationDescriptionID,
                        principalTable: "PatentPublicationDescriptions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatentPublications_Patents_PatentID",
                        column: x => x.PatentID,
                        principalTable: "Patents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentTransactions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PatentID = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NotificationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PatentTransactionNameID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PatentTransactions_Patents_PatentID",
                        column: x => x.PatentID,
                        principalTable: "Patents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatentTransactions_PatentTransactionNames_PatentTransaction~",
                        column: x => x.PatentTransactionNameID,
                        principalTable: "PatentTransactionNames",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkServices",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Class = table.Column<int>(type: "integer", nullable: true),
                    Service = table.Column<string>(type: "text", nullable: true),
                    TradeMarkID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkServices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TradeMarkServices_TradeMarks_TradeMarkID",
                        column: x => x.TradeMarkID,
                        principalTable: "TradeMarks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkTransactions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    TradeMarkTransactionTypeID = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NotificationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TradeMarkTransactionNameID = table.Column<Guid>(type: "uuid", nullable: true),
                    TradeMarkTransactionDescriptionID = table.Column<Guid>(type: "uuid", nullable: true),
                    TradeMarkID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TradeMarkTransactions_TradeMarks_TradeMarkID",
                        column: x => x.TradeMarkID,
                        principalTable: "TradeMarks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TradeMarkTransactions_TradeMarkTransactionDescriptions_Trad~",
                        column: x => x.TradeMarkTransactionDescriptionID,
                        principalTable: "TradeMarkTransactionDescriptions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeMarkTransactions_TradeMarkTransactionNames_TradeMarkTr~",
                        column: x => x.TradeMarkTransactionNameID,
                        principalTable: "TradeMarkTransactionNames",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeMarkTransactions_TradeMarkTransactionTypes_TradeMarkTr~",
                        column: x => x.TradeMarkTransactionTypeID,
                        principalTable: "TradeMarkTransactionTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignProductClassesRelations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    LocarnoClassID = table.Column<Guid>(type: "uuid", nullable: false),
                    DesignProductID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignProductClassesRelations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DesignProductClassesRelations_DesignProducts_DesignProductID",
                        column: x => x.DesignProductID,
                        principalTable: "DesignProducts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignProductClassesRelations_LocarnoClasses_LocarnoClassID",
                        column: x => x.LocarnoClassID,
                        principalTable: "LocarnoClasses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignProductImages",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    DesignProductID = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageID = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignProductImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DesignProductImages_DesignProducts_DesignProductID",
                        column: x => x.DesignProductID,
                        principalTable: "DesignProducts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradeMarkTransactionDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    DecisionReason = table.Column<string>(type: "text", nullable: true),
                    AboutMark = table.Column<string>(type: "text", nullable: true),
                    TradeMarkTransactionID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarkTransactionDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TradeMarkTransactionDetails_TradeMarkTransactions_TradeMark~",
                        column: x => x.TradeMarkTransactionID,
                        principalTable: "TradeMarkTransactions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attorneys_AttorneyCompanyID",
                table: "Attorneys",
                column: "AttorneyCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignerRelations_DesignerID",
                table: "DesignerRelations",
                column: "DesignerID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignerRelations_DesignID",
                table: "DesignerRelations",
                column: "DesignID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignProductClassesRelations_DesignProductID",
                table: "DesignProductClassesRelations",
                column: "DesignProductID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignProductClassesRelations_LocarnoClassID",
                table: "DesignProductClassesRelations",
                column: "LocarnoClassID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignProductImages_DesignProductID",
                table: "DesignProductImages",
                column: "DesignProductID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignProducts_DesignID",
                table: "DesignProducts",
                column: "DesignID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignProducts_DesignPriortyCountryID",
                table: "DesignProducts",
                column: "DesignPriortyCountryID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignProducts_DesignProductPriortyTypeID",
                table: "DesignProducts",
                column: "DesignProductPriortyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Designs_AttorneyID",
                table: "Designs",
                column: "AttorneyID");

            migrationBuilder.CreateIndex(
                name: "IX_Designs_DesignStatusID",
                table: "Designs",
                column: "DesignStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignTransactionDescriptions_DesignTransactionDescriptionD~",
                table: "DesignTransactionDescriptions",
                column: "DesignTransactionDescriptionDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignTransactions_DesignID",
                table: "DesignTransactions",
                column: "DesignID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignTransactions_DesignTransactionDescriptionID",
                table: "DesignTransactions",
                column: "DesignTransactionDescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignTransactions_DesignTransactionTypeID",
                table: "DesignTransactions",
                column: "DesignTransactionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignTransactionTypes_DesignTransactionTypeDetailID",
                table: "DesignTransactionTypes",
                column: "DesignTransactionTypeDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_HolderRelations_HolderID",
                table: "HolderRelations",
                column: "HolderID");

            migrationBuilder.CreateIndex(
                name: "IX_InventorRelations_InventorID",
                table: "InventorRelations",
                column: "InventorID");

            migrationBuilder.CreateIndex(
                name: "IX_InventorRelations_PatentID",
                table: "InventorRelations",
                column: "PatentID");

            migrationBuilder.CreateIndex(
                name: "IX_PatentClasses_PatentClassTypeID",
                table: "PatentClasses",
                column: "PatentClassTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PatentClassRelations_PatentClassID",
                table: "PatentClassRelations",
                column: "PatentClassID");

            migrationBuilder.CreateIndex(
                name: "IX_PatentClassRelations_PatentID",
                table: "PatentClassRelations",
                column: "PatentID");

            migrationBuilder.CreateIndex(
                name: "IX_PatentPayments_PatentID",
                table: "PatentPayments",
                column: "PatentID");

            migrationBuilder.CreateIndex(
                name: "IX_PatentPDFs_PatentID",
                table: "PatentPDFs",
                column: "PatentID");

            migrationBuilder.CreateIndex(
                name: "IX_PatentPriorties_PatentID",
                table: "PatentPriorties",
                column: "PatentID");

            migrationBuilder.CreateIndex(
                name: "IX_PatentPriorties_PatentPriortyCountryID",
                table: "PatentPriorties",
                column: "PatentPriortyCountryID");

            migrationBuilder.CreateIndex(
                name: "IX_PatentPublications_PatentID",
                table: "PatentPublications",
                column: "PatentID");

            migrationBuilder.CreateIndex(
                name: "IX_PatentPublications_PatentPublicationDescriptionID",
                table: "PatentPublications",
                column: "PatentPublicationDescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Patents_AttorneyID",
                table: "Patents",
                column: "AttorneyID");

            migrationBuilder.CreateIndex(
                name: "IX_Patents_PatentApplicationTypeID",
                table: "Patents",
                column: "PatentApplicationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Patents_PatentProtectionTypeID",
                table: "Patents",
                column: "PatentProtectionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PatentTransactions_PatentID",
                table: "PatentTransactions",
                column: "PatentID");

            migrationBuilder.CreateIndex(
                name: "IX_PatentTransactions_PatentTransactionNameID",
                table: "PatentTransactions",
                column: "PatentTransactionNameID");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarkDecisions_TradeMarkDecisionReasonID",
                table: "TradeMarkDecisions",
                column: "TradeMarkDecisionReasonID");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarkPriorties_TradeMarkPriortyCountryID",
                table: "TradeMarkPriorties",
                column: "TradeMarkPriortyCountryID");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarks_AttorneyID",
                table: "TradeMarks",
                column: "AttorneyID");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarks_TradeMarkDecisionID",
                table: "TradeMarks",
                column: "TradeMarkDecisionID");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarks_TradeMarkPriortyID",
                table: "TradeMarks",
                column: "TradeMarkPriortyID");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarks_TradeMarkStatusID",
                table: "TradeMarks",
                column: "TradeMarkStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarks_TradeMarkTypeID",
                table: "TradeMarks",
                column: "TradeMarkTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarkServices_TradeMarkID",
                table: "TradeMarkServices",
                column: "TradeMarkID");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarkTransactionDetails_TradeMarkTransactionID",
                table: "TradeMarkTransactionDetails",
                column: "TradeMarkTransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarkTransactions_TradeMarkID",
                table: "TradeMarkTransactions",
                column: "TradeMarkID");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarkTransactions_TradeMarkTransactionDescriptionID",
                table: "TradeMarkTransactions",
                column: "TradeMarkTransactionDescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarkTransactions_TradeMarkTransactionNameID",
                table: "TradeMarkTransactions",
                column: "TradeMarkTransactionNameID");

            migrationBuilder.CreateIndex(
                name: "IX_TradeMarkTransactions_TradeMarkTransactionTypeID",
                table: "TradeMarkTransactions",
                column: "TradeMarkTransactionTypeID");
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
                name: "PatentPDFs");

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
