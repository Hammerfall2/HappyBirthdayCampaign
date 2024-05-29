using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyBirthdayCampain.DAL.Maps
{
	internal static class EmployeeMapper
	{
		public static BOL.EmployeeDTO Map(DAL.Employee src)
		{ 
			return new BOL.EmployeeDTO()
			{ 
				Key = new BOL.EmployeeKey(){Id= src.Id },
				UserName = src.UserName,
				Password = src.Password,
				FirstName = src.FirstName,
				LastName = src.LastName,
			};
		}
	}
}
