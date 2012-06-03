using System;
using System.Reflection;
using log4net;
using Reports.Core;
using Reports.Core.Domain;

namespace Reports.Presenters.Services.Impl
{
    [Serializable]
    public class UserDto : IUser
    {
        protected static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public UserRole UserRole { get; set; }
        public bool IsAdministrator { get; set; }

        public static IUser CreateUser(User user )
        {
            return new UserDto
                       {
                           Id = user.Id, 
                           Login = user.Login, 
                           Name = user.FullName, 
                           UserRole = user.UserRole,
                           IsAdministrator = user.IsAdministrator,
                       };
        }
        public string Serialize()
        {
            return Id+"$"+Login+"$"+Name+"$"+(int)UserRole+"$"+IsAdministrator;
        }
        public static IUser Deserialize(string data)
        {
            if(string.IsNullOrEmpty(data))
                throw new ArgumentException("Ошибка определения пользователя.");
            string[] parts = data.Split('$');
            if (parts.Length != 5)
            {
                Log.ErrorFormat("Ошибка определения пользователя: {0}",data);
                throw new ArgumentException("Ошибка определения пользователя.");
            }
            try
            {
                return new UserDto
                {
                    Id = int.Parse(parts[0]),
                    Login = parts[1],
                    Name = parts[2],
                    UserRole = (UserRole)int.Parse(parts[3]),
                    IsAdministrator = bool.Parse(parts[4]),
                };

            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Ошибка определения пользователя: {0}", data), ex);
                throw new ArgumentException("Ошибка определения пользователя.");
            }
        }
    }
}