using HappyBirthdayCampain.BOL;
using HappyBirthdayCampain.DAL;
using HappyBirthdayCampain.BLL.PasswordManager;
using Isopoh.Cryptography.Argon2;


namespace HappyBirthdayCampain.BLL
{
	public interface IUserManager
	{ 
        /// <summary>
        /// User Verification log in 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns>bool</returns>
		bool LogInVerification(string userName, string password, out BOL.EmployeeDTO user);

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="key"></param>
        /// <returns>.EmployeeDTO</returns>
		BOL.EmployeeDTO GetUser(BOL.EmployeeKey key);

    }
    

    public class UserManager : IUserManager
    {
        //dependancy injection
        private readonly ICampaignDataDal _action;
        private readonly IBllLogger _logger;
        private  IPasswordVerifier _passwordVerifier;

        public UserManager(ICampaignDataDal dbAction, IBllLogger logger, IPasswordVerifier passwordVerifier)
        {
			_action = dbAction;
			_logger = logger;
            _passwordVerifier = passwordVerifier;
        }


        public bool LogInVerification(string userName, string password, out BOL.EmployeeDTO user)
        {
			user = null;
            PasswordVerifyFactory passwordVerify = new PasswordVerifyFactory(_logger);
           

            _logger.Debug("Call UserManager::LogInVerification()");
            

			if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
			{
                var ex = new BllException("Името и паролата трябва да са попълнени");
                _logger.Error($"ERROR CampaignManager::LogInVerification", ex);
                throw ex;
            }

            _logger.Info("Login User");
            var tmpUser = _action.TryGetEmployeeByUsername(userName); 
			
			if (tmpUser == null)
                return false;

            _passwordVerifier = passwordVerify.GetPasswordVerifier(tmpUser.PasswordType);

            if (!_passwordVerifier.VerifyPassword(tmpUser, password))
                return false;
            else if(tmpUser.PasswordType == PasswordType.PlainText)
            {
                _logger.Info("Update User Password Argon2 Hash");
                var passwordHash = Argon2.Hash(password, timeCost: 10, memoryCost: 65536, parallelism: 5, Argon2Type.HybridAddressing, hashLength: 50);
                try
                {
                    _action.UpdateUserPassword(tmpUser.Key, passwordHash);
                }
                catch( DalException e)
                {
                    var ex = new BllException("The issue with password update");
                    _logger.Error($"ERROR CampaignManager::LogInVerification", ex);
                    throw ex;
                }
                
            }

             user = tmpUser;
            _logger.Debug("Return UserManager::LogInVerification()");
            return true;
        }

		//public string UserFirstName(string user)
		//public string UserLastName(string user)
		//public int UserId(string user)
		//public string UserNameById(int Id)
		public BOL.EmployeeDTO GetUser(BOL.EmployeeKey key)
		{
            _logger.Debug("Call UserManager::GetUser()");
           
            try
			{
                _logger.Info("Get Employee data with key");
                var user = _action.GetEmployeeById(key);

                _logger.Debug("Return UserManager::GetUser()");
                return user;
            }
			catch (DalException e )
			{
                var ex = new BllException("Служителят не съществува", e);
                _logger.Error($"ERROR CampaignManager::GetUser( {key.DumpEmployeeKey()})", ex);
                throw ex;
            }

		}
    }
}
