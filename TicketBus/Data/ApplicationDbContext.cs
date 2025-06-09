using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketBus.Models;

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
        public DbSet<ChatRequest> ChatRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding data for VehicleType
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

            // Seeding data for Cities
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

            // Configure primary keys
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

            // Configure relationships
            // Passenger and ApplicationUser
            modelBuilder.Entity<Passenger>()
                .HasOne(p => p.ApplicationUser)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            // Passenger and Bill (1:N)
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Passenger)
                .WithMany(p => p.Bills)
                .HasForeignKey(b => b.IdPassenger)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            // Bill and Ticket (1:N)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Bill)
                .WithMany(b => b.Tickets)
                .HasForeignKey(t => t.IdBill)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            // Brand and Coach (1:N)
            modelBuilder.Entity<Brand>()
                .HasMany(b => b.Coaches)
                .WithOne(c => c.Brand)
                .HasForeignKey(c => c.IdBrand)
                .OnDelete(DeleteBehavior.Cascade);

            // Brand and ApplicationUser
            modelBuilder.Entity<Brand>()
                .HasOne(b => b.ApplicationUser)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            // Brand and RegistForm (1:1)
            modelBuilder.Entity<Brand>()
                .HasOne(b => b.RegistForm)
                .WithOne(r => r.Brand)
                .HasForeignKey<Brand>(b => b.RegistFormId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            // Brand and Pickup (1:N)
            modelBuilder.Entity<Brand>()
                .HasMany(b => b.Pickups)
                .WithOne(p => p.Brand)
                .HasForeignKey(p => p.IdBrand)
                .OnDelete(DeleteBehavior.Cascade);

            // Brand and DropOff (1:N)
            modelBuilder.Entity<Brand>()
                .HasMany(b => b.DropOffs)
                .WithOne(d => d.Brand)
                .HasForeignKey(d => d.IdBrand)
                .OnDelete(DeleteBehavior.Cascade);

            // BusRoute and RouteStop
            modelBuilder.Entity<RouteStop>()
                .HasOne(rs => rs.BusRoute)
                .WithMany(r => r.RouteStops)
                .HasForeignKey(rs => rs.IdRoute)
                .OnDelete(DeleteBehavior.NoAction);

            // BusRoute and Brand
            modelBuilder.Entity<BusRoute>()
                .HasOne(r => r.Brand)
                .WithMany()
                .HasForeignKey(r => r.IdBrand)
                .OnDelete(DeleteBehavior.NoAction);

            // BusRoute and StartCity
            modelBuilder.Entity<BusRoute>()
                .HasOne(r => r.StartCity)
                .WithMany(c => c.StartRoutes)
                .HasForeignKey(r => r.IdStartCity)
                .OnDelete(DeleteBehavior.NoAction);

            // BusRoute and EndCity
            modelBuilder.Entity<BusRoute>()
                .HasOne(r => r.EndCity)
                .WithMany(c => c.EndRoutes)
                .HasForeignKey(r => r.IdEndCity)
                .OnDelete(DeleteBehavior.NoAction);

            // BusRoute and Pickup (1:N)
            modelBuilder.Entity<BusRoute>()
                .HasMany(r => r.Pickups)
                .WithOne(p => p.BusRoute)
                .HasForeignKey(p => p.IdRoute)
                .OnDelete(DeleteBehavior.NoAction);

            // BusRoute and DropOff (1:N)
            modelBuilder.Entity<BusRoute>()
                .HasMany(r => r.DropOffs)
                .WithOne(d => d.BusRoute)
                .HasForeignKey(d => d.IdRoute)
                .OnDelete(DeleteBehavior.NoAction);

            // VehicleType and Coach (1:N)
            modelBuilder.Entity<VehicleType>()
                .HasMany(vt => vt.Coaches)
                .WithOne(c => c.VehicleType)
                .HasForeignKey(c => c.IdType)
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
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder.Entity<Price>()
                .HasOne(p => p.ScheduleDetails)
                .WithMany(sd => sd.Prices)
                .HasForeignKey(p => p.IdSchedule)
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
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

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
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Price)
                .WithMany()
                .HasForeignKey(t => t.IdPrice)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Employee)
                .WithMany()
                .HasForeignKey(t => t.IdEmployee)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.BusRoute)
                .WithMany()
                .HasForeignKey(t => t.IdRoute)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            // Configure enum conversion for TicketState
            modelBuilder.Entity<Ticket>()
                .Property(t => t.State)
                .HasConversion<int>();

            // ChatRoom và ChatMessage
            modelBuilder.Entity<ChatRoom>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.ChatRoom)
                .HasForeignKey(m => m.ChatRoomId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ChatRoom>()
                .HasMany(c => c.Participants)
                .WithMany(u => u.ChatRooms);

            modelBuilder.Entity<ChatMessage>()
                .Property(m => m.SentDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<ChatMessage>()
                .HasIndex(cm => cm.ChatRoomId);

            modelBuilder.Entity<ChatMessage>()
                .HasIndex(cm => cm.SenderId);

            modelBuilder.Entity<ChatMessage>()
                .HasIndex(cm => cm.ReceiverId);

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

            // ChatRequest
            modelBuilder.Entity<ChatRequest>()
                .HasOne(cr => cr.Sender)
                .WithMany()
                .HasForeignKey(cr => cr.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChatRequest>()
                .HasOne(cr => cr.Receiver)
                .WithMany()
                .HasForeignKey(cr => cr.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ChatRequest>()
                .Property(cr => cr.ReceiverId)
                .IsRequired(false);

            modelBuilder.Entity<ChatRequest>()
                .Property(cr => cr.CreatedDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}