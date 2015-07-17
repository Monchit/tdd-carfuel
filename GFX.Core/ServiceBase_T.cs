using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace GFX.Core
{
    public abstract class ServiceBase<T> : IService<T> where T : class
    {

        public ServiceBase()//Constructor
            : this(null)
        {
            //
        }
        public ServiceBase(RootClass root)//Constructor
        {
            this.Root = root;
        }

        public RootClass Root { get; set; }//Reference
        public DbContext Context { get; set; }//Reference

        // factory method
        public abstract IRepository<T> Repository { get; set; }//abstract must override

        public virtual IQueryable<T> All()//virtual overridable
        {
            return Query(_ => true);
        }

        public abstract T Find(params object[] keys);

        public virtual IQueryable<T> Query(Func<T, bool> predicate)
        {
            if (Repository == null) return null;
            return Repository.Query(predicate);
        }


        public virtual T Add(T item)
        {
            if (Repository == null) return null;
            return Repository.Add(item);
        }

        public virtual void Remove(T item)
        {
            if (Repository == null) return;
            Repository.Remove(item);
        }

        public virtual int SaveChanges()
        {
            if (Repository == null) return -1;
            return Repository.SaveChanges();
        }

        public virtual bool RequiresOwnDbContext//Default -> Share DbContext
        {
            get { return false; }
        }

        public virtual void SetModified(T item)
        {
            this.Context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual void SetAdded(T item)
        {
            this.Context.Entry(item).State = System.Data.Entity.EntityState.Added;
        }

    }
}