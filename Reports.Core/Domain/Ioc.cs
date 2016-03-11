using System;
//using Acr3S.Core.Properties;

namespace Reports.Core
{
    public static class Ioc
    {
        private static CoreContainer _container;

        public static T Resolve<T>()
        {
            if (_container == null)
                throw new InvalidOperationException(Resources.Ioc_NotInitialized);

            return _container.Resolve<T>();
        }

        public static T Resolve<T>(string key)
        {
             if (_container == null)
                throw new InvalidOperationException(Resources.Ioc_NotInitialized);

            return _container.Resolve<T>(key);
        }
        public static CoreContainer Default
        {
            get { return _container; }
        }

        public static CoreContainer Container
        {
            set
            {
                _container = value;
                if (_container != null)
                    _container.Initialize();
            }
        }
    }
}