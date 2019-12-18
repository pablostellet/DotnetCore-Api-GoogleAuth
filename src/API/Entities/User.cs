namespace API.Entities
{
    public class User
    {
        public System.Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public string OauthSubject { get; set; }
        public string OauthIssuer { get; set; }
    }

    public class UserTokenVm
    {
        public UserTokenVm(User user)
        {
            this.User = user;
        }

        public User User { get; set; }
        public string Token { get; set; }
    }

    public static class UserTokenVmExtension
    {
        public static UserTokenVm WithoutPassword(this UserTokenVm userVm)
        {
            userVm.User.Password = null;
            return userVm;
        }
    }
}