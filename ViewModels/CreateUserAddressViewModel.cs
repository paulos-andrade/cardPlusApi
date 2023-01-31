using Flunt.Notifications;
using Flunt.Validations;
using Flunt.Extensions.Br.Validations;
using cadPlus_Api.Models;

namespace cadPlus_Api.ViewModels
{
    public class CreateUserAddressViewModel : Notifiable<Notification>
    {
        public string AddressName { get; set; }
        public string CEP { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }

        public UserAddress Validate()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNull(Name, "Informe o Nome!")
                .IsGreaterThan(Name, 4, "O Nome deve conter mais que 4 caracteres!")
                .IsEmail(Mail, "Email Inválido!")
                .IsCpf(CPF, "Cpf Inválido")
                .IsPhoneNumber(Phone, "Telefone Inválido")
                .IsNotNull(AddressName, "Informe um endereço Válido!")
                .IsZipCode(CEP, "Informe um CEP Válido!");

            AddNotifications(contract);

            return new UserAddress(Name, CPF, Phone, Mail, CEP, AddressName);
        }

    }
}
