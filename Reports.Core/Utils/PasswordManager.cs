// Copyright (C) 2007-2008 CorePartners. All rights reserved.
//

using System;
using System.Security.Cryptography;
using System.Web.Security;

namespace Reports.Core.Utils
{
    public sealed class PasswordManager
    {
        #region Constructor

        private PasswordManager()
        {
        }

        #endregion


        #region Constants

        public const int SaltSize = 16;
        public const string PasswordFormat = "sha1";

        #endregion


        #region Methods

        public static string CreateSalt()
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[SaltSize];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        public static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd =
             FormsAuthentication.HashPasswordForStoringInConfigFile(
             saltAndPwd, PasswordFormat);

            return hashedPwd;
        }

        #endregion
    }
}