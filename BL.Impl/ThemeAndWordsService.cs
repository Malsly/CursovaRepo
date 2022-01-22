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
    public class ThemeAndWordsService : IServise<ThemeAndWords>
    {
        private IUnitOfWork unitOfWork;
        private IGenericRepository<ThemeAndWords> rep;

        public ThemeAndWordsService()
        {
            unitOfWork = new UnitOfWork();
            rep = unitOfWork.ThemeAndWordsRepository;
        }

        public IGenericRepository<ThemeAndWords> Rep
        {
            get
            {
                return rep;
            }
        }

        public Result<ThemeAndWords> Add(ThemeAndWords dto)
        {
            Rep.Insert(dto);
            unitOfWork.Save();
            return new Result<ThemeAndWords>()
            {
                Status = Status.Success,
                Value = dto
            };
        }

        public Result<ThemeAndWords> Delete(int id)
        {
            Rep.Delete(id);
            unitOfWork.Save();
            return new Result<ThemeAndWords>()
            {
                Status = Status.Success,
            };
        }

        public Result<ThemeAndWords> Delete(ThemeAndWords dto)
        {
            return this.Delete(dto.Id);
        }

        public Result<ThemeAndWords> Get(int id)
        {
            return new Result<ThemeAndWords>()
            {
                Status = Status.Success,
                Value = Rep.GetByID(id)
            };
        }

        public Result<List<ThemeAndWords>> GetAll()
        {
            unitOfWork.Save();
            return new Result<List<ThemeAndWords>>()
            {
                Status = Status.Success,
                Value = Rep.Get().ToList()
            };
        }

        public Result<ThemeAndWords> Update(ThemeAndWords dto)
        {
            Rep.Update(dto);
            unitOfWork.Save();
            return new Result<ThemeAndWords>()
            {
                Status = Status.Success,
                Value = dto
            };
        }
    }
}
