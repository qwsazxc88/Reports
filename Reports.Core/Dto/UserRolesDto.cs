using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dto
{
    public class UserRolesDto
    {
        public User user { get; set; }
        public List<UserRole> roles { get; set; }
    }
}