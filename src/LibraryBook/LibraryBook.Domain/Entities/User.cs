using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Domain.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
                
        }

        public User(string fullName, string email, string phoneNumber, string password, string profilePic, bool active, int? code, string? refreshToken, DateTime refreshTokenExpireTime)
        {
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            ProfilePic = profilePic;
            Active = active;
            Code = code;
            RefreshToken = refreshToken;
            RefreshTokenExpireTime = refreshTokenExpireTime;
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ProfilePic { get; set; }
        public bool Active { get; set; }
        public int? Code { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpireTime { get; set; }
    }
}
