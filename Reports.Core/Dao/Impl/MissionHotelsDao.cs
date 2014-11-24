using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionHotelsDao : DefaultDao<MissionHotels>, IMissionHotelsDao
    {
        protected IUserDao userDao;
        public IUserDao UserDao
        {
            get { return Validate.Dependency(userDao); }
            set { userDao = value; }
        }

        public MissionHotelsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        protected const string sqlSelectForMHotelList = @"SELECT Id as Id, Name as Name, Account as Account FROM dbo.Contractor order by Name";


        /// <summary>
        /// Гостиницы.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="role"></param>
        /// <param name="Name"></param>
        /// <param name="Account"></param>
        /// <returns></returns>
        public IList<MissionHotelsDto> GetHotels(UserRole role,
                int Id,
                string Name,
                string Account)
        {
            string sqlQuery = sqlSelectForMHotelList;

            IQuery query = CreateHotelQuery(sqlQuery);
            IList<MissionHotelsDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(MissionHotelsDto))).List<MissionHotelsDto>();
            return documentList;
        }
        public virtual IQuery CreateHotelQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Account", NHibernateUtil.String);
        }
    }
}