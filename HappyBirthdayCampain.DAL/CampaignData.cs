//using HappyBirthdayCampain.BOL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HappyBirthdayCampain.BOL;
using HappyBirthdayCampain.DAL.Maps;


namespace HappyBirthdayCampain.DAL
{
    public class CampaignData : ICampaignData
    {

        private CampaignDb DbModel1 { get; set; } = new CampaignDb();

		//public bool IsUserAndPasswordExist(string username, string password)
		//public string GetUserFirstName(string usrname)
		//public string GetUserLastName(string usrname)
		//public int GetUserId(string usrname)
		public BOL.EmployeeDTO TryGetEmployeeByUsername(string username)
		{
			try 
			{ 
				var dbObj = DbModel1.Employees.SingleOrDefault(emp => emp.UserName == username); 
				if (dbObj == null) 
					return null;

				var res = EmployeeMapper.Map(dbObj);
				return res;
			} 
			catch(Exception ex) 
			{ 
				throw new DalException(ex.Message); 
			}
		}

		//public string GetUserNameById(int Id)
		public BOL.EmployeeDTO GetEmployeeById(int EmployeeId)
		{
			try 
			{ 
				var dbObj = DbModel1.Employees.Single(user => user.Id == EmployeeId); 
				var res = EmployeeMapper.Map(dbObj);
				return res;
			} 
			catch(Exception ex) 
			{ 
				throw new DalException(ex.Message); 
			}
		}

    }
}