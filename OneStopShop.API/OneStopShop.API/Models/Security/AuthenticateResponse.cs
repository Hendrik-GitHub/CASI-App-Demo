using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneStopShop.API.Models.Security
{
    public class AuthenticateResponse
    {
        public string UserName { get; set; }

        public int UserID { get; set; }

        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

        public AuthenticateResponse(Entities.User userDetails, string token, string refreshToken, DateTime expiration)
        {
            UserName = userDetails.username;
            Token = token;
            Expiration = expiration;
            RefreshToken = refreshToken;
        }
    }
}
