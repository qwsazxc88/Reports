using NHibernate;

namespace Reports.Core.Services
{
    public interface ISessionManager
    {
        ISession CurrentSession { get; }
    }
}