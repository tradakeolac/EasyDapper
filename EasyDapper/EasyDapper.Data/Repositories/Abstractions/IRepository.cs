namespace EasyDapper.Data.Repositories.Abstractions
{
    using System;
    using System.Linq.Expressions;
    using Entities;
    using EasyDapper.Data.Specifications;

    public interface IRepository : IDataLoaderRepository
    {
        /// <summary> 
        /// Adds the specified entity. 
        /// </summary> 
        /// <typeparam name="TEntity">The type of the entity.</typeparam> 
        /// <param name="entity">The entity.</param> 
        void Add<TEntity>(TEntity entity) where TEntity : class;
        
        /// <summary> 
        /// Deletes the specified entity. 
        /// </summary> 
        /// <typeparam name="TEntity">The type of the entity.</typeparam> 
        /// <param name="entity">The entity.</param> 
        void Delete<TEntity>(TEntity entity) where TEntity : class;

        /// <summary> 
        /// Deletes one or many entities matching the specified criteria 
        /// </summary> 
        /// <typeparam name="TEntity">The type of the entity.</typeparam> 
        /// <param name="criteria">The criteria.</param> 
        void Delete<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        /// <summary> 
        /// Deletes entities which satify specificatiion 
        /// </summary> 
        /// <typeparam name="TEntity">The type of the entity.</typeparam> 
        /// <param name="criteria">The criteria.</param> 
        void Delete<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;

        /// <summary> 
        /// Updates changes of the existing entity.  
        /// The caller must later call SaveChanges() on the repository explicitly to save the entity to database 
        /// </summary> 
        /// <typeparam name="TEntity">The type of the entity.</typeparam> 
        /// <param name="entity">The entity.</param> 
        void Update<TEntity>(TEntity entity) where TEntity : class;
    }
}
