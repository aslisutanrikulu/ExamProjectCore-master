using ExamProjectCore.Business.Abstract;
using ExamProjectCore.DataAccess.Abstract;
using ExamProjectCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamProjectCore.Business.Concrete
{
    public class QuestionManager : IQuestionService
    {
        private IQuestionDal _questionDal;

        public QuestionManager(IQuestionDal questionDal)
        {
            _questionDal = questionDal;
        }


        public void Create(Question entity)
        {
            _questionDal.Create(entity);
        }

        public void Delete(Question entity)
        {
            _questionDal.Delete(entity);
        }

        public List<Question> GetAll()
        {
            return _questionDal.GetAll();
        }

        public Question GetById(int id)
        {
            return _questionDal.GetById(id);
        }

        public void Update(Question entity)
        {
            _questionDal.Update(entity);
        }
    }
}
