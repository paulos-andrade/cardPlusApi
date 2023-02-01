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
                var users = await context.Users.FirstOrDefaultAsync(x => x.UserId == id);
                return Results.Ok(users);

            }).Produces<User>();

            app.MapPost("v1/insertUserAndAddress", async (
                ApplicationDbContext context, CreateUserAddressViewModel createUserAddressViewModel) =>
            {
                var model = createUserAddressViewModel.Validate();

                if (createUserAddressViewModel.IsValid)
                {
                    try
                    {
                        Address address = new(model.AddressName, model.CEP);

                        User user = new(model.Name, model.CPF, model.Phone, model.Mail);

                        var addressValidationReturn = Validations.ValidateExistingItemsAddresses(address);
                        if (addressValidationReturn != "")
                            return Results.BadRequest(addressValidationReturn);

                        var userValidationReturn = Validations.ValidateHasExistingItemsUsers(user);
                        if (userValidationReturn != "")
                            return Results.BadRequest(userValidationReturn);

                        context.Users.Add(user); //adiciona user
                        user.Addresses.Add(address);
                        await context.SaveChangesAsync();
                        return Results.Created($"/v1/insertUser/{user.UserId}", user);
                    }
                    catch (Exception)
                    {
                        Results.BadRequest();
                        throw;
                    }
                }
                return Results.BadRequest(createUserAddressViewModel.Notifications);
            });

            app.MapPut("v1/updateUser/{id}", async (int id,
               ApplicationDbContext context, CreateUserViewModel CreateUserViewModel) =>
            {
                var model = CreateUserViewModel.Validate();

                if (CreateUserViewModel.IsValid)
                {
                    var user = await context.Users.FirstOrDefaultAsync(x => x.UserId == id);
                    if (user == null)
                        return Results.NotFound();

                    try
                    {
                        var userValidationReturn = Validations.ValidateHasExistingItemsUsers(user);
                        if (userValidationReturn != "")
                            return Results.BadRequest(userValidationReturn);

                        user.Name = model.Name;
                        user.CPF = model.CPF;
                        user.Phone = user.Phone;
                        user.Mail = model.Mail;
                        context.Users.Add(user); //adiciona user
                        await context.SaveChangesAsync();
                        return Results.Ok();
                    }
                    catch (Exception)
                    {
                        Results.BadRequest();
                        throw;
                    }
                }
                return Results.BadRequest();
            });

            app.MapPut("v1/updateUser/user/{id}/address/{addressId}", async (int id, int addressId,
               ApplicationDbContext context, CreateAddressViewModel CreateAddressViewModel) =>
            {
                var model = CreateAddressViewModel.Validate();

                if (CreateAddressViewModel.IsValid)
                {
                    var address = await context.Addresses.FirstOrDefaultAsync(x => x.UserId == id && x.AddressId == addressId);
                    if (address == null)
                        return Results.NotFound();

                    try
                    {
                        var userValidationReturn = Validations.ValidateExistingItemsAddresses(address);
                        if (userValidationReturn != "")
                            return Results.BadRequest(userValidationReturn);

                        address.AddressName = model.AddressName;
                        address.CEP = address.CEP;
                        context.Addresses.Add(address); //adiciona address
                        await context.SaveChangesAsync();
                        return Results.Ok();
                    }
                    catch (Exception)
                    {
                        Results.BadRequest();
                        throw;
                    }
                }
                return Results.BadRequest();
            });
        }
    }
}
