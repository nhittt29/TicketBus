﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class RouteStop
    {
        [Key]
        public int IdStop { get; set; }
        public string? StopCode { get; set; }
        [ForeignKey("BusRoute")]
        public int? IdRoute { get; set; }
        public string? StopName { get; set; }
        public int? StopOrder { get; set; }
        public TimeSpan? Time { get; set; }

        public BusRoute? BusRoute { get; set; }
    }
}
