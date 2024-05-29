using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyBirthdayCampain.UI.Maps
{
	internal static class EmployeeMapper
	{
		public static Models.Employee Map(BOL.EmployeeDTO src)
		{ 
			return new Models.Employee()
			{ 
				Id = src.Key.Id,
				FullName = src.FirstName + " " + src.LastName,
			};
		}
	}
}
