using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class RequestAttachmentDao : DefaultDao<RequestAttachment>, IRequestAttachmentDao
    {
        public const string RootFilesFolderPath = "RootFilesFolderPath";
        public RequestAttachmentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region IRequestAttachmentDao Members

        public virtual RequestAttachment FindByRequestIdAndTypeId(int id, RequestAttachmentTypeEnum type)
        {
            ICriteria criteria = Session.CreateCriteria(typeof (RequestAttachment));
            criteria.Add(Restrictions.Eq("RequestId", id));
            criteria.Add(Restrictions.Eq("RequestType", (int)type));
            return (RequestAttachment) criteria.UniqueResult();
        }
        public virtual IList<RequestAttachment> FindManyByRequestIdAndTypeId(int id, RequestAttachmentTypeEnum type)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(RequestAttachment));
            criteria.Add(Restrictions.Eq("RequestId", id));
            criteria.Add(Restrictions.Eq("RequestType", (int)type));
            return criteria.List<RequestAttachment>();
        }
         public virtual int UpdateAttachement(int id,byte[] context)
        {
            string rootPath = GetRootPath();
            string name = GetFileName();
            string filePath = Path.Combine(rootPath, name);
            SaveFile(filePath, context);
            return Session.CreateSQLQuery
                (@"update [dbo].[RequestAttachment]  set Context=null,
                                        [FilePath]=:filepath
                                        where Id = :id").
                SetString("filepath", name).
                SetInt32("id", id).
                ExecuteUpdate();
        }
        /*public virtual int GetAttachmentsCount(int entityId)
        {
            return (int)Session.CreateCriteria(typeof(RequestAttachment))
            .Add(Restrictions.Eq("RequestId", entityId))
            .SetProjection(Projections.RowCount()).UniqueResult();
        }*/
         public virtual int DeleteForEntityId(int entityId,RequestAttachmentTypeEnum type)
        {
            List<RequestAttachment> list = FindManyByRequestIdAndTypeId(entityId, type).ToList();
            foreach (RequestAttachment attachment in list)
                DeleteWithFile(attachment);
            Session.Flush();
            return list.Count;
        }
        public virtual IList<IdContextDto> FindOld(int beforeId)
        {
            string sqlQuery = string.Format(@" select top {0} Id,Context from [dbo].[RequestAttachment] 
                where Id < {1}
                and Context is not null
                order by id desc", 6,beforeId);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Context", NHibernateUtil.BinaryBlob);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdContextDto))).List<IdContextDto>();
            //return Session.Query<RequestAttachment>().Where(x => x.Id < beforeId && x.Context != null)
            //    .OrderByDescending(a => a.Id).Take(100).ToList(); 
            //ICriteria criteria = Session.CreateCriteria(typeof(RequestAttachment));
            //criteria.Add(Restrictions.IsNotNull("Context"));
            //criteria.SetTimeout(600);
            //return criteria.List<RequestAttachment>();
        }
        public virtual List<RequestAttachment> FindOldEntities(int beforeId)
        {
            Log.DebugFormat("Before FindOld beforeId {0}", beforeId);
            return Session.Query<RequestAttachment>().Where(x => x.Id < beforeId && x.Context != null)
                    .OrderByDescending(a => a.Id).Take(100).ToList(); 
        }
//        public virtual int UpdateAttachement(int id,byte[] context)
//        {
//            const string sqlQuery =
//                @"delete from dbo.RequestAttachment
//                where RequestId = :entityId";
//            ISQLQuery query = Session.CreateSQLQuery(sqlQuery);
//            query.SetInt32("entityId", entityId);
//            return query.ExecuteUpdate();
//        }
        public IList<IdEntityIdDto> LoadAttachmentsForEntitiesIdsList(List<int> entityIds, RequestAttachmentTypeEnum type)
        {

            if (entityIds.Count == 0)
                return new List<IdEntityIdDto>();

            const string sqlQuery = @"select Id,RequestId as EntityId from [dbo].[RequestAttachment] where 
                              [RequestType] = :typeId and RequestId in (:entitiesList)";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
              AddScalar("Id", NHibernateUtil.Int32).
              AddScalar("EntityId", NHibernateUtil.Int32).
              SetInt32("typeId", (int)type).
              SetParameterList("entitiesList", entityIds);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdEntityIdDto))).List<IdEntityIdDto>();
        }
        public int DeleteAttachmentsForEntitiesIdsList(List<int> entityIds, RequestAttachmentTypeEnum type)
        {
            if (entityIds.Count == 0)
                return 0;
            List<RequestAttachment> entities = (from attach in Session.Query<RequestAttachment>()
                    where entityIds.Contains(attach.RequestId) && attach.RequestType == (int)type
                    select attach).ToList();
            foreach (RequestAttachment entity in entities)
                Delete(entity);
            Session.Flush();
            return entities.Count;
            /*const string sqlQuery = @"delete from [dbo].[RequestAttachment] where 
                              [RequestType] = :typeId and RequestId in (:entitiesList)";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                            SetInt32("typeId", (int)type).
                            SetParameterList("entitiesList", entityIds);
            return query.ExecuteUpdate();*/
//            return list.Count;
//            const string sqlQuery =
//                @"delete from dbo.RequestAttachment
//                where RequestId = :entityId";
//            ISQLQuery query = Session.CreateSQLQuery(sqlQuery);
//            query.SetInt32("entityId", entityId);
//            return query.ExecuteUpdate();
        }
        public virtual void Evict(RequestAttachment entity)
        {
            Session.Evict(entity);
        }
        public void SaveFileNotChangeAndFlush(RequestAttachment entity)
        {
            Session.Save(entity);
            Session.Flush();
        }
        public override void SaveAndFlush(RequestAttachment entity)
        {
            string rootPath = GetRootPath();
            string name = GetFileName();
            string filePath = Path.Combine(rootPath, name);
            entity.FileContext = entity.GetContext();
            if(!string.IsNullOrEmpty(entity.FilePath))
            {
                string oldFilePath = Path.Combine(rootPath, entity.FilePath);
                try
                {
                    File.Delete(oldFilePath);
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Cannot delete file {0}", oldFilePath), ex);
                }
            }
            SaveFile(filePath, entity.GetContext());
            entity.FilePath = name;
            entity.SetContext(null);
            Session.Save(entity);
            Session.Flush();
        }
        public virtual void DeleteWithFile(RequestAttachment entity)
        {
            if (entity.GetContext() == null)
            {
                string rootPath = GetRootPath();
                string filePath = Path.Combine(rootPath, entity.FilePath);
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Cannot delete file {0}", filePath), ex);
                }
            }
            Session.Delete(entity);
        }
        public override void Delete(int entityId)
        {
            RequestAttachment entity = Session.Load<RequestAttachment>(entityId);
            DeleteWithFile(entity);
            Session.Flush();
        }
        public override void Delete(RequestAttachment entity)
        {
            DeleteWithFile(entity);
            Session.Flush();
        }
        public override RequestAttachment Load(int id)
        {
            RequestAttachment entity = Session.Load<RequestAttachment>(id);
            if(entity.GetContext() == null)
            {
                string rootPath = GetRootPath();
                string filePath = Path.Combine(rootPath, entity.FilePath);
                entity.FileContext = GetFileContext(filePath);
            }
            return entity;
        }
        public void CloneAttach(int AttachId,int TargetRequestId)
        {
            string sql=@"INSERT INTO RequestAttachment
                            (Version, FileName, ContextType,Context,RequestId,RequestType,DateCreated,Description,CreatorRoleId,FilePath,DocumentsCount,UserId)
                            SELECT 1 as Version,
		                            FileName,
		                            ContextType,
		                            Context,
		                            {1},
		                            RequestType,
		                            DateCreated,
		                            Description,
		                            CreatorRoleId,
		                            FilePath,
		                            DocumentsCount,
		                            UserId
                            FROM RequestAttachment
                            Where id={0}";
            var query=Session.CreateSQLQuery(String.Format(sql, AttachId, TargetRequestId));
            query.List();
        }
        protected static string GetRootPath()
        {
            string rootPath = ConfigurationManager.AppSettings[RootFilesFolderPath];
            if(string.IsNullOrEmpty(rootPath))
                throw new ArgumentException(string.Format("Не найдена настройка {0} в конфигурационном файле",RootFilesFolderPath));
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);
            return rootPath;
        }
        protected static string GetFileName()
        {
            return string.Format("{0}.bin", Guid.NewGuid());
        }
        protected static void SaveFile(string path,byte[] context)
        {
            File.WriteAllBytes(path,context);
        }
        public static byte[] GetFileContext(string path)
        {
            return File.ReadAllBytes(path);
        }
        #endregion
    }
}