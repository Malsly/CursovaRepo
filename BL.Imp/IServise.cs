using DAL.Abs;
using Entities;
using System;
using System.Collections.Generic;

namespace BL.Abs
{   
    public interface IServise<TEntity> where TEntity : class
    {
        public IGenericRepository<TEntity> Rep { get; }
        public Result<List<TEntity>> GetAll();
        public Result<TEntity> Get(int id);
        public Result<TEntity> Add(TEntity dto);
        public Result<TEntity> Update(TEntity dto);
        public Result<TEntity> Delete(int id);
        public Result<TEntity> Delete(TEntity dto);
    }
}
