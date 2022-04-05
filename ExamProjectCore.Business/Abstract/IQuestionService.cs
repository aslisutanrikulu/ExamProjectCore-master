using ExamProjectCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamProjectCore.Business.Abstract
{
    public interface IQuestionService
    {
        Question GetById(int id);
        List<Question> GetAll();
        void Create(Question entity);
        void Update(Question entity);
        void Delete(Question entity);
    }
}
