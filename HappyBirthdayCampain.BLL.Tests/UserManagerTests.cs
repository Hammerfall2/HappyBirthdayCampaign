using HappyBirthdayCampain.BOL;
using HappyBirthdayCampain.BLL.PasswordManager;
using HappyBirthdayCampain.DAL;
using Moq;
using NUnit.Framework;

namespace HappyBirthdayCampain.BLL.Tests
{
	[TestFixture]
	public partial class UserManagerTests
	{
		private Mock<ICampaignDataDal> _mockDataDal;
		private Mock<IBllLogger> _mockLogger;
        private Mock<IPasswordVerifier> _mockPassVerifier;
        private IUserManager _userManager;

		[SetUp]
		public void Setup()
		{
			_mockDataDal = new Mock<ICampaignDataDal>();
			_mockLogger = new Mock<IBllLogger>();
			_mockPassVerifier = new Mock<IPasswordVerifier>();
			_userManager = new UserManager(_mockDataDal.Object, _mockLogger.Object, _mockPassVerifier.Object);
		}

		[Test]
		public void LogInVerification_WhenValidCredentials_WithArgonPasswordInDB_ReturnsTrueAndUser()
		{
			// Arrange
			string userName = "testUser";
			string password = "test";
			PasswordType passType = PasswordType.Argon2;
			BOL.EmployeeDTO expectedUser = new BOL.EmployeeDTO()
			{
				UserName = userName,
				Password = "$argon2i$v=19$m=16,t=2,p=1$MldMRWRhVzd6VWdqWWJOdg$0Z2364HURtioAQ+OoCA5aA",
				PasswordType = passType
			};

			_mockDataDal.Setup(d => d.TryGetEmployeeByUsername(userName)).Returns(expectedUser);

			// Act
			bool result = _userManager.LogInVerification(userName, password, out BOL.EmployeeDTO user);

			// Assert
			Assert.IsTrue(result);
			Assert.AreEqual(expectedUser, user);
		}

		[Test]
		public void LogInVerification_WhenValidCredentials_WithPlainTextPasswordInDB_UpdatePasswordWithHash_ReturnsTrueAndUser()
		{
			// Arrange
			string userName = "testUser";
			string password = "test";
			string passwordHash = "$argon2i$v=19$m=16,t=2,p=1$MldMRWRhVzd6VWdqWWJOdg$0Z2364HURtioAQ+OoCA5aA";
			PasswordType passType = PasswordType.PlainText;
			BOL.EmployeeDTO expectedUser = new BOL.EmployeeDTO()
			{
				Key = new BOL.EmployeeKey(){Id=666},
				UserName = userName,
				Password = password,
				PasswordType=passType
			};

			_mockDataDal.Setup(d => d.TryGetEmployeeByUsername(userName)).Returns(expectedUser);
			_mockDataDal.Setup(d => d.UpdateUserPassword(expectedUser.Key, passwordHash));

			// Act
			bool result = _userManager.LogInVerification(userName, password, out BOL.EmployeeDTO user);

			// Assert
			Assert.IsTrue(result);
			Assert.AreEqual(expectedUser, user);
			_mockDataDal.Verify(d => d.UpdateUserPassword(It.IsAny<BOL.EmployeeKey>(), It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void LogInVerification_WhenInvalidUserName_ReturnsFalse()
		{
			// Arrange
			string userName = "testUser";
			string password = "testPassword";
			BOL.EmployeeDTO expectedUser = null;

			_mockDataDal.Setup(d => d.TryGetEmployeeByUsername(userName)).Returns(expectedUser);

			// Act
			bool result = _userManager.LogInVerification(userName, password, out BOL.EmployeeDTO user);

			// Assert
			Assert.IsFalse(result);
			Assert.IsNull(user);
		}

		[Test]
		public void LogInVerification_WhenEmptyUserName_ThrowsBllException()
		{
			// Arrange
			string userName = "";
			string password = "testPassword";

			// Act & Assert
			Assert.Throws<BllException>(() => _userManager.LogInVerification(userName, password, out BOL.EmployeeDTO user));
		}

		[Test]
		public void LogInVerification_WhenEmptyPassword_ThrowsBllException()
		{
			// Arrange
			string userName = "testUser";
			string password = "";

			// Act & Assert
			Assert.Throws<BllException>(() => _userManager.LogInVerification(userName, password, out BOL.EmployeeDTO user));
		}

		[Test]
		public void LogInVerification_WhenInvalidPassword_ReturnFalse()
		{
			// Arrange
			string userName = "testUser";
			string password = "testPassword";
			string hashedPassword = "hashedPassword";
			PasswordType passwordType = PasswordType.Argon2;
			BOL.EmployeeDTO expectedUser = new BOL.EmployeeDTO { Password = hashedPassword, PasswordType=passwordType };

			_mockDataDal.Setup(d => d.TryGetEmployeeByUsername(userName)).Returns(expectedUser);

			// Act
			bool result = _userManager.LogInVerification(userName, password, out BOL.EmployeeDTO user);

			// Assert
			Assert.IsFalse(result);
			Assert.IsNull(user);
		}

		[Test]
		public void GetUser_WhenValidKey_ReturnsEmployee()
		{
			// Arrange
			BOL.EmployeeKey key = new BOL.EmployeeKey();
			BOL.EmployeeDTO expectedUser = new BOL.EmployeeDTO();

			_mockDataDal.Setup(d => d.GetEmployeeById(key)).Returns(expectedUser);

			// Act
			BOL.EmployeeDTO result = _userManager.GetUser(key);

			// Assert
			Assert.AreEqual(expectedUser, result);
		}

		[Test]
		public void GetUser_WhenInvalidKey_ThrowsBllException()
		{
			// Arrange
			BOL.EmployeeKey key = new BOL.EmployeeKey();

			_mockDataDal.Setup(d => d.GetEmployeeById(key)).Throws<DalException>();

			// Act & Assert
			Assert.Throws<BllException>(() => _userManager.GetUser(key));
		}
	}
}
