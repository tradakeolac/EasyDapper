namespace EasyDapper.Data.Dapper.Extensions
{
    using System;
    using System.Data;

    static class DbConnectionExtensions
    {
        /// <summary>
        /// Method to execute function inside open/close db transaction
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="dbConnection"/> is <c>null</c>.</exception>
        /// <exception cref="Exception"></exception>
        internal static TResult Run<TResult>(this IDbConnection dbConnection, Func<IDbConnection, TResult> func)
        {
            if (dbConnection == null)
                throw new ArgumentNullException(nameof(dbConnection), "The connection can not be null!");
            try
            {
                dbConnection.Open();
                return func(dbConnection);
            }
            catch(Exception ex)
            {
                // Log
                throw;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        /// <summary>
        /// Method to execute function inside open/close db transaction
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="action"></param>
        /// <exception cref="Exception"></exception>
        /// <exception cref="ArgumentNullException"><paramref name="dbConnection"/> is <c>null</c>.</exception>
        internal static void Run(this IDbConnection dbConnection, Action<IDbConnection> action)
        {
            if (dbConnection == null)
                throw new ArgumentNullException(nameof(dbConnection), "The connection can not be null!");

            try
            {
                dbConnection.Open();
                // Run action
                action(dbConnection);
            }
            catch(Exception ex)
            {
                // Log
                throw;
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
