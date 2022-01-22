using BL.Abs;
using DAL.Abs;
using DAL.Impl;
using Entities;
using Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Impl
{
    public class WordService : IServise<Word>
    {
        private IUnitOfWork unitOfWork;
        private IGenericRepository<Word> rep;

        public WordService()
        {
            unitOfWork = new UnitOfWork();
            rep = unitOfWork.WordRepository;
        }

        public IGenericRepository<Word> Rep
        {
            get
            {
                return rep;
            }
        }

        public Result<Word> Add(Word dto)
        {
            Rep.Insert(dto);
            unitOfWork.Save();
            return new Result<Word>()
            {
                Status = Status.Success,
                Value = dto
            };
        }

        public Result<Word> Delete(int id)
        {
            Rep.Delete(id);
            unitOfWork.Save();
            return new Result<Word>()
            {
                Status = Status.Success,
            };
        }

        public Result<Word> Delete(Word dto)
        {
            return this.Delete(dto.Id);
        }

        public Result<Word> Get(int id)
        {
            return new Result<Word>()
            {
                Status = Status.Success,
                Value = Rep.GetByID(id)
            };
        }

        public Result<List<Word>> GetAll()
        {
            return new Result<List<Word>>()
            {
                Status = Status.Success,
                Value = Rep.Get().ToList()
            };
        }

        public Result<Word> Update(Word dto)
        {
            Rep.Update(dto);
            unitOfWork.Save();
            return new Result<Word>()
            {
                Status = Status.Success,
                Value = dto
            };
        }
    }
}
