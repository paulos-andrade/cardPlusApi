﻿using Flunt.Notifications;
using Flunt.Validations;
using Flunt.Extensions.Br.Validations;

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
        public string Password { get; set; }

        public UserAndAddress Validate()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNull(Name, "Informe o Nome!")
                .IsGreaterThan(Name, 4, "O Nome deve conter mais que 4 caracteres!")
                .IsEmail(Mail, "Email Inválido!")
                .IsCpf(CPF, "Cpf Inválido")
                .IsPhoneNumber(Phone, "Telefone Inválido")
                .IsNotNull(AddressName, "Informe um endereço Válido!")
                .IsZipCode(CEP, "Informe um CEP Válido!")
                .IsCustomPassword(Password, "A senha precisa ...");

            AddNotifications(contract);


            return new UserAndAddress(Name, CPF, Phone, Mail, CEP, AddressName, Password);
        }

    }
    public static class Extension
    {
        public static Contract<T> IsCustomPassword<T>(this Contract<T> contract, string val, string message)
        {
            return contract.Matches(val, Extension.CustomRegexPattern.IsNotPasswordRegexPattern, message);
        }
        public static class CustomRegexPattern
        {
            public static string IsNotPasswordRegexPattern = "^(.{0,7}|[^0-9]*|[^A-Z]*|[^a-z]*|[a-zA-Z0-9]*)$";
        }
    }
}
