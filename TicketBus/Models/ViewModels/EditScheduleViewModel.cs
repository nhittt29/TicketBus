namespace TicketBus.Models.ViewModels
{
    public class EditScheduleViewModel
    {
        public int IdSchedule { get; set; }

        public int? IdCoach { get; set; }

        public int? IdRoute { get; set; }

        public TimeSpan? DepartTime { get; set; }

        public TimeSpan? ArriveTime { get; set; }
    }
}
