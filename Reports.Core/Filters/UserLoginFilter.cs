using System;
using System.Collections.Generic;
using System.Text;

namespace Reports.Core.Filters
{
    public class UserLoginFilter : BaseListFilter
    {
        #region Fields
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private DateTime? dateFrom;
        private DateTime? dateTo;
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
        #endregion
        #region Constructors

        public UserLoginFilter()
        {
        }

        public UserLoginFilter(string firstName, string middleName, string lastName, 
            DateTime? dateFrom,DateTime? dateTo,
			string sortExpression, bool sortAscending, int maxResults, int firstResult)
        {
            _firstName = firstName;
            _middleName = middleName;
            _lastName = lastName;
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
            SortExpression = sortExpression;
            SortAscending = sortAscending;
            MaxResults = maxResults;
            FirstResult = firstResult;
        }
        #endregion
    }
}
