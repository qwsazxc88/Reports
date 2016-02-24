using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace Reports.Core.Domain
{
    public class User : AbstractEntityWithVersion//AbstractUsedEntity
    {
        #region Constants

        //private const string UserRoleSeparator = ", ";

        public const int MaxGeneratedPasswordLength = 12;
        public const int MaxUIPasswordLength = 20;

        //private const string SuperAdminLogin = "admin";
        //private const string SuperAdminPassw = "admin";
        #endregion

        #region Fields
        //private string _firstName;
        //private string _lastName;
        //private string _middleName;
        //private string _branch;
        //private string _position;
        //private string _department;

        //private string _login;
        private string _password;
        //private bool _isAdministrator;
        //private bool _isFirstTimeLogin = true;
        //private bool _isActive = true;
        //private DateTime _dateCreated;
        //private DateTime _date;
        //private bool _canAnswer = true;
        //private DateTime? birthday;

        #endregion

        #region Properties
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string AlternativeMail { get; set; }
        #region Deleted
        //public const string FirstNameFieldName = "FirstName";
        //public virtual string FirstName { get; set; }
        //{
        //    get { return _firstName; }
        //    set { _firstName = value; }
        //}
        //public const string LastNameFieldName = "LastName";
        //public virtual string LastName { get; set; }
        //{
        //    get { return _lastName; }
        //    set { _lastName = value; }
        //}
        //public const string MiddleNameFieldName = "MiddleName";
        //public virtual string MiddleName { get; set; }
        //{
        //    get { return _middleName; }
        //    set { _middleName = value; }
        //}
        //public virtual string Branch
        //{
        //    get { return _branch; }
        //    set { _branch = value; }
        //}
        //public virtual string Position
        //{
        //    get { return _position; }
        //    set { _position = value; }
        //}
        //public virtual string Department
        //{
        //    get { return _department; }
        //    set { _department = value; }
        //}
        #endregion

        public virtual bool IsFirstTimeLogin { get; set; }
        //{
        //    get { return _isFirstTimeLogin; }
        //    set { _isFirstTimeLogin = value; }
        //}
        public virtual bool IsAdministrator { get; set; }
        //{
        //    get { return _isAdministrator; }
        //    set { _isAdministrator = value; }
        //}
        public virtual string CodePeople { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsNew { get; set; }
        public virtual bool IsMainManager { get; set; }
        //{
        //    get { return _isActive; }
        //    set { _isActive = value; }
        //}
        public virtual string Login { get; set; }
        //{
        //    get { return _login; }
        //    set { _login = value; }
        //}

        public virtual string Code { get; set; }
        public virtual int? ContractType { get; set; }

        public virtual string Password
        {
            get { return _password; }
            set {SetPassword(value);}
        }
        public virtual IList<VacationSaldo> VacationSaldo { get; set; }
        public virtual DateTime? DateAccept { get; set; }
        //{
        //    get { return _dateCreated; }
        //    set { _dateCreated = value; }
        //}
        public virtual DateTime? DateRelease { get; set; }

        public virtual string Comment { get; set; }

        #region Deleted
        //{
        //    get { return _date; }
        //    set { _date = value; }
        //}
        //public virtual bool CanAnswer
        //{
        //    get { return _canAnswer; }
        //    set { _canAnswer = value; }
        //}
        //public virtual DateTime? Birthday
        //{
        //    get { return birthday; }
        //    set { birthday = value; }
        //}

        //public virtual Role Role { get; set; }
        #endregion

        public virtual int RoleId { get; set; }
        public virtual UserRole UserRole 
        { 
            get { return (UserRole)RoleId; }
        }
        
        // Extended roles
        public virtual IList<ClearanceChecklistRoleRecord> ClearanceChecklistRoleRecords { get; set; }
        public virtual IList<ManualRoleRecord> ManualRoleRecords { get; set; }

        //public virtual bool IsUserResponser
        //{
        //    get { return false; }
        //}

        public virtual User Manager { get; set; }
        //public virtual User PersonnelManager { get; set; }
        public virtual IList<User> Personnels { get; set; }
        public virtual IList<AcceptRequestDate> AcceptRequests { get; set; }
        //public virtual User BudgetManager { get; set; }
        //public virtual User OutsourcingManager { get; set; }

        public virtual Organization Organization { get; set; }
        //public virtual Department Department { get; set; }
        public virtual Position Position { get; set; }
        public virtual Department Department { get; set; }
        public virtual Schedule HoursType { get; set; }
        public virtual string Cnilc { get; set; }
        public virtual string Address { get; set; }
        public virtual IList<Dismissal> Dismissals { get; set; }
        public virtual string LoginAd { get; set; }
        public virtual decimal? Rate { get; set; }
        public virtual bool GivesCredit { get; set; }
        public virtual int? Level { get; set; }
        public virtual int? Grade { get; set; }
        public virtual bool? ExperienceIn1C { get; set; }
        public virtual bool? IsFixedTermContract { get; set; }
        public virtual string AccessGroupCode { get; set; }
        public virtual bool? IsPregnant { get; set; }
        public virtual int? SEPId { get; set; }
        /// <summary>
        /// Постоянное место работы в штатной расстановке.
        /// </summary>
        public virtual StaffEstablishedPostUserLinks RegularEstablishedPostUserLinks { get; set; }
        /// <summary>
        /// Временное место работы в штатной расстановке.
        /// </summary>
        public virtual StaffEstablishedPostUserLinks TempEstablishedPostUserLinks { get; set; }
        /// <summary>
        /// Надбавки
        /// </summary>
        public virtual IList<StaffPostChargeLinks> StaffPostCharges { get; set; }
        /// <summary>
        /// Штатная единица.
        /// </summary>
        //public virtual StaffEstablishedPost StaffEstablishedPost { get; set; }
        /// <summary>
        /// Северные надбавки.
        /// </summary>
        public virtual IList<StaffUserNorthAdditional> UserNorthAdditionals { get; set; }
        /// <summary>
        /// Отпуска по уходу за ребенком.
        /// </summary>
        public virtual IList<ChildVacation> ChildVacation { get; set; }
        /// <summary>
        /// Кадровые перемещения сотрудника.
        /// </summary>
        public virtual IList<StaffMovements> StaffMovements { get; set; }
        /// <summary>
        /// Права членов правления для согласования заявок в штатном расписании по принадлежности к подразделениям.
        /// </summary>
        public virtual IList<DirectorsRight> DirectorsRight { get; set; }
        #endregion

        #region Constructors

        public User()
        {
            this.ClearanceChecklistRoleRecords = new List<ClearanceChecklistRoleRecord>();
            this.Personnels = new List<User>();
        }

        public User(/*string firstName, string middleName, string lastName,*/
            string name,string code) : this()
        {
            //FirstName = firstName;
            //LastName = lastName;
            //MiddleName = middleName;
            Name = name;
            Code = code;

            //_firstName = firstName;
            //_middleName = middleName;
            //_lastName = lastName;
            //_branch = branch;
            //_department = department;
            //_position = position;
        }

        #endregion

        #region Methods

		public virtual string FullName
		{
			get { return GetFullName(Name/*LastName, FirstName, MiddleName*/); }
		}
        public static string GetFullName(/*string lastName,string firstName,string middleName*/string name)
        {
            //string result = "";
            //if (!string.IsNullOrEmpty(lastName))
            //    result = lastName;
            //if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(firstName))
            //    result += " ";
            //if (!string.IsNullOrEmpty(firstName))
            //    result += firstName;
            //if (!string.IsNullOrEmpty(middleName))
            //    result += " " + middleName;//_middleName.Substring(0,1).ToUpper()+".";
            //return result;
            return name;
        }
        #region Validate Password Methods

        public virtual string GenerateNewPassword()
        {
            string guidResult = Guid.NewGuid().ToString();

            guidResult = guidResult.Replace("-", string.Empty);

            string unhashedPwd = guidResult.Substring(0, MaxGeneratedPasswordLength);

//            SetPassword(unhashedPwd);
            return unhashedPwd;
        }


        public virtual bool PasswordIsValid(string unhashedPassw)
        {
//            string pwd = PasswordManager.CreatePasswordHash(unhashedPassw, _salt);
			return _password == unhashedPassw;
        }

        private void SetPassword(string unhashedPassw)
        {
//            _salt = PasswordManager.CreateSalt();
			_password = unhashedPassw;//PasswordManager.CreatePasswordHash(unhashedPassw, _salt);
        }
        #endregion

        #endregion

        #region System.Object overrides

        public override string ToString()
        {
            return Id.ToString();
        }

        public override bool Equals(object obj)
        {
            User user = obj as User;
            if (user != null)
                return Id == user.Id;

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

        #region MetaData

        //private static int _maxPasswordLength = 10;
        //private static int _maxDegreeLength;
		//private static int _maxEmailLength;
		//private static int _maxPhone1Length;
		//private static int _maxPhone2Length;
		//private static int _maxAddressLength;
		//private static int _maxMasterCustomerIdLength;
		//private static int _maxSubCustomerIdLength;

		//private static string _phoneRegEx = @"([\ \-\)\(\d]?([e|E][x|X][t|T])?)*";//" @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}( x\d{0,})?";
		//private static string _emailRegEx = @"^[\w-]+(\.[\w-]+)*@([a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*?\.[a-zA-Z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$";//@".*@.*\..*";

		private const string _passwordRegEx = @"[0-9A-Za-z]{6}[0-9A-Za-z]*";//" @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}( x\d{0,})?";

        public static int MaxLoginLength { get; internal set; }

        /*public static int MaxPasswordLength
        {
            get { return _maxPasswordLength; }
            internal set { _maxPasswordLength = value; }
        }*/

        //public static int MaxFirstNameLength { get; internal set; }

        //public static int MaxMiddleNameLength { get; internal set; }

        //public static int MaxLastNameLength { get; internal set; }

        //public static int MaxDegreeLength
		//{
		//    get { return _maxDegreeLength; }
		//    internal set { _maxDegreeLength = value; }
		//}

		//public static int MaxEmailLength
		//{
		//    get { return _maxEmailLength; }
		//    internal set { _maxEmailLength = value; }
		//}

		//public static int MaxPhone1Length
		//{
		//    get { return _maxPhone1Length; }
		//    internal set { _maxPhone1Length = value; }
		//}

		//public static int MaxPhone2Length
		//{
		//    get { return _maxPhone2Length; }
		//    internal set { _maxPhone2Length = value; }
		//}

		//public static int MaxAddressLength
		//{
		//    get { return _maxAddressLength; }
		//    internal set { _maxAddressLength = value; }
		//}

		//public static int MaxMasterCustomerIdLength
		//{
		//    get { return _maxMasterCustomerIdLength; }
		//    internal set { _maxMasterCustomerIdLength = value; }
		//}

		//public static int MaxSubCustomerIdLength
		//{
		//    get { return _maxSubCustomerIdLength; }
		//    internal set { _maxSubCustomerIdLength = value; }
		//}
		//public static string PhoneRegExt
		//{
		//    get { return _phoneRegEx; }
		//}

		//public static string EMailRegExt
		//{
		//    get { return _emailRegEx; }
		//}
		public static string PasswordRegEx
		{
			get { return _passwordRegEx; }
		}
        #endregion
    }
}