
using System.Collections.Generic;
using System.Web.Mvc;
using HappyBirthdayCampain.BOL;
using HappyBirthdayCampain.DAL;

namespace HappyBirthdayCampain.BLL
{
	public interface IUserManager
	{ 
		bool LogInVerification(string userName, string password, out BOL.EmployeeDTO user);
		BOL.EmployeeDTO GetUser(BOL.EmployeeKey key);

    }

	public class UserManager : IUserManager
    {
        //dependancy injection
        private readonly ICampaignData _action;
		

        public UserManager(ICampaignData dbAction)
        {
            _action = dbAction;
        }

        public bool LogInVerification(string userName, string password, out BOL.EmployeeDTO user)
        {
			user = null;

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password)) 
				throw new BllException("Името и паролата трябва да са попълнени");

			var tmpUser = _action.TryGetEmployeeByUsername(userName); 
			
			if (tmpUser == null)
				return false;

			//check password (it must be hashed in the db, so there will be some crypto - eg ARGON2)
			if (password != tmpUser.Password)
				return false;

			user = tmpUser;
			return true;
        }

		//public string UserFirstName(string user)
		//public string UserLastName(string user)
		//public int UserId(string user)
		//public string UserNameById(int Id)
		public BOL.EmployeeDTO GetUser(BOL.EmployeeKey key)
		{ 
			var user = _action.GetEmployeeById(key.Id);
			return user;
		}
    }
}
