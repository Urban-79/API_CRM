namespace API_CRM.Class
{
    public class User
    {
        public int Id { get; set; }
        public string AdresseMail { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
