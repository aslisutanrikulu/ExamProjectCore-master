using ExamProjectCore.DataAccess.Abstract;
using ExamProjectCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamProjectCore.DataAccess.Concrete
{
   public class EFCoreUserDal : EfCoreGenericRepository<User, ExamContext>, IUserDal
    {
    }
}
