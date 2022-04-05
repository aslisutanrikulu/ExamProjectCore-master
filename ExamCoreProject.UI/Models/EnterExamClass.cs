using ExamProjectCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamCoreProject.UI.Models
{
    public class EnterExamClass
    {
        public Exam exam { get; set; }
        public List<Question> questions { get; set; }
        public List<Option> options { get; set; }
    }
}
