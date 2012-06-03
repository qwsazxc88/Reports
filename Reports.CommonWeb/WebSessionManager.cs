using System;
using System.Reflection;
using System.Threading;
using log4net;
using NHibernate;
using Reports.Core;
using Reports.Core.Services;

namespace Reports.CommonWeb
{
    /// <summary>
    /// Manages NHibernate session in Http context;
    /// </summary>
    public class WebSessionManager : IWebSessionManager
    {
        public const string SessionStorageKey = "Acr3S.NHibernateSession";
        protected static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ISessionFactory _sessionFactory;
        private readonly IHttpContextService _httpContextService;

        public WebSessionManager(ISessionFactory sessionFactory, IHttpContextService httpContextService)
        {
            Validate.NotNull(sessionFactory, "sessionFactory");
            Validate.NotNull(sessionFactory, "httpContextService");

            _sessionFactory = sessionFactory;
            _httpContextService = httpContextService;
        }


        private ISession Storage
        {
            get { return (ISession) _httpContextService.Items[SessionStorageKey]; }
            set { _httpContextService.Items[SessionStorageKey] = value; }
        }

        public bool HasSession
        {
            get { return Storage != null; }
        }

        public ISession CurrentSession
        {
            get
            {
                ISession currentSession = Storage;
                if (currentSession == null)
                {
                    Log.DebugFormat("No current session, creating new per thread {0}",
                                    Thread.CurrentThread.ManagedThreadId);
                    currentSession = _sessionFactory.OpenSession();
                    Log.DebugFormat("New current session per thread {0} created {1}",
                            Thread.CurrentThread.ManagedThreadId, currentSession.GetHashCode());
                    try
                    {
                        currentSession.BeginTransaction();
                    }
                    catch (Exception)
                    {
                        currentSession.Close();
                        throw;
                    }
                }

                Storage = currentSession;
                return currentSession;
            }
        }

        public void CommitAndClose()
        {
            ISession currentSession = Storage;
            Storage = null;
            if (currentSession != null)
                try
                {
                    ITransaction tx = currentSession.Transaction;
                    if (tx != null && tx.IsActive)
                    {
                        if (!_httpContextService.HasError)
                        {
                            try
                            {
                                tx.Commit();
                                Log.DebugFormat("Commit transaction per session {0}, thread {1} success.",
                                                currentSession.GetHashCode(),
                                                Thread.CurrentThread.ManagedThreadId);
                            }
                            catch (Exception ex)
                            {
                                Log.Error("Error on tx.Commit()", ex);
                                try
                                {
                                    tx.Rollback();
                                }
                                catch (Exception rollEx)
                                {

                                    Log.Error("Error on tx.Rollback()", rollEx);
                                }
                                //throw;
                            }
                        }
                        else
                            tx.Rollback();

                    }
                }
                finally
                {
                    currentSession.Close();
                }
        }

        public void HandleEndRequest()
        {
            CommitAndClose();
        }
    }
}