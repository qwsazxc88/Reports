
namespace Reports.Core.Filters
{
    public class UserListFilter : BaseListFilter
    {

        #region Fields

        private string _firstName;
        private string _middleName;
        private string _lastName;
		private string _branch;
		private string _position;
		private string _department;
        private bool _status;
        //private bool? _creationStatus;
		//private SystemRole? _systemRole;
		//private ContentManagementRole? _contentRole;

        #endregion

        #region Properties

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string MiddleName
        {
            get { return _middleName; }
            set { _middleName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

		public string Branch
		{
			get { return _branch; }
			set { _branch = value; }
		}
		public string Position
		{
			get { return _position; }
			set { _position = value; }
		}
		public string Department
		{
			get { return _department; }
			set { _department = value; }
		}
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

		//public bool? CreationStatus
		//{
		//    get { return _creationStatus; }
		//    set { _creationStatus = value; }
		//}

		//public SystemRole? SystemRole
		//{
		//    get { return _systemRole; }
		//    set { _systemRole = value; }
		//}

		//public ContentManagementRole? ContentRole
		//{
		//    get { return _contentRole; }
		//    set { _contentRole = value; }
		//}

        #endregion

        #region Methods

        #endregion

        #region Constructors

        public UserListFilter()
        {
        }

        public UserListFilter(string firstName, string middleName, string lastName, 
			//bool status, bool? creationStatus, SystemRole? systemRole, ContentManagementRole? contentRole, 
			string branch,string department,string position,
			string sortExpression, bool sortAscending, int maxResults, int firstResult)
        {
            _firstName = firstName;
            _middleName = middleName;
            _lastName = lastName;
			_branch = branch;
			_department = department;
			_position = position;
//            _status = status;

			//_creationStatus = creationStatus;
			//_systemRole = systemRole;
			//_contentRole = contentRole;
            SortExpression = sortExpression;
            SortAscending = sortAscending;
            MaxResults = maxResults;
            FirstResult = firstResult;
        }

        #endregion

        #region Events

        #endregion

        #region Helpers

        #endregion

    }
}
