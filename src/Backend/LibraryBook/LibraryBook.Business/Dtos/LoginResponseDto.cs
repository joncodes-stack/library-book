using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Business.Dtos
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenDto UserToken { get; set; }
    }

    public class UserTokenDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
