using AutoMapper;
using BLL.DTOs;
using APIServiceResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using BLL.Models;
using System.Security.Cryptography;
using System.Net;
using System.Security.Principal;
using System.Reflection;
using Entities;

namespace BLL.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly IConfiguration _config;
        private readonly ILogicServices _svc;
        private readonly DateTime _expireDate = DateTime.Now.AddMinutes(5);

   

        private  struct jwtOpt
        {
            public string _key;
            public string _issuer;
            public string _audience;
        }

        private readonly jwtOpt _jwtOpt;
        public LoginServices(ILogicServices svc, IConfiguration config) 
        {
            _svc = svc;
            _config = config;


            var key = _config["Jwt:Key"];
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];

            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
                throw new Exception("Authentication Option Failed {key,issuer,audience} Set appsettings.json file");

            _jwtOpt = new jwtOpt
            {
                 _key = key,
                _audience = audience,
                _issuer = issuer
            };

           
        }

        
        public async Task<ApiServiceResult<dtoUserLogin>> UserLogin(UserLoginModel model, bool IsRefreshToken = false)
        {
            try
            {
                if (model == null) return new ApiServiceResult<dtoUserLogin>(null, true, "UserLogin is null");
                if (string.IsNullOrEmpty(model.Email)) return new ApiServiceResult<dtoUserLogin>(null, true, "UserLogin, Email is null");
                if (string.IsNullOrEmpty(model.Password)) return new ApiServiceResult<dtoUserLogin>(null, true, "UserLogin, Password is null");

                

                var _user = _svc.SeedDataSvc.GetUsers()
                                .Where(a => a.Email == model.Email && a.Password == model.Password)
                                .SingleOrDefault();

                if (_user == null) return new ApiServiceResult<dtoUserLogin>(null, true, "User Email/Password Wrong");

                
                ApiServiceResult<dtoUserLogin> result = null;

                if(IsRefreshToken == false)                
                result = CreateToken(_user);
                else
               result = CreateRefreshToken(_user);

                return result;

            }
            catch (Exception ex)
            {

                return new ApiServiceResult<dtoUserLogin>(null, true, ex.Message);
            }
        }

        private ApiServiceResult<dtoUserLogin> CreateRefreshToken(TblUser model)
        {
            try
            {
                

                var _principal = GetPrincipalToken(model.Token);
                if (_principal.IsError) return new ApiServiceResult<dtoUserLogin>(null, true, _principal.Msg);


                var newToken = CreateToken(model);
                if (newToken.IsError) return new ApiServiceResult<dtoUserLogin>(null, true, newToken.Msg);

                model.Token = newToken.Data.Token;
                model.RefreshToken = CreateRefreshToken();
                

                return new ApiServiceResult<dtoUserLogin>(null);
            }
            catch (Exception ex)
            {
                return new ApiServiceResult<dtoUserLogin>(null, true, ex.Message);
            }
        }

        private ApiServiceResult<ClaimsPrincipal> GetPrincipalToken(string token)
        {
            try {


                if (string.IsNullOrEmpty(token)) return new ApiServiceResult<ClaimsPrincipal>(null, true, "Login Token Is Null");

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, GetTokenParameters(), out securityToken);
            



                return new ApiServiceResult<ClaimsPrincipal>(principal);

            } catch(Exception ex)
            {
                return new ApiServiceResult<ClaimsPrincipal>(null, true, ex.Message);
            }
        }

        private ApiServiceResult<dtoUserLogin> CreateToken(TblUser model)
        {

            try
            {
                var r = _jwtOpt;

                if (model != null)
                {



                    var tokenDescriptor = new SecurityTokenDescriptor
                    {

                        Subject = new ClaimsIdentity(new[]
                        {
                        new Claim("Id", Guid.NewGuid().ToString().Replace("-", "")),
                        new Claim("email", model.Email),
                        new Claim("userId", model.Id.ToString()),
                    }),

                        Expires = _expireDate,
                        Issuer = _jwtOpt._issuer,
                        Audience = _jwtOpt._audience,
                        SigningCredentials =  new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOpt._key)), SecurityAlgorithms.HmacSha512Signature),
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);


                    dtoUserLogin res = new dtoUserLogin()
                    {
                        UserId = model.Id,
                        Email = model.Email,
                        Token = jwtToken,
                        IsLogin = true,
                        SessionDuration = DateTime.Now -  tokenDescriptor.Expires.Value

                    };

                    model.Token = jwtToken;
                    model.RefreshTokenExpiry = _expireDate;
                    model.RefreshToken = CreateRefreshToken();


                    return new ApiServiceResult<dtoUserLogin>(res);

                }
                else
                {

                    return new ApiServiceResult<dtoUserLogin>(null, true, "Login Service Failed");
                }


            }
            catch (Exception ex)
            {
                return new ApiServiceResult<dtoUserLogin>(null, true, ex.Message);
            }
        }
        private string CreateRefreshToken()
        {
            var _randomKey = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(_randomKey);
                return Convert.ToBase64String(_randomKey);
            }
        }

        private TokenValidationParameters GetTokenParameters()
        {
            return new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOpt._key)),
                ValidateLifetime = false
            };

        }

      

}
}
