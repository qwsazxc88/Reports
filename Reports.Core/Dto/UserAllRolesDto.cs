using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class UserAllRolesDto
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string PeopleCode { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}
