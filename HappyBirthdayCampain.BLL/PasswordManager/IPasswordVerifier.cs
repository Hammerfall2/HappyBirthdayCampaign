using HappyBirthdayCampain.BOL;

namespace HappyBirthdayCampain.BLL.PasswordManager
{
    public interface IPasswordVerifier
    {
        /// <summary>
        /// Verify Password 
        /// </summary>
        /// <param name="employeeDTO"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool VerifyPassword(EmployeeDTO employeeDTO, string password);
    }
}
