using ExamProjectCore.Business.Abstract;
using ExamProjectCore.DataAccess.Abstract;
using ExamProjectCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamProjectCore.Business.Concrete
{
   public class OptionManager : IOptionService
    {
        private IOptionDal _optionDal;

        public OptionManager(IOptionDal optionDal)
        {
            _optionDal = optionDal;
        }


        public void Create(Option entity)
        {
            _optionDal.Create(entity);
        }

        public void Delete(Option entity)
        {
            _optionDal.Delete(entity);
        }

        public List<Option> GetAll()
        {
            return _optionDal.GetAll();
        }

        public Option GetById(int id)
        {
            return _optionDal.GetById(id);
        }

        public void Update(Option entity)
        {
            _optionDal.Update(entity);
        }
    }
}
