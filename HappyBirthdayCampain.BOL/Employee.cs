//using System.Data.Entity.Spatial;

namespace HappyBirthdayCampain.BOL
{

	public partial class EmployeeKey
    {
        public int Id { get; set; }
    }

	public partial class EmployeeDTO
    {
        public EmployeeKey Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

}
