using ExamProjectCore.Business.Abstract;
using ExamProjectCore.DataAccess.Abstract;
using ExamProjectCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamProjectCore.Business.Concrete
{
    public class ExamManager : IExamService
    {
        private IExamDal _examDal;

        public ExamManager(IExamDal examDal)
        {
            _examDal = examDal;
        }


        public void Create(Exam entity)
        {
            _examDal.Create(entity);
        }

        public void Delete(Exam entity)
        {
            _examDal.Delete(entity);
        }

        public List<Exam> GetAll()
        {
            return _examDal.GetAll();
        }

        public Exam GetById(int id)
        {
            return _examDal.GetById(id);
        }

        public void Update(Exam entity)
        {
            _examDal.Update(entity);
        }
    }
}
