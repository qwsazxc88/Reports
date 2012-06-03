using System;
using System.Collections.Generic;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Services;
using Reports.Core.Utils;

namespace Reports.Core.Dao.Impl
{
    public class DBVersionDao : DefaultDao<DBVersion>, IDBVersionDao
    {
        #region Constructors
        public DBVersionDao(ISessionManager sessionManager): base(sessionManager)
        {

        }
        #endregion
        #region Methods
        public Version GetDatabaseVersion()
        {
                IList<DBVersion> list = FindAll();
                if (list.Count != 1)
                    throw new IncorrectDatabaseVersionException(Resources.IncorrectDatabaseVersion);
                return new Version(list[0].Version);
        }
        #endregion

    }
}
