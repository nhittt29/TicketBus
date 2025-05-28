namespace TicketBus.Models.ViewModels
{
    public class EditUserRolesViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public List<string> Roles { get; set; }
        public string SelectedRole { get; set; }
    }
}
