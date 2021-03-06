using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using System.Linq.Expressions;
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
        void DeleteAndFlush(TEntity entity);
        void Delete(TIdentifier entityId);
        void DeleteAndFlush(TIdentifier entityId);
        TEntity FindById(TIdentifier id);
        IList<TEntity> FindAll();
        void RollbackTran();
        void BeginTran();
        void CommitTran();
    }

    public interface IDao<TEntity> : IDao<TEntity, int>
        where TEntity : IEntity<int>
    {
        TEntity Load(string id);
        TEntity FindById(string id);
        void Update(Func<TEntity, bool> predicate, Action<TEntity> action);
        IList<TEntity> Find(Func<TEntity, bool> predicate);
        IList<TEntity> QueryExpression(Expression<Func<TEntity, bool>> predicate);
        IList<TEntity> LoadAll();
        IList<TEntity> LoadAllSorted();
//        bool IsSameNameEntityExists(Type type,int entityId, string name);
//        ContentManagementRole GetUserRoles(User user, AbstractSecuredEntity item, Category cat);
    }
    public interface IDaoSorted<TEntity> : IDao<TEntity>
        where TEntity : IEntity<int>,ISortOrder
    {
        List<TEntity> LoadAllSortedByOrder();
    }

    public interface IUsedDao<TEntity> : IDao<TEntity>
    where TEntity : AbstractUsedEntity 
    {
        IList<TEntity> FindAllWithUsedFlag(string fkExistsViewName);
        TEntity FindByIdWithUsedFlag(int id, string fkExistsViewName);
    }

}