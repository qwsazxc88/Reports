using System;
using System.Collections.Generic;
using System.Text;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IDBVersionDao : IDao<DBVersion>
    {
        Version GetDatabaseVersion();
    }
}
