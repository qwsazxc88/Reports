using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dao.Impl;
using Reports.Core.Dto;
using NHibernate.Criterion;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class NewsDao: DefaultDao<News>, INewsDao
    {
        public NewsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<NewsDto> GetNews(int page, int count)
        {
            var crit = Session.CreateCriteria<News>();
            crit.Add(Restrictions.Eq("IsVisible", true));
            crit.AddOrder(Order.Desc("PostDate"));
            var result =crit.List<News>().Skip(page * count).Take(count);
            return result.Select(x => new NewsDto { Header = x.Header, Text = x.Text, PostDate = x.PostDate.ToShortDateString() }).ToList();
        }
    }
}
