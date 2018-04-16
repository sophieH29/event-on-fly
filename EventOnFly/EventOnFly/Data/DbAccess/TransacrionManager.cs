﻿using System;
using System.Threading.Tasks;
using System.Transactions;

namespace EventOnFly.Data.DbAccess
{
    public interface ITransacrionManager
    {
        void ExecuteWithinTransaction(Func<Task> action);

        Task<T> ExecuteWithinTransaction<T>(Func<Task<T>> action);
    }

    public class TransacrionManager: ITransacrionManager
    {
        public async void ExecuteWithinTransaction(Func<Task> action)
        {
            var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                await action();
                scope.Complete();
            }
            finally
            {
                scope.Dispose();
            }
        }

        public async Task<T> ExecuteWithinTransaction<T>(Func<Task<T>> action)
        {
            var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var result = await action();
                scope.Complete();
                return result;
            }
            finally
            {
                scope.Dispose();
            }
        }
    }
}