using ExamProjectCore.Business.Abstract;
using ExamProjectCore.DataAccess.Abstract;
using ExamProjectCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamProjectCore.Business.Concrete
{
    public class CorrectAnswerManager: ICorrectAnswerService
    {
        private ICorrectAnswerDal _correctDal;

        public CorrectAnswerManager(ICorrectAnswerDal correctDal)
        {
            _correctDal = correctDal;
        }


        public void Create(CorrectAnswer entity)
        {
            _correctDal.Create(entity);
        }

        public void Delete(CorrectAnswer entity)
        {
            _correctDal.Delete(entity);
        }

        public List<CorrectAnswer> GetAll()
        {
            return _correctDal.GetAll();
        }

        public CorrectAnswer GetById(int id)
        {
            return _correctDal.GetById(id);
        }

        public void Update(CorrectAnswer entity)
        {
            _correctDal.Update(entity);
        }
    }
}
