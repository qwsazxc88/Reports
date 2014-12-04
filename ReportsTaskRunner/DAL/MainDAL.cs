using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace ReportsTaskRunner.DAL
{
    class MainDAL
    {
        public static ReportsDataContext db { get; private set; }

        static MainDAL()
        {
            db = new ReportsDataContext();
        }
    }
}
