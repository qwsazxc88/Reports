using System;
using System.Collections.Generic;
using System.Text;
using Reports.Core.Domain;

namespace Reports.Core.Dto
{
    public class UserLoginItemDto
    {
        //private string firstName;
        //private string lastName;
        //private string middleName;
        private int id;
        private string fullName;
        private DateTime date;
        private int userId;

        public UserLoginItemDto(UserLogin item)
		{
            id = item.Id;
			fullName = item.User.FullName;
            userId = item.User.Id;
            date = item.Date;
		}
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        //public string FirstName
        //{
        //    get { return firstName; }
        //    set { firstName = value; }
        //}
        //public string LastName
        //{
        //    get { return lastName; }
        //    set { lastName = value; }
        //}
        //public string MiddleName
        //{
        //    get { return middleName; }
        //    set { middleName = value; }
        //}
    }
}
