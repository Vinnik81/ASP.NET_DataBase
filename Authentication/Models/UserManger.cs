using System.Text.Json;

namespace Authentication.Models
{
    public class UserManger : IUserManger
    {
        private readonly UsersDbContext usersDbContext;

        public UserManger(UsersDbContext usersDbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.usersDbContext = usersDbContext;
            HttpContextAccessor = httpContextAccessor;
        }

        public IHttpContextAccessor HttpContextAccessor { get; }

        public UserCredentials GetUserCredentials()
        {
            if (HttpContextAccessor.HttpContext.Request.Cookies.ContainsKey("auth"))
            {
                var hash = HttpContextAccessor.HttpContext.Request.Cookies["auth"];
                var json = AesOperation.DecryptString("b14ca5898a4e4133bbce2ea2315a1916", hash);
                return JsonSerializer.Deserialize<UserCredentials>(json);

            }
            else
            {

                return null;
            }

        }

        public bool Login(string username, string password)
        {
            var passwordHash = SHA256Encryptor.Encrypt(password);
            var users = usersDbContext.Users.FirstOrDefault(x => x.PasswordHash == passwordHash && x.Login == username);
            if (users != null)
            {
                var userCredentials = new UserCredentials
                {
                    Login = users.Login,
                    IsAdmin = users.IsAdmin
                };


                var json = JsonSerializer.Serialize(userCredentials);
                var encrypt = AesOperation.EncryptString("b14ca5898a4e4133bbce2ea2315a1916", json);
                HttpContextAccessor.HttpContext.Response.Cookies.Append("auth", encrypt);
                //httpContextAccessor
                return true;
            }
            return false;

        }
    }
}
