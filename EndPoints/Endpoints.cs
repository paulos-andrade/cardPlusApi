using cadPlus_Api.Data;
using cadPlus_Api.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace EndPoints
{

    public static class EndpointsList
    {
        public static void RegisterUsersApis(this WebApplication app)
        {
            //User Login
            app.MapPost("v1/login/{pw}", async (
                int userId, ApplicationDbContext context, CreateAddressViewModel createAddressViewModel) =>
            {
              
                return Results.BadRequest(createAddressViewModel.Notifications);
            });

            //User Register
            //Register User And Address
            app.MapPost("v1/registerUser", async (
                ApplicationDbContext context, CreateUserAddressViewModel createUserAddressViewModel) =>
            {
                var model = createUserAddressViewModel.Validate();

                if (createUserAddressViewModel.IsValid)
                {
                    try
                    {
                        Address address = new(model.AddressName, model.CEP);

                        User user = new(model.Name, model.CPF, model.Phone, model.Mail, model.Password);

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

            app.MapPost("v1/login/{pw}", async (
                int userId, ApplicationDbContext context, CreateAddressViewModel createAddressViewModel) =>
            {

                return Results.BadRequest(createAddressViewModel.Notifications);
            });

            //Get ALL Users
            app.MapGet("v1/getAllUsers", async (ApplicationDbContext context) =>
            {
                var users = await context.Users.ToListAsync();
                var Addres = await context.Addresses.ToListAsync(); //forçar o carregamento dos filhos que não estavam vindo
                return Results.Ok(users);

            }).Produces<User>(); // retorna o Schema do user

            //Get A User
            app.MapGet("v1/getUser/{id}", async (int id, ApplicationDbContext context) =>
            {
                var users = await context.Users.FirstOrDefaultAsync(x => x.UserId == id);
                return Results.Ok(users);

            }).Produces<User>();

            //Add Address
            app.MapPost("v1/user/{userId}/addAddress/", async (
                int userId,ApplicationDbContext context, CreateAddressViewModel createAddressViewModel) =>
            {
                var model = createAddressViewModel.Validate();

                if (createAddressViewModel.IsValid)
                {
                    try
                    {
                        Address address = new(model.AddressName, model.CEP, model.UserId = userId);

                        var addressValidationReturn = Validations.ValidateExistingItemsAddresses(address);
                        if (addressValidationReturn != "")
                            return Results.BadRequest(addressValidationReturn);

                        context.Addresses.Add(address); //adiciona Address
                        await context.SaveChangesAsync();
                        return Results.Created($"/v1/addAddress/{address.AddressId}", address);
                    }
                    catch (Exception)
                    {
                        Results.BadRequest();
                        throw;
                    }
                }
                return Results.BadRequest(createAddressViewModel.Notifications);
            });


            //Update User
            app.MapPut("v1/updateUser/{id}", async (int id,
               ApplicationDbContext context, CreateUserViewModel CreateUserViewModel) =>
            {
                var model = CreateUserViewModel.Validate();

                if (CreateUserViewModel.IsValid)
                {
                    var user = await context.Users.FirstOrDefaultAsync(x => x.UserId == id);
                    if (user == null)
                        return Results.NotFound();

                    var userValidationReturn = Validations.ValidateHasExistingItemsUsers(user);
                    if (userValidationReturn != "")
                        return Results.BadRequest(userValidationReturn);

                    try
                    {
                        user.Name = model.Name;
                        user.CPF = model.CPF;
                        user.Phone = model.Phone;
                        user.Mail = model.Mail;
                        context.Users.Update(user); //adiciona user
                        await context.SaveChangesAsync();
                        return Results.Ok();
                    }
                    catch (Exception)
                    {
                        Results.BadRequest();
                        throw;
                    }
                }
                return Results.BadRequest(CreateUserViewModel.Notifications);
            });

            //Update Address
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
                        address.CEP = model.CEP;
                        context.Addresses.Update(address); //adiciona address
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
