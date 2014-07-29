using System;
using System.Configuration;
using System.IO;
using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Enum;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class RequestPrintFormDao : DefaultDao<RequestPrintForm>, IRequestPrintFormDao
    {
        public const string RootPrintFormFilesFolderPath = "RootPrintFormFilesFolderPath";
        public RequestPrintFormDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual RequestPrintForm FindByRequestAndTypeId(int requestId, RequestPrintFormTypeEnum typeId)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(RequestPrintForm));
            criteria.Add(Restrictions.Eq("RequestId", requestId))
                    .Add(Restrictions.Eq("RequestTypeId",(int)typeId));
            RequestPrintForm entity = (RequestPrintForm)criteria.UniqueResult();
            if (entity == null)
                return entity;
            if (entity.Context == null)
            {
                string rootPath = GetRootPath();
                string filePath = Path.Combine(rootPath, entity.FilePath);
                entity.Context = RequestAttachmentDao.GetFileContext(filePath);
            }
            return entity;
        }
        protected static string GetRootPath()
        {
            string rootPath = ConfigurationManager.AppSettings[RootPrintFormFilesFolderPath];
            if (string.IsNullOrEmpty(rootPath))
                throw new ArgumentException(string.Format("Не найдена настройка {0} в конфигурационном файле", RootPrintFormFilesFolderPath));
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);
            return rootPath;
        }
    }
}