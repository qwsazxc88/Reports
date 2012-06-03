using Reports.Core.Domain;

namespace Reports.Core.Dto
{
    public class UsersListItemDto
    {
//		private int number;

        public UsersListItemDto(int id, /*int number,*/string fullName, string branch, string department,
                                string position, string login)
        {
            Id = id;
//			this.number = number;
            FullName = fullName;
            Position = position;
            Branch = branch;
            Department = department;
            Position = position;
            Login = login;
        }

        public UsersListItemDto(User user)
        {
            Id = user.Id;
            //number = number;
            FullName = user.FullName; //user.LastName+" "+user.FirstName+" "+user.MiddleName;
            //branch = user.Branch;
            //department = user.Department;
            //position = user.Position;
            Login = user.Login;
        }


        public int Id { get; set; }

        //public int Number
        //{
        //    get { return number; }
        //    set { number = value; }
        //}
        public string FullName { get; set; }

        public string Branch { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }

        public string Login { get; set; }
    }
}