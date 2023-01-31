using cadPlus_Api.Data;
using cadPlus_Api.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace EndPoints
{

    public static class EndpointsList
    {
        public static void RegisterUsersApis(this WebApplication app)
        {

            app.MapGet("v1/getAllUsers", async (ApplicationDbContext context) =>
            {
                var users = await context.Users.ToListAsync();
                return Results.Ok(users);

            }).Produces<User>(); // retorna o Schema do user

            app.MapGet("v1/getUser/{id}", async (int id, ApplicationDbContext context) =>
            {
                var users = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                return Results.Ok(users);

            }).Produces<User>();

            app.MapPost("v1/insertUserAndAddress", async (
                ApplicationDbContext context, CreateUserAddressViewModel createUserAddressViewModel) =>
            {
                var model = createUserAddressViewModel.Validate();
                List<Address> list = new List<Address>();

            if (createUserAddressViewModel.IsValid)
                {
                    Address address = new (
                       model.AddressName,
                       model.CEP);

                    User user = new (
                        model.Name,
                        model.CPF,
                        model.Phone,
                        model.Mail);

                    var addressValidationReturn = Validations.ValidateExistingItemsAddresses(address);
                    var userValidationReturn = Validations.ValidateHasExistingItemsUsers(user);
                    if (userValidationReturn != "") return Results.BadRequest(userValidationReturn);
                    if(addressValidationReturn != "") return Results.BadRequest(userValidationReturn);

                    list.Add(address);
                    ICollection<Address> Adresses = list;
                    user.Addresses = Adresses;
                    context.Users.Add(user); //adiciona user
                    await context.SaveChangesAsync();
                    return Results.Created($"/v1/insertUser/{user.Id}", user);
                }
                return Results.BadRequest(createUserAddressViewModel.Notifications);
            });



        }
    }
}
