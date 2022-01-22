using DAL.Abs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed;

        private readonly static CrosswordContext Context = new CrosswordContext();
        private readonly static GenericRepository<ThemeAndWords> themeAndWordsRepository = new GenericRepository<ThemeAndWords>(Context);
        private readonly static GenericRepository<Word> wordRepository = new GenericRepository<Word>(Context);
        
        public IGenericRepository<ThemeAndWords> ThemeAndWordsRepository
        {
            get
            {
                return themeAndWordsRepository;
            }
        }
        public IGenericRepository<Word> WordRepository
        {
            get
            {
                return wordRepository;
            }
        }

        public void Save()
        {
            (Context as CrosswordContext).SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
