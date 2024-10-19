namespace authenticat.Models
{
    public class user
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public string Role { get; set; }
    }
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
