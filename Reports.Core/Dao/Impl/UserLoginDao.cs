using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Filters;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class UserLoginDao : DefaultDao<UserLogin>, IUserLoginDao
    {
        public const string LastNameSortFieldName = "LastName";
        public const string FirstNameSortFieldName = "FirstName";
        public const string MiddleNameSortFieldName = "MiddleName";

        //private const string ObjectPropertyFormat = "{0}.{1}";
        private const string UserLoginAlias = "UserLogin";
        private const string UserAlias = "users";

        public UserLoginDao(ISessionManager sessionManager): base(sessionManager)
        {

        }
        public IList<UserLoginItemDto> FindByFilter(UserLoginFilter filter, out int count)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(UserLogin),UserLoginAlias);
            ICriteria criteria2 = Session.CreateCriteria(typeof(UserLogin),UserLoginAlias);
            Junction criterion = Restrictions.Conjunction();
            if(filter.DateFrom.HasValue)
                criterion.Add(Restrictions.Ge("Date", filter.DateFrom.Value));
            if (filter.DateTo.HasValue)
                criterion.Add(Restrictions.Le("Date", filter.DateTo.Value.Date.AddDays(1).AddMilliseconds(-1)));

            criteria.CreateAlias(string.Format(ObjectPropertyFormat, UserLoginAlias, UserLogin.UserName), UserAlias,JoinType.InnerJoin);
            criteria2.CreateAlias(string.Format(ObjectPropertyFormat, UserLoginAlias, UserLogin.UserName), UserAlias, JoinType.InnerJoin);

            if (!string.IsNullOrEmpty(filter.FirstName) || 
                !string.IsNullOrEmpty(filter.MiddleName) ||
                !string.IsNullOrEmpty(filter.LastName))
            {
                Junction filterUser = Restrictions.Conjunction();
                if (!string.IsNullOrEmpty(filter.FirstName))
                    filterUser.Add(Restrictions.Like(string.Format(ObjectPropertyFormat, UserAlias, "Name"), filter.FirstName,MatchMode.Start));
                //if(!string.IsNullOrEmpty(filter.LastName))
                //    filterUser.Add(Restrictions.Like(string.Format(ObjectPropertyFormat, UserAlias, User.LastNameFieldName), filter.LastName, MatchMode.Start));
                //if(!string.IsNullOrEmpty(filter.MiddleName))
                //    filterUser.Add(Restrictions.Like(string.Format(ObjectPropertyFormat, UserAlias, User.MiddleNameFieldName), filter.MiddleName, MatchMode.Start));
                criterion.Add(filterUser);
            }
            criteria.Add(criterion);
            criteria2.Add(criterion);
            criteria2.SetProjection(Projections.RowCount());
            count = (int)criteria2.UniqueResult();
            if (filter.FirstResult > count) filter.FirstResult = 0;
            //criteria.Add(criterion);
            if (filter.MaxResults != -1)
                criteria.SetMaxResults(filter.MaxResults);
            if (filter.FirstResult != -1)
                criteria.SetFirstResult(filter.FirstResult);
            if (!string.IsNullOrEmpty(filter.SortExpression))
            {
                //if (filter.SortExpression.CompareTo(LastNameSortFieldName) == 0)
                //{
                //    criteria.AddOrder(new Order(string.Format(ObjectPropertyFormat, UserAlias, User.LastNameFieldName), filter.SortAscending));
                //    criteria.AddOrder(new Order(string.Format(ObjectPropertyFormat, UserAlias, User.FirstNameFieldName), filter.SortAscending));
                //    criteria.AddOrder(new Order(string.Format(ObjectPropertyFormat, UserAlias, User.MiddleNameFieldName), filter.SortAscending));
                //}
                //else
                    criteria.AddOrder(new Order(filter.SortExpression, filter.SortAscending));
            }
            IList<UserLogin> list = criteria.List<UserLogin>();
            IList<UserLoginItemDto> result = new List<UserLoginItemDto>();
            if (list.Count <= 0)
                return result;
            foreach (UserLogin user in list)
            {
                UserLoginItemDto dto = new UserLoginItemDto(user);
                result.Add(dto);
            }
            return result;
        }

    }
}
