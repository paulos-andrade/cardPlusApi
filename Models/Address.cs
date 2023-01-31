public class Address
{
    public int Id { get; set; }
    public string AddressName { get; set; }
    public string CEP { get; set; }
    public User User { get; set; }

    public Address(string AddressName, string CEP)
    {
        this.AddressName = AddressName;
        this.CEP = CEP;
    }
}

