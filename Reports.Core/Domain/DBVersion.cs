using System;
using System.Collections.Generic;
using System.Text;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Версия структуры БД
    /// </summary>
    public class DBVersion : AbstractEntity
    {
        #region Fields
        private string _version;
        #endregion
        #region Properties
        public virtual string Version
        {
            get { return _version; }
            set { _version = value; }
        }
        #endregion
        #region Constructors
        public DBVersion()
        {
        }
        public DBVersion(string version)
        {
            _version = version;
        }
        #endregion

    }
}
