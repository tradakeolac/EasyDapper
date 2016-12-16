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
        internal static TResult Run<TResult>(this IDbConnection dbConnection, Func<IDbConnection, TResult> func)
        {
            dbConnection.Open();
            var result = func(dbConnection);
            dbConnection.Close();
            return result;
        }

        /// <summary>
        /// Method to execute function inside open/close db transaction
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="action"></param>
        internal static void Run(this IDbConnection dbConnection, Action<IDbConnection> action)
        {
            dbConnection.Open();
            // Run action
            action(dbConnection);

            dbConnection.Close();
        }
    }
}
