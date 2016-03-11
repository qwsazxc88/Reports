using System;
using NHibernate;
using Configuration=NHibernate.Cfg.Configuration;

namespace Reports.Core
{
    /// <summary>
    /// Helper class managing global session factory instance
    /// </summary>
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static Configuration _configuration;
        private static object _syncRoot = new object();
        private static volatile bool _init;

        private static void Initialize()
        {
            if (! _init)
            {
                lock (_syncRoot)
                    if (! _init)
                    {
                        BuildSessionFactory();
                        _init = true;
                    }
            }
        }

        private static void BuildSessionFactory()
        {
			try
			{
				_configuration = new Configuration();
				_configuration.Configure();

				_sessionFactory = _configuration.BuildSessionFactory();
			}
			catch(Exception ex)
			{
				log4net.LogManager.GetLogger(typeof(NHibernateHelper)).Error(ex);
				throw; 
			}
        }

        public static ISessionFactory GetSessionFactory()
        {
            Initialize();
            return _sessionFactory;
        }

        public static Configuration GetConfiguration()
        {
            Initialize();
            return _configuration;
        }
    }
}