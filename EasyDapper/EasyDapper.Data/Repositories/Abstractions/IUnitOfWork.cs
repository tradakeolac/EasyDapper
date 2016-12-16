namespace EasyDapper.Data.Repositories.Abstractions
{
    using System;

    /// <summary> 
    /// The UnitOfWork interface. 
    /// </summary> 
    public interface IUnitOfWork : IDisposable
    {
        /// <summary> 
        /// Gets a value indicating whether is in transaction. 
        /// </summary> 
        bool IsInTransaction { get; }

        /// <summary> 
        /// The save changes. 
        /// </summary> 
        void SaveChanges();
        
        /// <summary> 
        /// The begin transaction. 
        /// </summary> 
        void BeginTransaction();
        
        /// <summary> 
        /// The roll back transaction. 
        /// </summary> 
        void RollBackTransaction();

        /// <summary> 
        /// The commit transaction. 
        /// </summary> 
        void CommitTransaction();
    }

}
