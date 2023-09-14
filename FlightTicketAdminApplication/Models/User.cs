namespace FlightTicketAdminApplication.Models
{

    public enum Role
    {
        STANDARD,ADMIN
    }


    public class User
    {
        
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Role Role { get; set; }
    }
}
