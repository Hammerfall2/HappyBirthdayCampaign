
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
				PasswordType = (BOL.PasswordType) src.PasswordType,
				FirstName = src.FirstName,
				LastName = src.LastName,
			};
		}


	}
}
