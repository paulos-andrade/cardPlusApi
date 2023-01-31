namespace cadPlus_Api.Models
{
    public class UserAddress
    {
        public string AddressName { get; set; }
        public string CEP { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }

        public UserAddress(string Name, string CPF, string Phone, string Mail, string CEP, string AddressName)
        {
            this.Name = Name;
            this.CPF = CPF;
            this.Phone = Phone;
            this.AddressName = AddressName;
            this.Mail = Mail;
            this.CEP = CEP;

        }
    }
}
