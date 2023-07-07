namespace Authentication.Models
{
    public interface IUserManger
    {
        bool Login(string username, string password);
        UserCredentials GetUserCredentials();
    }
}
