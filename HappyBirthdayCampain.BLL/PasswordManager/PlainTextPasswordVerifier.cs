using HappyBirthdayCampain.BOL;

namespace HappyBirthdayCampain.BLL.PasswordManager
{
    public class PlainTextPasswordVerifier : IPasswordVerifier
    {

        private readonly IBllLogger _logger;

        public PlainTextPasswordVerifier(IBllLogger logger) { _logger = logger; }

        public bool VerifyPassword(EmployeeDTO dTO, string UI_Password)
        {
            _logger.Debug("Call PlainTextPasswordVerifier::VerifyPassword()");

            var bRes = (dTO.Password == UI_Password && !dTO.Password.Contains("argon2"));
            _logger.Debug($"Return {bRes} PlainTextPasswordVerifier::VerifyPassword()");
            return bRes;
        }

    }
}
