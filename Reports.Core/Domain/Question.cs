using System;
using System.Text;
using Reports.Core.Utils;

namespace Reports.Core.Domain
{
    public class Question : AbstractUsedEntityWithVersion//AbstractUsedEntity
    {
        #region Constants
        #endregion

        #region Fields
        private User _user;
		private DateTime _date;
        private string _text;
        private User _answerUser;
		private DateTime? _answerDate;
        private string _answerText;


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
        public virtual string Text
		{
			get { return _text; }
			set { _text = value; }
		}
        public const string AnswerUserName = "AnswerUser";
        public virtual User AnswerUser
        {
            get { return _answerUser; }
            set { _answerUser = value; }
        }
		public virtual DateTime? AnswerDate
		{
			get { return _answerDate; }
			set { _answerDate = value; }
		}
        public virtual string AnswerText
		{
			get { return _answerText; }
			set { _answerText = value; }
		}
        #endregion

        #region Constructors

        public Question()
        {
        }

        public Question(User user,string text)
        {
            _user = user;
            _date = DateTime.Now;
            _text = text;
        }

        #endregion
        #region Methods
        #endregion

        #region System.Object overrides
        #endregion

        #region MetaData
        private static int _maxTextLength;
        private static int _maxAnswerLength;

        public static int MaxTextLength
        {
            get { return _maxTextLength; }
            internal set { _maxTextLength = value; }
        }
        public static int MaxAnswerLength
        {
            get { return _maxAnswerLength; }
            internal set { _maxAnswerLength = value; }
        }
        #endregion
    }
}