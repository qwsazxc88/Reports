using System;
using System.Text;
using Reports.Core.Utils;

namespace Reports.Core.Domain
{
    public class UserLogin : AbstractEntityWithVersion//AbstractUsedEntity
    {
        #region Constants
        #endregion

        #region Fields
        private User _user;
		private DateTime _date;

        #endregion

        #region Properties

        public const string UserName = "User";
        public virtual User User
        {
            get { return _user; }
            set { _user = value; }
        }
		public virtual DateTime Date
		{
			get { return _date; }
			set { _date = value; }
		}
        public virtual int? RoleId { get; set; }

        #endregion

        #region Constructors

        public UserLogin()
        {
        }

        public UserLogin(User user)
        {
            _user = user;
            _date = DateTime.Now;
            //RoleId = user.RoleId;
        }

        #endregion
        #region Methods
        #endregion

        #region System.Object overrides
        #endregion

        #region MetaData
        #endregion
    }
}