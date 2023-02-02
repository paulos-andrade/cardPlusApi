using Flunt.Notifications;
using Flunt.Validations;
using Flunt.Extensions.Br.Validations;

namespace cadPlus_Api.ViewModels
{
    public class CreateUserViewModel : Notifiable<Notification>
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }


        public User Validate()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNull(Name, "Informe o Nome!")
                .IsGreaterThan(Name, 4, "O Nome deve conter mais que 4 caracteres!")
                .IsEmail(Mail, "Email Inválido!")
                .IsCpf(CPF, "Cpf Inválido")
                .IsPhoneNumber(Phone, "Telefone Inválido");
            AddNotifications(contract);

            return new User(Name, CPF, Phone, Mail, Password);
        }

        public static Contract<T> IsPassword<T>(this Contract<T> contract, string val, string key, string message)
        {
            return contract.Matches(val, CustomRegexPattern.PhoneRegexPattern, key, message);
        }
    }
}
