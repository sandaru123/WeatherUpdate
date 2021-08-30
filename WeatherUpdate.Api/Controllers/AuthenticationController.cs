using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WeatherUpdate.Api.Model.User.Login;
using WeatherUpdate.Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherUpdate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region Property
        /// <summary>
        /// Property Declaration
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IConfiguration _config;

        private readonly IUserRepository userRepository;
        #endregion

        #region Contructor Injector
        /// <summary>
        /// Constructor Injection to access all methods or simply DI(Dependency Injection)
        /// </summary>
        public AuthenticationController(IConfiguration config, IUserRepository _userRepository)
        {
            _config = config;
            userRepository = _userRepository;
        }
        #endregion

        #region GenerateJWT
        /// <summary>
        /// Generate Json Web Token Method
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

        #region AuthenticateUser
       
        private async Task<int> AuthenticateUser(LoginModel login)
        {
            //Validate the User Credentials    
            //Demo Purpose, I have Passed HardCoded User Information    
            var id = await userRepository.GetUserIdByUsernameAndPasswordAsync(login.UserName, login.Password);

            if (id != 0)
            {
                return id;
            }

            return 0;
        }
        #endregion

        #region Login Validation
        /// <summary>
        /// Login Authenticaton using JWT Token Authentication
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginModel data)
        {
            IActionResult response = Unauthorized();
            if (data!= null)
            {
                var user = await AuthenticateUser(data);
                if (user != 0)
                {
                    var tokenString = GenerateJSONWebToken();
                    var tokenBool = await userRepository.UpdateTokenByIdAsync(user, tokenString);

                    if (tokenBool)
                    {
                        response = Ok(new { Token = tokenString, Message = "Success" });
                    }
                    
                }
            }
            return response;
        }
        #endregion

        #region Get
        /// <summary>
        /// Authorize the Method
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(Get))]
        public async Task<IEnumerable<string>> Get()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            return new string[] { accessToken };
        }


        #endregion

    }

   

}
