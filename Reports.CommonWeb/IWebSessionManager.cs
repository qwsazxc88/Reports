using Reports.Core.Services;

namespace Reports.CommonWeb
{
    public interface IWebSessionManager : ISessionManager
    {
        void HandleEndRequest();
    }
}