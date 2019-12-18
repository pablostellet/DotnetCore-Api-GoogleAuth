using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using API.Models;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public interface IAuthService
    {
        UserTokenVm Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        Task<User> AuthenticateGoogle(GoogleJsonWebSignature.Payload payload);

    }

    public class AuthService : IAuthService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = new Guid("something"), FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };

        private readonly AppSettings _appSettings;

        public AuthService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public UserTokenVm Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            var userTokenVm = new UserTokenVm(user);

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userTokenVm.Token = tokenHandler.WriteToken(token);

            return userTokenVm.WithoutPassword();
        }

        public IEnumerable<User> GetAll()
        {
            return _users.WithoutPasswords();
        }

        public async Task<User> AuthenticateGoogle(GoogleJsonWebSignature.Payload payload)
        {
            await Task.Delay(1);
            return this.FindUserOrAdd(payload);
        }

        private User FindUserOrAdd(GoogleJsonWebSignature.Payload payload)
        {
            var u = _users.Where(x => x.Email == payload.Email).FirstOrDefault();
            if (u == null)
            {
                u = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = payload.Name,
                    Email = payload.Email,
                    OauthSubject = payload.Subject,
                    OauthIssuer = payload.Issuer
                };
                _users.Add(u);
            }
            return u;
        }

        private void Refresh()
        {
            if (_users.Count == 0)
            {
                _users.Add(new User() { Id = Guid.NewGuid(), FirstName = "Test Person1", Email = "testperson1@gmail.com" });
                _users.Add(new User() { Id = Guid.NewGuid(), FirstName = "Test Person2", Email = "testperson2@gmail.com" });
                _users.Add(new User() { Id = Guid.NewGuid(), FirstName = "Test Person3", Email = "testperson3@gmail.com" });
            }
        }
    }
}