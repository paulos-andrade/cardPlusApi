public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CPF { get; set; }
    public string Phone { get; set; }
    public ICollection<Address> Addresses { get; set; }
}