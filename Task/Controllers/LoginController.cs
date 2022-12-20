using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Assignment.Models;
using Assignment.Helpers.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;

namespace Assignment.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppSettings _appSettings;

        public LoginController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginModel loginModel = new LoginModel();
            return View(loginModel);
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            string hardCodedUsername = "user1234";
            string hardCodedPassword = "AwesomePassword123";

            if (ModelState.IsValid)
            {
                bool isValidUser = (hardCodedPassword == loginModel.Password && hardCodedUsername == loginModel.Username);

                if (isValidUser)
                {
                    var token = generateJwtToken(loginModel);
                    var authenticatedUser = new AuthenticatedModel(loginModel, token);
                    return View("SuccessfulLogin", authenticatedUser);
                }
                this.ViewBag.Message = "Invalid username or password!";
            }

            return View(loginModel);
        }

        //ideally this method will be part of a usersService class
        //that will hold all users related methods

        private string generateJwtToken(LoginModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(type:"username", user.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
