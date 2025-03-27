using HappyBirthdayCampain.BOL;
using Isopoh.Cryptography.Argon2;

namespace HappyBirthdayCampain.BLL.PasswordManager
{
    public class Argon2PasswordVerifier : IPasswordVerifier
    {
        private readonly IBllLogger _logger;
        public Argon2PasswordVerifier(IBllLogger logger) { _logger = logger; }

        public bool VerifyPassword(EmployeeDTO dTO, string UI_Password)
        {
            _logger.Debug("Call Argon2PasswordVerifier::VerifyPassword()");


            var bRes = Argon2.Verify(dTO.Password, UI_Password);

            _logger.Debug($"Return {bRes} Argon2PasswordVerifier::VerifyPassword()");
            return bRes;

        }
    }
}
