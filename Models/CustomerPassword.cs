namespace Models
{
    public class CustomerPassword :Customer
    {
        public string Password { get; set; }
        public CustomerPassword() : base() { }
        public CustomerPassword(int id, string name, string email, string password ,bool isAdmin = false) : base(id, name, email, isAdmin)
        {
           Password = password;
        }
    }

}