using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneStopShop.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneStopShop.API.Models.Security;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using OneStopShop.API.Entities;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;

namespace OneStopShop.API.Controllers
{
    [Route("api/AuthController")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private IOneStopShopRepository _oneStopShopRepository;
        private ILogger<SecurityController> _logger;

        public SecurityController(IOneStopShopRepository oneStopShopRepository, ILogger<SecurityController> logger)
        {
            _oneStopShopRepository = oneStopShopRepository;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] CredentialDTO cred)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Do the login credentials check here
                    User userDetails = await _oneStopShopRepository.Login(cred);

                    if (userDetails != null)
                    {
                        DateTime tokenExpiryDateTime = DateTime.Now.AddMinutes(Convert.ToInt32(Startup.Configuration["tokens:expiryMinutes"]));

                        string token = this.GenerateJwtToken(userDetails, tokenExpiryDateTime);
                        RefreshToken refreshToken = GenerateRefreshToken(this.GetIPAddress(), userDetails.userid);

                        //Build the OK response
                        AuthenticateResponse response = new AuthenticateResponse(userDetails, token, refreshToken.token, tokenExpiryDateTime);

                        //Save the refresh token in the database
                        _oneStopShopRepository.AddRefreshToken(refreshToken);

                        //Return the OK response
                        return Ok(response);
                    }
                }

                return BadRequest("Login failed!");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occured in the login method in the AuthController.");
                return BadRequest("An error has occurred! Could not create login token!" + ex.ToString());
            }
        }

        private string GenerateJwtToken(Entities.User UserDetails, DateTime expiryDate)
        {
            var claims = new[]
            {
                new Claim("userid", UserDetails.userid.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.Configuration["tokens:key"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken token = new JwtSecurityToken(Startup.Configuration["tokens:issuer"],
                Startup.Configuration["tokens:audience"],
                claims,
                expires: expiryDate,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken(string ipAddress, int userID)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);

                return new RefreshToken
                {
                    token = Convert.ToBase64String(randomBytes),
                    expires = DateTime.UtcNow.AddDays(7),
                    created = DateTime.UtcNow,
                    createdbyipaddress = ipAddress,
                    userid = userID
                };
            }
        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var refreshToken = Request.Cookies["OneStopShopRefreshToken"];
                string ipAddress = this.GetIPAddress();

                //Check if the refresh token exists
                RefreshToken storedDBToken = _oneStopShopRepository.ListRefreshTokenDetails(refreshToken);

                if (storedDBToken == null)
                {
                    return Unauthorized();
                }

                //Check if the token is active and not yet expired
                bool IsExpired = false;

                if (DateTime.UtcNow >= storedDBToken.expires)
                {
                    IsExpired = true;
                }

                bool IsActive = false;

                if (storedDBToken.revoked == null && !IsExpired)
                {
                    IsActive = true;
                }

                if (!IsActive)
                {
                    return Unauthorized();
                }

                //Check if the IP address of the request matches the IP address of the stored refresh token
                if (ipAddress != storedDBToken.createdbyipaddress)
                {
                    //Cancel the token
                    storedDBToken.revokedbyipaddress = ipAddress;
                    storedDBToken.revoked = DateTime.UtcNow;
                    _oneStopShopRepository.RevokeRefreshToken(storedDBToken);

                    return Unauthorized();
                }

                //Replace old refresh token with a new one and save
                var newRefreshToken = this.GenerateRefreshToken(ipAddress, storedDBToken.userid);

                storedDBToken.revoked = DateTime.UtcNow;
                storedDBToken.revokedbyipaddress = ipAddress;
                storedDBToken.revokedbyipaddress = newRefreshToken.token;

                //Save the refresh token to the database
                _oneStopShopRepository.AddRefreshToken(newRefreshToken);

                //Generate new jwt
                DateTime tokenExpiryDateTime = DateTime.Now.AddMinutes(Convert.ToInt32(Startup.Configuration["tokens:expiryMinutes"]));
                User userDetails = _oneStopShopRepository.ListUserDetails(storedDBToken.userid);


                string token = this.GenerateJwtToken(userDetails, tokenExpiryDateTime);

                AuthenticateResponse response = new AuthenticateResponse(userDetails, token, newRefreshToken.token, tokenExpiryDateTime);

                this.SetTokenCookie(response.RefreshToken);

                return Ok(response);
            }
            catch
            {
                return Unauthorized();
            }
        }

        [AllowAnonymous]
        [HttpPost("revokeToken")]
        public async Task<IActionResult> RevokeToken()
        {
            try
            {
                var refreshToken = Request.Cookies["OneStopShopRefreshToken"];
                string ipAddress = this.GetIPAddress();

                //Check if the refresh token exists
                RefreshToken storedDBToken = _oneStopShopRepository.ListRefreshTokenDetails(refreshToken);

                if (storedDBToken == null)
                {
                    return Unauthorized();
                }

                //Check if the token is active and not yet expired
                bool IsExpired = false;

                if (DateTime.UtcNow >= storedDBToken.expires)
                {
                    IsExpired = true;
                }

                bool IsActive = false;

                if (storedDBToken.revoked == null && !IsExpired)
                {
                    IsActive = true;
                }

                if (!IsActive)
                {
                    return Unauthorized();
                }

                //Check if the IP address of the request matches the IP address of the stored refresh token
                if (ipAddress != storedDBToken.createdbyipaddress)
                {
                    //Cancel the token
                    storedDBToken.revokedbyipaddress = ipAddress;
                    storedDBToken.revoked = DateTime.UtcNow;
                    _oneStopShopRepository.RevokeRefreshToken(storedDBToken);

                    return Unauthorized();
                }

                storedDBToken.revokedbyipaddress = ipAddress;
                storedDBToken.revoked = DateTime.UtcNow;
                _oneStopShopRepository.RevokeRefreshToken(storedDBToken);

                return Ok();
            }
            catch
            {
                return Unauthorized();
            }
        }

        private string GetIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return Request.Headers["X-Forwarded-For"];
            }
            else
            {
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("OneStopShopRefreshToken", token, cookieOptions);
        }
    }
}
