using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class JC_System : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionControl",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ViewControlID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionControl", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttachmentPath = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CountryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Floor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    BuildingID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LogData",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LogTableID = table.Column<int>(nullable: false),
                    ColumnName = table.Column<string>(nullable: true),
                    OldData = table.Column<string>(nullable: true),
                    NewData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LogTable",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    TableName = table.Column<string>(nullable: true),
                    Operation = table.Column<string>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogTable", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Lookup",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    TypeID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookup", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MedicalHistory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    PatientTypeID = table.Column<int>(nullable: true),
                    LastVisit = table.Column<DateTime>(nullable: true),
                    AdmissionTypeID = table.Column<int>(nullable: true),
                    PayTypeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PatientAdmission",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: true),
                    DepartmentID = table.Column<int>(nullable: true),
                    AppointmentID = table.Column<int>(nullable: true),
                    PurposeID = table.Column<int>(nullable: true),
                    ElectiveID = table.Column<int>(nullable: true),
                    AdmissionID = table.Column<int>(nullable: true),
                    AdmissionTypeID = table.Column<int>(nullable: true),
                    BuildingID = table.Column<int>(nullable: true),
                    FloorID = table.Column<int>(nullable: true),
                    RoomID = table.Column<int>(nullable: true),
                    BedID = table.Column<int>(nullable: true),
                    TypeID = table.Column<int>(nullable: true),
                    ReservedBy = table.Column<int>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAdmission", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoomNumber = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FloorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ViewControl",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Uri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewControl", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TitleID = table.Column<int>(nullable: true),
                    ArFirstName = table.Column<string>(nullable: true),
                    ArSecondName = table.Column<string>(nullable: true),
                    ArThirdName = table.Column<string>(nullable: true),
                    ArFamilyName = table.Column<string>(nullable: true),
                    EnFirstName = table.Column<string>(nullable: true),
                    EnSecondName = table.Column<string>(nullable: true),
                    EnThirdName = table.Column<string>(nullable: true),
                    EnFamilyName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    ExpiryDate = table.Column<DateTime>(nullable: true),
                    RoleID = table.Column<int>(nullable: true),
                    StatusID = table.Column<int>(nullable: true),
                    SecurityGroupID = table.Column<int>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    SignaturePath = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    Telephone2 = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Mobile2 = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    ComputerName = table.Column<string>(nullable: true),
                    IPAddress = table.Column<string>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedBy = table.Column<int>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Lookup_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Lookup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bed",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    BedNumber = table.Column<int>(nullable: true),
                    RoomID = table.Column<int>(nullable: true),
                    FloorID = table.Column<int>(nullable: true),
                    BuildingID = table.Column<int>(nullable: true),
                    DepartmentID = table.Column<int>(nullable: true),
                    Isolated = table.Column<bool>(nullable: false),
                    StatusID = table.Column<int>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bed", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bed_Building_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Building",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bed_Lookup_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Lookup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bed_Floor_FloorID",
                        column: x => x.FloorID,
                        principalTable: "Floor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bed_Room_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Room",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bed_Lookup_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Lookup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessPermission",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ViewControlID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessPermission", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccessPermission_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessPermission_ViewControl_ViewControlID",
                        column: x => x.ViewControlID,
                        principalTable: "ViewControl",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionPermission",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ActionID = table.Column<int>(nullable: false),
                    ViewControlID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    ActionControlID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionPermission", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActionPermission_ActionControl_ActionControlID",
                        column: x => x.ActionControlID,
                        principalTable: "ActionControl",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActionPermission_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionPermission_ViewControl_ViewControlID",
                        column: x => x.ViewControlID,
                        principalTable: "ViewControl",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DoctorID = table.Column<int>(nullable: true),
                    UserID = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    Time = table.Column<DateTime>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    DepartmentID = table.Column<int>(nullable: true),
                    SpecialityID = table.Column<int>(nullable: true),
                    RoomID = table.Column<int>(nullable: true),
                    PurposeID = table.Column<int>(nullable: true),
                    ByDoctor = table.Column<bool>(nullable: true),
                    SelfAppointment = table.Column<bool>(nullable: true),
                    AppointmentStatusID = table.Column<int>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Appointment_Lookup_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Lookup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointment_User_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointment_Room_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Room",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointment_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientPersonalInformation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RegistrationNo = table.Column<int>(nullable: true),
                    UserID = table.Column<int>(nullable: true),
                    CountryID = table.Column<int>(nullable: true),
                    CityID = table.Column<int>(nullable: true),
                    AdmissionID = table.Column<int>(nullable: true),
                    IdentificationID = table.Column<int>(nullable: true),
                    IdentificationTypeID = table.Column<string>(nullable: true),
                    PlaceOfBirth = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    GenderID = table.Column<int>(nullable: true),
                    BloodGroupID = table.Column<int>(nullable: true),
                    ReservedID = table.Column<int>(nullable: true),
                    ReligionID = table.Column<int>(nullable: true),
                    SocialStatusID = table.Column<int>(nullable: true),
                    HaveInsurance = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPersonalInformation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PatientPersonalInformation_Lookup_BloodGroupID",
                        column: x => x.BloodGroupID,
                        principalTable: "Lookup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientPersonalInformation_City_CityID",
                        column: x => x.CityID,
                        principalTable: "City",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientPersonalInformation_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientPersonalInformation_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientRelative",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RelativeName = table.Column<string>(nullable: true),
                    SameAddress = table.Column<bool>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    RelationID = table.Column<int>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    StatusID = table.Column<int>(nullable: true),
                    UserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientRelative", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PatientRelative_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonalInformation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: true),
                    DepartmentID = table.Column<int>(nullable: true),
                    SubDepartmentID = table.Column<int>(nullable: true),
                    PositionID = table.Column<int>(nullable: true),
                    SpecialityID = table.Column<int>(nullable: true),
                    BranchID = table.Column<int>(nullable: true),
                    ResponsibleOfficerID = table.Column<int>(nullable: true),
                    EmploymentTypeID = table.Column<int>(nullable: true),
                    EmploymentClassID = table.Column<int>(nullable: true),
                    CountryID = table.Column<int>(nullable: true),
                    CityID = table.Column<int>(nullable: true),
                    IdentificationID = table.Column<int>(nullable: true),
                    IdentificationTypeID = table.Column<string>(nullable: true),
                    PlaceOfBirth = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    GenderID = table.Column<int>(nullable: true),
                    ReligionID = table.Column<int>(nullable: true),
                    ReservedID = table.Column<int>(nullable: true),
                    PurposeID = table.Column<int>(nullable: true),
                    SocialStatusID = table.Column<int>(nullable: true),
                    EducationLevel = table.Column<string>(nullable: true),
                    DateOfHiring = table.Column<DateTime>(nullable: true),
                    DateOfStartJob = table.Column<DateTime>(nullable: true),
                    ProbationPeriod = table.Column<int>(nullable: false),
                    ProbationTime = table.Column<DateTime>(nullable: true),
                    DateOfEndContract = table.Column<DateTime>(nullable: true),
                    DateOfEndJob = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInformation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PersonalInformation_Branch_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branch",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalInformation_City_CityID",
                        column: x => x.CityID,
                        principalTable: "City",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalInformation_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalInformation_Lookup_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Lookup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalInformation_Lookup_EmploymentClassID",
                        column: x => x.EmploymentClassID,
                        principalTable: "Lookup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalInformation_Lookup_EmploymentTypeID",
                        column: x => x.EmploymentTypeID,
                        principalTable: "Lookup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalInformation_Lookup_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Lookup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalInformation_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BedAllocation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    UserID = table.Column<int>(nullable: true),
                    BedID = table.Column<int>(nullable: true),
                    StatusID = table.Column<int>(nullable: true),
                    Duration = table.Column<int>(nullable: true),
                    AllocatedDate = table.Column<DateTime>(nullable: false),
                    DepartureDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BedAllocation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BedAllocation_Bed_BedID",
                        column: x => x.BedID,
                        principalTable: "Bed",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessPermission_RoleID",
                table: "AccessPermission",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_AccessPermission_ViewControlID",
                table: "AccessPermission",
                column: "ViewControlID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionPermission_ActionControlID",
                table: "ActionPermission",
                column: "ActionControlID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionPermission_RoleID",
                table: "ActionPermission",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionPermission_ViewControlID",
                table: "ActionPermission",
                column: "ViewControlID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_DepartmentID",
                table: "Appointment",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_DoctorID",
                table: "Appointment",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_RoomID",
                table: "Appointment",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_UserID",
                table: "Appointment",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Bed_BuildingID",
                table: "Bed",
                column: "BuildingID");

            migrationBuilder.CreateIndex(
                name: "IX_Bed_DepartmentID",
                table: "Bed",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Bed_FloorID",
                table: "Bed",
                column: "FloorID");

            migrationBuilder.CreateIndex(
                name: "IX_Bed_RoomID",
                table: "Bed",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Bed_StatusID",
                table: "Bed",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_BedAllocation_BedID",
                table: "BedAllocation",
                column: "BedID",
                unique: true,
                filter: "[BedID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPersonalInformation_BloodGroupID",
                table: "PatientPersonalInformation",
                column: "BloodGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPersonalInformation_CityID",
                table: "PatientPersonalInformation",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPersonalInformation_CountryID",
                table: "PatientPersonalInformation",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPersonalInformation_UserID",
                table: "PatientPersonalInformation",
                column: "UserID",
                unique: true,
                filter: "[UserID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRelative_UserID",
                table: "PatientRelative",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInformation_BranchID",
                table: "PersonalInformation",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInformation_CityID",
                table: "PersonalInformation",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInformation_CountryID",
                table: "PersonalInformation",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInformation_DepartmentID",
                table: "PersonalInformation",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInformation_EmploymentClassID",
                table: "PersonalInformation",
                column: "EmploymentClassID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInformation_EmploymentTypeID",
                table: "PersonalInformation",
                column: "EmploymentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInformation_PositionID",
                table: "PersonalInformation",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInformation_UserID",
                table: "PersonalInformation",
                column: "UserID",
                unique: true,
                filter: "[UserID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleID",
                table: "User",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_User_StatusID",
                table: "User",
                column: "StatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessPermission");

            migrationBuilder.DropTable(
                name: "ActionPermission");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "BedAllocation");

            migrationBuilder.DropTable(
                name: "LogData");

            migrationBuilder.DropTable(
                name: "LogTable");

            migrationBuilder.DropTable(
                name: "MedicalHistory");

            migrationBuilder.DropTable(
                name: "PatientAdmission");

            migrationBuilder.DropTable(
                name: "PatientPersonalInformation");

            migrationBuilder.DropTable(
                name: "PatientRelative");

            migrationBuilder.DropTable(
                name: "PersonalInformation");

            migrationBuilder.DropTable(
                name: "ActionControl");

            migrationBuilder.DropTable(
                name: "ViewControl");

            migrationBuilder.DropTable(
                name: "Bed");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Floor");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Lookup");
        }
    }
}
