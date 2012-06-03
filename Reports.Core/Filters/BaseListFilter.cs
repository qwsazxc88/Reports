using System;
using System.Collections.Generic;
using System.Text;

namespace Reports.Core.Filters
{
    public abstract class BaseListFilter
    {
        #region Fields

        private string _sortExpression;
        private bool _sortAscending = true;
        private int _maxResults = -1;
        private int _firstResult = -1;

        #endregion

        #region Properties

        public string SortExpression
        {
            get { return _sortExpression; }
            set { _sortExpression = value; }
        }

        public bool SortAscending
        {
            get { return _sortAscending; }
            set { _sortAscending = value; }
        }

        public int MaxResults
        {
            get { return _maxResults; }
            set { _maxResults = value; }
        }

        public int FirstResult
        {
            get { return _firstResult; }
            set { _firstResult = value; }
        }

        #endregion

        #region Methods

        #endregion

        #region Constructors

        #endregion

        #region Events

        #endregion

        #region Helpers

        #endregion

    }
}
