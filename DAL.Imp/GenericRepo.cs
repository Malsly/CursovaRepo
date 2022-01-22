using DAL.Abs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Impl
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public CrosswordContext Context { get; set; }
        public System.Data.Entity.DbSet<TEntity> DbSet { get; set; }

        public GenericRepository(CrosswordContext context)
        {
            this.Context = context;
            this.DbSet = (context as CrosswordContext).Set<TEntity>();
        }

        public void Delete(int id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }
        public void Delete(int? id)
        {
            if (id != null)
            {
                TEntity entityToDelete = DbSet.Find(id);
                Delete(entityToDelete);
            }
        }

        public void Delete(TEntity entityToDelete)
        {
            if ((Context as CrosswordContext).Entry(entityToDelete).State == System.Data.Entity.EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public IEnumerable<TEntity> Get()
        {
            IQueryable<TEntity> query = DbSet;
            return query.ToList();
        }

        public TEntity GetByID(int id)
        {
            TEntity entityByID = DbSet.Find(id);
            return entityByID;
        }

        public TEntity GetByID(int? id)
        {
            if (id != null)
            {
                TEntity entityByID = DbSet.Find(id);
                return entityByID;
            }
            return null;
        }

        public void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            (Context as CrosswordContext).Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
        }

    }
}