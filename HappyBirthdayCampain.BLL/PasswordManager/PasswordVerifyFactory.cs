using HappyBirthdayCampain.BOL;
using HappyBirthdayCampain.BLL.PasswordManager;


namespace HappyBirthdayCampain.BLL
{

    public class PasswordVerifyFactory
    {
     
        private readonly IBllLogger _logger;
        public PasswordVerifyFactory(IBllLogger logger) {_logger = logger; }
        public IPasswordVerifier GetPasswordVerifier(PasswordType passwordType)
        {
            _logger.Debug("Call PasswordVerifyFactory::GetPasswordVerifier()");

            switch (passwordType)
            {
                case PasswordType.PlainText:
                    _logger.Debug("Return PlainTextPasswordVerifier(campaignData)");
                    return new PlainTextPasswordVerifier(_logger);
                    
                case PasswordType.Argon2:
                    _logger.Debug("Return Argon2PasswordVerifier()");
                    return new Argon2PasswordVerifier(_logger);
                default:
                    throw new BllException("Incorrect Password Type");
            }
        }
    }
}
