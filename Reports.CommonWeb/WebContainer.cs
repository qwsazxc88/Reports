using Castle.Core;
using Castle.MicroKernel.Registration;
using Reports.Core;
using Reports.Core.Services;

namespace Reports.CommonWeb
{
    public class WebContainer : CoreContainer
    {
        public override void AddComponents()
        {
            base.AddComponents();
            Register(Component.For(typeof(IHttpContextService)).ImplementedBy(typeof(HttpContextService))
                    .Named("IHttpContextService").LifeStyle.Is(type));
            //AddComponent("IHttpContextService", typeof(IHttpContextService), typeof(HttpContextService));
            Register(Component.For(typeof(ISessionManager)).ImplementedBy(typeof(WebSessionManager))
                    .Named("ISessionManager").LifeStyle.Is(type));
            //AddComponent("ISessionManager", typeof(ISessionManager), typeof(WebSessionManager));
            Register(Component.For(typeof(IWebSessionManager)).ImplementedBy(typeof(WebSessionManager))
                    .Named("IWebSessionManager").LifeStyle.Is(type));
            //AddComponent("IWebSessionManager", typeof (IWebSessionManager), typeof (WebSessionManager));
        }
    }
}