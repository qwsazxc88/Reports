using System;
using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IDao<TEntity, TIdentifier>
        where TEntity : IEntity<TIdentifier>
    {
        TEntity Load(TIdentifier id);
        TEntity Get(TIdentifier id);
        void Save(TEntity entity);
        void SaveAndFlush(TEntity entity);
        TEntity SaveFlush(TEntity entity);
        TEntity Merge(TEntity entity);
        TEntity MergeAndFlush(TEntity entity);
        void Flush();
        void Delete(TEntity entity);
        void Delete(TIdentifier entityId);
        TEntity FindById(TIdentifier id);
        IList<TEntity> FindAll();
        void RollbackTran();
    }

    public interface IDao<TEntity> : IDao<TEntity, int>
        where TEntity : IEntity<int>
    {
        TEntity Load(string id);
        TEntity FindById(string id);
        IList<TEntity> LoadAll();
        IList<TEntity> LoadAllSorted();
//        bool IsSameNameEntityExists(Type type,int entityId, string name);
//        ContentManagementRole GetUserRoles(User user, AbstractSecuredEntity item, Category cat);
    }

    public interface IUsedDao<TEntity> : IDao<TEntity>
    where TEntity : AbstractUsedEntity 
    {
        IList<TEntity> FindAllWithUsedFlag(string fkExistsViewName);
        TEntity FindByIdWithUsedFlag(int id, string fkExistsViewName);
    }

}