using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Services
{
    public interface IEmailSender
    {
        Task SendEmailConfirmationAsync(string email, object callbackUrl);
    }
}
