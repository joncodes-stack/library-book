using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.CrossCutting.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email);
        Task SendConfirmEmailAsync(string email, string content);
    }
}
