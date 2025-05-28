using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketBus.Migrations
{
    /// <inheritdoc />
    public partial class updateTicket_Bill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    IdCity = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameCity = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.IdCity);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    IdPos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamePos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.IdPos);
                });

            migrationBuilder.CreateTable(
                name: "TypeNews",
                columns: table => new
                {
                    IdTypeNews = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameTypeNews = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeNews", x => x.IdTypeNews);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    IdType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeatCount = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.IdType);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    IdBrand = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    RegistFormId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.IdBrand);
                    table.ForeignKey(
                        name: "FK_Brands_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    IdPassenger = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassengerCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamePassenger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.IdPassenger);
                    table.ForeignKey(
                        name: "FK_Passengers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    IdDistrict = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.IdDistrict);
                    table.ForeignKey(
                        name: "FK_Districts_Cities_IdCity",
                        column: x => x.IdCity,
                        principalTable: "Cities",
                        principalColumn: "IdCity");
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    IdNews = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTypeNews = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.IdNews);
                    table.ForeignKey(
                        name: "FK_News_TypeNews_IdTypeNews",
                        column: x => x.IdTypeNews,
                        principalTable: "TypeNews",
                        principalColumn: "IdTypeNews");
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    IdCoach = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoachCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NumberPlate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    IdType = table.Column<int>(type: "int", nullable: false),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Documents = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdBrand = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.IdCoach);
                    table.ForeignKey(
                        name: "FK_Coaches_Brands_IdBrand",
                        column: x => x.IdBrand,
                        principalTable: "Brands",
                        principalColumn: "IdBrand",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Coaches_VehicleTypes_IdType",
                        column: x => x.IdType,
                        principalTable: "VehicleTypes",
                        principalColumn: "IdType");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    IdEmployee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEmployee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdBrand = table.Column<int>(type: "int", nullable: true),
                    IdPos = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.IdEmployee);
                    table.ForeignKey(
                        name: "FK_Employees_Brands_IdBrand",
                        column: x => x.IdBrand,
                        principalTable: "Brands",
                        principalColumn: "IdBrand");
                    table.ForeignKey(
                        name: "FK_Employees_Positions_IdPos",
                        column: x => x.IdPos,
                        principalTable: "Positions",
                        principalColumn: "IdPos");
                });

            migrationBuilder.CreateTable(
                name: "RegistForms",
                columns: table => new
                {
                    IdRegist = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdBrand = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RejectReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistForms", x => x.IdRegist);
                    table.ForeignKey(
                        name: "FK_RegistForms_Brands_IdBrand",
                        column: x => x.IdBrand,
                        principalTable: "Brands",
                        principalColumn: "IdBrand");
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    IdBill = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeatQuantity = table.Column<int>(type: "int", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdPassenger = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.IdBill);
                    table.ForeignKey(
                        name: "FK_Bills_Passengers_IdPassenger",
                        column: x => x.IdPassenger,
                        principalTable: "Passengers",
                        principalColumn: "IdPassenger");
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    IdFeedback = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedbackCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdPassenger = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.IdFeedback);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Passengers_IdPassenger",
                        column: x => x.IdPassenger,
                        principalTable: "Passengers",
                        principalColumn: "IdPassenger");
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    IdSeat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    IdCoach = table.Column<int>(type: "int", nullable: false),
                    CoachIdCoach = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.IdSeat);
                    table.ForeignKey(
                        name: "FK_Seats_Coaches_CoachIdCoach",
                        column: x => x.CoachIdCoach,
                        principalTable: "Coaches",
                        principalColumn: "IdCoach");
                    table.ForeignKey(
                        name: "FK_Seats_Coaches_IdCoach",
                        column: x => x.IdCoach,
                        principalTable: "Coaches",
                        principalColumn: "IdCoach");
                });

            migrationBuilder.CreateTable(
                name: "BusRoutes",
                columns: table => new
                {
                    IdRoute = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameRoute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distance = table.Column<int>(type: "int", nullable: true),
                    IdBrand = table.Column<int>(type: "int", nullable: false),
                    IdRegist = table.Column<int>(type: "int", nullable: true),
                    IdStartCity = table.Column<int>(type: "int", nullable: true),
                    IdEndCity = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    TravelTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusRoutes", x => x.IdRoute);
                    table.ForeignKey(
                        name: "FK_BusRoutes_Brands_IdBrand",
                        column: x => x.IdBrand,
                        principalTable: "Brands",
                        principalColumn: "IdBrand");
                    table.ForeignKey(
                        name: "FK_BusRoutes_Cities_IdEndCity",
                        column: x => x.IdEndCity,
                        principalTable: "Cities",
                        principalColumn: "IdCity");
                    table.ForeignKey(
                        name: "FK_BusRoutes_Cities_IdStartCity",
                        column: x => x.IdStartCity,
                        principalTable: "Cities",
                        principalColumn: "IdCity");
                    table.ForeignKey(
                        name: "FK_BusRoutes_RegistForms_IdRegist",
                        column: x => x.IdRegist,
                        principalTable: "RegistForms",
                        principalColumn: "IdRegist");
                });

            migrationBuilder.CreateTable(
                name: "DropOffs",
                columns: table => new
                {
                    IdDropOff = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DropOffName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCity = table.Column<int>(type: "int", nullable: true),
                    IdBrand = table.Column<int>(type: "int", nullable: false),
                    IdRoute = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DropOffs", x => x.IdDropOff);
                    table.ForeignKey(
                        name: "FK_DropOffs_Brands_IdBrand",
                        column: x => x.IdBrand,
                        principalTable: "Brands",
                        principalColumn: "IdBrand",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DropOffs_BusRoutes_IdRoute",
                        column: x => x.IdRoute,
                        principalTable: "BusRoutes",
                        principalColumn: "IdRoute");
                    table.ForeignKey(
                        name: "FK_DropOffs_Cities_IdCity",
                        column: x => x.IdCity,
                        principalTable: "Cities",
                        principalColumn: "IdCity");
                });

            migrationBuilder.CreateTable(
                name: "Pickups",
                columns: table => new
                {
                    IdPickup = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCity = table.Column<int>(type: "int", nullable: true),
                    IdBrand = table.Column<int>(type: "int", nullable: false),
                    IdRoute = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pickups", x => x.IdPickup);
                    table.ForeignKey(
                        name: "FK_Pickups_Brands_IdBrand",
                        column: x => x.IdBrand,
                        principalTable: "Brands",
                        principalColumn: "IdBrand",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pickups_BusRoutes_IdRoute",
                        column: x => x.IdRoute,
                        principalTable: "BusRoutes",
                        principalColumn: "IdRoute");
                    table.ForeignKey(
                        name: "FK_Pickups_Cities_IdCity",
                        column: x => x.IdCity,
                        principalTable: "Cities",
                        principalColumn: "IdCity");
                });

            migrationBuilder.CreateTable(
                name: "RouteStops",
                columns: table => new
                {
                    IdStop = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StopCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdRoute = table.Column<int>(type: "int", nullable: false),
                    StopName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCity = table.Column<int>(type: "int", nullable: false),
                    StopOrder = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteStops", x => x.IdStop);
                    table.ForeignKey(
                        name: "FK_RouteStops_BusRoutes_IdRoute",
                        column: x => x.IdRoute,
                        principalTable: "BusRoutes",
                        principalColumn: "IdRoute");
                    table.ForeignKey(
                        name: "FK_RouteStops_Cities_IdCity",
                        column: x => x.IdCity,
                        principalTable: "Cities",
                        principalColumn: "IdCity",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleDetails",
                columns: table => new
                {
                    IdSchedule = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCoach = table.Column<int>(type: "int", nullable: false),
                    IdRoute = table.Column<int>(type: "int", nullable: false),
                    DepartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ArriveTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleDetails", x => x.IdSchedule);
                    table.ForeignKey(
                        name: "FK_ScheduleDetails_BusRoutes_IdRoute",
                        column: x => x.IdRoute,
                        principalTable: "BusRoutes",
                        principalColumn: "IdRoute");
                    table.ForeignKey(
                        name: "FK_ScheduleDetails_Coaches_IdCoach",
                        column: x => x.IdCoach,
                        principalTable: "Coaches",
                        principalColumn: "IdCoach");
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    IdPrice = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSchedule = table.Column<int>(type: "int", nullable: false),
                    PriceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdRoute = table.Column<int>(type: "int", nullable: true),
                    IdStopStart = table.Column<int>(type: "int", nullable: false),
                    IdStopEnd = table.Column<int>(type: "int", nullable: false),
                    IdCoach = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.IdPrice);
                    table.ForeignKey(
                        name: "FK_Prices_BusRoutes_IdRoute",
                        column: x => x.IdRoute,
                        principalTable: "BusRoutes",
                        principalColumn: "IdRoute");
                    table.ForeignKey(
                        name: "FK_Prices_Coaches_IdCoach",
                        column: x => x.IdCoach,
                        principalTable: "Coaches",
                        principalColumn: "IdCoach");
                    table.ForeignKey(
                        name: "FK_Prices_RouteStops_IdStopEnd",
                        column: x => x.IdStopEnd,
                        principalTable: "RouteStops",
                        principalColumn: "IdStop");
                    table.ForeignKey(
                        name: "FK_Prices_RouteStops_IdStopStart",
                        column: x => x.IdStopStart,
                        principalTable: "RouteStops",
                        principalColumn: "IdStop");
                    table.ForeignKey(
                        name: "FK_Prices_ScheduleDetails_IdSchedule",
                        column: x => x.IdSchedule,
                        principalTable: "ScheduleDetails",
                        principalColumn: "IdSchedule");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    IdTicket = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdSeat = table.Column<int>(type: "int", nullable: true),
                    IdPrice = table.Column<int>(type: "int", nullable: true),
                    IdBill = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    IdEmployee = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.IdTicket);
                    table.ForeignKey(
                        name: "FK_Tickets_Bills_IdBill",
                        column: x => x.IdBill,
                        principalTable: "Bills",
                        principalColumn: "IdBill");
                    table.ForeignKey(
                        name: "FK_Tickets_Employees_IdEmployee",
                        column: x => x.IdEmployee,
                        principalTable: "Employees",
                        principalColumn: "IdEmployee");
                    table.ForeignKey(
                        name: "FK_Tickets_Prices_IdPrice",
                        column: x => x.IdPrice,
                        principalTable: "Prices",
                        principalColumn: "IdPrice");
                    table.ForeignKey(
                        name: "FK_Tickets_Seats_IdSeat",
                        column: x => x.IdSeat,
                        principalTable: "Seats",
                        principalColumn: "IdSeat");
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "IdCity", "CityCode", "NameCity" },
                values: new object[,]
                {
                    { 1, "TP01", "Hà Nội" },
                    { 2, "TP02", "Hồ Chí Minh" },
                    { 3, "TP03", "Hải Phòng" },
                    { 4, "TP04", "Đà Nẵng" },
                    { 5, "TP05", "Cần Thơ" },
                    { 6, "TP06", "An Giang" },
                    { 7, "TP07", "Bà Rịa - Vũng Tàu" },
                    { 8, "TP08", "Bắc Giang" },
                    { 9, "TP09", "Bắc Kạn" },
                    { 10, "TP10", "Bạc Liêu" },
                    { 11, "TP11", "Bắc Ninh" },
                    { 12, "TP12", "Bến Tre" },
                    { 13, "TP13", "Bình Định" },
                    { 14, "TP14", "Bình Dương" },
                    { 15, "TP15", "Bình Phước" },
                    { 16, "TP16", "Bình Thuận" },
                    { 17, "TP17", "Cà Mau" },
                    { 18, "TP18", "Cao Bằng" },
                    { 19, "TP19", "Đắk Lắk" },
                    { 20, "TP20", "Đắk Nông" },
                    { 21, "TP21", "Điện Biên" },
                    { 22, "TP22", "Đồng Nai" },
                    { 23, "TP23", "Đồng Tháp" },
                    { 24, "TP24", "Gia Lai" },
                    { 25, "TP25", "Hà Giang" },
                    { 26, "TP26", "Hà Nam" },
                    { 27, "TP27", "Hà Tĩnh" },
                    { 28, "TP28", "Hải Dương" },
                    { 29, "TP29", "Hậu Giang" },
                    { 30, "TP30", "Hòa Bình" },
                    { 31, "TP31", "Hưng Yên" },
                    { 32, "TP32", "Khánh Hòa" },
                    { 33, "TP33", "Kiên Giang" },
                    { 34, "TP34", "Kon Tum" },
                    { 35, "TP35", "Lai Châu" },
                    { 36, "TP36", "Lâm Đồng" },
                    { 37, "TP37", "Lạng Sơn" },
                    { 38, "TP38", "Lào Cai" },
                    { 39, "TP39", "Long An" },
                    { 40, "TP40", "Nam Định" },
                    { 41, "TP41", "Nghệ An" },
                    { 42, "TP42", "Ninh Bình" },
                    { 43, "TP43", "Ninh Thuận" },
                    { 44, "TP44", "Phú Thọ" },
                    { 45, "TP45", "Phú Yên" },
                    { 46, "TP46", "Quảng Bình" },
                    { 47, "TP47", "Quảng Nam" },
                    { 48, "TP48", "Quảng Ngãi" },
                    { 49, "TP49", "Quảng Ninh" },
                    { 50, "TP50", "Quảng Trị" },
                    { 51, "TP51", "Sóc Trăng" },
                    { 52, "TP52", "Sơn La" },
                    { 53, "TP53", "Tây Ninh" },
                    { 54, "TP54", "Thái Bình" },
                    { 55, "TP55", "Thái Nguyên" },
                    { 56, "TP56", "Thanh Hóa" },
                    { 57, "TP57", "Thừa Thiên Huế" },
                    { 58, "TP58", "Tiền Giang" },
                    { 59, "TP59", "Trà Vinh" },
                    { 60, "TP60", "Tuyên Quang" },
                    { 61, "TP61", "Vĩnh Long" },
                    { 62, "TP62", "Vĩnh Phúc" },
                    { 63, "TP63", "Yên Bái" }
                });

            migrationBuilder.InsertData(
                table: "VehicleTypes",
                columns: new[] { "IdType", "NameType", "SeatCount", "State", "TypeCode" },
                values: new object[,]
                {
                    { 1, "Giường nằm CLC 34 chỗ", 34, 0, "VT001" },
                    { 2, "Giường nằm CLC 40 chỗ", 40, 0, "VT002" },
                    { 3, "Giường nằm CLC VIP 20 chỗ", 20, 0, "VT003" },
                    { 4, "Giường nằm massage 34 chỗ", 34, 0, "VT004" },
                    { 5, "Giường nằm massage 40 chỗ", 40, 0, "VT005" },
                    { 6, "Giường nằm đôi VIP 22 chỗ", 22, 0, "VT006" },
                    { 7, "Ghé Nằm CLC 34 chỗ", 34, 0, "VT007" },
                    { 8, "Ghé Nằm CLC 40 chỗ", 40, 0, "VT008" },
                    { 9, "Ghé Nằm VIP 20 chỗ", 20, 0, "VT009" },
                    { 10, "Ghé Nằm massage 34 chỗ", 34, 0, "VT010" },
                    { 11, "Ghế ngồi CLC 45 chỗ", 45, 0, "VT011" },
                    { 12, "Ghế ngồi CLC 50 chỗ", 50, 0, "VT012" },
                    { 13, "Ghế ngồi VIP 32 chỗ", 32, 0, "VT013" },
                    { 14, "Ghế ngồi Limousine 28 chỗ", 28, 0, "VT014" },
                    { 15, "Limousine DCar VIP 9 chỗ", 9, 0, "VT015" },
                    { 16, "Limousine President 11 chỗ", 11, 0, "VT016" },
                    { 17, "Limousine Fuso Rosa 17 chỗ", 17, 0, "VT017" },
                    { 18, "Limousine Skybus 19 chỗ", 19, 0, "VT018" },
                    { 19, "Limousine Jet VIP 22 chỗ", 22, 0, "VT019" },
                    { 20, "Limousine Auto Kingdom 26 chỗ", 26, 0, "VT020" },
                    { 21, "Xe khách giường nằm 34 chỗ", 34, 0, "VT021" },
                    { 22, "Xe khách giường nằm 40 chỗ", 40, 0, "VT022" },
                    { 23, "Xe khách giường đôi 20 chỗ", 20, 0, "VT023" },
                    { 24, "Xe khách giường đôi 34 chỗ", 34, 0, "VT024" },
                    { 25, "Xe ghế ngồi 12 chỗ Transit", 12, 0, "VT025" },
                    { 26, "Xe ghế ngồi 29 chỗ County", 29, 0, "VT026" },
                    { 27, "Xe ghế ngồi 34 chỗ Thaco Garden", 34, 1, "VT027" },
                    { 28, "Xe ghế ngồi 50 chỗ Giáp Bát Express", 50, 1, "VT028" },
                    { 29, "Xe giường nằm massage 38 chỗ", 38, 0, "VT029" },
                    { 30, "Xe giường nằm CLC 32 chỗ", 32, 0, "VT030" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_IdPassenger",
                table: "Bills",
                column: "IdPassenger");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_UserId",
                table: "Brands",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusRoutes_IdBrand",
                table: "BusRoutes",
                column: "IdBrand");

            migrationBuilder.CreateIndex(
                name: "IX_BusRoutes_IdEndCity",
                table: "BusRoutes",
                column: "IdEndCity");

            migrationBuilder.CreateIndex(
                name: "IX_BusRoutes_IdRegist",
                table: "BusRoutes",
                column: "IdRegist");

            migrationBuilder.CreateIndex(
                name: "IX_BusRoutes_IdStartCity",
                table: "BusRoutes",
                column: "IdStartCity");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_IdBrand",
                table: "Coaches",
                column: "IdBrand");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_IdType",
                table: "Coaches",
                column: "IdType");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_IdCity",
                table: "Districts",
                column: "IdCity");

            migrationBuilder.CreateIndex(
                name: "IX_DropOffs_IdBrand",
                table: "DropOffs",
                column: "IdBrand");

            migrationBuilder.CreateIndex(
                name: "IX_DropOffs_IdCity",
                table: "DropOffs",
                column: "IdCity");

            migrationBuilder.CreateIndex(
                name: "IX_DropOffs_IdRoute",
                table: "DropOffs",
                column: "IdRoute");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdBrand",
                table: "Employees",
                column: "IdBrand");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdPos",
                table: "Employees",
                column: "IdPos");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_IdPassenger",
                table: "Feedbacks",
                column: "IdPassenger");

            migrationBuilder.CreateIndex(
                name: "IX_News_IdTypeNews",
                table: "News",
                column: "IdTypeNews");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_UserId",
                table: "Passengers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_IdBrand",
                table: "Pickups",
                column: "IdBrand");

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_IdCity",
                table: "Pickups",
                column: "IdCity");

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_IdRoute",
                table: "Pickups",
                column: "IdRoute");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_IdCoach",
                table: "Prices",
                column: "IdCoach");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_IdRoute",
                table: "Prices",
                column: "IdRoute");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_IdSchedule",
                table: "Prices",
                column: "IdSchedule");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_IdStopEnd",
                table: "Prices",
                column: "IdStopEnd");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_IdStopStart",
                table: "Prices",
                column: "IdStopStart");

            migrationBuilder.CreateIndex(
                name: "IX_RegistForms_IdBrand",
                table: "RegistForms",
                column: "IdBrand",
                unique: true,
                filter: "[IdBrand] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RouteStops_IdCity",
                table: "RouteStops",
                column: "IdCity");

            migrationBuilder.CreateIndex(
                name: "IX_RouteStops_IdRoute",
                table: "RouteStops",
                column: "IdRoute");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_IdCoach",
                table: "ScheduleDetails",
                column: "IdCoach");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_IdRoute",
                table: "ScheduleDetails",
                column: "IdRoute");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_CoachIdCoach",
                table: "Seats",
                column: "CoachIdCoach");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_IdCoach",
                table: "Seats",
                column: "IdCoach");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_IdBill",
                table: "Tickets",
                column: "IdBill");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_IdEmployee",
                table: "Tickets",
                column: "IdEmployee");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_IdPrice",
                table: "Tickets",
                column: "IdPrice");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_IdSeat",
                table: "Tickets",
                column: "IdSeat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "DropOffs");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Pickups");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "TypeNews");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "RouteStops");

            migrationBuilder.DropTable(
                name: "ScheduleDetails");

            migrationBuilder.DropTable(
                name: "BusRoutes");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "RegistForms");

            migrationBuilder.DropTable(
                name: "VehicleTypes");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
