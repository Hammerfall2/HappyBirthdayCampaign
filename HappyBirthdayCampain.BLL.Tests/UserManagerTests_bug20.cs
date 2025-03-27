using HappyBirthdayCampain.BOL;
using HappyBirthdayCampain.BLL.PasswordManager;
using HappyBirthdayCampain.DAL;
using Moq;
using NUnit.Framework;

namespace HappyBirthdayCampain.BLL.Tests
{
	partial class UserManagerTests
	{
		[Test]
		public void LogInVerification_bug20()
		{
			// Arrange
			string userName = "testUser";
			string passwordHash = "$argon2i$v=19$m=16,t=2,p=1$MldMRWRhVzd6VWdqWWJOdg$0Z2364HURtioAQ+OoCA5aA";
			string passwordHashHash = "$argon2i$v=19$m=16,t=2,p=1$MldMRWRhVzd6VWdqWWJOdg$DKlmrs7J4dBOZgdCQjsA3w";
			PasswordType passwordType = PasswordType.PlainText;


			BOL.EmployeeDTO expectedUser = new BOL.EmployeeDTO()
			{
				Key = new BOL.EmployeeKey(){Id=666},
				UserName = userName,
				Password = passwordHash,
				PasswordType = passwordType

			};

			_mockDataDal.Setup(d => d.TryGetEmployeeByUsername(userName)).Returns(expectedUser);
			_mockDataDal.Setup(d => d.UpdateUserPassword(expectedUser.Key, passwordHashHash));

			// Act
			bool result = _userManager.LogInVerification(userName, passwordHash, out BOL.EmployeeDTO user);

			// Assert
			_mockDataDal.Verify(d => d.UpdateUserPassword(It.IsAny<BOL.EmployeeKey>(), It.IsAny<string>()), Times.Never);
			Assert.IsNull(user);
			Assert.IsFalse(result);
		}
	}
}
