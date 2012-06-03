using System;
using System.Collections.Generic;
using System.Text;
using Reports.Core.Domain;

namespace Reports.Core.Filters
{
    public class QuestionFilter : BaseListFilter
    {
        #region Fields
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private DateTime? dateFrom;
        private DateTime? dateTo;
        private QuestionTypes type;
        private int userId;
        private bool  canAnswer;
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
        public DateTime? DateFrom
        {
            get { return dateFrom; }
            set { dateFrom = value; }
        }
        public DateTime? DateTo
        {
            get { return dateTo; }
            set { dateTo = value; }
        }
        public QuestionTypes Type
        {
            get { return type; }
            set { type = value; }
        }
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public bool CanAnswer
        {
            get { return canAnswer; }
            set { canAnswer = value; }
        }
        #endregion
                #region Constructors

        public QuestionFilter()
        {
        }

        public QuestionFilter(string firstName, string middleName, string lastName,
            DateTime? dateFrom, DateTime? dateTo, QuestionTypes type,int userId,bool canAnswer,
			string sortExpression, bool sortAscending, int maxResults, int firstResult)
        {
            _firstName = firstName;
            _middleName = middleName;
            _lastName = lastName;
			this.dateFrom = dateFrom;
            this.dateTo = dateTo;
            this.type = type;
            this.userId = userId;
            SortExpression = sortExpression;
            SortAscending = sortAscending;
            MaxResults = maxResults;
            FirstResult = firstResult;
            this.canAnswer = canAnswer;
        }

        #endregion

    }
}
