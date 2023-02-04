using Flunt.Notifications;
using Flunt.Validations;
using Flunt.Extensions.Br.Validations;

namespace cadPlus_Api.ViewModels

{
    public class CreateAddressViewModel : Notifiable<Notification>
    {
        public string AddressName { get; set; }
        public string CEP { get; set; }
        public User User { get; set; }
        public int ? AddressId { get; set; }
        public Address Validate()
        {

            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNull(AddressName, "Informe um endereço Válido!")
                .IsZipCode(CEP, "Informe um CEP Válido!");

            AddNotifications(contract);

            return new Address(AddressName, CEP);
        }
    }

}
