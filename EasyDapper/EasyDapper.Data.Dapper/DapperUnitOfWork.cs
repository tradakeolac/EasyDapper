namespace EasyDapper.Data.Dapper
{
    using System;
    using System.Data;
    using EasyDapper.Data.Repositories.Abstractions;

    public class DapperUnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public DapperUnitOfWork(IDbConnection connection)
        {
            this._connection = connection;
        }
        
        public bool IsInTransaction
        {
            get
            {
                return this._transaction != null;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        
        public void SaveChanges()
        {
            this._transaction.Commit();
        }

        public void BeginTransaction()
        {
            this._transaction = _connection.BeginTransaction();
        }

        public void RollBackTransaction()
        {
            this._transaction.Rollback();
        }

        public void CommitTransaction()
        {
            this._transaction.Commit();
        }        
    }
}
