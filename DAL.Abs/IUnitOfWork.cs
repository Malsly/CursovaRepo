using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abs
{
    public interface IUnitOfWork: IDisposable
    {
        public IGenericRepository<ThemeAndWords> ThemeAndWordsRepository { get; }
        public IGenericRepository<Word> WordRepository { get; }
        void Save();
    }
}
