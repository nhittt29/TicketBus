using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketBus.Models;
using System.Text.Json;

namespace TicketBus.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<DropOff> DropOffs { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Pickup> Pickups { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<RegistForm> RegistForms { get; set; }
        public DbSet<BusRoute> BusRoutes { get; set; }
        public DbSet<RouteStop> RouteStops { get; set; }
        public DbSet<ScheduleDetails> ScheduleDetails { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TypeNews> TypeNews { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        //Khung chat
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Dữ liệu mẫu cho VehicleType
            modelBuilder.Entity<VehicleType>().HasData(
                new VehicleType { IdType = 1, TypeCode = "VT001", NameType = "Giường nằm CLC 34 chỗ", SeatCount = 34, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 2, TypeCode = "VT002", NameType = "Giường nằm CLC 40 chỗ", SeatCount = 40, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 3, TypeCode = "VT003", NameType = "Giường nằm CLC VIP 20 chỗ", SeatCount = 20, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 4, TypeCode = "VT004", NameType = "Giường nằm massage 34 chỗ", SeatCount = 34, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 5, TypeCode = "VT005", NameType = "Giường nằm massage 40 chỗ", SeatCount = 40, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 6, TypeCode = "VT006", NameType = "Giường nằm đôi VIP 22 chỗ", SeatCount = 22, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 7, TypeCode = "VT007", NameType = "Ghé Nằm CLC 34 chỗ", SeatCount = 34, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 8, TypeCode = "VT008", NameType = "Ghé Nằm CLC 40 chỗ", SeatCount = 40, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 9, TypeCode = "VT009", NameType = "Ghé Nằm VIP 20 chỗ", SeatCount = 20, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 10, TypeCode = "VT010", NameType = "Ghé Nằm massage 34 chỗ", SeatCount = 34, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 11, TypeCode = "VT011", NameType = "Ghế ngồi CLC 45 chỗ", SeatCount = 45, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 12, TypeCode = "VT012", NameType = "Ghế ngồi CLC 50 chỗ", SeatCount = 50, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 13, TypeCode = "VT013", NameType = "Ghế ngồi VIP 32 chỗ", SeatCount = 32, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 14, TypeCode = "VT014", NameType = "Ghế ngồi Limousine 28 chỗ", SeatCount = 28, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 15, TypeCode = "VT015", NameType = "Limousine DCar VIP 9 chỗ", SeatCount = 9, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 16, TypeCode = "VT016", NameType = "Limousine President 11 chỗ", SeatCount = 11, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 17, TypeCode = "VT017", NameType = "Limousine Fuso Rosa 17 chỗ", SeatCount = 17, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 18, TypeCode = "VT018", NameType = "Limousine Skybus 19 chỗ", SeatCount = 19, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 19, TypeCode = "VT019", NameType = "Limousine Jet VIP 22 chỗ", SeatCount = 22, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 20, TypeCode = "VT020", NameType = "Limousine Auto Kingdom 26 chỗ", SeatCount = 26, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 21, TypeCode = "VT021", NameType = "Xe khách giường nằm 34 chỗ", SeatCount = 34, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 22, TypeCode = "VT022", NameType = "Xe khách giường nằm 40 chỗ", SeatCount = 40, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 23, TypeCode = "VT023", NameType = "Xe khách giường đôi 20 chỗ", SeatCount = 20, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 24, TypeCode = "VT024", NameType = "Xe khách giường đôi 34 chỗ", SeatCount = 34, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 25, TypeCode = "VT025", NameType = "Xe ghế ngồi 12 chỗ Transit", SeatCount = 12, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 26, TypeCode = "VT026", NameType = "Xe ghế ngồi 29 chỗ County", SeatCount = 29, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 27, TypeCode = "VT027", NameType = "Xe ghế ngồi 34 chỗ Thaco Garden", SeatCount = 34, State = VehicleTypeState.KhongHoatDong },
                new VehicleType { IdType = 28, TypeCode = "VT028", NameType = "Xe ghế ngồi 50 chỗ Giáp Bát Express", SeatCount = 50, State = VehicleTypeState.KhongHoatDong },
                new VehicleType { IdType = 29, TypeCode = "VT029", NameType = "Xe giường nằm massage 38 chỗ", SeatCount = 38, State = VehicleTypeState.HoatDong },
                new VehicleType { IdType = 30, TypeCode = "VT030", NameType = "Xe giường nằm CLC 32 chỗ", SeatCount = 32, State = VehicleTypeState.HoatDong }
            );

            // Seeding dữ liệu cho bảng Cities
            modelBuilder.Entity<City>().HasData(
                new City { IdCity = 1, CityCode = "TP01", NameCity = "Hà Nội" },
                new City { IdCity = 2, CityCode = "TP02", NameCity = "Hồ Chí Minh" },
                new City { IdCity = 3, CityCode = "TP03", NameCity = "Hải Phòng" },
                new City { IdCity = 4, CityCode = "TP04", NameCity = "Đà Nẵng" },
                new City { IdCity = 5, CityCode = "TP05", NameCity = "Cần Thơ" },
                new City { IdCity = 6, CityCode = "TP06", NameCity = "An Giang" },
                new City { IdCity = 7, CityCode = "TP07", NameCity = "Bà Rịa - Vũng Tàu" },
                new City { IdCity = 8, CityCode = "TP08", NameCity = "Bắc Giang" },
                new City { IdCity = 9, CityCode = "TP09", NameCity = "Bắc Kạn" },
                new City { IdCity = 10, CityCode = "TP10", NameCity = "Bạc Liêu" },
                new City { IdCity = 11, CityCode = "TP11", NameCity = "Bắc Ninh" },
                new City { IdCity = 12, CityCode = "TP12", NameCity = "Bến Tre" },
                new City { IdCity = 13, CityCode = "TP13", NameCity = "Bình Định" },
                new City { IdCity = 14, CityCode = "TP14", NameCity = "Bình Dương" },
                new City { IdCity = 15, CityCode = "TP15", NameCity = "Bình Phước" },
                new City { IdCity = 16, CityCode = "TP16", NameCity = "Bình Thuận" },
                new City { IdCity = 17, CityCode = "TP17", NameCity = "Cà Mau" },
                new City { IdCity = 18, CityCode = "TP18", NameCity = "Cao Bằng" },
                new City { IdCity = 19, CityCode = "TP19", NameCity = "Đắk Lắk" },
                new City { IdCity = 20, CityCode = "TP20", NameCity = "Đắk Nông" },
                new City { IdCity = 21, CityCode = "TP21", NameCity = "Điện Biên" },
                new City { IdCity = 22, CityCode = "TP22", NameCity = "Đồng Nai" },
                new City { IdCity = 23, CityCode = "TP23", NameCity = "Đồng Tháp" },
                new City { IdCity = 24, CityCode = "TP24", NameCity = "Gia Lai" },
                new City { IdCity = 25, CityCode = "TP25", NameCity = "Hà Giang" },
                new City { IdCity = 26, CityCode = "TP26", NameCity = "Hà Nam" },
                new City { IdCity = 27, CityCode = "TP27", NameCity = "Hà Tĩnh" },
                new City { IdCity = 28, CityCode = "TP28", NameCity = "Hải Dương" },
                new City { IdCity = 29, CityCode = "TP29", NameCity = "Hậu Giang" },
                new City { IdCity = 30, CityCode = "TP30", NameCity = "Hòa Bình" },
                new City { IdCity = 31, CityCode = "TP31", NameCity = "Hưng Yên" },
                new City { IdCity = 32, CityCode = "TP32", NameCity = "Khánh Hòa" },
                new City { IdCity = 33, CityCode = "TP33", NameCity = "Kiên Giang" },
                new City { IdCity = 34, CityCode = "TP34", NameCity = "Kon Tum" },
                new City { IdCity = 35, CityCode = "TP35", NameCity = "Lai Châu" },
                new City { IdCity = 36, CityCode = "TP36", NameCity = "Lâm Đồng" },
                new City { IdCity = 37, CityCode = "TP37", NameCity = "Lạng Sơn" },
                new City { IdCity = 38, CityCode = "TP38", NameCity = "Lào Cai" },
                new City { IdCity = 39, CityCode = "TP39", NameCity = "Long An" },
                new City { IdCity = 40, CityCode = "TP40", NameCity = "Nam Định" },
                new City { IdCity = 41, CityCode = "TP41", NameCity = "Nghệ An" },
                new City { IdCity = 42, CityCode = "TP42", NameCity = "Ninh Bình" },
                new City { IdCity = 43, CityCode = "TP43", NameCity = "Ninh Thuận" },
                new City { IdCity = 44, CityCode = "TP44", NameCity = "Phú Thọ" },
                new City { IdCity = 45, CityCode = "TP45", NameCity = "Phú Yên" },
                new City { IdCity = 46, CityCode = "TP46", NameCity = "Quảng Bình" },
                new City { IdCity = 47, CityCode = "TP47", NameCity = "Quảng Nam" },
                new City { IdCity = 48, CityCode = "TP48", NameCity = "Quảng Ngãi" },
                new City { IdCity = 49, CityCode = "TP49", NameCity = "Quảng Ninh" },
                new City { IdCity = 50, CityCode = "TP50", NameCity = "Quảng Trị" },
                new City { IdCity = 51, CityCode = "TP51", NameCity = "Sóc Trăng" },
                new City { IdCity = 52, CityCode = "TP52", NameCity = "Sơn La" },
                new City { IdCity = 53, CityCode = "TP53", NameCity = "Tây Ninh" },
                new City { IdCity = 54, CityCode = "TP54", NameCity = "Thái Bình" },
                new City { IdCity = 55, CityCode = "TP55", NameCity = "Thái Nguyên" },
                new City { IdCity = 56, CityCode = "TP56", NameCity = "Thanh Hóa" },
                new City { IdCity = 57, CityCode = "TP57", NameCity = "Thừa Thiên Huế" },
                new City { IdCity = 58, CityCode = "TP58", NameCity = "Tiền Giang" },
                new City { IdCity = 59, CityCode = "TP59", NameCity = "Trà Vinh" },
                new City { IdCity = 60, CityCode = "TP60", NameCity = "Tuyên Quang" },
                new City { IdCity = 61, CityCode = "TP61", NameCity = "Vĩnh Long" },
                new City { IdCity = 62, CityCode = "TP62", NameCity = "Vĩnh Phúc" },
                new City { IdCity = 63, CityCode = "TP63", NameCity = "Yên Bái" }
            );

            // Seeding dữ liệu cho bảng Districts
            // Tôi sẽ gán IdDistrict tăng dần từ 1 trở lên
            int districtId = 1;

            // Hà Nội (IdCity = 1)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-01", NameDistrict = "Ba Đình", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-02", NameDistrict = "Hoàn Kiếm", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-03", NameDistrict = "Hai Bà Trưng", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-04", NameDistrict = "Đống Đa", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-05", NameDistrict = "Tây Hồ", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-06", NameDistrict = "Cầu Giấy", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-07", NameDistrict = "Thanh Xuân", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-08", NameDistrict = "Hoàng Mai", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-09", NameDistrict = "Long Biên", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-10", NameDistrict = "Bắc Từ Liêm", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-11", NameDistrict = "Nam Từ Liêm", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-12", NameDistrict = "Hà Đông", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-13", NameDistrict = "Thanh Trì", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-14", NameDistrict = "Gia Lâm", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-15", NameDistrict = "Đông Anh", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-16", NameDistrict = "Sóc Sơn", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-17", NameDistrict = "Hoài Đức", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-18", NameDistrict = "Đan Phượng", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-19", NameDistrict = "Phúc Thọ", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-20", NameDistrict = "Thạch Thất", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-21", NameDistrict = "Quốc Oai", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-22", NameDistrict = "Chương Mỹ", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-23", NameDistrict = "Thanh Oai", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-24", NameDistrict = "Mỹ Đức", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-25", NameDistrict = "Ứng Hòa", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-26", NameDistrict = "Phú Xuyên", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-27", NameDistrict = "Thường Tín", IdCity = 1 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-28", NameDistrict = "Mê Linh", IdCity = 1 }
            );

            // Hồ Chí Minh (IdCity = 2)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-01", NameDistrict = "Quận 1", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-02", NameDistrict = "Quận 2", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-03", NameDistrict = "Quận 3", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-04", NameDistrict = "Quận 4", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-05", NameDistrict = "Quận 5", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-06", NameDistrict = "Quận 6", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-07", NameDistrict = "Quận 7", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-08", NameDistrict = "Quận 8", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-09", NameDistrict = "Quận 9", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-10", NameDistrict = "Quận 10", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-11", NameDistrict = "Quận 11", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-12", NameDistrict = "Quận 12", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-13", NameDistrict = "Bình Tân", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-14", NameDistrict = "Tân Bình", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-15", NameDistrict = "Tân Phú", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-16", NameDistrict = "Gò Vấp", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-17", NameDistrict = "Phú Nhuận", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-18", NameDistrict = "Bình Thạnh", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-19", NameDistrict = "Củ Chi", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-20", NameDistrict = "Hóc Môn", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-21", NameDistrict = "Nhà Bè", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-22", NameDistrict = "Cần Giờ", IdCity = 2 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HCM-23", NameDistrict = "Bình Chánh", IdCity = 2 }
            );

            // Hải Phòng (IdCity = 3)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HP-01", NameDistrict = "Hồng Bàng", IdCity = 3 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HP-02", NameDistrict = "Lê Chân", IdCity = 3 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HP-03", NameDistrict = "Ngô Quyền", IdCity = 3 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HP-04", NameDistrict = "Hải An", IdCity = 3 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HP-05", NameDistrict = "Kiến An", IdCity = 3 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HP-06", NameDistrict = "Dương Kinh", IdCity = 3 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HP-07", NameDistrict = "Đồ Sơn", IdCity = 3 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HP-08", NameDistrict = "Thủy Nguyên", IdCity = 3 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HP-09", NameDistrict = "An Dương", IdCity = 3 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HP-10", NameDistrict = "An Lão", IdCity = 3 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HP-11", NameDistrict = "Kiến Thụy", IdCity = 3 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HP-12", NameDistrict = "Tiên Lãng", IdCity = 3 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HP-13", NameDistrict = "Vĩnh Bảo", IdCity = 3 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HP-14", NameDistrict = "Cát Hải", IdCity = 3 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HP-15", NameDistrict = "Bạch Long Vĩ", IdCity = 3 }
            );

            // Đà Nẵng (IdCity = 4)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-01", NameDistrict = "Hải Châu", IdCity = 4 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-02", NameDistrict = "Thanh Khê", IdCity = 4 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-03", NameDistrict = "Sơn Trà", IdCity = 4 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-04", NameDistrict = "Ngũ Hành Sơn", IdCity = 4 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-05", NameDistrict = "Liên Chiểu", IdCity = 4 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-06", NameDistrict = "Cẩm Lệ", IdCity = 4 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-07", NameDistrict = "Hòa Vang", IdCity = 4 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-08", NameDistrict = "Hoàng Sa", IdCity = 4 }
            );

            // Cần Thơ (IdCity = 5)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CT-01", NameDistrict = "Ninh Kiều", IdCity = 5 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CT-02", NameDistrict = "Bình Thủy", IdCity = 5 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CT-03", NameDistrict = "Cái Răng", IdCity = 5 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CT-04", NameDistrict = "Ô Môn", IdCity = 5 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CT-05", NameDistrict = "Thốt Nốt", IdCity = 5 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CT-06", NameDistrict = "Phong Điền", IdCity = 5 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CT-07", NameDistrict = "Cờ Đỏ", IdCity = 5 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CT-08", NameDistrict = "Vĩnh Thạnh", IdCity = 5 }
            );

            // An Giang (IdCity = 6)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-AG-01", NameDistrict = "TP. Long Xuyên", IdCity = 6 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-AG-02", NameDistrict = "TP. Châu Đốc", IdCity = 6 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-AG-03", NameDistrict = "TX. Tân Châu", IdCity = 6 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-AG-04", NameDistrict = "Huyện An Phú", IdCity = 6 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-AG-05", NameDistrict = "Huyện Châu Phú", IdCity = 6 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-AG-06", NameDistrict = "Huyện Châu Thành", IdCity = 6 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-AG-07", NameDistrict = "Huyện Chợ Mới", IdCity = 6 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-AG-08", NameDistrict = "Huyện Phú Tân", IdCity = 6 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-AG-09", NameDistrict = "Huyện Thoại Sơn", IdCity = 6 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-AG-10", NameDistrict = "Huyện Tri Tôn", IdCity = 6 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-AG-11", NameDistrict = "Huyện Tịnh Biên", IdCity = 6 }
            );

            // Bà Rịa - Vũng Tàu (IdCity = 7)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BRVT-01", NameDistrict = "TP. Vũng Tàu", IdCity = 7 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BRVT-02", NameDistrict = "TP. Bà Rịa", IdCity = 7 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BRVT-03", NameDistrict = "TX. Phú Mỹ", IdCity = 7 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BRVT-04", NameDistrict = "Huyện Châu Đức", IdCity = 7 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BRVT-05", NameDistrict = "Huyện Côn Đảo", IdCity = 7 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BRVT-06", NameDistrict = "Huyện Đất Đỏ", IdCity = 7 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BRVT-07", NameDistrict = "Huyện Long Điền", IdCity = 7 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BRVT-08", NameDistrict = "Huyện Xuyên Mộc", IdCity = 7 }
            );

            // Bắc Giang (IdCity = 8)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BG-01", NameDistrict = "TP. Bắc Giang", IdCity = 8 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BG-02", NameDistrict = "Huyện Hiệp Hòa", IdCity = 8 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BG-03", NameDistrict = "Huyện Lạng Giang", IdCity = 8 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BG-04", NameDistrict = "Huyện Lục Nam", IdCity = 8 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BG-05", NameDistrict = "Huyện Lục Ngạn", IdCity = 8 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BG-06", NameDistrict = "Huyện Sơn Động", IdCity = 8 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BG-07", NameDistrict = "Huyện Tân Yên", IdCity = 8 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BG-08", NameDistrict = "Huyện Việt Yên", IdCity = 8 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BG-09", NameDistrict = "Huyện Yên Dũng", IdCity = 8 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BG-10", NameDistrict = "Huyện Yên Thế", IdCity = 8 }
            );

            // Bắc Kạn (IdCity = 9)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BK-01", NameDistrict = "TP. Bắc Kạn", IdCity = 9 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BK-02", NameDistrict = "Huyện Ba Bể", IdCity = 9 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BK-03", NameDistrict = "Huyện Bạch Thông", IdCity = 9 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BK-04", NameDistrict = "Huyện Chợ Đồn", IdCity = 9 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BK-05", NameDistrict = "Huyện Chợ Mới", IdCity = 9 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BK-06", NameDistrict = "Huyện Na Rì", IdCity = 9 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BK-07", NameDistrict = "Huyện Ngân Sơn", IdCity = 9 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BK-08", NameDistrict = "Huyện Pác Nặm", IdCity = 9 }
            );

            // Bạc Liêu (IdCity = 10)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BL-01", NameDistrict = "TP. Bạc Liêu", IdCity = 10 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BL-02", NameDistrict = "TX. Giá Rai", IdCity = 10 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BL-03", NameDistrict = "Huyện Đông Hải", IdCity = 10 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BL-04", NameDistrict = "Huyện Hoà Bình", IdCity = 10 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BL-05", NameDistrict = "Huyện Hồng Dân", IdCity = 10 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BL-06", NameDistrict = "Huyện Phước Long", IdCity = 10 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BL-07", NameDistrict = "Huyện Vĩnh Lợi", IdCity = 10 }
            );

            // Bắc Ninh (IdCity = 11)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BN-01", NameDistrict = "TP. Bắc Ninh", IdCity = 11 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BN-02", NameDistrict = "TX. Từ Sơn", IdCity = 11 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BN-03", NameDistrict = "Huyện Gia Bình", IdCity = 11 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BN-04", NameDistrict = "Huyện Lương Tài", IdCity = 11 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BN-05", NameDistrict = "Huyện Quế Võ", IdCity = 11 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BN-06", NameDistrict = "Huyện Thuận Thành", IdCity = 11 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BN-07", NameDistrict = "Huyện Tiên Du", IdCity = 11 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BN-08", NameDistrict = "Huyện Yên Phong", IdCity = 11 }
            );

            // Bến Tre (IdCity = 12)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-01", NameDistrict = "TP. Bến Tre", IdCity = 12 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-02", NameDistrict = "Huyện Ba Tri", IdCity = 12 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-03", NameDistrict = "Huyện Bình Đại", IdCity = 12 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-04", NameDistrict = "Huyện Châu Thành", IdCity = 12 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-05", NameDistrict = "Huyện Chợ Lách", IdCity = 12 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-06", NameDistrict = "Huyện Giồng Trôm", IdCity = 12 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-07", NameDistrict = "Huyện Mỏ Cày Bắc", IdCity = 12 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-08", NameDistrict = "Huyện Mỏ Cày Nam", IdCity = 12 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-09", NameDistrict = "Huyện Thạnh Phú", IdCity = 12 }
            );

            // Bình Định (IdCity = 13)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-01", NameDistrict = "TP. Quy Nhơn", IdCity = 13 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-02", NameDistrict = "TX. An Nhơn", IdCity = 13 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-03", NameDistrict = "TX. Hoài Nhơn", IdCity = 13 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-04", NameDistrict = "Huyện An Lão", IdCity = 13 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-05", NameDistrict = "Huyện Hoài Ân", IdCity = 13 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-06", NameDistrict = "Huyện Phù Cát", IdCity = 13 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-07", NameDistrict = "Huyện Phù Mỹ", IdCity = 13 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-08", NameDistrict = "Huyện Tây Sơn", IdCity = 13 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-09", NameDistrict = "Huyện Tuy Phước", IdCity = 13 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-10", NameDistrict = "Huyện Vân Canh", IdCity = 13 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-11", NameDistrict = "Huyện Vĩnh Thạnh", IdCity = 13 }
            );

            // Bình Dương (IdCity = 14)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-01", NameDistrict = "TP. Thủ Dầu Một", IdCity = 14 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-02", NameDistrict = "TP. Dĩ An", IdCity = 14 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-03", NameDistrict = "TP. Thuận An", IdCity = 14 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-04", NameDistrict = "TX. Bến Cát", IdCity = 14 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-05", NameDistrict = "TX. Tân Uyên", IdCity = 14 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-06", NameDistrict = "Huyện Bàu Bàng", IdCity = 14 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-07", NameDistrict = "Huyện Bắc Tân Uyên", IdCity = 14 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-08", NameDistrict = "Huyện Dầu Tiếng", IdCity = 14 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BD-09", NameDistrict = "Huyện Phú Giáo", IdCity = 14 }
            );

            // Bình Phước (IdCity = 15)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BP-01", NameDistrict = "TP. Đồng Xoài", IdCity = 15 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BP-02", NameDistrict = "TX. Bình Long", IdCity = 15 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BP-03", NameDistrict = "TX. Phước Long", IdCity = 15 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BP-04", NameDistrict = "Huyện Bù Đăng", IdCity = 15 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BP-05", NameDistrict = "Huyện Bù Đốp", IdCity = 15 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BP-06", NameDistrict = "Huyện Bù Gia Mập", IdCity = 15 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BP-07", NameDistrict = "Huyện Chơn Thành", IdCity = 15 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BP-08", NameDistrict = "Huyện Đồng Phú", IdCity = 15 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BP-09", NameDistrict = "Huyện Hớn Quản", IdCity = 15 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BP-10", NameDistrict = "Huyện Lộc Ninh", IdCity = 15 }
            );

            // Bình Thuận (IdCity = 16)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-01", NameDistrict = "TP. Phan Thiết", IdCity = 16 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-02", NameDistrict = "TX. La Gi", IdCity = 16 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-03", NameDistrict = "Huyện Bắc Bình", IdCity = 16 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-04", NameDistrict = "Huyện Đức Linh", IdCity = 16 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-05", NameDistrict = "Huyện Hàm Tân", IdCity = 16 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-06", NameDistrict = "Huyện Hàm Thuận Bắc", IdCity = 16 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-07", NameDistrict = "Huyện Hàm Thuận Nam", IdCity = 16 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-08", NameDistrict = "Huyện Phú Quý", IdCity = 16 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-09", NameDistrict = "Huyện Tánh Linh", IdCity = 16 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-BT-10", NameDistrict = "Huyện Tuy Phong", IdCity = 16 }
            );

            // Cà Mau (IdCity = 17)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CM-01", NameDistrict = "TP. Cà Mau", IdCity = 17 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CM-02", NameDistrict = "Huyện Cái Nước", IdCity = 17 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CM-03", NameDistrict = "Huyện Đầm Dơi", IdCity = 17 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CM-04", NameDistrict = "Huyện Năm Căn", IdCity = 17 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CM-05", NameDistrict = "Huyện Ngọc Hiển", IdCity = 17 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CM-06", NameDistrict = "Huyện Phú Tân", IdCity = 17 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CM-07", NameDistrict = "Huyện Thới Bình", IdCity = 17 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CM-08", NameDistrict = "Huyện Trần Văn Thời", IdCity = 17 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CM-09", NameDistrict = "Huyện U Minh", IdCity = 17 }
            );

            // Cao Bằng (IdCity = 18)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CB-01", NameDistrict = "TP. Cao Bằng", IdCity = 18 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CB-02", NameDistrict = "Huyện Bảo Lạc", IdCity = 18 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CB-03", NameDistrict = "Huyện Bảo Lâm", IdCity = 18 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CB-04", NameDistrict = "Huyện Hạ Lang", IdCity = 18 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CB-05", NameDistrict = "Huyện Hà Quảng", IdCity = 18 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CB-06", NameDistrict = "Huyện Hòa An", IdCity = 18 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CB-07", NameDistrict = "Huyện Nguyên Bình", IdCity = 18 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CB-08", NameDistrict = "Huyện Quảng Hòa", IdCity = 18 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CB-09", NameDistrict = "Huyện Thạch An", IdCity = 18 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-CB-10", NameDistrict = "Huyện Trùng Khánh", IdCity = 18 }
            );

            // Đắk Lắk (IdCity = 19)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DL-01", NameDistrict = "TP. Buôn Ma Thuột", IdCity = 19 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DL-02", NameDistrict = "TX. Buôn Hồ", IdCity = 19 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DL-03", NameDistrict = "Huyện Buôn Đôn", IdCity = 19 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DL-04", NameDistrict = "Huyện Cư Kuin", IdCity = 19 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DL-05", NameDistrict = "Huyện Cư M’gar", IdCity = 19 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DL-06", NameDistrict = "Huyện Ea H’leo", IdCity = 19 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DL-07", NameDistrict = "Huyện Ea Kar", IdCity = 19 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DL-08", NameDistrict = "Huyện Ea Súp", IdCity = 19 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DL-09", NameDistrict = "Huyện Krông Ana", IdCity = 19 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DL-10", NameDistrict = "Huyện Krông Bông", IdCity = 19 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DL-11", NameDistrict = "Huyện Krông Búk", IdCity = 19 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DL-12", NameDistrict = "Huyện Krông Năng", IdCity = 19 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DL-13", NameDistrict = "Huyện Krông Pắc", IdCity = 19 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DL-14", NameDistrict = "Huyện Lắk", IdCity = 19 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DL-15", NameDistrict = "Huyện M’Đrắk", IdCity = 19 }
            );

            // Đắk Nông (IdCity = 20)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-01", NameDistrict = "TP. Gia Nghĩa", IdCity = 20 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-02", NameDistrict = "Huyện Cư Jút", IdCity = 20 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-03", NameDistrict = "Huyện Đắk Glong", IdCity = 20 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-04", NameDistrict = "Huyện Đắk Mil", IdCity = 20 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-05", NameDistrict = "Huyện Đắk R’lấp", IdCity = 20 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-06", NameDistrict = "Huyện Đắk Song", IdCity = 20 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-07", NameDistrict = "Huyện Krông Nô", IdCity = 20 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-08", NameDistrict = "Huyện Tuy Đức", IdCity = 20 }
            );

            // Điện Biên (IdCity = 21)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DB-01", NameDistrict = "TP. Điện Biên Phủ", IdCity = 21 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DB-02", NameDistrict = "TX. Mường Lay", IdCity = 21 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DB-03", NameDistrict = "Huyện Điện Biên", IdCity = 21 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DB-04", NameDistrict = "Huyện Điện Biên Đông", IdCity = 21 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DB-05", NameDistrict = "Huyện Mường Ảng", IdCity = 21 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DB-06", NameDistrict = "Huyện Mường Chà", IdCity = 21 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DB-07", NameDistrict = "Huyện Mường Nhé", IdCity = 21 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DB-08", NameDistrict = "Huyện Nậm Pồ", IdCity = 21 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DB-09", NameDistrict = "Huyện Tủa Chùa", IdCity = 21 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DB-10", NameDistrict = "Huyện Tuần Giáo", IdCity = 21 }
            );

            // Đồng Nai (IdCity = 22)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-01", NameDistrict = "TP. Biên Hòa", IdCity = 22 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-02", NameDistrict = "TP. Long Khánh", IdCity = 22 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-03", NameDistrict = "Huyện Cẩm Mỹ", IdCity = 22 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-04", NameDistrict = "Huyện Định Quán", IdCity = 22 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-05", NameDistrict = "Huyện Long Thành", IdCity = 22 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-06", NameDistrict = "Huyện Nhơn Trạch", IdCity = 22 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-07", NameDistrict = "Huyện Tân Phú", IdCity = 22 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-08", NameDistrict = "Huyện Thống Nhất", IdCity = 22 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-09", NameDistrict = "Huyện Trảng Bom", IdCity = 22 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-10", NameDistrict = "Huyện Vĩnh Cửu", IdCity = 22 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DN-11", NameDistrict = "Huyện Xuân Lộc", IdCity = 22 }
            );

            // Đồng Tháp (IdCity = 23)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DT-01", NameDistrict = "TP. Cao Lãnh", IdCity = 23 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DT-02", NameDistrict = "TP. Sa Đéc", IdCity = 23 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DT-03", NameDistrict = "TX. Hồng Ngự", IdCity = 23 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DT-04", NameDistrict = "Huyện Cao Lãnh", IdCity = 23 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DT-05", NameDistrict = "Huyện Châu Thành", IdCity = 23 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DT-06", NameDistrict = "Huyện Hồng Ngự", IdCity = 23 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DT-07", NameDistrict = "Huyện Lai Vung", IdCity = 23 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DT-08", NameDistrict = "Huyện Lấp Vò", IdCity = 23 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DT-09", NameDistrict = "Huyện Tam Nông", IdCity = 23 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DT-10", NameDistrict = "Huyện Tân Hồng", IdCity = 23 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DT-11", NameDistrict = "Huyện Thanh Bình", IdCity = 23 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-DT-12", NameDistrict = "Huyện Tháp Mười", IdCity = 23 }
            );

            // Gia Lai (IdCity = 24)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-GL-01", NameDistrict = "TP. Pleiku", IdCity = 24 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-GL-02", NameDistrict = "TX. An Khê", IdCity = 24 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-GL-03", NameDistrict = "TX. Ayun Pa", IdCity = 24 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-GL-04", NameDistrict = "Huyện Chư Păh", IdCity = 24 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-GL-05", NameDistrict = "Huyện Chư Prông", IdCity = 24 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-GL-06", NameDistrict = "Huyện Chư Sê", IdCity = 24 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-GL-07", NameDistrict = "Huyện Chư Pưh", IdCity = 24 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-GL-08", NameDistrict = "Huyện Đăk Đoa", IdCity = 24 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-GL-09", NameDistrict = "Huyện Đăk Pơ", IdCity = 24 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-GL-10", NameDistrict = "Huyện Đức Cơ", IdCity = 24 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-GL-11", NameDistrict = "Huyện Ia Grai", IdCity = 24 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-GL-12", NameDistrict = "Huyện Ia Pa", IdCity = 24 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-GL-13", NameDistrict = "Huyện KBang", IdCity = 24 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-GL-14", NameDistrict = "Huyện Kông Chro", IdCity = 24 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-GL-15", NameDistrict = "Huyện Krông Pa", IdCity = 24 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-GL-16", NameDistrict = "Huyện Mang Yang", IdCity = 24 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-GL-17", NameDistrict = "Huyện Phú Thiện", IdCity = 24 }
            );

            // Hà Giang (IdCity = 25)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-01", NameDistrict = "TP. Hà Giang", IdCity = 25 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-02", NameDistrict = "Huyện Bắc Mê", IdCity = 25 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-03", NameDistrict = "Huyện Bắc Quang", IdCity = 25 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-04", NameDistrict = "Huyện Đồng Văn", IdCity = 25 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-05", NameDistrict = "Huyện Hoàng Su Phì", IdCity = 25 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-06", NameDistrict = "Huyện Mèo Vạc", IdCity = 25 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-07", NameDistrict = "Huyện Quản Bạ", IdCity = 25 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-08", NameDistrict = "Huyện Quang Bình", IdCity = 25 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-09", NameDistrict = "Huyện Vị Xuyên", IdCity = 25 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-10", NameDistrict = "Huyện Xín Mần", IdCity = 25 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-11", NameDistrict = "Huyện Yên Minh", IdCity = 25 }
            );

            // Hà Nam (IdCity = 26)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-01", NameDistrict = "TP. Phủ Lý", IdCity = 26 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-02", NameDistrict = "TX. Duy Tiên", IdCity = 26 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-03", NameDistrict = "Huyện Bình Lục", IdCity = 26 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-04", NameDistrict = "Huyện Kim Bảng", IdCity = 26 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-05", NameDistrict = "Huyện Lý Nhân", IdCity = 26 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HN-06", NameDistrict = "Huyện Thanh Liêm", IdCity = 26 }
            );

            // Hà Tĩnh (IdCity = 27)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HT-01", NameDistrict = "TP. Hà Tĩnh", IdCity = 27 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HT-02", NameDistrict = "TX. Hồng Lĩnh", IdCity = 27 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HT-03", NameDistrict = "TX. Kỳ Anh", IdCity = 27 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HT-04", NameDistrict = "Huyện Cẩm Xuyên", IdCity = 27 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HT-05", NameDistrict = "Huyện Can Lộc", IdCity = 27 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HT-06", NameDistrict = "Huyện Đức Thọ", IdCity = 27 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HT-07", NameDistrict = "Huyện Hương Khê", IdCity = 27 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HT-08", NameDistrict = "Huyện Hương Sơn", IdCity = 27 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HT-09", NameDistrict = "Huyện Kỳ Anh", IdCity = 27 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HT-10", NameDistrict = "Huyện Lộc Hà", IdCity = 27 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HT-11", NameDistrict = "Huyện Nghi Xuân", IdCity = 27 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HT-12", NameDistrict = "Huyện Thạch Hà", IdCity = 27 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HT-13", NameDistrict = "Huyện Vũ Quang", IdCity = 27 }
            );

            // Hải Dương (IdCity = 28)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HD-01", NameDistrict = "TP. Hải Dương", IdCity = 28 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HD-02", NameDistrict = "TX. Chí Linh", IdCity = 28 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HD-03", NameDistrict = "Huyện Bình Giang", IdCity = 28 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HD-04", NameDistrict = "Huyện Cẩm Giàng", IdCity = 28 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HD-05", NameDistrict = "Huyện Gia Lộc", IdCity = 28 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HD-06", NameDistrict = "Huyện Kim Thành", IdCity = 28 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HD-07", NameDistrict = "Huyện Nam Sách", IdCity = 28 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HD-08", NameDistrict = "Huyện Ninh Giang", IdCity = 28 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HD-09", NameDistrict = "Huyện Thanh Hà", IdCity = 28 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HD-10", NameDistrict = "Huyện Thanh Miện", IdCity = 28 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HD-11", NameDistrict = "Huyện Tứ Kỳ", IdCity = 28 }
            );

            // Hậu Giang (IdCity = 29)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-01", NameDistrict = "TP. Vị Thanh", IdCity = 29 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-02", NameDistrict = "TX. Long Mỹ", IdCity = 29 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-03", NameDistrict = "TX. Ngã Bảy", IdCity = 29 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-04", NameDistrict = "Huyện Châu Thành", IdCity = 29 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-05", NameDistrict = "Huyện Châu Thành A", IdCity = 29 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-06", NameDistrict = "Huyện Long Mỹ", IdCity = 29 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-07", NameDistrict = "Huyện Phụng Hiệp", IdCity = 29 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HG-08", NameDistrict = "Huyện Vị Thủy", IdCity = 29 }
            );

            // Hòa Bình (IdCity = 30)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HB-01", NameDistrict = "TP. Hòa Bình", IdCity = 30 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HB-02", NameDistrict = "Huyện Cao Phong", IdCity = 30 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HB-03", NameDistrict = "Huyện Đà Bắc", IdCity = 30 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HB-04", NameDistrict = "Huyện Kim Bôi", IdCity = 30 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HB-05", NameDistrict = "Huyện Kỳ Sơn", IdCity = 30 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HB-06", NameDistrict = "Huyện Lạc Sơn", IdCity = 30 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HB-07", NameDistrict = "Huyện Lạc Thủy", IdCity = 30 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HB-08", NameDistrict = "Huyện Lương Sơn", IdCity = 30 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HB-09", NameDistrict = "Huyện Mai Châu", IdCity = 30 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HB-10", NameDistrict = "Huyện Tân Lạc", IdCity = 30 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HB-11", NameDistrict = "Huyện Yên Thủy", IdCity = 30 }
            );

            // Hưng Yên (IdCity = 31)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HY-01", NameDistrict = "TP. Hưng Yên", IdCity = 31 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HY-02", NameDistrict = "Huyện Ân Thi", IdCity = 31 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HY-03", NameDistrict = "Huyện Khoái Châu", IdCity = 31 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HY-04", NameDistrict = "Huyện Kim Động", IdCity = 31 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HY-05", NameDistrict = "Huyện Mỹ Hào", IdCity = 31 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HY-06", NameDistrict = "Huyện Phù Cừ", IdCity = 31 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HY-07", NameDistrict = "Huyện Tiên Lữ", IdCity = 31 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HY-08", NameDistrict = "Huyện Văn Giang", IdCity = 31 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HY-09", NameDistrict = "Huyện Văn Lâm", IdCity = 31 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-HY-10", NameDistrict = "Huyện Yên Mỹ", IdCity = 31 }
            );

            // Khánh Hòa (IdCity = 32)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KH-01", NameDistrict = "TP. Nha Trang", IdCity = 32 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KH-02", NameDistrict = "TP. Cam Ranh", IdCity = 32 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KH-03", NameDistrict = "TX. Ninh Hòa", IdCity = 32 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KH-04", NameDistrict = "Huyện Cam Lâm", IdCity = 32 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KH-05", NameDistrict = "Huyện Diên Khánh", IdCity = 32 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KH-06", NameDistrict = "Huyện Khánh Sơn", IdCity = 32 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KH-07", NameDistrict = "Huyện Khánh Vĩnh", IdCity = 32 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KH-08", NameDistrict = "Huyện Trường Sa", IdCity = 32 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KH-09", NameDistrict = "Huyện Vạn Ninh", IdCity = 32 }
            );

            // Kiên Giang (IdCity = 33)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KG-01", NameDistrict = "TP. Rạch Giá", IdCity = 33 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KG-02", NameDistrict = "TP. Hà Tiên", IdCity = 33 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KG-03", NameDistrict = "Huyện An Biên", IdCity = 33 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KG-04", NameDistrict = "Huyện An Minh", IdCity = 33 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KG-05", NameDistrict = "Huyện Châu Thành", IdCity = 33 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KG-06", NameDistrict = "Huyện Giang Thành", IdCity = 33 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KG-07", NameDistrict = "Huyện Giồng Riềng", IdCity = 33 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KG-08", NameDistrict = "Huyện Gò Quao", IdCity = 33 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KG-09", NameDistrict = "Huyện Hòn Đất", IdCity = 33 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KG-10", NameDistrict = "Huyện Kiên Hải", IdCity = 33 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KG-11", NameDistrict = "Huyện Kiên Lương", IdCity = 33 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KG-12", NameDistrict = "Huyện Phú Quốc", IdCity = 33 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KG-13", NameDistrict = "Huyện Tân Hiệp", IdCity = 33 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KG-14", NameDistrict = "Huyện U Minh Thượng", IdCity = 33 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KG-15", NameDistrict = "Huyện Vĩnh Thuận", IdCity = 33 }
            );

            // Kon Tum (IdCity = 34) - Tiếp tục từ đây
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KT-01", NameDistrict = "TP. Kon Tum", IdCity = 34 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KT-02", NameDistrict = "Huyện Đắk Glei", IdCity = 34 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KT-03", NameDistrict = "Huyện Đắk Hà", IdCity = 34 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KT-04", NameDistrict = "Huyện Đắk Tô", IdCity = 34 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KT-05", NameDistrict = "Huyện Kon Plông", IdCity = 34 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KT-06", NameDistrict = "Huyện Kon Rẫy", IdCity = 34 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KT-07", NameDistrict = "Huyện Ngọc Hồi", IdCity = 34 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KT-08", NameDistrict = "Huyện Sa Thầy", IdCity = 34 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-KT-09", NameDistrict = "Huyện Tu Mơ Rông", IdCity = 34 }
            );

            // Lai Châu (IdCity = 35)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LC-01", NameDistrict = "TP. Lai Châu", IdCity = 35 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LC-02", NameDistrict = "Huyện Mường Tè", IdCity = 35 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LC-03", NameDistrict = "Huyện Nậm Nhùn", IdCity = 35 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LC-04", NameDistrict = "Huyện Phong Thổ", IdCity = 35 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LC-05", NameDistrict = "Huyện Sìn Hồ", IdCity = 35 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LC-06", NameDistrict = "Huyện Tam Đường", IdCity = 35 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LC-07", NameDistrict = "Huyện Tân Uyên", IdCity = 35 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LC-08", NameDistrict = "Huyện Than Uyên", IdCity = 35 }
            );

            // Lâm Đồng (IdCity = 36)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LD-01", NameDistrict = "TP. Đà Lạt", IdCity = 36 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LD-02", NameDistrict = "TP. Bảo Lộc", IdCity = 36 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LD-03", NameDistrict = "Huyện Bảo Lâm", IdCity = 36 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LD-04", NameDistrict = "Huyện Cát Tiên", IdCity = 36 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LD-05", NameDistrict = "Huyện Đạ Huoai", IdCity = 36 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LD-06", NameDistrict = "Huyện Đạ Tẻh", IdCity = 36 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LD-07", NameDistrict = "Huyện Đam Rông", IdCity = 36 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LD-08", NameDistrict = "Huyện Di Linh", IdCity = 36 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LD-09", NameDistrict = "Huyện Đơn Dương", IdCity = 36 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LD-10", NameDistrict = "Huyện Đức Trọng", IdCity = 36 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LD-11", NameDistrict = "Huyện Lạc Dương", IdCity = 36 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LD-12", NameDistrict = "Huyện Lâm Hà", IdCity = 36 }
            );

            // Lạng Sơn (IdCity = 37)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LS-01", NameDistrict = "TP. Lạng Sơn", IdCity = 37 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LS-02", NameDistrict = "Huyện Bắc Sơn", IdCity = 37 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LS-03", NameDistrict = "Huyện Bình Gia", IdCity = 37 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LS-04", NameDistrict = "Huyện Cao Lộc", IdCity = 37 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LS-05", NameDistrict = "Huyện Chi Lăng", IdCity = 37 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LS-06", NameDistrict = "Huyện Đình Lập", IdCity = 37 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LS-07", NameDistrict = "Huyện Hữu Lũng", IdCity = 37 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LS-08", NameDistrict = "Huyện Lộc Bình", IdCity = 37 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LS-09", NameDistrict = "Huyện Tràng Định", IdCity = 37 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LS-10", NameDistrict = "Huyện Văn Lãng", IdCity = 37 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LS-11", NameDistrict = "Huyện Văn Quan", IdCity = 37 }
            );

            // Lào Cai (IdCity = 38)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LC-01", NameDistrict = "TP. Lào Cai", IdCity = 38 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LC-02", NameDistrict = "TX. Sa Pa", IdCity = 38 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LC-03", NameDistrict = "Huyện Bắc Hà", IdCity = 38 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LC-04", NameDistrict = "Huyện Bảo Thắng", IdCity = 38 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LC-05", NameDistrict = "Huyện Bảo Yên", IdCity = 38 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LC-06", NameDistrict = "Huyện Bát Xát", IdCity = 38 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LC-07", NameDistrict = "Huyện Mường Khương", IdCity = 38 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LC-08", NameDistrict = "Huyện Si Ma Cai", IdCity = 38 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LC-09", NameDistrict = "Huyện Văn Bàn", IdCity = 38 }
            );

            // Long An (IdCity = 39)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LA-01", NameDistrict = "TP. Tân An", IdCity = 39 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LA-02", NameDistrict = "TX. Kiến Tường", IdCity = 39 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LA-03", NameDistrict = "Huyện Bến Lức", IdCity = 39 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LA-04", NameDistrict = "Huyện Cần Đước", IdCity = 39 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LA-05", NameDistrict = "Huyện Cần Giuộc", IdCity = 39 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LA-06", NameDistrict = "Huyện Châu Thành", IdCity = 39 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LA-07", NameDistrict = "Huyện Đức Hòa", IdCity = 39 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LA-08", NameDistrict = "Huyện Đức Huệ", IdCity = 39 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LA-09", NameDistrict = "Huyện Mộc Hóa", IdCity = 39 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LA-10", NameDistrict = "Huyện Tân Hưng", IdCity = 39 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LA-11", NameDistrict = "Huyện Tân Thạnh", IdCity = 39 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LA-12", NameDistrict = "Huyện Tân Trụ", IdCity = 39 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LA-13", NameDistrict = "Huyện Thạnh Hóa", IdCity = 39 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LA-14", NameDistrict = "Huyện Thủ Thừa", IdCity = 39 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-LA-15", NameDistrict = "Huyện Vĩnh Hưng", IdCity = 39 }
            );

            // Nam Định (IdCity = 40)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ND-01", NameDistrict = "Mỹ Lộc", IdCity = 40 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ND-02", NameDistrict = "Vụ Bản", IdCity = 40 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ND-03", NameDistrict = "Nam Trực", IdCity = 40 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ND-04", NameDistrict = "Trực Ninh", IdCity = 40 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ND-05", NameDistrict = "Xuân Trường", IdCity = 40 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ND-06", NameDistrict = "Giao Thủy", IdCity = 40 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ND-07", NameDistrict = "Hải Hậu", IdCity = 40 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ND-08", NameDistrict = "Nghĩa Hưng", IdCity = 40 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ND-09", NameDistrict = "Ý Yên", IdCity = 40 }
                // Loại bỏ "Kiến Xương" và "Tiền Hải" vì chúng thuộc Thái Bình (IdCity = 54)
            );

            // Nghệ An (IdCity = 41)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-01", NameDistrict = "TP Vinh", IdCity = 41 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-02", NameDistrict = "Thị xã Cửa Lò", IdCity = 41 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-03", NameDistrict = "Thị xã Thái Hòa", IdCity = 41 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-04", NameDistrict = "Quỳnh Lưu", IdCity = 41 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-05", NameDistrict = "Diễn Châu", IdCity = 41 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-06", NameDistrict = "Nghi Lộc", IdCity = 41 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-07", NameDistrict = "Yên Thành", IdCity = 41 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-08", NameDistrict = "Hưng Nguyên", IdCity = 41 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-09", NameDistrict = "Quỳ Hợp", IdCity = 41 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-10", NameDistrict = "Quỳ Châu", IdCity = 41 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-11", NameDistrict = "Tân Kỳ", IdCity = 41 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-12", NameDistrict = "Đô Lương", IdCity = 41 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-13", NameDistrict = "Anh Sơn", IdCity = 41 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-14", NameDistrict = "Con Cuông", IdCity = 41 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-15", NameDistrict = "Tương Dương", IdCity = 41 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-16", NameDistrict = "Kỳ Sơn", IdCity = 41 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-17", NameDistrict = "Nam Đàn", IdCity = 41 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NA-18", NameDistrict = "Thanh Chương", IdCity = 41 }
            );

            // Ninh Bình (IdCity = 42)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NB-01", NameDistrict = "Gia Viễn", IdCity = 42 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NB-02", NameDistrict = "TP Hoa Lư", IdCity = 42 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NB-03", NameDistrict = "Kim Sơn", IdCity = 42 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NB-04", NameDistrict = "Nho Quan", IdCity = 42 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NB-05", NameDistrict = "TP Tam Điệp", IdCity = 42 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NB-06", NameDistrict = "Yên Khánh", IdCity = 42 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NB-07", NameDistrict = "Yên Mô", IdCity = 42 }
            );

            // Ninh Thuận (IdCity = 43)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NT-01", NameDistrict = "Bác Ái", IdCity = 43 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NT-02", NameDistrict = "Ninh Hải", IdCity = 43 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NT-03", NameDistrict = "Ninh Phước", IdCity = 43 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NT-04", NameDistrict = "Thuận Bắc", IdCity = 43 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NT-05", NameDistrict = "Thuận Nam", IdCity = 43 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NT-06", NameDistrict = "Phan Rang-Tháp Chàm", IdCity = 43 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-NT-07", NameDistrict = "Ninh Sơn", IdCity = 43 }
            );

            // Phú Thọ (IdCity = 44)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PT-01", NameDistrict = "Cẩm Khê", IdCity = 44 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PT-02", NameDistrict = "Đoan Hùng", IdCity = 44 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PT-03", NameDistrict = "Hạ Hòa", IdCity = 44 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PT-04", NameDistrict = "Thanh Ba", IdCity = 44 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PT-05", NameDistrict = "Phù Ninh", IdCity = 44 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PT-06", NameDistrict = "Tam Nông", IdCity = 44 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PT-07", NameDistrict = "Tân Sơn", IdCity = 44 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PT-08", NameDistrict = "Thanh Sơn", IdCity = 44 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PT-09", NameDistrict = "Thanh Thủy", IdCity = 44 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PT-10", NameDistrict = "Yên Lập", IdCity = 44 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PT-11", NameDistrict = "Lâm Thao", IdCity = 44 }
            );

            // Phú Yên (IdCity = 45)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PY-01", NameDistrict = "Tuy An", IdCity = 45 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PY-02", NameDistrict = "Sơn Hòa", IdCity = 45 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PY-03", NameDistrict = "TP Tuy Hòa", IdCity = 45 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PY-04", NameDistrict = "Phú Hòa", IdCity = 45 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PY-05", NameDistrict = "Thị xã Sông Cầu", IdCity = 45 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PY-06", NameDistrict = "Đồng Xuân", IdCity = 45 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PY-07", NameDistrict = "Sông Hinh", IdCity = 45 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PY-08", NameDistrict = "Tây Hòa", IdCity = 45 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-PY-09", NameDistrict = "Thị xã Đông Hòa", IdCity = 45 }
            );

            // Quảng Bình (IdCity = 46)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QB-01", NameDistrict = "Quảng Ninh", IdCity = 46 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QB-02", NameDistrict = "TP Đồng Hới", IdCity = 46 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QB-03", NameDistrict = "Lệ Thủy", IdCity = 46 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QB-04", NameDistrict = "Bố Trạch", IdCity = 46 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QB-05", NameDistrict = "Thị xã Ba Đồn", IdCity = 46 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QB-06", NameDistrict = "Phong Nha", IdCity = 46 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QB-07", NameDistrict = "Minh Hóa", IdCity = 46 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QB-08", NameDistrict = "Tuyên Hóa", IdCity = 46 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QB-09", NameDistrict = "Quảng Trạch", IdCity = 46 }
            );

            // Quảng Nam (IdCity = 47)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-01", NameDistrict = "TP Tam Kỳ", IdCity = 47 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-02", NameDistrict = "TP Hội An", IdCity = 47 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-03", NameDistrict = "Tiên Phước", IdCity = 47 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-04", NameDistrict = "Quế Sơn", IdCity = 47 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-05", NameDistrict = "Duy Xuyên", IdCity = 47 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-06", NameDistrict = "Thăng Bình", IdCity = 47 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-07", NameDistrict = "Đại Lộc", IdCity = 47 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-08", NameDistrict = "Đông Giang", IdCity = 47 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-09", NameDistrict = "Tây Giang", IdCity = 47 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-10", NameDistrict = "Trà My", IdCity = 47 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-11", NameDistrict = "Phước Sơn", IdCity = 47 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-12", NameDistrict = "Núi Thành", IdCity = 47 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-13", NameDistrict = "Phú Ninh", IdCity = 47 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-14", NameDistrict = "Hiệp Đức", IdCity = 47 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-15", NameDistrict = "Thị xã Điện Bàn", IdCity = 47 }
            );

            // Quảng Ngãi (IdCity = 48)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QNG-01", NameDistrict = "TP Quảng Ngãi", IdCity = 48 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QNG-02", NameDistrict = "Lý Sơn", IdCity = 48 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QNG-03", NameDistrict = "Ba Tơ", IdCity = 48 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QNG-04", NameDistrict = "Thị Xã Đức Phổ", IdCity = 48 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QNG-05", NameDistrict = "Bình Sơn", IdCity = 48 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QNG-06", NameDistrict = "Minh Long", IdCity = 48 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QNG-07", NameDistrict = "Mộ Đức", IdCity = 48 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QNG-08", NameDistrict = "Nghĩa Hành", IdCity = 48 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QNG-09", NameDistrict = "Sơn Hà", IdCity = 48 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QNG-10", NameDistrict = "Sơn Tây", IdCity = 48 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QNG-11", NameDistrict = "Sơn Tịnh", IdCity = 48 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QNG-12", NameDistrict = "Trà Bồng", IdCity = 48 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QNG-13", NameDistrict = "Tư Nghĩa", IdCity = 48 }
            );

            // Quảng Ninh (IdCity = 49)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-01", NameDistrict = "Vịnh Hạ Long", IdCity = 49 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-02", NameDistrict = "Móng Cái", IdCity = 49 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-03", NameDistrict = "Yên Hưng", IdCity = 49 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-04", NameDistrict = "Đình Lập", IdCity = 49 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-05", NameDistrict = "Đông Triều", IdCity = 49 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-06", NameDistrict = "Bình Liêu", IdCity = 49 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-07", NameDistrict = "Tiên Yên", IdCity = 49 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-08", NameDistrict = "Ba Chẽ", IdCity = 49 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-09", NameDistrict = "Hoành Bồ", IdCity = 49 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-10", NameDistrict = "Cẩm Phả", IdCity = 49 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-11", NameDistrict = "Đầm Hà", IdCity = 49 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-12", NameDistrict = "Hà Cối", IdCity = 49 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-13", NameDistrict = "Thị xã Hồng Gai", IdCity = 49 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-14", NameDistrict = "Thị xã Cẩm Phả", IdCity = 49 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QN-15", NameDistrict = "Thị xã Uông Bí", IdCity = 49 }
            );

            // Quảng Trị (IdCity = 50)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QT-01", NameDistrict = "TP Đông Hà", IdCity = 50 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QT-02", NameDistrict = "Hải Lăng", IdCity = 50 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QT-03", NameDistrict = "Triệu Phong", IdCity = 50 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QT-04", NameDistrict = "Vĩnh Linh", IdCity = 50 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QT-05", NameDistrict = "Cam Lộ", IdCity = 50 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QT-06", NameDistrict = "Hướng Hóa", IdCity = 50 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QT-07", NameDistrict = "Đakrông", IdCity = 50 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QT-08", NameDistrict = "Cồn Cỏ", IdCity = 50 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-QT-09", NameDistrict = "Thị xã Quảng Trị", IdCity = 50 }
            );

            // Sóc Trăng (IdCity = 51)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ST-01", NameDistrict = "TP Sóc Trăng", IdCity = 51 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ST-02", NameDistrict = "Ngã Năm", IdCity = 51 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ST-03", NameDistrict = "Mỹ Tú", IdCity = 51 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ST-04", NameDistrict = "Mỹ Xuyên", IdCity = 51 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ST-05", NameDistrict = "Long Phú", IdCity = 51 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ST-06", NameDistrict = "Châu Thành", IdCity = 51 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ST-07", NameDistrict = "Kế Sách", IdCity = 51 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ST-08", NameDistrict = "Cù Lao Dung", IdCity = 51 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-ST-09", NameDistrict = "Vĩnh Châu", IdCity = 51 }
            );

            // Sơn La (IdCity = 52)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-SL-01", NameDistrict = "TP Sơn La", IdCity = 52 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-SL-02", NameDistrict = "Mộc Châu", IdCity = 52 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-SL-03", NameDistrict = "Bắc Yên", IdCity = 52 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-SL-04", NameDistrict = "Mai Sơn", IdCity = 52 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-SL-05", NameDistrict = "Sông Mã", IdCity = 52 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-SL-06", NameDistrict = "Sốp Cộp", IdCity = 52 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-SL-07", NameDistrict = "Phù Yên", IdCity = 52 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-SL-08", NameDistrict = "Yên Sơn", IdCity = 52 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-SL-09", NameDistrict = "Thuận Châu", IdCity = 52 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-SL-10", NameDistrict = "Mường La", IdCity = 52 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-SL-11", NameDistrict = "Quỳnh Nhai", IdCity = 52 }
            );

            // Tây Ninh (IdCity = 53)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TN-01", NameDistrict = "Thị xã Tây Ninh", IdCity = 53 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TN-02", NameDistrict = "Hòa Thành", IdCity = 53 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TN-03", NameDistrict = "Châu Thành", IdCity = 53 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TN-04", NameDistrict = "Tân Biên", IdCity = 53 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TN-05", NameDistrict = "Tân Châu", IdCity = 53 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TN-06", NameDistrict = "Dương Minh Châu", IdCity = 53 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TN-07", NameDistrict = "Bến Cầu", IdCity = 53 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TN-08", NameDistrict = "Gò Dầu", IdCity = 53 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TN-09", NameDistrict = "Trảng Bàng", IdCity = 53 }
            );

            // Thái Bình (IdCity = 54)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TB-01", NameDistrict = "TP Thái Bình", IdCity = 54 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TB-02", NameDistrict = "Hưng Hà", IdCity = 54 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TB-03", NameDistrict = "Quỳnh Phụ", IdCity = 54 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TB-04", NameDistrict = "Vũ Thư", IdCity = 54 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TB-05", NameDistrict = "Đông Hưng", IdCity = 54 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TB-06", NameDistrict = "Tiền Hải", IdCity = 54 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TB-07", NameDistrict = "Kiến Xương", IdCity = 54 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TB-08", NameDistrict = "Thái Thụy", IdCity = 54 }
            );

            // Thái Nguyên (IdCity = 55)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TN-01", NameDistrict = "TP Thái Nguyên", IdCity = 55 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TN-02", NameDistrict = "TP Sông Công", IdCity = 55 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TN-03", NameDistrict = "TP Phổ Yên", IdCity = 55 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TN-04", NameDistrict = "Đại Từ", IdCity = 55 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TN-05", NameDistrict = "Phú Lương", IdCity = 55 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TN-06", NameDistrict = "Võ Nhai", IdCity = 55 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TN-07", NameDistrict = "Đông Hỷ", IdCity = 55 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TN-08", NameDistrict = "Phú Bình", IdCity = 55 }
            );

            // Thanh Hóa (IdCity = 56)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-01", NameDistrict = "Thị xã Thanh Hóa", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-02", NameDistrict = "Bá Thước", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-03", NameDistrict = "Cẩm Thủy", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-04", NameDistrict = "Đông Sơn", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-05", NameDistrict = "Hà Trung", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-06", NameDistrict = "Hậu Lộc", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-07", NameDistrict = "Hoằng Hóa", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-08", NameDistrict = "Lang Chánh", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-09", NameDistrict = "Nga Sơn", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-10", NameDistrict = "Ngọc Lặc", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-11", NameDistrict = "Như Xuân", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-12", NameDistrict = "Nông Cống", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-13", NameDistrict = "Quan Hóa", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-14", NameDistrict = "Quảng Xương", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-15", NameDistrict = "Thạch Thành", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-16", NameDistrict = "Thọ Xuân", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-17", NameDistrict = "Thiệu Hóa", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-18", NameDistrict = "Thường Xuân", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-19", NameDistrict = "Tĩnh Gia", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-20", NameDistrict = "Vĩnh Lộc", IdCity = 56 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TH-21", NameDistrict = "Yên Định", IdCity = 56 }
            );

            // Thừa Thiên Huế (IdCity = 57)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TTH-01", NameDistrict = "TP Huế", IdCity = 57 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TTH-02", NameDistrict = "Phú Vang", IdCity = 57 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TTH-03", NameDistrict = "Hương Trà", IdCity = 57 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TTH-04", NameDistrict = "Hương Thủy", IdCity = 57 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TTH-05", NameDistrict = "Nam Đông", IdCity = 57 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TTH-06", NameDistrict = "Phong Điền", IdCity = 57 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TTH-07", NameDistrict = "Quảng Điền", IdCity = 57 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TTH-08", NameDistrict = "A Lưới", IdCity = 57 }
            );

            // Tiền Giang (IdCity = 58)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TG-01", NameDistrict = "Thị xã Gò Công", IdCity = 58 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TG-02", NameDistrict = "Thị xã Cai Lậy", IdCity = 58 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TG-03", NameDistrict = "Gò Công Đông", IdCity = 58 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TG-04", NameDistrict = "Gò Công Tây", IdCity = 58 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TG-05", NameDistrict = "Tiền Giang", IdCity = 58 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TG-06", NameDistrict = "TP Mỹ Tho", IdCity = 58 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TG-07", NameDistrict = "Tân Phú Đông", IdCity = 58 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TG-08", NameDistrict = "Thiên Giang", IdCity = 58 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TG-09", NameDistrict = "Cái Bè", IdCity = 58 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TG-10", NameDistrict = "Châu Thành", IdCity = 58 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TG-11", NameDistrict = "Tân Túc", IdCity = 58 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TG-12", NameDistrict = "Chợ Gạo", IdCity = 58 }
            );

            // Trà Vinh (IdCity = 59)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TV-01", NameDistrict = "TP Trà Vinh", IdCity = 59 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TV-02", NameDistrict = "Càng Long", IdCity = 59 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TV-03", NameDistrict = "Châu Thành", IdCity = 59 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TV-04", NameDistrict = "Tiểu Cần", IdCity = 59 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TV-05", NameDistrict = "Cầu Kè", IdCity = 59 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TV-06", NameDistrict = "Trà Cú", IdCity = 59 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TV-07", NameDistrict = "Cầu Ngang", IdCity = 59 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TV-08", NameDistrict = "Duyên Hải", IdCity = 59 }
            );

            // Tuyên Quang (IdCity = 60)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TQ-01", NameDistrict = "TP Tuyên Quang", IdCity = 60 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TQ-02", NameDistrict = "Yên Sơn", IdCity = 60 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TQ-03", NameDistrict = "Chiêm Hóa", IdCity = 60 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TQ-04", NameDistrict = "Na Hang", IdCity = 60 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TQ-05", NameDistrict = "Hàm Yên", IdCity = 60 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TQ-06", NameDistrict = "Sơn Dương", IdCity = 60 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-TQ-07", NameDistrict = "Lâm Bình", IdCity = 60 }
            );

            // Vĩnh Long (IdCity = 61)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-VL-01", NameDistrict = "Vũng Liêm", IdCity = 61 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-VL-02", NameDistrict = "Trà Ôn", IdCity = 61 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-VL-03", NameDistrict = "Long Hồ", IdCity = 61 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-VL-04", NameDistrict = "Mang Thít", IdCity = 61 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-VL-05", NameDistrict = "Tam Bình", IdCity = 61 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-VL-06", NameDistrict = "Bình Tân", IdCity = 61 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-VL-07", NameDistrict = "Thành phố Vĩnh Long", IdCity = 61 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-VL-08", NameDistrict = "Thị xã Bình Minh", IdCity = 61 }
            );

            // Vĩnh Phúc (IdCity = 62)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-VP-01", NameDistrict = "Tam Dương", IdCity = 62 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-VP-02", NameDistrict = "Tam Đảo", IdCity = 62 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-VP-03", NameDistrict = "Bình Xuyên", IdCity = 62 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-VP-04", NameDistrict = "Lập Thạch", IdCity = 62 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-VP-05", NameDistrict = "Yên Lạc", IdCity = 62 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-VP-06", NameDistrict = "Vĩnh Tường", IdCity = 62 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-VP-07", NameDistrict = "Sông Lô", IdCity = 62 }
            );

            // Yên Bái (IdCity = 63)
            modelBuilder.Entity<District>().HasData(
                new District { IdDistrict = districtId++, DistrictCode = "DIST-YB-01", NameDistrict = "Yên Bái", IdCity = 63 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-YB-02", NameDistrict = "Trấn Yên", IdCity = 63 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-YB-03", NameDistrict = "Văn Chấn", IdCity = 63 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-YB-04", NameDistrict = "Văn Yên", IdCity = 63 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-YB-05", NameDistrict = "Mù Cang Chải", IdCity = 63 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-YB-06", NameDistrict = "Đà Sơn", IdCity = 63 },
                new District { IdDistrict = districtId++, DistrictCode = "DIST-YB-07", NameDistrict = "Trạm Tấu", IdCity = 63 }
            );

            // Cấu hình khóa chính
            modelBuilder.Entity<Bill>().HasKey(b => b.IdBill);
            modelBuilder.Entity<Brand>().HasKey(b => b.IdBrand);
            modelBuilder.Entity<City>().HasKey(c => c.IdCity);
            modelBuilder.Entity<Coach>().HasKey(c => c.IdCoach);
            modelBuilder.Entity<District>().HasKey(d => d.IdDistrict);
            modelBuilder.Entity<DropOff>().HasKey(d => d.IdDropOff);
            modelBuilder.Entity<Employee>().HasKey(e => e.IdEmployee);
            modelBuilder.Entity<Feedback>().HasKey(f => f.IdFeedback);
            modelBuilder.Entity<News>().HasKey(n => n.IdNews);
            modelBuilder.Entity<Passenger>().HasKey(p => p.IdPassenger);
            modelBuilder.Entity<Pickup>().HasKey(p => p.IdPickup);
            modelBuilder.Entity<Position>().HasKey(p => p.IdPos);
            modelBuilder.Entity<Price>().HasKey(p => p.IdPrice);
            modelBuilder.Entity<RegistForm>().HasKey(r => r.IdRegist);
            modelBuilder.Entity<BusRoute>().HasKey(r => r.IdRoute);
            modelBuilder.Entity<RouteStop>().HasKey(rs => rs.IdStop);
            modelBuilder.Entity<Seat>().HasKey(s => s.IdSeat);
            modelBuilder.Entity<Ticket>().HasKey(t => t.IdTicket);
            modelBuilder.Entity<TypeNews>().HasKey(tn => tn.IdTypeNews);
            modelBuilder.Entity<VehicleType>().HasKey(vt => vt.IdType);
            modelBuilder.Entity<ScheduleDetails>().HasKey(sd => sd.IdSchedule);
            modelBuilder.Entity<ChatRoom>().HasKey(cr => cr.Id);
            modelBuilder.Entity<ChatMessage>().HasKey(cm => cm.Id);

            // Cấu hình mối quan hệ
            // Brand và Coach (1-N)
            modelBuilder.Entity<Brand>()
                .HasMany(b => b.Coaches)
                .WithOne(c => c.Brand)
                .HasForeignKey(c => c.IdBrand)
                .OnDelete(DeleteBehavior.Cascade);

            // Brand và ApplicationUser
            modelBuilder.Entity<Brand>()
                .HasOne(b => b.ApplicationUser)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Brand và RegistForm (1:1)
            modelBuilder.Entity<Brand>()
                .HasOne(b => b.RegistForm)
                .WithOne(r => r.Brand)
                .HasForeignKey<Brand>(b => b.RegistFormId)
                .OnDelete(DeleteBehavior.NoAction);

            // Brand và Pickup (1:N)
            modelBuilder.Entity<Brand>()
                .HasMany(b => b.Pickups)
                .WithOne(p => p.Brand)
                .HasForeignKey(p => p.IdBrand)
                .OnDelete(DeleteBehavior.Cascade);

            // Brand và DropOff (1:N)
            modelBuilder.Entity<Brand>()
                .HasMany(b => b.DropOffs)
                .WithOne(d => d.Brand)
                .HasForeignKey(d => d.IdBrand)
                .OnDelete(DeleteBehavior.Cascade);

            // BusRoute và RouteStop
            modelBuilder.Entity<RouteStop>()
                .HasOne(rs => rs.BusRoute)
                .WithMany(r => r.RouteStops)
                .HasForeignKey(rs => rs.IdRoute)
                .OnDelete(DeleteBehavior.NoAction);

            // BusRoute và Brand
            modelBuilder.Entity<BusRoute>()
                .HasOne(r => r.Brand)
                .WithMany()
                .HasForeignKey(r => r.IdBrand);

            // BusRoute và StartCity
            modelBuilder.Entity<BusRoute>()
                .HasOne(r => r.StartCity)
                .WithMany(c => c.StartRoutes)
                .HasForeignKey(r => r.IdStartCity)
                .OnDelete(DeleteBehavior.NoAction);

            // BusRoute và EndCity
            modelBuilder.Entity<BusRoute>()
                .HasOne(r => r.EndCity)
                .WithMany(c => c.EndRoutes)
                .HasForeignKey(r => r.IdEndCity)
                .OnDelete(DeleteBehavior.NoAction);

            // BusRoute và Pickup (1:N)
            modelBuilder.Entity<BusRoute>()
                .HasMany(r => r.Pickups)
                .WithOne(p => p.BusRoute)
                .HasForeignKey(p => p.IdRoute)
                .OnDelete(DeleteBehavior.NoAction);

            // BusRoute và DropOff (1:N)
            modelBuilder.Entity<BusRoute>()
                .HasMany(r => r.DropOffs)
                .WithOne(d => d.BusRoute)
                .HasForeignKey(d => d.IdRoute)
                .OnDelete(DeleteBehavior.NoAction);

            // VehicleType và Coach (1-N)
            modelBuilder.Entity<VehicleType>()
                .HasMany(vt => vt.Coaches)
                .WithOne(c => c.VehicleType)
                .HasForeignKey(c => c.IdType)
                .OnDelete(DeleteBehavior.NoAction);

            // Bill
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Passenger)
                .WithMany()
                .HasForeignKey(b => b.IdPassenger)
                .OnDelete(DeleteBehavior.NoAction);

            // District
            modelBuilder.Entity<District>()
                .HasOne(d => d.City)
                .WithMany(c => c.Districts)
                .HasForeignKey(d => d.IdCity)
                .OnDelete(DeleteBehavior.NoAction);

            // DropOff
            modelBuilder.Entity<DropOff>()
                .HasOne(d => d.City)
                .WithMany()
                .HasForeignKey(d => d.IdCity)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DropOff>()
                .HasOne(d => d.BusRoute)
                .WithMany(r => r.DropOffs)
                .HasForeignKey(d => d.IdRoute)
                .OnDelete(DeleteBehavior.NoAction);

            // Employee
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Brand)
                .WithMany()
                .HasForeignKey(e => e.IdBrand)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Position)
                .WithMany()
                .HasForeignKey(e => e.IdPos)
                .OnDelete(DeleteBehavior.NoAction);

            // Feedback
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Passenger)
                .WithMany()
                .HasForeignKey(f => f.IdPassenger)
                .OnDelete(DeleteBehavior.NoAction);

            // News
            modelBuilder.Entity<News>()
                .HasOne(n => n.TypeNews)
                .WithMany()
                .HasForeignKey(n => n.IdTypeNews)
                .OnDelete(DeleteBehavior.NoAction);

            // Pickup
            modelBuilder.Entity<Pickup>()
                .HasOne(p => p.City)
                .WithMany()
                .HasForeignKey(p => p.IdCity)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Pickup>()
                .HasOne(p => p.BusRoute)
                .WithMany(r => r.Pickups)
                .HasForeignKey(p => p.IdRoute)
                .OnDelete(DeleteBehavior.NoAction);

            // Price
            modelBuilder.Entity<Price>()
                .HasOne(p => p.BusRoute)
                .WithMany()
                .HasForeignKey(p => p.IdRoute)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Price>()
                .HasOne(p => p.RouteStopStart)
                .WithMany()
                .HasForeignKey(p => p.IdStopStart)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Price>()
                .HasOne(p => p.RouteStopEnd)
                .WithMany()
                .HasForeignKey(p => p.IdStopEnd)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Price>()
                .HasOne(p => p.Coach)
                .WithMany()
                .HasForeignKey(p => p.IdCoach)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Price>()
                .HasOne(p => p.ScheduleDetails)
                .WithMany(sd => sd.Prices)
                .HasForeignKey(p => p.IdSchedule)
                .OnDelete(DeleteBehavior.NoAction);

            // RegistForm
            modelBuilder.Entity<RegistForm>()
                .HasOne(r => r.Brand)
                .WithOne(b => b.RegistForm)
                .HasForeignKey<RegistForm>(r => r.IdBrand)
                .OnDelete(DeleteBehavior.NoAction);

            // RouteStop
            modelBuilder.Entity<RouteStop>()
                .HasOne(rs => rs.BusRoute)
                .WithMany(r => r.RouteStops)
                .HasForeignKey(rs => rs.IdRoute)
                .OnDelete(DeleteBehavior.NoAction);

            // ScheduleDetails
            modelBuilder.Entity<ScheduleDetails>()
                .HasOne(sd => sd.Coach)
                .WithMany()
                .HasForeignKey(sd => sd.IdCoach)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ScheduleDetails>()
                .HasOne(sd => sd.BusRoute)
                .WithMany()
                .HasForeignKey(sd => sd.IdRoute)
                .OnDelete(DeleteBehavior.NoAction);

            // Seat
            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Coach)
                .WithMany()
                .HasForeignKey(s => s.IdCoach)
                .OnDelete(DeleteBehavior.NoAction);

            // Ticket
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Seat)
                .WithMany()
                .HasForeignKey(t => t.IdSeat)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Price)
                .WithMany()
                .HasForeignKey(t => t.IdPrice)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Passenger)
                .WithMany()
                .HasForeignKey(t => t.IdPassenger)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Employee)
                .WithMany()
                .HasForeignKey(t => t.IdEmployee)
                .OnDelete(DeleteBehavior.NoAction);

            // ChatRoom và ChatMessage
            modelBuilder.Entity<ChatRoom>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.ChatRoom)
                .HasForeignKey(m => m.ChatRoomId)
                .OnDelete(DeleteBehavior.NoAction); // Thêm để tránh xóa cascade

            modelBuilder.Entity<ChatRoom>()
                .HasMany(c => c.Participants)
                .WithMany(u => u.ChatRooms);

            modelBuilder.Entity<ChatMessage>()
                .Property(m => m.SentDate)
                .HasDefaultValueSql("GETDATE()");

            // Thêm chỉ mục để cải thiện hiệu suất
            modelBuilder.Entity<ChatMessage>()
                .HasIndex(cm => cm.ChatRoomId);

            modelBuilder.Entity<ChatMessage>()
                .HasIndex(cm => cm.SenderId);

            modelBuilder.Entity<ChatMessage>()
                .HasIndex(cm => cm.ReceiverId);

            // Cấu hình mối quan hệ của ChatMessage với ApplicationUser
            modelBuilder.Entity<ChatMessage>()
                .HasOne(cm => cm.Sender)
                .WithMany()
                .HasForeignKey(cm => cm.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(cm => cm.Receiver)
                .WithMany()
                .HasForeignKey(cm => cm.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}