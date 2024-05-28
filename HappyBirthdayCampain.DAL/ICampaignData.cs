//using HappyBirthdayCampain.BOL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HappyBirthdayCampain.DAL
{
    public interface ICampaignData
    {
		BOL.EmployeeDTO TryGetEmployeeByUsername(string username);
		BOL.EmployeeDTO GetEmployeeById(int EmployeeId);
      

    }
}
