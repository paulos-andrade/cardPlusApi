public class User
{
    public User()
    {
        this.Addresses = new HashSet<Address>();
    }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string CPF { get; set; }
    public string Phone { get; set; }
    public string Mail { get; set; }
    //public string Password { get; set; }
    public string Password { get; set; }
    public User(string Name, string CPF,string Phone,string Mail, string Password)
    {
        this.Name = Name;
        this.CPF = CPF;
        this.Phone = Phone;
        this.Mail = Mail;
        this.Password = Password;
    }
    public ICollection<Address>? Addresses { get; set; } = new List<Address>();
    

}