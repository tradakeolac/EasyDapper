namespace EasyDapper.Data.Repositories.Abstractions
{
    using Criteria;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using EasyDapper.Data.Specifications;

    public interface IDataLoaderRepository
    {
        /// <summary> 
        /// Finds entities based on provided criteria. 
        /// </summary> 
        /// <typeparam name="TEntity">The type of the entity.</typeparam> 
        /// <param name="criteria">The criteria.</param> 
        /// <returns></returns> 
        IEnumerable<TEntity> Find<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;

        /// <summary> 
        /// Finds entities based on provided criteria. 
        /// </summary> 
        /// <typeparam name="TEntity">The type of the entity.</typeparam> 
        /// <param name="criteria">The criteria.</param> 
        /// <returns></returns> 
        IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        /// <summary> 
        /// Finds one entity based on provided criteria. 
        /// </summary> 
        /// <typeparam name="TEntity">The type of the entity.</typeparam> 
        /// <param name="criteria">The criteria.</param> 
        /// <returns></returns> 
        TEntity FindOne<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;

        /// <summary> 
        /// Finds one entity based on provided criteria. 
        /// </summary> 
        /// <typeparam name="TEntity">The type of the entity.</typeparam> 
        /// <param name="criteria">The criteria.</param> 
        /// <returns></returns> 
        TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById<TEntity>(object id) where TEntity : class;

        /// <summary>
        /// Get result with paging
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> criteria, int page, int pageSize) where TEntity : class;

        /// <summary>
        /// Get result with paging
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> criteria, IList<ISortCriteria> sorts, int page, int pageSize) where TEntity : class;

        /// <summary>
        /// Get result with paging
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get<TEntity>(ISpecification<TEntity> criteria, int page, int pageSize) where TEntity : class;

        /// <summary>
        /// Get result with paging
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get<TEntity>(ISpecification<TEntity> criteria, IList<ISortCriteria> sorts, int page, int pageSize) where TEntity : class;

        /// <summary> 
        /// Gets all. 
        /// </summary> 
        /// <typeparam name="TEntity">The type of the entity.</typeparam> 
        /// <returns></returns> 
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;
        
        /// <summary> 
        /// Counts the specified entities. 
        /// </summary> 
        /// <typeparam name="TEntity">The type of the entity.</typeparam> 
        /// <returns></returns> 
        int Count<TEntity>() where TEntity : class;

        /// <summary> 
        /// Counts entities with the specified criteria. 
        /// </summary> 
        /// <typeparam name="TEntity">The type of the entity.</typeparam> 
        /// <param name="criteria">The criteria.</param> 
        /// <returns></returns> 
        int Count<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        /// <summary> 
        /// Counts entities satifying specification. 
        /// </summary> 
        /// <typeparam name="TEntity">The type of the entity.</typeparam> 
        /// <param name="criteria">The criteria.</param> 
        /// <returns></returns> 
        int Count<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;

    }
}
