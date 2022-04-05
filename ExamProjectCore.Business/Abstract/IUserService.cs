using ExamProjectCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamProjectCore.Business.Abstract
{
    public interface IUserService
    {
        User GetById(int id);
        List<User> GetAll();
        void Create(User entity);
        void Update(User entity);
        void Delete(User entity);
    }
}
