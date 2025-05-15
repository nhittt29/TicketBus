using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketBus.Migrations
{
    /// <inheritdoc />
    public partial class CreatePJ : Migration
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
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
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
                name: "ChatRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRooms", x => x.Id);
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
                name: "Passengers",
                columns: table => new
                {
                    IdPassenger = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassengerCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamePassenger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCard = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.IdPassenger);
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                name: "ApplicationUserChatRoom",
                columns: table => new
                {
                    ChatRoomsId = table.Column<int>(type: "int", nullable: false),
                    ParticipantsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserChatRoom", x => new { x.ChatRoomsId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserChatRoom_AspNetUsers_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserChatRoom_ChatRooms_ChatRoomsId",
                        column: x => x.ChatRoomsId,
                        principalTable: "ChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatRoomId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatMessages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatMessages_ChatRooms_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalTable: "ChatRooms",
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
                        principalColumn: "IdBrand",
                        onDelete: ReferentialAction.Cascade);
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
                    IdSeat = table.Column<int>(type: "int", nullable: true),
                    IdPrice = table.Column<int>(type: "int", nullable: true),
                    IdPassenger = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    IdEmployee = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.IdTicket);
                    table.ForeignKey(
                        name: "FK_Tickets_Employees_IdEmployee",
                        column: x => x.IdEmployee,
                        principalTable: "Employees",
                        principalColumn: "IdEmployee");
                    table.ForeignKey(
                        name: "FK_Tickets_Passengers_IdPassenger",
                        column: x => x.IdPassenger,
                        principalTable: "Passengers",
                        principalColumn: "IdPassenger");
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

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "IdDistrict", "DistrictCode", "IdCity", "NameDistrict" },
                values: new object[,]
                {
                    { 1, "DIST-HN-01", 1, "Ba Đình" },
                    { 2, "DIST-HN-02", 1, "Hoàn Kiếm" },
                    { 3, "DIST-HN-03", 1, "Hai Bà Trưng" },
                    { 4, "DIST-HN-04", 1, "Đống Đa" },
                    { 5, "DIST-HN-05", 1, "Tây Hồ" },
                    { 6, "DIST-HN-06", 1, "Cầu Giấy" },
                    { 7, "DIST-HN-07", 1, "Thanh Xuân" },
                    { 8, "DIST-HN-08", 1, "Hoàng Mai" },
                    { 9, "DIST-HN-09", 1, "Long Biên" },
                    { 10, "DIST-HN-10", 1, "Bắc Từ Liêm" },
                    { 11, "DIST-HN-11", 1, "Nam Từ Liêm" },
                    { 12, "DIST-HN-12", 1, "Hà Đông" },
                    { 13, "DIST-HN-13", 1, "Thanh Trì" },
                    { 14, "DIST-HN-14", 1, "Gia Lâm" },
                    { 15, "DIST-HN-15", 1, "Đông Anh" },
                    { 16, "DIST-HN-16", 1, "Sóc Sơn" },
                    { 17, "DIST-HN-17", 1, "Hoài Đức" },
                    { 18, "DIST-HN-18", 1, "Đan Phượng" },
                    { 19, "DIST-HN-19", 1, "Phúc Thọ" },
                    { 20, "DIST-HN-20", 1, "Thạch Thất" },
                    { 21, "DIST-HN-21", 1, "Quốc Oai" },
                    { 22, "DIST-HN-22", 1, "Chương Mỹ" },
                    { 23, "DIST-HN-23", 1, "Thanh Oai" },
                    { 24, "DIST-HN-24", 1, "Mỹ Đức" },
                    { 25, "DIST-HN-25", 1, "Ứng Hòa" },
                    { 26, "DIST-HN-26", 1, "Phú Xuyên" },
                    { 27, "DIST-HN-27", 1, "Thường Tín" },
                    { 28, "DIST-HN-28", 1, "Mê Linh" },
                    { 29, "DIST-HCM-01", 2, "Quận 1" },
                    { 30, "DIST-HCM-02", 2, "Quận 2" },
                    { 31, "DIST-HCM-03", 2, "Quận 3" },
                    { 32, "DIST-HCM-04", 2, "Quận 4" },
                    { 33, "DIST-HCM-05", 2, "Quận 5" },
                    { 34, "DIST-HCM-06", 2, "Quận 6" },
                    { 35, "DIST-HCM-07", 2, "Quận 7" },
                    { 36, "DIST-HCM-08", 2, "Quận 8" },
                    { 37, "DIST-HCM-09", 2, "Quận 9" },
                    { 38, "DIST-HCM-10", 2, "Quận 10" },
                    { 39, "DIST-HCM-11", 2, "Quận 11" },
                    { 40, "DIST-HCM-12", 2, "Quận 12" },
                    { 41, "DIST-HCM-13", 2, "Bình Tân" },
                    { 42, "DIST-HCM-14", 2, "Tân Bình" },
                    { 43, "DIST-HCM-15", 2, "Tân Phú" },
                    { 44, "DIST-HCM-16", 2, "Gò Vấp" },
                    { 45, "DIST-HCM-17", 2, "Phú Nhuận" },
                    { 46, "DIST-HCM-18", 2, "Bình Thạnh" },
                    { 47, "DIST-HCM-19", 2, "Củ Chi" },
                    { 48, "DIST-HCM-20", 2, "Hóc Môn" },
                    { 49, "DIST-HCM-21", 2, "Nhà Bè" },
                    { 50, "DIST-HCM-22", 2, "Cần Giờ" },
                    { 51, "DIST-HCM-23", 2, "Bình Chánh" },
                    { 52, "DIST-HP-01", 3, "Hồng Bàng" },
                    { 53, "DIST-HP-02", 3, "Lê Chân" },
                    { 54, "DIST-HP-03", 3, "Ngô Quyền" },
                    { 55, "DIST-HP-04", 3, "Hải An" },
                    { 56, "DIST-HP-05", 3, "Kiến An" },
                    { 57, "DIST-HP-06", 3, "Dương Kinh" },
                    { 58, "DIST-HP-07", 3, "Đồ Sơn" },
                    { 59, "DIST-HP-08", 3, "Thủy Nguyên" },
                    { 60, "DIST-HP-09", 3, "An Dương" },
                    { 61, "DIST-HP-10", 3, "An Lão" },
                    { 62, "DIST-HP-11", 3, "Kiến Thụy" },
                    { 63, "DIST-HP-12", 3, "Tiên Lãng" },
                    { 64, "DIST-HP-13", 3, "Vĩnh Bảo" },
                    { 65, "DIST-HP-14", 3, "Cát Hải" },
                    { 66, "DIST-HP-15", 3, "Bạch Long Vĩ" },
                    { 67, "DIST-DN-01", 4, "Hải Châu" },
                    { 68, "DIST-DN-02", 4, "Thanh Khê" },
                    { 69, "DIST-DN-03", 4, "Sơn Trà" },
                    { 70, "DIST-DN-04", 4, "Ngũ Hành Sơn" },
                    { 71, "DIST-DN-05", 4, "Liên Chiểu" },
                    { 72, "DIST-DN-06", 4, "Cẩm Lệ" },
                    { 73, "DIST-DN-07", 4, "Hòa Vang" },
                    { 74, "DIST-DN-08", 4, "Hoàng Sa" },
                    { 75, "DIST-CT-01", 5, "Ninh Kiều" },
                    { 76, "DIST-CT-02", 5, "Bình Thủy" },
                    { 77, "DIST-CT-03", 5, "Cái Răng" },
                    { 78, "DIST-CT-04", 5, "Ô Môn" },
                    { 79, "DIST-CT-05", 5, "Thốt Nốt" },
                    { 80, "DIST-CT-06", 5, "Phong Điền" },
                    { 81, "DIST-CT-07", 5, "Cờ Đỏ" },
                    { 82, "DIST-CT-08", 5, "Vĩnh Thạnh" },
                    { 83, "DIST-AG-01", 6, "TP. Long Xuyên" },
                    { 84, "DIST-AG-02", 6, "TP. Châu Đốc" },
                    { 85, "DIST-AG-03", 6, "TX. Tân Châu" },
                    { 86, "DIST-AG-04", 6, "Huyện An Phú" },
                    { 87, "DIST-AG-05", 6, "Huyện Châu Phú" },
                    { 88, "DIST-AG-06", 6, "Huyện Châu Thành" },
                    { 89, "DIST-AG-07", 6, "Huyện Chợ Mới" },
                    { 90, "DIST-AG-08", 6, "Huyện Phú Tân" },
                    { 91, "DIST-AG-09", 6, "Huyện Thoại Sơn" },
                    { 92, "DIST-AG-10", 6, "Huyện Tri Tôn" },
                    { 93, "DIST-AG-11", 6, "Huyện Tịnh Biên" },
                    { 94, "DIST-BRVT-01", 7, "TP. Vũng Tàu" },
                    { 95, "DIST-BRVT-02", 7, "TP. Bà Rịa" },
                    { 96, "DIST-BRVT-03", 7, "TX. Phú Mỹ" },
                    { 97, "DIST-BRVT-04", 7, "Huyện Châu Đức" },
                    { 98, "DIST-BRVT-05", 7, "Huyện Côn Đảo" },
                    { 99, "DIST-BRVT-06", 7, "Huyện Đất Đỏ" },
                    { 100, "DIST-BRVT-07", 7, "Huyện Long Điền" },
                    { 101, "DIST-BRVT-08", 7, "Huyện Xuyên Mộc" },
                    { 102, "DIST-BG-01", 8, "TP. Bắc Giang" },
                    { 103, "DIST-BG-02", 8, "Huyện Hiệp Hòa" },
                    { 104, "DIST-BG-03", 8, "Huyện Lạng Giang" },
                    { 105, "DIST-BG-04", 8, "Huyện Lục Nam" },
                    { 106, "DIST-BG-05", 8, "Huyện Lục Ngạn" },
                    { 107, "DIST-BG-06", 8, "Huyện Sơn Động" },
                    { 108, "DIST-BG-07", 8, "Huyện Tân Yên" },
                    { 109, "DIST-BG-08", 8, "Huyện Việt Yên" },
                    { 110, "DIST-BG-09", 8, "Huyện Yên Dũng" },
                    { 111, "DIST-BG-10", 8, "Huyện Yên Thế" },
                    { 112, "DIST-BK-01", 9, "TP. Bắc Kạn" },
                    { 113, "DIST-BK-02", 9, "Huyện Ba Bể" },
                    { 114, "DIST-BK-03", 9, "Huyện Bạch Thông" },
                    { 115, "DIST-BK-04", 9, "Huyện Chợ Đồn" },
                    { 116, "DIST-BK-05", 9, "Huyện Chợ Mới" },
                    { 117, "DIST-BK-06", 9, "Huyện Na Rì" },
                    { 118, "DIST-BK-07", 9, "Huyện Ngân Sơn" },
                    { 119, "DIST-BK-08", 9, "Huyện Pác Nặm" },
                    { 120, "DIST-BL-01", 10, "TP. Bạc Liêu" },
                    { 121, "DIST-BL-02", 10, "TX. Giá Rai" },
                    { 122, "DIST-BL-03", 10, "Huyện Đông Hải" },
                    { 123, "DIST-BL-04", 10, "Huyện Hoà Bình" },
                    { 124, "DIST-BL-05", 10, "Huyện Hồng Dân" },
                    { 125, "DIST-BL-06", 10, "Huyện Phước Long" },
                    { 126, "DIST-BL-07", 10, "Huyện Vĩnh Lợi" },
                    { 127, "DIST-BN-01", 11, "TP. Bắc Ninh" },
                    { 128, "DIST-BN-02", 11, "TX. Từ Sơn" },
                    { 129, "DIST-BN-03", 11, "Huyện Gia Bình" },
                    { 130, "DIST-BN-04", 11, "Huyện Lương Tài" },
                    { 131, "DIST-BN-05", 11, "Huyện Quế Võ" },
                    { 132, "DIST-BN-06", 11, "Huyện Thuận Thành" },
                    { 133, "DIST-BN-07", 11, "Huyện Tiên Du" },
                    { 134, "DIST-BN-08", 11, "Huyện Yên Phong" },
                    { 135, "DIST-BT-01", 12, "TP. Bến Tre" },
                    { 136, "DIST-BT-02", 12, "Huyện Ba Tri" },
                    { 137, "DIST-BT-03", 12, "Huyện Bình Đại" },
                    { 138, "DIST-BT-04", 12, "Huyện Châu Thành" },
                    { 139, "DIST-BT-05", 12, "Huyện Chợ Lách" },
                    { 140, "DIST-BT-06", 12, "Huyện Giồng Trôm" },
                    { 141, "DIST-BT-07", 12, "Huyện Mỏ Cày Bắc" },
                    { 142, "DIST-BT-08", 12, "Huyện Mỏ Cày Nam" },
                    { 143, "DIST-BT-09", 12, "Huyện Thạnh Phú" },
                    { 144, "DIST-BD-01", 13, "TP. Quy Nhơn" },
                    { 145, "DIST-BD-02", 13, "TX. An Nhơn" },
                    { 146, "DIST-BD-03", 13, "TX. Hoài Nhơn" },
                    { 147, "DIST-BD-04", 13, "Huyện An Lão" },
                    { 148, "DIST-BD-05", 13, "Huyện Hoài Ân" },
                    { 149, "DIST-BD-06", 13, "Huyện Phù Cát" },
                    { 150, "DIST-BD-07", 13, "Huyện Phù Mỹ" },
                    { 151, "DIST-BD-08", 13, "Huyện Tây Sơn" },
                    { 152, "DIST-BD-09", 13, "Huyện Tuy Phước" },
                    { 153, "DIST-BD-10", 13, "Huyện Vân Canh" },
                    { 154, "DIST-BD-11", 13, "Huyện Vĩnh Thạnh" },
                    { 155, "DIST-BD-01", 14, "TP. Thủ Dầu Một" },
                    { 156, "DIST-BD-02", 14, "TP. Dĩ An" },
                    { 157, "DIST-BD-03", 14, "TP. Thuận An" },
                    { 158, "DIST-BD-04", 14, "TX. Bến Cát" },
                    { 159, "DIST-BD-05", 14, "TX. Tân Uyên" },
                    { 160, "DIST-BD-06", 14, "Huyện Bàu Bàng" },
                    { 161, "DIST-BD-07", 14, "Huyện Bắc Tân Uyên" },
                    { 162, "DIST-BD-08", 14, "Huyện Dầu Tiếng" },
                    { 163, "DIST-BD-09", 14, "Huyện Phú Giáo" },
                    { 164, "DIST-BP-01", 15, "TP. Đồng Xoài" },
                    { 165, "DIST-BP-02", 15, "TX. Bình Long" },
                    { 166, "DIST-BP-03", 15, "TX. Phước Long" },
                    { 167, "DIST-BP-04", 15, "Huyện Bù Đăng" },
                    { 168, "DIST-BP-05", 15, "Huyện Bù Đốp" },
                    { 169, "DIST-BP-06", 15, "Huyện Bù Gia Mập" },
                    { 170, "DIST-BP-07", 15, "Huyện Chơn Thành" },
                    { 171, "DIST-BP-08", 15, "Huyện Đồng Phú" },
                    { 172, "DIST-BP-09", 15, "Huyện Hớn Quản" },
                    { 173, "DIST-BP-10", 15, "Huyện Lộc Ninh" },
                    { 174, "DIST-BT-01", 16, "TP. Phan Thiết" },
                    { 175, "DIST-BT-02", 16, "TX. La Gi" },
                    { 176, "DIST-BT-03", 16, "Huyện Bắc Bình" },
                    { 177, "DIST-BT-04", 16, "Huyện Đức Linh" },
                    { 178, "DIST-BT-05", 16, "Huyện Hàm Tân" },
                    { 179, "DIST-BT-06", 16, "Huyện Hàm Thuận Bắc" },
                    { 180, "DIST-BT-07", 16, "Huyện Hàm Thuận Nam" },
                    { 181, "DIST-BT-08", 16, "Huyện Phú Quý" },
                    { 182, "DIST-BT-09", 16, "Huyện Tánh Linh" },
                    { 183, "DIST-BT-10", 16, "Huyện Tuy Phong" },
                    { 184, "DIST-CM-01", 17, "TP. Cà Mau" },
                    { 185, "DIST-CM-02", 17, "Huyện Cái Nước" },
                    { 186, "DIST-CM-03", 17, "Huyện Đầm Dơi" },
                    { 187, "DIST-CM-04", 17, "Huyện Năm Căn" },
                    { 188, "DIST-CM-05", 17, "Huyện Ngọc Hiển" },
                    { 189, "DIST-CM-06", 17, "Huyện Phú Tân" },
                    { 190, "DIST-CM-07", 17, "Huyện Thới Bình" },
                    { 191, "DIST-CM-08", 17, "Huyện Trần Văn Thời" },
                    { 192, "DIST-CM-09", 17, "Huyện U Minh" },
                    { 193, "DIST-CB-01", 18, "TP. Cao Bằng" },
                    { 194, "DIST-CB-02", 18, "Huyện Bảo Lạc" },
                    { 195, "DIST-CB-03", 18, "Huyện Bảo Lâm" },
                    { 196, "DIST-CB-04", 18, "Huyện Hạ Lang" },
                    { 197, "DIST-CB-05", 18, "Huyện Hà Quảng" },
                    { 198, "DIST-CB-06", 18, "Huyện Hòa An" },
                    { 199, "DIST-CB-07", 18, "Huyện Nguyên Bình" },
                    { 200, "DIST-CB-08", 18, "Huyện Quảng Hòa" },
                    { 201, "DIST-CB-09", 18, "Huyện Thạch An" },
                    { 202, "DIST-CB-10", 18, "Huyện Trùng Khánh" },
                    { 203, "DIST-DL-01", 19, "TP. Buôn Ma Thuột" },
                    { 204, "DIST-DL-02", 19, "TX. Buôn Hồ" },
                    { 205, "DIST-DL-03", 19, "Huyện Buôn Đôn" },
                    { 206, "DIST-DL-04", 19, "Huyện Cư Kuin" },
                    { 207, "DIST-DL-05", 19, "Huyện Cư M’gar" },
                    { 208, "DIST-DL-06", 19, "Huyện Ea H’leo" },
                    { 209, "DIST-DL-07", 19, "Huyện Ea Kar" },
                    { 210, "DIST-DL-08", 19, "Huyện Ea Súp" },
                    { 211, "DIST-DL-09", 19, "Huyện Krông Ana" },
                    { 212, "DIST-DL-10", 19, "Huyện Krông Bông" },
                    { 213, "DIST-DL-11", 19, "Huyện Krông Búk" },
                    { 214, "DIST-DL-12", 19, "Huyện Krông Năng" },
                    { 215, "DIST-DL-13", 19, "Huyện Krông Pắc" },
                    { 216, "DIST-DL-14", 19, "Huyện Lắk" },
                    { 217, "DIST-DL-15", 19, "Huyện M’Đrắk" },
                    { 218, "DIST-DN-01", 20, "TP. Gia Nghĩa" },
                    { 219, "DIST-DN-02", 20, "Huyện Cư Jút" },
                    { 220, "DIST-DN-03", 20, "Huyện Đắk Glong" },
                    { 221, "DIST-DN-04", 20, "Huyện Đắk Mil" },
                    { 222, "DIST-DN-05", 20, "Huyện Đắk R’lấp" },
                    { 223, "DIST-DN-06", 20, "Huyện Đắk Song" },
                    { 224, "DIST-DN-07", 20, "Huyện Krông Nô" },
                    { 225, "DIST-DN-08", 20, "Huyện Tuy Đức" },
                    { 226, "DIST-DB-01", 21, "TP. Điện Biên Phủ" },
                    { 227, "DIST-DB-02", 21, "TX. Mường Lay" },
                    { 228, "DIST-DB-03", 21, "Huyện Điện Biên" },
                    { 229, "DIST-DB-04", 21, "Huyện Điện Biên Đông" },
                    { 230, "DIST-DB-05", 21, "Huyện Mường Ảng" },
                    { 231, "DIST-DB-06", 21, "Huyện Mường Chà" },
                    { 232, "DIST-DB-07", 21, "Huyện Mường Nhé" },
                    { 233, "DIST-DB-08", 21, "Huyện Nậm Pồ" },
                    { 234, "DIST-DB-09", 21, "Huyện Tủa Chùa" },
                    { 235, "DIST-DB-10", 21, "Huyện Tuần Giáo" },
                    { 236, "DIST-DN-01", 22, "TP. Biên Hòa" },
                    { 237, "DIST-DN-02", 22, "TP. Long Khánh" },
                    { 238, "DIST-DN-03", 22, "Huyện Cẩm Mỹ" },
                    { 239, "DIST-DN-04", 22, "Huyện Định Quán" },
                    { 240, "DIST-DN-05", 22, "Huyện Long Thành" },
                    { 241, "DIST-DN-06", 22, "Huyện Nhơn Trạch" },
                    { 242, "DIST-DN-07", 22, "Huyện Tân Phú" },
                    { 243, "DIST-DN-08", 22, "Huyện Thống Nhất" },
                    { 244, "DIST-DN-09", 22, "Huyện Trảng Bom" },
                    { 245, "DIST-DN-10", 22, "Huyện Vĩnh Cửu" },
                    { 246, "DIST-DN-11", 22, "Huyện Xuân Lộc" },
                    { 247, "DIST-DT-01", 23, "TP. Cao Lãnh" },
                    { 248, "DIST-DT-02", 23, "TP. Sa Đéc" },
                    { 249, "DIST-DT-03", 23, "TX. Hồng Ngự" },
                    { 250, "DIST-DT-04", 23, "Huyện Cao Lãnh" },
                    { 251, "DIST-DT-05", 23, "Huyện Châu Thành" },
                    { 252, "DIST-DT-06", 23, "Huyện Hồng Ngự" },
                    { 253, "DIST-DT-07", 23, "Huyện Lai Vung" },
                    { 254, "DIST-DT-08", 23, "Huyện Lấp Vò" },
                    { 255, "DIST-DT-09", 23, "Huyện Tam Nông" },
                    { 256, "DIST-DT-10", 23, "Huyện Tân Hồng" },
                    { 257, "DIST-DT-11", 23, "Huyện Thanh Bình" },
                    { 258, "DIST-DT-12", 23, "Huyện Tháp Mười" },
                    { 259, "DIST-GL-01", 24, "TP. Pleiku" },
                    { 260, "DIST-GL-02", 24, "TX. An Khê" },
                    { 261, "DIST-GL-03", 24, "TX. Ayun Pa" },
                    { 262, "DIST-GL-04", 24, "Huyện Chư Păh" },
                    { 263, "DIST-GL-05", 24, "Huyện Chư Prông" },
                    { 264, "DIST-GL-06", 24, "Huyện Chư Sê" },
                    { 265, "DIST-GL-07", 24, "Huyện Chư Pưh" },
                    { 266, "DIST-GL-08", 24, "Huyện Đăk Đoa" },
                    { 267, "DIST-GL-09", 24, "Huyện Đăk Pơ" },
                    { 268, "DIST-GL-10", 24, "Huyện Đức Cơ" },
                    { 269, "DIST-GL-11", 24, "Huyện Ia Grai" },
                    { 270, "DIST-GL-12", 24, "Huyện Ia Pa" },
                    { 271, "DIST-GL-13", 24, "Huyện KBang" },
                    { 272, "DIST-GL-14", 24, "Huyện Kông Chro" },
                    { 273, "DIST-GL-15", 24, "Huyện Krông Pa" },
                    { 274, "DIST-GL-16", 24, "Huyện Mang Yang" },
                    { 275, "DIST-GL-17", 24, "Huyện Phú Thiện" },
                    { 276, "DIST-HG-01", 25, "TP. Hà Giang" },
                    { 277, "DIST-HG-02", 25, "Huyện Bắc Mê" },
                    { 278, "DIST-HG-03", 25, "Huyện Bắc Quang" },
                    { 279, "DIST-HG-04", 25, "Huyện Đồng Văn" },
                    { 280, "DIST-HG-05", 25, "Huyện Hoàng Su Phì" },
                    { 281, "DIST-HG-06", 25, "Huyện Mèo Vạc" },
                    { 282, "DIST-HG-07", 25, "Huyện Quản Bạ" },
                    { 283, "DIST-HG-08", 25, "Huyện Quang Bình" },
                    { 284, "DIST-HG-09", 25, "Huyện Vị Xuyên" },
                    { 285, "DIST-HG-10", 25, "Huyện Xín Mần" },
                    { 286, "DIST-HG-11", 25, "Huyện Yên Minh" },
                    { 287, "DIST-HN-01", 26, "TP. Phủ Lý" },
                    { 288, "DIST-HN-02", 26, "TX. Duy Tiên" },
                    { 289, "DIST-HN-03", 26, "Huyện Bình Lục" },
                    { 290, "DIST-HN-04", 26, "Huyện Kim Bảng" },
                    { 291, "DIST-HN-05", 26, "Huyện Lý Nhân" },
                    { 292, "DIST-HN-06", 26, "Huyện Thanh Liêm" },
                    { 293, "DIST-HT-01", 27, "TP. Hà Tĩnh" },
                    { 294, "DIST-HT-02", 27, "TX. Hồng Lĩnh" },
                    { 295, "DIST-HT-03", 27, "TX. Kỳ Anh" },
                    { 296, "DIST-HT-04", 27, "Huyện Cẩm Xuyên" },
                    { 297, "DIST-HT-05", 27, "Huyện Can Lộc" },
                    { 298, "DIST-HT-06", 27, "Huyện Đức Thọ" },
                    { 299, "DIST-HT-07", 27, "Huyện Hương Khê" },
                    { 300, "DIST-HT-08", 27, "Huyện Hương Sơn" },
                    { 301, "DIST-HT-09", 27, "Huyện Kỳ Anh" },
                    { 302, "DIST-HT-10", 27, "Huyện Lộc Hà" },
                    { 303, "DIST-HT-11", 27, "Huyện Nghi Xuân" },
                    { 304, "DIST-HT-12", 27, "Huyện Thạch Hà" },
                    { 305, "DIST-HT-13", 27, "Huyện Vũ Quang" },
                    { 306, "DIST-HD-01", 28, "TP. Hải Dương" },
                    { 307, "DIST-HD-02", 28, "TX. Chí Linh" },
                    { 308, "DIST-HD-03", 28, "Huyện Bình Giang" },
                    { 309, "DIST-HD-04", 28, "Huyện Cẩm Giàng" },
                    { 310, "DIST-HD-05", 28, "Huyện Gia Lộc" },
                    { 311, "DIST-HD-06", 28, "Huyện Kim Thành" },
                    { 312, "DIST-HD-07", 28, "Huyện Nam Sách" },
                    { 313, "DIST-HD-08", 28, "Huyện Ninh Giang" },
                    { 314, "DIST-HD-09", 28, "Huyện Thanh Hà" },
                    { 315, "DIST-HD-10", 28, "Huyện Thanh Miện" },
                    { 316, "DIST-HD-11", 28, "Huyện Tứ Kỳ" },
                    { 317, "DIST-HG-01", 29, "TP. Vị Thanh" },
                    { 318, "DIST-HG-02", 29, "TX. Long Mỹ" },
                    { 319, "DIST-HG-03", 29, "TX. Ngã Bảy" },
                    { 320, "DIST-HG-04", 29, "Huyện Châu Thành" },
                    { 321, "DIST-HG-05", 29, "Huyện Châu Thành A" },
                    { 322, "DIST-HG-06", 29, "Huyện Long Mỹ" },
                    { 323, "DIST-HG-07", 29, "Huyện Phụng Hiệp" },
                    { 324, "DIST-HG-08", 29, "Huyện Vị Thủy" },
                    { 325, "DIST-HB-01", 30, "TP. Hòa Bình" },
                    { 326, "DIST-HB-02", 30, "Huyện Cao Phong" },
                    { 327, "DIST-HB-03", 30, "Huyện Đà Bắc" },
                    { 328, "DIST-HB-04", 30, "Huyện Kim Bôi" },
                    { 329, "DIST-HB-05", 30, "Huyện Kỳ Sơn" },
                    { 330, "DIST-HB-06", 30, "Huyện Lạc Sơn" },
                    { 331, "DIST-HB-07", 30, "Huyện Lạc Thủy" },
                    { 332, "DIST-HB-08", 30, "Huyện Lương Sơn" },
                    { 333, "DIST-HB-09", 30, "Huyện Mai Châu" },
                    { 334, "DIST-HB-10", 30, "Huyện Tân Lạc" },
                    { 335, "DIST-HB-11", 30, "Huyện Yên Thủy" },
                    { 336, "DIST-HY-01", 31, "TP. Hưng Yên" },
                    { 337, "DIST-HY-02", 31, "Huyện Ân Thi" },
                    { 338, "DIST-HY-03", 31, "Huyện Khoái Châu" },
                    { 339, "DIST-HY-04", 31, "Huyện Kim Động" },
                    { 340, "DIST-HY-05", 31, "Huyện Mỹ Hào" },
                    { 341, "DIST-HY-06", 31, "Huyện Phù Cừ" },
                    { 342, "DIST-HY-07", 31, "Huyện Tiên Lữ" },
                    { 343, "DIST-HY-08", 31, "Huyện Văn Giang" },
                    { 344, "DIST-HY-09", 31, "Huyện Văn Lâm" },
                    { 345, "DIST-HY-10", 31, "Huyện Yên Mỹ" },
                    { 346, "DIST-KH-01", 32, "TP. Nha Trang" },
                    { 347, "DIST-KH-02", 32, "TP. Cam Ranh" },
                    { 348, "DIST-KH-03", 32, "TX. Ninh Hòa" },
                    { 349, "DIST-KH-04", 32, "Huyện Cam Lâm" },
                    { 350, "DIST-KH-05", 32, "Huyện Diên Khánh" },
                    { 351, "DIST-KH-06", 32, "Huyện Khánh Sơn" },
                    { 352, "DIST-KH-07", 32, "Huyện Khánh Vĩnh" },
                    { 353, "DIST-KH-08", 32, "Huyện Trường Sa" },
                    { 354, "DIST-KH-09", 32, "Huyện Vạn Ninh" },
                    { 355, "DIST-KG-01", 33, "TP. Rạch Giá" },
                    { 356, "DIST-KG-02", 33, "TP. Hà Tiên" },
                    { 357, "DIST-KG-03", 33, "Huyện An Biên" },
                    { 358, "DIST-KG-04", 33, "Huyện An Minh" },
                    { 359, "DIST-KG-05", 33, "Huyện Châu Thành" },
                    { 360, "DIST-KG-06", 33, "Huyện Giang Thành" },
                    { 361, "DIST-KG-07", 33, "Huyện Giồng Riềng" },
                    { 362, "DIST-KG-08", 33, "Huyện Gò Quao" },
                    { 363, "DIST-KG-09", 33, "Huyện Hòn Đất" },
                    { 364, "DIST-KG-10", 33, "Huyện Kiên Hải" },
                    { 365, "DIST-KG-11", 33, "Huyện Kiên Lương" },
                    { 366, "DIST-KG-12", 33, "Huyện Phú Quốc" },
                    { 367, "DIST-KG-13", 33, "Huyện Tân Hiệp" },
                    { 368, "DIST-KG-14", 33, "Huyện U Minh Thượng" },
                    { 369, "DIST-KG-15", 33, "Huyện Vĩnh Thuận" },
                    { 370, "DIST-KT-01", 34, "TP. Kon Tum" },
                    { 371, "DIST-KT-02", 34, "Huyện Đắk Glei" },
                    { 372, "DIST-KT-03", 34, "Huyện Đắk Hà" },
                    { 373, "DIST-KT-04", 34, "Huyện Đắk Tô" },
                    { 374, "DIST-KT-05", 34, "Huyện Kon Plông" },
                    { 375, "DIST-KT-06", 34, "Huyện Kon Rẫy" },
                    { 376, "DIST-KT-07", 34, "Huyện Ngọc Hồi" },
                    { 377, "DIST-KT-08", 34, "Huyện Sa Thầy" },
                    { 378, "DIST-KT-09", 34, "Huyện Tu Mơ Rông" },
                    { 379, "DIST-LC-01", 35, "TP. Lai Châu" },
                    { 380, "DIST-LC-02", 35, "Huyện Mường Tè" },
                    { 381, "DIST-LC-03", 35, "Huyện Nậm Nhùn" },
                    { 382, "DIST-LC-04", 35, "Huyện Phong Thổ" },
                    { 383, "DIST-LC-05", 35, "Huyện Sìn Hồ" },
                    { 384, "DIST-LC-06", 35, "Huyện Tam Đường" },
                    { 385, "DIST-LC-07", 35, "Huyện Tân Uyên" },
                    { 386, "DIST-LC-08", 35, "Huyện Than Uyên" },
                    { 387, "DIST-LD-01", 36, "TP. Đà Lạt" },
                    { 388, "DIST-LD-02", 36, "TP. Bảo Lộc" },
                    { 389, "DIST-LD-03", 36, "Huyện Bảo Lâm" },
                    { 390, "DIST-LD-04", 36, "Huyện Cát Tiên" },
                    { 391, "DIST-LD-05", 36, "Huyện Đạ Huoai" },
                    { 392, "DIST-LD-06", 36, "Huyện Đạ Tẻh" },
                    { 393, "DIST-LD-07", 36, "Huyện Đam Rông" },
                    { 394, "DIST-LD-08", 36, "Huyện Di Linh" },
                    { 395, "DIST-LD-09", 36, "Huyện Đơn Dương" },
                    { 396, "DIST-LD-10", 36, "Huyện Đức Trọng" },
                    { 397, "DIST-LD-11", 36, "Huyện Lạc Dương" },
                    { 398, "DIST-LD-12", 36, "Huyện Lâm Hà" },
                    { 399, "DIST-LS-01", 37, "TP. Lạng Sơn" },
                    { 400, "DIST-LS-02", 37, "Huyện Bắc Sơn" },
                    { 401, "DIST-LS-03", 37, "Huyện Bình Gia" },
                    { 402, "DIST-LS-04", 37, "Huyện Cao Lộc" },
                    { 403, "DIST-LS-05", 37, "Huyện Chi Lăng" },
                    { 404, "DIST-LS-06", 37, "Huyện Đình Lập" },
                    { 405, "DIST-LS-07", 37, "Huyện Hữu Lũng" },
                    { 406, "DIST-LS-08", 37, "Huyện Lộc Bình" },
                    { 407, "DIST-LS-09", 37, "Huyện Tràng Định" },
                    { 408, "DIST-LS-10", 37, "Huyện Văn Lãng" },
                    { 409, "DIST-LS-11", 37, "Huyện Văn Quan" },
                    { 410, "DIST-LC-01", 38, "TP. Lào Cai" },
                    { 411, "DIST-LC-02", 38, "TX. Sa Pa" },
                    { 412, "DIST-LC-03", 38, "Huyện Bắc Hà" },
                    { 413, "DIST-LC-04", 38, "Huyện Bảo Thắng" },
                    { 414, "DIST-LC-05", 38, "Huyện Bảo Yên" },
                    { 415, "DIST-LC-06", 38, "Huyện Bát Xát" },
                    { 416, "DIST-LC-07", 38, "Huyện Mường Khương" },
                    { 417, "DIST-LC-08", 38, "Huyện Si Ma Cai" },
                    { 418, "DIST-LC-09", 38, "Huyện Văn Bàn" },
                    { 419, "DIST-LA-01", 39, "TP. Tân An" },
                    { 420, "DIST-LA-02", 39, "TX. Kiến Tường" },
                    { 421, "DIST-LA-03", 39, "Huyện Bến Lức" },
                    { 422, "DIST-LA-04", 39, "Huyện Cần Đước" },
                    { 423, "DIST-LA-05", 39, "Huyện Cần Giuộc" },
                    { 424, "DIST-LA-06", 39, "Huyện Châu Thành" },
                    { 425, "DIST-LA-07", 39, "Huyện Đức Hòa" },
                    { 426, "DIST-LA-08", 39, "Huyện Đức Huệ" },
                    { 427, "DIST-LA-09", 39, "Huyện Mộc Hóa" },
                    { 428, "DIST-LA-10", 39, "Huyện Tân Hưng" },
                    { 429, "DIST-LA-11", 39, "Huyện Tân Thạnh" },
                    { 430, "DIST-LA-12", 39, "Huyện Tân Trụ" },
                    { 431, "DIST-LA-13", 39, "Huyện Thạnh Hóa" },
                    { 432, "DIST-LA-14", 39, "Huyện Thủ Thừa" },
                    { 433, "DIST-LA-15", 39, "Huyện Vĩnh Hưng" },
                    { 434, "DIST-ND-01", 40, "Mỹ Lộc" },
                    { 435, "DIST-ND-02", 40, "Vụ Bản" },
                    { 436, "DIST-ND-03", 40, "Nam Trực" },
                    { 437, "DIST-ND-04", 40, "Trực Ninh" },
                    { 438, "DIST-ND-05", 40, "Xuân Trường" },
                    { 439, "DIST-ND-06", 40, "Giao Thủy" },
                    { 440, "DIST-ND-07", 40, "Hải Hậu" },
                    { 441, "DIST-ND-08", 40, "Nghĩa Hưng" },
                    { 442, "DIST-ND-09", 40, "Ý Yên" },
                    { 443, "DIST-NA-01", 41, "TP Vinh" },
                    { 444, "DIST-NA-02", 41, "Thị xã Cửa Lò" },
                    { 445, "DIST-NA-03", 41, "Thị xã Thái Hòa" },
                    { 446, "DIST-NA-04", 41, "Quỳnh Lưu" },
                    { 447, "DIST-NA-05", 41, "Diễn Châu" },
                    { 448, "DIST-NA-06", 41, "Nghi Lộc" },
                    { 449, "DIST-NA-07", 41, "Yên Thành" },
                    { 450, "DIST-NA-08", 41, "Hưng Nguyên" },
                    { 451, "DIST-NA-09", 41, "Quỳ Hợp" },
                    { 452, "DIST-NA-10", 41, "Quỳ Châu" },
                    { 453, "DIST-NA-11", 41, "Tân Kỳ" },
                    { 454, "DIST-NA-12", 41, "Đô Lương" },
                    { 455, "DIST-NA-13", 41, "Anh Sơn" },
                    { 456, "DIST-NA-14", 41, "Con Cuông" },
                    { 457, "DIST-NA-15", 41, "Tương Dương" },
                    { 458, "DIST-NA-16", 41, "Kỳ Sơn" },
                    { 459, "DIST-NA-17", 41, "Nam Đàn" },
                    { 460, "DIST-NA-18", 41, "Thanh Chương" },
                    { 461, "DIST-NB-01", 42, "Gia Viễn" },
                    { 462, "DIST-NB-02", 42, "TP Hoa Lư" },
                    { 463, "DIST-NB-03", 42, "Kim Sơn" },
                    { 464, "DIST-NB-04", 42, "Nho Quan" },
                    { 465, "DIST-NB-05", 42, "TP Tam Điệp" },
                    { 466, "DIST-NB-06", 42, "Yên Khánh" },
                    { 467, "DIST-NB-07", 42, "Yên Mô" },
                    { 468, "DIST-NT-01", 43, "Bác Ái" },
                    { 469, "DIST-NT-02", 43, "Ninh Hải" },
                    { 470, "DIST-NT-03", 43, "Ninh Phước" },
                    { 471, "DIST-NT-04", 43, "Thuận Bắc" },
                    { 472, "DIST-NT-05", 43, "Thuận Nam" },
                    { 473, "DIST-NT-06", 43, "Phan Rang-Tháp Chàm" },
                    { 474, "DIST-NT-07", 43, "Ninh Sơn" },
                    { 475, "DIST-PT-01", 44, "Cẩm Khê" },
                    { 476, "DIST-PT-02", 44, "Đoan Hùng" },
                    { 477, "DIST-PT-03", 44, "Hạ Hòa" },
                    { 478, "DIST-PT-04", 44, "Thanh Ba" },
                    { 479, "DIST-PT-05", 44, "Phù Ninh" },
                    { 480, "DIST-PT-06", 44, "Tam Nông" },
                    { 481, "DIST-PT-07", 44, "Tân Sơn" },
                    { 482, "DIST-PT-08", 44, "Thanh Sơn" },
                    { 483, "DIST-PT-09", 44, "Thanh Thủy" },
                    { 484, "DIST-PT-10", 44, "Yên Lập" },
                    { 485, "DIST-PT-11", 44, "Lâm Thao" },
                    { 486, "DIST-PY-01", 45, "Tuy An" },
                    { 487, "DIST-PY-02", 45, "Sơn Hòa" },
                    { 488, "DIST-PY-03", 45, "TP Tuy Hòa" },
                    { 489, "DIST-PY-04", 45, "Phú Hòa" },
                    { 490, "DIST-PY-05", 45, "Thị xã Sông Cầu" },
                    { 491, "DIST-PY-06", 45, "Đồng Xuân" },
                    { 492, "DIST-PY-07", 45, "Sông Hinh" },
                    { 493, "DIST-PY-08", 45, "Tây Hòa" },
                    { 494, "DIST-PY-09", 45, "Thị xã Đông Hòa" },
                    { 495, "DIST-QB-01", 46, "Quảng Ninh" },
                    { 496, "DIST-QB-02", 46, "TP Đồng Hới" },
                    { 497, "DIST-QB-03", 46, "Lệ Thủy" },
                    { 498, "DIST-QB-04", 46, "Bố Trạch" },
                    { 499, "DIST-QB-05", 46, "Thị xã Ba Đồn" },
                    { 500, "DIST-QB-06", 46, "Phong Nha" },
                    { 501, "DIST-QB-07", 46, "Minh Hóa" },
                    { 502, "DIST-QB-08", 46, "Tuyên Hóa" },
                    { 503, "DIST-QB-09", 46, "Quảng Trạch" },
                    { 504, "DIST-QN-01", 47, "TP Tam Kỳ" },
                    { 505, "DIST-QN-02", 47, "TP Hội An" },
                    { 506, "DIST-QN-03", 47, "Tiên Phước" },
                    { 507, "DIST-QN-04", 47, "Quế Sơn" },
                    { 508, "DIST-QN-05", 47, "Duy Xuyên" },
                    { 509, "DIST-QN-06", 47, "Thăng Bình" },
                    { 510, "DIST-QN-07", 47, "Đại Lộc" },
                    { 511, "DIST-QN-08", 47, "Đông Giang" },
                    { 512, "DIST-QN-09", 47, "Tây Giang" },
                    { 513, "DIST-QN-10", 47, "Trà My" },
                    { 514, "DIST-QN-11", 47, "Phước Sơn" },
                    { 515, "DIST-QN-12", 47, "Núi Thành" },
                    { 516, "DIST-QN-13", 47, "Phú Ninh" },
                    { 517, "DIST-QN-14", 47, "Hiệp Đức" },
                    { 518, "DIST-QN-15", 47, "Thị xã Điện Bàn" },
                    { 519, "DIST-QNG-01", 48, "TP Quảng Ngãi" },
                    { 520, "DIST-QNG-02", 48, "Lý Sơn" },
                    { 521, "DIST-QNG-03", 48, "Ba Tơ" },
                    { 522, "DIST-QNG-04", 48, "Thị Xã Đức Phổ" },
                    { 523, "DIST-QNG-05", 48, "Bình Sơn" },
                    { 524, "DIST-QNG-06", 48, "Minh Long" },
                    { 525, "DIST-QNG-07", 48, "Mộ Đức" },
                    { 526, "DIST-QNG-08", 48, "Nghĩa Hành" },
                    { 527, "DIST-QNG-09", 48, "Sơn Hà" },
                    { 528, "DIST-QNG-10", 48, "Sơn Tây" },
                    { 529, "DIST-QNG-11", 48, "Sơn Tịnh" },
                    { 530, "DIST-QNG-12", 48, "Trà Bồng" },
                    { 531, "DIST-QNG-13", 48, "Tư Nghĩa" },
                    { 532, "DIST-QN-01", 49, "Vịnh Hạ Long" },
                    { 533, "DIST-QN-02", 49, "Móng Cái" },
                    { 534, "DIST-QN-03", 49, "Yên Hưng" },
                    { 535, "DIST-QN-04", 49, "Đình Lập" },
                    { 536, "DIST-QN-05", 49, "Đông Triều" },
                    { 537, "DIST-QN-06", 49, "Bình Liêu" },
                    { 538, "DIST-QN-07", 49, "Tiên Yên" },
                    { 539, "DIST-QN-08", 49, "Ba Chẽ" },
                    { 540, "DIST-QN-09", 49, "Hoành Bồ" },
                    { 541, "DIST-QN-10", 49, "Cẩm Phả" },
                    { 542, "DIST-QN-11", 49, "Đầm Hà" },
                    { 543, "DIST-QN-12", 49, "Hà Cối" },
                    { 544, "DIST-QN-13", 49, "Thị xã Hồng Gai" },
                    { 545, "DIST-QN-14", 49, "Thị xã Cẩm Phả" },
                    { 546, "DIST-QN-15", 49, "Thị xã Uông Bí" },
                    { 547, "DIST-QT-01", 50, "TP Đông Hà" },
                    { 548, "DIST-QT-02", 50, "Hải Lăng" },
                    { 549, "DIST-QT-03", 50, "Triệu Phong" },
                    { 550, "DIST-QT-04", 50, "Vĩnh Linh" },
                    { 551, "DIST-QT-05", 50, "Cam Lộ" },
                    { 552, "DIST-QT-06", 50, "Hướng Hóa" },
                    { 553, "DIST-QT-07", 50, "Đakrông" },
                    { 554, "DIST-QT-08", 50, "Cồn Cỏ" },
                    { 555, "DIST-QT-09", 50, "Thị xã Quảng Trị" },
                    { 556, "DIST-ST-01", 51, "TP Sóc Trăng" },
                    { 557, "DIST-ST-02", 51, "Ngã Năm" },
                    { 558, "DIST-ST-03", 51, "Mỹ Tú" },
                    { 559, "DIST-ST-04", 51, "Mỹ Xuyên" },
                    { 560, "DIST-ST-05", 51, "Long Phú" },
                    { 561, "DIST-ST-06", 51, "Châu Thành" },
                    { 562, "DIST-ST-07", 51, "Kế Sách" },
                    { 563, "DIST-ST-08", 51, "Cù Lao Dung" },
                    { 564, "DIST-ST-09", 51, "Vĩnh Châu" },
                    { 565, "DIST-SL-01", 52, "TP Sơn La" },
                    { 566, "DIST-SL-02", 52, "Mộc Châu" },
                    { 567, "DIST-SL-03", 52, "Bắc Yên" },
                    { 568, "DIST-SL-04", 52, "Mai Sơn" },
                    { 569, "DIST-SL-05", 52, "Sông Mã" },
                    { 570, "DIST-SL-06", 52, "Sốp Cộp" },
                    { 571, "DIST-SL-07", 52, "Phù Yên" },
                    { 572, "DIST-SL-08", 52, "Yên Sơn" },
                    { 573, "DIST-SL-09", 52, "Thuận Châu" },
                    { 574, "DIST-SL-10", 52, "Mường La" },
                    { 575, "DIST-SL-11", 52, "Quỳnh Nhai" },
                    { 576, "DIST-TN-01", 53, "Thị xã Tây Ninh" },
                    { 577, "DIST-TN-02", 53, "Hòa Thành" },
                    { 578, "DIST-TN-03", 53, "Châu Thành" },
                    { 579, "DIST-TN-04", 53, "Tân Biên" },
                    { 580, "DIST-TN-05", 53, "Tân Châu" },
                    { 581, "DIST-TN-06", 53, "Dương Minh Châu" },
                    { 582, "DIST-TN-07", 53, "Bến Cầu" },
                    { 583, "DIST-TN-08", 53, "Gò Dầu" },
                    { 584, "DIST-TN-09", 53, "Trảng Bàng" },
                    { 585, "DIST-TB-01", 54, "TP Thái Bình" },
                    { 586, "DIST-TB-02", 54, "Hưng Hà" },
                    { 587, "DIST-TB-03", 54, "Quỳnh Phụ" },
                    { 588, "DIST-TB-04", 54, "Vũ Thư" },
                    { 589, "DIST-TB-05", 54, "Đông Hưng" },
                    { 590, "DIST-TB-06", 54, "Tiền Hải" },
                    { 591, "DIST-TB-07", 54, "Kiến Xương" },
                    { 592, "DIST-TB-08", 54, "Thái Thụy" },
                    { 593, "DIST-TN-01", 55, "TP Thái Nguyên" },
                    { 594, "DIST-TN-02", 55, "TP Sông Công" },
                    { 595, "DIST-TN-03", 55, "TP Phổ Yên" },
                    { 596, "DIST-TN-04", 55, "Đại Từ" },
                    { 597, "DIST-TN-05", 55, "Phú Lương" },
                    { 598, "DIST-TN-06", 55, "Võ Nhai" },
                    { 599, "DIST-TN-07", 55, "Đông Hỷ" },
                    { 600, "DIST-TN-08", 55, "Phú Bình" },
                    { 601, "DIST-TH-01", 56, "Thị xã Thanh Hóa" },
                    { 602, "DIST-TH-02", 56, "Bá Thước" },
                    { 603, "DIST-TH-03", 56, "Cẩm Thủy" },
                    { 604, "DIST-TH-04", 56, "Đông Sơn" },
                    { 605, "DIST-TH-05", 56, "Hà Trung" },
                    { 606, "DIST-TH-06", 56, "Hậu Lộc" },
                    { 607, "DIST-TH-07", 56, "Hoằng Hóa" },
                    { 608, "DIST-TH-08", 56, "Lang Chánh" },
                    { 609, "DIST-TH-09", 56, "Nga Sơn" },
                    { 610, "DIST-TH-10", 56, "Ngọc Lặc" },
                    { 611, "DIST-TH-11", 56, "Như Xuân" },
                    { 612, "DIST-TH-12", 56, "Nông Cống" },
                    { 613, "DIST-TH-13", 56, "Quan Hóa" },
                    { 614, "DIST-TH-14", 56, "Quảng Xương" },
                    { 615, "DIST-TH-15", 56, "Thạch Thành" },
                    { 616, "DIST-TH-16", 56, "Thọ Xuân" },
                    { 617, "DIST-TH-17", 56, "Thiệu Hóa" },
                    { 618, "DIST-TH-18", 56, "Thường Xuân" },
                    { 619, "DIST-TH-19", 56, "Tĩnh Gia" },
                    { 620, "DIST-TH-20", 56, "Vĩnh Lộc" },
                    { 621, "DIST-TH-21", 56, "Yên Định" },
                    { 622, "DIST-TTH-01", 57, "TP Huế" },
                    { 623, "DIST-TTH-02", 57, "Phú Vang" },
                    { 624, "DIST-TTH-03", 57, "Hương Trà" },
                    { 625, "DIST-TTH-04", 57, "Hương Thủy" },
                    { 626, "DIST-TTH-05", 57, "Nam Đông" },
                    { 627, "DIST-TTH-06", 57, "Phong Điền" },
                    { 628, "DIST-TTH-07", 57, "Quảng Điền" },
                    { 629, "DIST-TTH-08", 57, "A Lưới" },
                    { 630, "DIST-TG-01", 58, "Thị xã Gò Công" },
                    { 631, "DIST-TG-02", 58, "Thị xã Cai Lậy" },
                    { 632, "DIST-TG-03", 58, "Gò Công Đông" },
                    { 633, "DIST-TG-04", 58, "Gò Công Tây" },
                    { 634, "DIST-TG-05", 58, "Tiền Giang" },
                    { 635, "DIST-TG-06", 58, "TP Mỹ Tho" },
                    { 636, "DIST-TG-07", 58, "Tân Phú Đông" },
                    { 637, "DIST-TG-08", 58, "Thiên Giang" },
                    { 638, "DIST-TG-09", 58, "Cái Bè" },
                    { 639, "DIST-TG-10", 58, "Châu Thành" },
                    { 640, "DIST-TG-11", 58, "Tân Túc" },
                    { 641, "DIST-TG-12", 58, "Chợ Gạo" },
                    { 642, "DIST-TV-01", 59, "TP Trà Vinh" },
                    { 643, "DIST-TV-02", 59, "Càng Long" },
                    { 644, "DIST-TV-03", 59, "Châu Thành" },
                    { 645, "DIST-TV-04", 59, "Tiểu Cần" },
                    { 646, "DIST-TV-05", 59, "Cầu Kè" },
                    { 647, "DIST-TV-06", 59, "Trà Cú" },
                    { 648, "DIST-TV-07", 59, "Cầu Ngang" },
                    { 649, "DIST-TV-08", 59, "Duyên Hải" },
                    { 650, "DIST-TQ-01", 60, "TP Tuyên Quang" },
                    { 651, "DIST-TQ-02", 60, "Yên Sơn" },
                    { 652, "DIST-TQ-03", 60, "Chiêm Hóa" },
                    { 653, "DIST-TQ-04", 60, "Na Hang" },
                    { 654, "DIST-TQ-05", 60, "Hàm Yên" },
                    { 655, "DIST-TQ-06", 60, "Sơn Dương" },
                    { 656, "DIST-TQ-07", 60, "Lâm Bình" },
                    { 657, "DIST-VL-01", 61, "Vũng Liêm" },
                    { 658, "DIST-VL-02", 61, "Trà Ôn" },
                    { 659, "DIST-VL-03", 61, "Long Hồ" },
                    { 660, "DIST-VL-04", 61, "Mang Thít" },
                    { 661, "DIST-VL-05", 61, "Tam Bình" },
                    { 662, "DIST-VL-06", 61, "Bình Tân" },
                    { 663, "DIST-VL-07", 61, "Thành phố Vĩnh Long" },
                    { 664, "DIST-VL-08", 61, "Thị xã Bình Minh" },
                    { 665, "DIST-VP-01", 62, "Tam Dương" },
                    { 666, "DIST-VP-02", 62, "Tam Đảo" },
                    { 667, "DIST-VP-03", 62, "Bình Xuyên" },
                    { 668, "DIST-VP-04", 62, "Lập Thạch" },
                    { 669, "DIST-VP-05", 62, "Yên Lạc" },
                    { 670, "DIST-VP-06", 62, "Vĩnh Tường" },
                    { 671, "DIST-VP-07", 62, "Sông Lô" },
                    { 672, "DIST-YB-01", 63, "Yên Bái" },
                    { 673, "DIST-YB-02", 63, "Trấn Yên" },
                    { 674, "DIST-YB-03", 63, "Văn Chấn" },
                    { 675, "DIST-YB-04", 63, "Văn Yên" },
                    { 676, "DIST-YB-05", 63, "Mù Cang Chải" },
                    { 677, "DIST-YB-06", 63, "Đà Sơn" },
                    { 678, "DIST-YB-07", 63, "Trạm Tấu" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserChatRoom_ParticipantsId",
                table: "ApplicationUserChatRoom",
                column: "ParticipantsId");

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
                name: "IX_ChatMessages_ChatRoomId",
                table: "ChatMessages",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ReceiverId",
                table: "ChatMessages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId",
                table: "ChatMessages",
                column: "SenderId");

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
                name: "IX_Tickets_IdEmployee",
                table: "Tickets",
                column: "IdEmployee");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_IdPassenger",
                table: "Tickets",
                column: "IdPassenger");

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
                name: "ApplicationUserChatRoom");

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
                name: "Bills");

            migrationBuilder.DropTable(
                name: "ChatMessages");

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
                name: "ChatRooms");

            migrationBuilder.DropTable(
                name: "TypeNews");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Seats");

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
