using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.CrossCutting.Interfaces
{
    public interface IEmailService
    {
        //Task SendEmailAsync(string email);
        Task SendEmailForgetAsync(string email, int code);
        Task SendConfirmEmailAsync(string email);
        Task SendValidatemEmailAsync(string email, string name, int code);
    }
}
