namespace API_CRM.Class
{
    public class User
    {
        public string AdresseMail { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
