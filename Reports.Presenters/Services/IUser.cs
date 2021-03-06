﻿using Reports.Core;

namespace Reports.Presenters.Services
{
    public interface IUser
    {
        int Id { get; set; }
        string Login { get; set; }
        string Name { get; set; }
        UserRole UserRole { get; set; }
        bool IsAdministrator { get; set; }

        string Serialize();

        //string[] Roles { get; set; }
        //IUser Deserialize(string data);
    }
}
