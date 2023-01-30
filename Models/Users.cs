public class Users
{
    public record User(Guid Id, string Name, string CPF, string Phone, bool Done);
}