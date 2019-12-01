﻿using System.Threading.Tasks;

namespace EfLearning.Api.EmailServices
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}