namespace Authentication.Models
{
    public interface IUserManger
    {
        bool Login(string username, string password, bool isAdmin);
        UserCredentials GetUserCredentials();
    }
}
