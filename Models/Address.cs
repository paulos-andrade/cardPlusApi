public class Address
{
    public int AddressId { get; set; }
    public string AddressName { get; set; }
    public string CEP { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }

    public Address(string AddressName, string CEP)
    {
        this.AddressName = AddressName;
        this.CEP = CEP;
    }
}

