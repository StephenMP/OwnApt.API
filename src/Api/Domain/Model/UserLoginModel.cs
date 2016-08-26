﻿using Microsoft.AspNet.Identity;

namespace OwnApt.Api.Domain.Model
{
    public class UserLoginModel
    {
        #region Public Fields + Properties

        public string Email { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
        public PasswordVerificationResult VerificationResult { get; set; }

        #endregion Public Fields + Properties
    }
}