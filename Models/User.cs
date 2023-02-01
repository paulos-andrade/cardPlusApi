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

    public User(string Name, string CPF,string Phone,string Mail)
    {
        this.Name = Name;
        this.CPF = CPF;
        this.Phone = Phone;
        this.Mail = Mail;
    }
    public ICollection<Address>? Addresses { get; set; } = new List<Address>();
    

}