using ExamProjectCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamProjectCore.Business.Abstract
{
   public interface IExamService
    {
        Exam GetById(int id);
        List<Exam> GetAll();
        void Create(Exam entity);
        void Update(Exam entity);
        void Delete(Exam entity);
    }
}
