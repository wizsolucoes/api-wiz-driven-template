﻿using Microsoft.EntityFrameworkCore.Storage;
using Wiz.Template.API.Infra.Context;
using System;

namespace Wiz.Template.API.Infra.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EntityContext _entityContext;
        private IDbContextTransaction _transaction;
        private bool disposedValue = false;

        public UnitOfWork(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public int Commit()
        {
            return _entityContext.SaveChanges();
        }
        public void BeginTransaction()
        {
            _transaction = _entityContext.Database.BeginTransaction();
        }
        public void BeginCommit()
        {
            _transaction.Commit();
        }
        public void BeginRollback()
        {
            _transaction.Rollback();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && _transaction != null)
                    _transaction.Dispose();

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}