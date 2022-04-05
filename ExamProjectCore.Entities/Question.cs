using System;
using System.Collections.Generic;
using System.Text;

namespace ExamProjectCore.Entities
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionContent { get; set; }
        public int ExamId { get; set; }
    }
}
