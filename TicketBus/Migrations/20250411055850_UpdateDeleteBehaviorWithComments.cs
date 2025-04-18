﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBus.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeleteBehaviorWithComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Passengers_IdPassenger",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRoutes_RegistForms_IdRegist",
                table: "BusRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_RegistForms_IdRegist",
                table: "Coaches");

            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_VehicleTypes_IdType",
                table: "Coaches");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountDetails_Bills_IdBill",
                table: "DiscountDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountDetails_Coupons_IdCoupon",
                table: "DiscountDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Cities_IdCity",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_DropOffs_Cities_IdCity",
                table: "DropOffs");

            migrationBuilder.DropForeignKey(
                name: "FK_DropOffs_RegistForms_IdRegist",
                table: "DropOffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Brands_IdBrand",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Positions_IdPos",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Passengers_IdPassenger",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_News_TypeNews_IdTypeNews",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_Pickups_Cities_IdCity",
                table: "Pickups");

            migrationBuilder.DropForeignKey(
                name: "FK_Pickups_RegistForms_IdRegist",
                table: "Pickups");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_BusRoutes_IdRoute",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Coaches_IdCoach",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_RouteStops_IdStopEnd",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_RouteStops_IdStopStart",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistForms_Brands_IdBrand",
                table: "RegistForms");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteStops_BusRoutes_IdRoute",
                table: "RouteStops");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleDetails_BusRoutes_IdRoute",
                table: "ScheduleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleDetails_Coaches_IdCoach",
                table: "ScheduleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Coaches_IdCoach",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceDetails_Services_IdService",
                table: "ServiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceDetails_VehicleTypes_IdType",
                table: "ServiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_BusRoutes_IdRoute",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Coaches_IdCoach",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Employees_IdEmployee",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Passengers_IdPassenger",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Prices_IdPrice",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Seats_IdSeat",
                table: "Tickets");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Passengers_IdPassenger",
                table: "Bills",
                column: "IdPassenger",
                principalTable: "Passengers",
                principalColumn: "IdPassenger",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusRoutes_RegistForms_IdRegist",
                table: "BusRoutes",
                column: "IdRegist",
                principalTable: "RegistForms",
                principalColumn: "IdRegist",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_RegistForms_IdRegist",
                table: "Coaches",
                column: "IdRegist",
                principalTable: "RegistForms",
                principalColumn: "IdRegist",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_VehicleTypes_IdType",
                table: "Coaches",
                column: "IdType",
                principalTable: "VehicleTypes",
                principalColumn: "IdType",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountDetails_Bills_IdBill",
                table: "DiscountDetails",
                column: "IdBill",
                principalTable: "Bills",
                principalColumn: "IdBill",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountDetails_Coupons_IdCoupon",
                table: "DiscountDetails",
                column: "IdCoupon",
                principalTable: "Coupons",
                principalColumn: "IdCoupon",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Cities_IdCity",
                table: "Districts",
                column: "IdCity",
                principalTable: "Cities",
                principalColumn: "IdCity",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DropOffs_Cities_IdCity",
                table: "DropOffs",
                column: "IdCity",
                principalTable: "Cities",
                principalColumn: "IdCity",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DropOffs_RegistForms_IdRegist",
                table: "DropOffs",
                column: "IdRegist",
                principalTable: "RegistForms",
                principalColumn: "IdRegist",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Brands_IdBrand",
                table: "Employees",
                column: "IdBrand",
                principalTable: "Brands",
                principalColumn: "IdBrand",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Positions_IdPos",
                table: "Employees",
                column: "IdPos",
                principalTable: "Positions",
                principalColumn: "IdPos",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Passengers_IdPassenger",
                table: "Feedbacks",
                column: "IdPassenger",
                principalTable: "Passengers",
                principalColumn: "IdPassenger",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_News_TypeNews_IdTypeNews",
                table: "News",
                column: "IdTypeNews",
                principalTable: "TypeNews",
                principalColumn: "IdTypeNews",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pickups_Cities_IdCity",
                table: "Pickups",
                column: "IdCity",
                principalTable: "Cities",
                principalColumn: "IdCity",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pickups_RegistForms_IdRegist",
                table: "Pickups",
                column: "IdRegist",
                principalTable: "RegistForms",
                principalColumn: "IdRegist",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_BusRoutes_IdRoute",
                table: "Prices",
                column: "IdRoute",
                principalTable: "BusRoutes",
                principalColumn: "IdRoute",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Coaches_IdCoach",
                table: "Prices",
                column: "IdCoach",
                principalTable: "Coaches",
                principalColumn: "IdCoach",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_RouteStops_IdStopEnd",
                table: "Prices",
                column: "IdStopEnd",
                principalTable: "RouteStops",
                principalColumn: "IdStop",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_RouteStops_IdStopStart",
                table: "Prices",
                column: "IdStopStart",
                principalTable: "RouteStops",
                principalColumn: "IdStop",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistForms_Brands_IdBrand",
                table: "RegistForms",
                column: "IdBrand",
                principalTable: "Brands",
                principalColumn: "IdBrand",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStops_BusRoutes_IdRoute",
                table: "RouteStops",
                column: "IdRoute",
                principalTable: "BusRoutes",
                principalColumn: "IdRoute",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleDetails_BusRoutes_IdRoute",
                table: "ScheduleDetails",
                column: "IdRoute",
                principalTable: "BusRoutes",
                principalColumn: "IdRoute",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleDetails_Coaches_IdCoach",
                table: "ScheduleDetails",
                column: "IdCoach",
                principalTable: "Coaches",
                principalColumn: "IdCoach",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Coaches_IdCoach",
                table: "Seats",
                column: "IdCoach",
                principalTable: "Coaches",
                principalColumn: "IdCoach",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceDetails_Services_IdService",
                table: "ServiceDetails",
                column: "IdService",
                principalTable: "Services",
                principalColumn: "IdService",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceDetails_VehicleTypes_IdType",
                table: "ServiceDetails",
                column: "IdType",
                principalTable: "VehicleTypes",
                principalColumn: "IdType",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_BusRoutes_IdRoute",
                table: "Tickets",
                column: "IdRoute",
                principalTable: "BusRoutes",
                principalColumn: "IdRoute",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Coaches_IdCoach",
                table: "Tickets",
                column: "IdCoach",
                principalTable: "Coaches",
                principalColumn: "IdCoach",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Employees_IdEmployee",
                table: "Tickets",
                column: "IdEmployee",
                principalTable: "Employees",
                principalColumn: "IdEmployee",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Passengers_IdPassenger",
                table: "Tickets",
                column: "IdPassenger",
                principalTable: "Passengers",
                principalColumn: "IdPassenger",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Prices_IdPrice",
                table: "Tickets",
                column: "IdPrice",
                principalTable: "Prices",
                principalColumn: "IdPrice",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Seats_IdSeat",
                table: "Tickets",
                column: "IdSeat",
                principalTable: "Seats",
                principalColumn: "IdSeat",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Passengers_IdPassenger",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRoutes_RegistForms_IdRegist",
                table: "BusRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_RegistForms_IdRegist",
                table: "Coaches");

            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_VehicleTypes_IdType",
                table: "Coaches");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountDetails_Bills_IdBill",
                table: "DiscountDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountDetails_Coupons_IdCoupon",
                table: "DiscountDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Cities_IdCity",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_DropOffs_Cities_IdCity",
                table: "DropOffs");

            migrationBuilder.DropForeignKey(
                name: "FK_DropOffs_RegistForms_IdRegist",
                table: "DropOffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Brands_IdBrand",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Positions_IdPos",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Passengers_IdPassenger",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_News_TypeNews_IdTypeNews",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_Pickups_Cities_IdCity",
                table: "Pickups");

            migrationBuilder.DropForeignKey(
                name: "FK_Pickups_RegistForms_IdRegist",
                table: "Pickups");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_BusRoutes_IdRoute",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Coaches_IdCoach",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_RouteStops_IdStopEnd",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_RouteStops_IdStopStart",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistForms_Brands_IdBrand",
                table: "RegistForms");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteStops_BusRoutes_IdRoute",
                table: "RouteStops");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleDetails_BusRoutes_IdRoute",
                table: "ScheduleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleDetails_Coaches_IdCoach",
                table: "ScheduleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Coaches_IdCoach",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceDetails_Services_IdService",
                table: "ServiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceDetails_VehicleTypes_IdType",
                table: "ServiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_BusRoutes_IdRoute",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Coaches_IdCoach",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Employees_IdEmployee",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Passengers_IdPassenger",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Prices_IdPrice",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Seats_IdSeat",
                table: "Tickets");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Passengers_IdPassenger",
                table: "Bills",
                column: "IdPassenger",
                principalTable: "Passengers",
                principalColumn: "IdPassenger",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusRoutes_RegistForms_IdRegist",
                table: "BusRoutes",
                column: "IdRegist",
                principalTable: "RegistForms",
                principalColumn: "IdRegist");

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_RegistForms_IdRegist",
                table: "Coaches",
                column: "IdRegist",
                principalTable: "RegistForms",
                principalColumn: "IdRegist",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_VehicleTypes_IdType",
                table: "Coaches",
                column: "IdType",
                principalTable: "VehicleTypes",
                principalColumn: "IdType",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountDetails_Bills_IdBill",
                table: "DiscountDetails",
                column: "IdBill",
                principalTable: "Bills",
                principalColumn: "IdBill",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountDetails_Coupons_IdCoupon",
                table: "DiscountDetails",
                column: "IdCoupon",
                principalTable: "Coupons",
                principalColumn: "IdCoupon",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Cities_IdCity",
                table: "Districts",
                column: "IdCity",
                principalTable: "Cities",
                principalColumn: "IdCity",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DropOffs_Cities_IdCity",
                table: "DropOffs",
                column: "IdCity",
                principalTable: "Cities",
                principalColumn: "IdCity",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DropOffs_RegistForms_IdRegist",
                table: "DropOffs",
                column: "IdRegist",
                principalTable: "RegistForms",
                principalColumn: "IdRegist",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Brands_IdBrand",
                table: "Employees",
                column: "IdBrand",
                principalTable: "Brands",
                principalColumn: "IdBrand",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Positions_IdPos",
                table: "Employees",
                column: "IdPos",
                principalTable: "Positions",
                principalColumn: "IdPos");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Passengers_IdPassenger",
                table: "Feedbacks",
                column: "IdPassenger",
                principalTable: "Passengers",
                principalColumn: "IdPassenger");

            migrationBuilder.AddForeignKey(
                name: "FK_News_TypeNews_IdTypeNews",
                table: "News",
                column: "IdTypeNews",
                principalTable: "TypeNews",
                principalColumn: "IdTypeNews",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pickups_Cities_IdCity",
                table: "Pickups",
                column: "IdCity",
                principalTable: "Cities",
                principalColumn: "IdCity",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pickups_RegistForms_IdRegist",
                table: "Pickups",
                column: "IdRegist",
                principalTable: "RegistForms",
                principalColumn: "IdRegist",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_BusRoutes_IdRoute",
                table: "Prices",
                column: "IdRoute",
                principalTable: "BusRoutes",
                principalColumn: "IdRoute");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Coaches_IdCoach",
                table: "Prices",
                column: "IdCoach",
                principalTable: "Coaches",
                principalColumn: "IdCoach");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_RouteStops_IdStopEnd",
                table: "Prices",
                column: "IdStopEnd",
                principalTable: "RouteStops",
                principalColumn: "IdStop");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_RouteStops_IdStopStart",
                table: "Prices",
                column: "IdStopStart",
                principalTable: "RouteStops",
                principalColumn: "IdStop");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistForms_Brands_IdBrand",
                table: "RegistForms",
                column: "IdBrand",
                principalTable: "Brands",
                principalColumn: "IdBrand",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStops_BusRoutes_IdRoute",
                table: "RouteStops",
                column: "IdRoute",
                principalTable: "BusRoutes",
                principalColumn: "IdRoute");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleDetails_BusRoutes_IdRoute",
                table: "ScheduleDetails",
                column: "IdRoute",
                principalTable: "BusRoutes",
                principalColumn: "IdRoute",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleDetails_Coaches_IdCoach",
                table: "ScheduleDetails",
                column: "IdCoach",
                principalTable: "Coaches",
                principalColumn: "IdCoach",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Coaches_IdCoach",
                table: "Seats",
                column: "IdCoach",
                principalTable: "Coaches",
                principalColumn: "IdCoach",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceDetails_Services_IdService",
                table: "ServiceDetails",
                column: "IdService",
                principalTable: "Services",
                principalColumn: "IdService",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceDetails_VehicleTypes_IdType",
                table: "ServiceDetails",
                column: "IdType",
                principalTable: "VehicleTypes",
                principalColumn: "IdType",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_BusRoutes_IdRoute",
                table: "Tickets",
                column: "IdRoute",
                principalTable: "BusRoutes",
                principalColumn: "IdRoute");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Coaches_IdCoach",
                table: "Tickets",
                column: "IdCoach",
                principalTable: "Coaches",
                principalColumn: "IdCoach");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Employees_IdEmployee",
                table: "Tickets",
                column: "IdEmployee",
                principalTable: "Employees",
                principalColumn: "IdEmployee");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Passengers_IdPassenger",
                table: "Tickets",
                column: "IdPassenger",
                principalTable: "Passengers",
                principalColumn: "IdPassenger");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Prices_IdPrice",
                table: "Tickets",
                column: "IdPrice",
                principalTable: "Prices",
                principalColumn: "IdPrice");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Seats_IdSeat",
                table: "Tickets",
                column: "IdSeat",
                principalTable: "Seats",
                principalColumn: "IdSeat");
        }
    }
}
